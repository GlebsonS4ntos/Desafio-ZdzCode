<template>
  <CadastroCategorias v-model:dialog="dialog"/>
  <div class="d-flex align-md-center">
    <h1 class="me-6">Categorias</h1>
    <v-icon color="green-darken-2" size="large" @click="dialog = !dialog">mdi-plus-circle</v-icon>
  </div>
</template>

<script>
import CadastroCategorias from '~/components/cadastroCategorias.vue'

export default {
  components: {
    CadastroCategorias
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