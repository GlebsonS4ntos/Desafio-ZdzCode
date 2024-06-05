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
                    <v-icon icon="mdi-delete-circle" color="error" size="large" @click="deletarCategoria(item.id)" class="mr-4"></v-icon>
                    <v-icon icon="mdi-pencil-circle" color="primary" size="large" @click="atualizarItem(item.id)"></v-icon>
                </td>
            </tr>
        </tbody>
    </v-table>
</template>

<script setup>
const items = ref([]);

const prop = defineProps({ atualizarTabela: Boolean, idCategoriaAtualizar: String, dialogAtualizar: Boolean });

const atualizarTabela = ref(prop.atualizarTabela);

const id = ref(prop.idCategoriaAtualizar);

const dialogAtualizar = ref(prop.dialogAtualizar);

const emit = defineEmits(['update:idCategoriaAtualizar','update:dialogAtualizar']);

watch(() => prop.atualizarTabela, (newVal) => {
    atualizarTabela.value = newVal
})

watch(atualizarTabela, async (newVal) => {
    emit('update:atualizarTabela', newVal)
    if (newVal) {
        await buscarCategorias();
        emit('update:atualizarTabela', false)
    }
})

watch(() => prop.idCategoriaAtualizar, (newVal) => {
    id.value = newVal
})

watch(() => prop.dialogAtualizar, (newVal) => {
    dialogAtualizar.value = newVal
})

watch(dialogAtualizar, (newVal) => {
    emit('update:dialogAtualizar', newVal)
})

const buscarCategorias = async () => { 
    try {
        const response = await fetch('https://localhost:44350/api/categories');
        const data = await response.json();
        items.value = await Promise.all(data.map(async (item) => {
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

const atualizarItem = (id) => {
    emit('update:dialogAtualizar', true);
    emit('update:idCategoriaAtualizar', id);
}

onMounted(() => {
    buscarCategorias();
})
</script>

<style></style>