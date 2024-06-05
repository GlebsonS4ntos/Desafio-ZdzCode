<template>
  <div class="d-flex align-md-center mb-6">
    <h1 class="me-6">Categorias</h1>
    <v-icon color="green-darken-2" size="large" @click="dialogAdicionar = !dialogAdicionar">mdi-plus-circle</v-icon>
  </div>
  <TabelaCategorias v-model:atualizarTabela="atualizarTabela" v-model:idCategoriaAtualizar="idCategoriaAtualizar"/>
  <CadastroCategorias v-model:dialogAdicionar="dialogAdicionar" v-model:atualizarTabela="atualizarTabela"/>
  <AtualizarCategoria v-model:dialogAtualizar="dialogAtualizar" v-model:idCategoriaAtualizar="idCategoriaAtualizar"/>
</template>

<script>
import AtualizarCategoria from '~/components/atualizarCategoria.vue';
import CadastroCategorias from '~/components/cadastroCategorias.vue'
import TabelaCategorias from '~/components/tabelaCategorias.vue'

export default {
  components: {
    CadastroCategorias,
    TabelaCategorias,
    AtualizarCategoria
  },
  data() {
    return {
      dialogAdicionar: false,
      dialogAtualizar: false,
      idCategoriaAtualizar: "",
      atualizarTabela: false
    }
  }
}
</script>

<style>
</style>