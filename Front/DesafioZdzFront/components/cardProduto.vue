<template>
    <v-sheet border="lg opacity-12" class="text-body-2 mx-auto" max-width="550">
        <v-container fluid>
            <v-row>
                <v-col cols="12" md="3">
                    <v-img height="120" :src="produto.imageUrl" cover aling="center"></v-img>
                </v-col>

                <v-col cols="12" md="9">
                    <div class="mb-4 d-flex justify-space-between">
                        <span>{{ produto.name }} -- {{ produto.categoryName }}</span>
                        <v-icon icon="mdi-delete-circle" color="error" size="large"></v-icon>
                    </div>
                    <div>
                        {{ produto.description }}
                    </div>
                    <div>
                        Quantidade: {{ produto.quantity }}
                    </div>
                    <div>
                        Preço: R$ {{ produto.price }}
                    </div>
                    <v-btn class="text-none mt-3" color="info" rounded="0" variant="flat" block text="EDITAR">
                        
                    </v-btn>
                </v-col>
            </v-row>
        </v-container>
    </v-sheet>
</template>

<script setup>
const prop = defineProps({
    produto: {
        type: Object,
        required: true
    }
})
</script>

<style></style>