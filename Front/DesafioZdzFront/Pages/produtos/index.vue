<template>
  <div class="d-flex align-md-center mb-12">
    <h1 class="me-6">Produtos</h1>
    <v-icon color="green-darken-2" size="large" @click="dialogAdicionar = !dialogAdicionar"> mdi-plus-circle </v-icon>
    <CadastroProdutos v-model:dialogAdicionar="dialogAdicionar" v-model:atualizarCards="atualizarCards"/>
  </div>
  <v-container>
    <v-row dense>
      <v-col cols="12" md="4" v-for="item in items">
        <CardProduto :produto="item" v-model:atualizarCards="atualizarCards" v-model:idProductAtualizar="idProductAtualizar" v-model:dialogAtualizar="dialogAtualizar"/>
      </v-col>
    </v-row>
  </v-container>
  <AtualizarProduto v-model:dialogAtualizar="dialogAtualizar" v-model:idProductAtualizar="idProductAtualizar" v-model:atualizarCards="atualizarCards"/>
</template>

<script>
import AtualizarProduto from '~/components/atualizarProduto.vue';
import CadastroProdutos from '~/components/cadastroProdutos.vue'
import CardProduto from '~/components/cardProduto.vue'

export default {
  components: {
    CadastroProdutos,
    CardProduto, 
    AtualizarProduto
  },
  data() {
    return {
      dialogAtualizar: false,
      dialogAdicionar: false,
      items: [],
      atualizarCards : false,
      idProductAtualizar: null
    }
  },
  methods: {
    async buscarProdutos() {
      try {
        const response = await fetch('https://localhost:44350/api/products');
        const data = await response.json();
        this.items = await Promise.all(data.map(async (item) => {
          item.categoryName = await this.buscarNameCategoriaById(item.categoryId);

          return item;
        }));
      }
      catch {
        console.log('Deu ruim');
      }
    },
    async buscarNameCategoriaById(id) {
      try {
        const response = await fetch('https://localhost:44350/api/categories/' + id);
        const data = await response.json();
        console.log(data.name)
        return data.name;
      }
      catch {
        console.log('Deu ruim');
      }
    }
  },
  watch: {
    atualizarCards(newVal) {
      if (newVal) {
        this.buscarProdutos();
        this.atualizarCards = false
      }
    }
  },
  mounted() {
    this.buscarProdutos();
  }
}
</script>

<style></style>