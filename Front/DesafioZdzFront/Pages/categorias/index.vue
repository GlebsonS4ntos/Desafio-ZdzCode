<template>
  <div class="d-flex align-md-center mb-6">
    <h1 class="me-6">Categorias</h1>
    <v-icon color="green-darken-2" size="large" @click="dialog = !dialog">mdi-plus-circle</v-icon>
  </div>
  <TabelaCategorias />
  <CadastroCategorias v-model:dialog="dialog"/>
</template>

<script>
import CadastroCategorias from '~/components/cadastroCategorias.vue'
import TabelaCategorias from '~/components/tabelaCategorias.vue'

export default {
  components: {
    CadastroCategorias,
    TabelaCategorias
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