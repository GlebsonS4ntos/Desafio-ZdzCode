<template>
  <div class="d-flex align-md-center">
    <h1 class="me-6">Produtos</h1>
    <v-icon color="green-darken-2" size="large" @click="dialog = !dialog"> mdi-plus-circle </v-icon>
    <CadastroProdutos v-model:dialog="dialog"/>
  </div>
</template>

<script>
import CadastroProdutos from '~/components/cadastroProdutos.vue';

export default {
  components: {
    CadastroProdutos
  },
  data() {
    return {
      dialog: false
    }
  }
}
</script>

<style>

</style>