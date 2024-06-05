<template>
    <div class="pa-4 text-center">
      <v-dialog v-model="internalDialog" max-width="600">
        <v-card prepend-icon="mdi-ballot-outline" title="Atualizar Categoria">
          <v-card-text>
            <form>
              <v-text-field label="Nome *" v-model="categoria.name" :rules="name"></v-text-field>
              <v-select :items="items" label="Categoria Pai" density="comfortable" v-model="categoria.partentCategoryId"
                item-value="id" item-title="name" clearable></v-select>
            </form>
          </v-card-text>
          <v-divider></v-divider>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text="Close" variant="plain" @click="closeDialog">FECHAR</v-btn>
            <v-btn color="primary" text="Save" variant="tonal" @click="saveDialog" :disabled="!isValid()">SALVAR</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </template>
  
  <script setup>
  import { ref, reactive, watch, onMounted } from 'vue'
  
  const categoria = reactive({
    id: null,
    name: "",
    partentCategoryId: ""
  });
  
  const props = defineProps({ dialogAtualizar: Boolean, idCategoriaAtualizar: String, atualizarTabela : Boolean })

  const idCategoriaAtualizar = ref(props.idCategoriaAtualizar);

  const emit = defineEmits(['update:dialogAtualizar'], ['update:atualizarTabela']) 

  const internalDialog = ref(props.dialogAtualizar)

  const atualizarTabela = ref(props.atualizarTabela)

  const items = ref([]);

  const isValid = () => {
    if(categoria.name == "" || categoria.name.length < 2) {
      return false;
    }
    return true;
  };
  
  const name = [
          value => {
            if (value.length > 2) return true
            return 'O nome deve ter 2 ou mais caracteres'
          },
        ];
  
  watch(() => props.dialogAtualizar, (newVal) => { 
    internalDialog.value = newVal
  })
  
  watch(() => props.idCategoriaAtualizar, (newVal) => {
    idCategoriaAtualizar.value = newVal
  })

  watch(() => props.atualizarTabela, (newVal) => {
    atualizarTabela.value = newVal
  })

  watch(atualizarTabela, (newVal) => {
    emit('update:atualizarTabela', newVal)
  })

  const closeDialog = () => { 
    internalDialog.value = false
    emit('update:dialogAtualizar', internalDialog.value)
    categoria.name = "";
    categoria.partentCategoryId = "";
  }
  
  watch(internalDialog, async (newVal) => { 
    emit('update:dialogAtualizar', newVal)
    if (newVal === false) {
      categoria.name = "";
      categoria.partentCategoryId = "";
    }else{
      await buscarCategoriaById(idCategoriaAtualizar.value)
      await buscarCategorias()
    }
  })

  const saveDialog = async () => {
    try {
      const response = await fetch('https://localhost:44350/api/categories/' + idCategoriaAtualizar.value, {
        headers: {
          'Content-Type': 'application/json'
        },
        method: 'PUT',
        body: JSON.stringify(categoria)
      });
      console.log(response);
    }
    catch {
      console.log('Deu ruim');
    }
  
    atualizarTabela.value = true;
    internalDialog.value = false;
    emit('update:dialogAtualizar', internalDialog.value);
    categoria.name = "";
    categoria.partentCategoryId = "";
    buscarCategorias();
  }
  
  const buscarCategorias = async () => {
    try {
      const response = await fetch('https://localhost:44350/api/categories');
      const data = await response.json();
      items.value = data;
    }
    catch {
      console.log('Deu ruim');
    }
  }
  
  const buscarCategoriaById = async (id) => {
    try {
      const response = await fetch('https://localhost:44350/api/categories/' + id);
      const data = await response.json();
      categoria.id = data.id;
      categoria.name = data.name;
      categoria.partentCategoryId = data.partentCategoryId;
    }
    catch {
      console.log('Deu ruim');
    }
  }

  onMounted(() => {
    buscarCategorias();
  })
  
  </script>
  
  <style></style>