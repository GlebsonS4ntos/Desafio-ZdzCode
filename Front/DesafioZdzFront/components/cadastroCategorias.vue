<template>
  <div class="pa-4 text-center">
    <v-dialog v-model="internalDialog" max-width="600">
      <v-card prepend-icon="mdi-ballot-outline" title="Adicionar Categoria">
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
  partentCategoryId: null
});

const isValid = () => {
  if(categoria.name == "" || categoria.name.length < 2) {
    return false;
  }
  return true;
};

const name = [
        value => {
          if (value.length >= 2) return true
          return 'O nome deve ter 2 ou mais caracteres'
        },
      ];

const items = ref([]);

const props = defineProps({ dialogAdicionar: Boolean, atualizarTabela: Boolean }) //Propriedade que vai vir do componente 

const emit = defineEmits(['update:dialogAdicionar','update:atualizarTabela']) //Emite o update pro componente pai

const internalDialog = ref(props.dialogAdicionar) //Variavel interna que vai receber o valor da propriedade e vai ser usada pra abrir/fechar o modal

const atualizarTabela = ref(props.atualizarTabela)

watch(() => props.dialogAdicionar, (newVal) => { //Watch para monitorar o valor da propriedade e alterar a variavel interna
  internalDialog.value = newVal
})

watch(() => props.atualizarTabela, (newVal) => {
  atualizarTabela.value = newVal
})

watch(atualizarTabela, (newVal) => {
  emit('update:atualizarTabela', newVal)
})

const closeDialog = () => { //Fechar o modal
  internalDialog.value = false
  emit('update:dialogAdicionar', internalDialog.value)
  categoria.name = "";
  categoria.partentCategoryId = null;
}

const saveDialog = async () => { //Cadastrar
  try {
    const response = await fetch('https://localhost:44350/api/categories', {
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'POST',
      body: JSON.stringify(categoria)
    });
  }
  catch {
    console.log('Deu ruim');
  }

  atualizarTabela.value = true;
  internalDialog.value = false;
  emit('update:dialogAdicionar', internalDialog.value);
  categoria.name = "";
  categoria.partentCategoryId = null;
  buscarCategorias();
}

watch(internalDialog, async (newVal) => { //Watch para monitorar o valor da variavel interna e alterar a propriedade na pagina de categorias 
  emit('update:dialogAdicionar', newVal)
  if (newVal === false) {
    categoria.name = "";
    categoria.partentCategoryId = null;
  }
  else {
    await buscarCategorias();
  }
})

const buscarCategorias = async () => { //Pegar as categorias pra selecionar a categoria pai na hora de adicionar
  try {
    const response = await fetch('https://localhost:44350/api/categories');
    const data = await response.json();
    items.value = data;
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