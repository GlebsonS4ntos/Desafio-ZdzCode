<template>
    <v-table>
        <thead>
            <tr>
                <th class="text-left">
                    Nome
                </th>
                <th class="text-left">
                    Categoria Pai
                </th>
                <th class="text-left">
                </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="item in items">
                <td>{{ item.name }}</td>
                <td>{{ item.partentCategoryName }}</td>
                <td>
                    <v-icon icon="mdi-delete-circle" color="error" size="large" @click="deletarCategoria(item.id)"></v-icon>
                </td>
            </tr>
        </tbody>
    </v-table>
</template>

<script setup>

const items = ref([]);

const buscarCategorias = async () => { 
    try {
        const response = await fetch('https://localhost:44350/api/categories');
        const data = await response.json();
        items.value = await Promise.all(data.map(async (item) => {
            /*Percore o resultado da api e pega o id da categoria pai se tiver, faz uma busca pelo id e retorna o nome e atribui o 
            resultado a uma nova propriedade do objeto, se n tiver coloca uma string padrao nessa nova propriedade*/
            if(item.partentCategoryId != null){
                item.partentCategoryName = await buscarNameCategoriaById(item.partentCategoryId);
            }
            else{
                item.partentCategoryName = "--";
            }
            return item;
        }));
    }
    catch {
        console.log('Deu ruim');
    }
}

var buscarNameCategoriaById = async (id) => {
    var categoria;
    try {
        const response = await fetch('https://localhost:44350/api/categories/' + id);
        const data = await response.json();
        categoria = data;
    }
    catch {
        console.log('Deu ruim');
    }

    return categoria.name;
}

const deletarCategoria = async (id) => {
    try {
        const response = await fetch('https://localhost:44350/api/categories/' + id, {
            method: 'DELETE'
        });
    }
    catch {
        console.log('Deu ruim');
    }

    buscarCategorias();
}

onMounted(() => {
    buscarCategorias();
})
</script>

<style></style>