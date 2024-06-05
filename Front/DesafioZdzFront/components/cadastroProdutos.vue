<template>
  <div class="pa-4 text-center">
    <v-dialog v-model="internalDialog" max-width="600">
      <v-card prepend-icon="mdi-train-car-container" title="Adicionar Produto">
        <v-card-text>
          <form>
            <v-text-field label="Nome *" v-model="produto.name" :rules="nameValidation"></v-text-field>
            <v-textarea clearable label="Descricao *" v-model="produto.description"
              :rules="descriptionValidation"></v-textarea>
            <v-select :items="categorias" label="Categoria *" density="comfortable" item-value="id" item-title="name"
              clearable v-model="produto.categoryId" :role="categoriaValidation"></v-select>
            <v-row>
              <v-col cols="12" sm="6">
                <v-text-field label="Preco *" v-model="produto.price" prefix="$" type="number" :rules="precoValidation"></v-text-field>
              </v-col>
              <v-col cols="12" sm="6">
                <v-text-field label="Quantidade *" v-model="produto.quantity" type="number" :rules="quantidadeValidation"></v-text-field>
              </v-col>
            </v-row>
            <v-text-field label="Url da Imagem *" v-model="produto.imageUrl" clearable :rules="imagemValidation"></v-text-field>
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

const prop = defineProps({ dialog: Boolean, atualizarCards: Boolean });

const produto = reactive({
  id: null,
  name: "",
  description: "",
  quantity: 0,
  price: 0.0,
  imageUrl: "",
  categoryId: ""
});

const internalDialog = ref(prop.dialog);

const emit = defineEmits(['update:dialog', 'update:atualizarCards']);	

const categorias = ref([]);

watch(() => prop.dialog, (newVal) => {
  internalDialog.value = newVal
});

watch(internalDialog, (newVal) => {
  emit('update:dialog', newVal)
  if (newVal === false) {
    produto.name = "";
    produto.description = "";
    produto.quantity = 0;
    produto.price = 0.0;
    produto.imageUrl = "";
    produto.categoryId = "";
  }
});

const closeDialog = () => {
  internalDialog.value = false
}

const buscarCategorias = async () => { //Pegar as categorias pra selecionar a categoria pai na hora de adicionar
  try {
    const response = await fetch('https://localhost:44350/api/categories');
    const data = await response.json();
    categorias.value = data;
  }
  catch {
    console.log('Deu ruim');
  }
}

onMounted(() => {
  buscarCategorias();
});

const saveDialog = async () => { //Cadastrar
  try {
    const response = await fetch('https://localhost:44350/api/products', {
      headers: {
        'Content-Type': 'application/json'
      },
      method: 'POST',
      body: JSON.stringify(produto)
    });
  }
  catch {
    console.log('Deu ruim');
  }

  internalDialog.value = false;
  emit('update:atualizarCards', true);
}

const isValid = () => {
  if (produto.name.length < 2) return false;

  if (produto.description.length < 5) return false;

  if (produto.quantity < 0) return false;

  if (produto.price < 0) return false;

  if (produto.categoryId == "") return false;

  if (produto.imageUrl.length < 10) return false;

  return true;
}

const nameValidation = [
  value => {
    if (value.length > 2) return true
    return 'O nome deve ter 2 ou mais caracteres'
  },
];

const descriptionValidation = [
  value => {
    if (value.length >= 5) return true
    return 'A descrição deve ter 5 ou mais caracteres'
  },
];

const categoriaValidation = [
  value => {
    if (value === "") return true
    return 'A categoria deve ser selecionada'
  },
];

const quantidadeValidation = [
  value => {
    if (value >= 0) return true
    return 'A quantidade deve ser maior ou igual a 0'
  },
];

const precoValidation = [
  value => {
    if (value >= 0) return true
    return 'O preco deve ser maior ou igual a 0'
  },
];

const imagemValidation = [
        value => {
          if (value.length >= 10) return true
          return 'A imagem deve ter 10 ou mais caracteres'
        },
      ];
</script>

<style></style>