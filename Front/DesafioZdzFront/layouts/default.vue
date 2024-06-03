<template>
    <v-app dark="true">
        <v-navigation-drawer v-model="drawer" :clipped="clipped" temporary location="left" >
            <v-list>
                <v-list-item v-for="(item, i) in items" :key="i" :value="item" color="primary" :to="item.to" exact>
                    <template v-slot:prepend>
                        <v-icon :icon="item.icon"></v-icon>
                    </template>
                    <v-list-item-title v-text="item.title"></v-list-item-title>
                </v-list-item>
            </v-list>
        </v-navigation-drawer>
        <v-app-bar :clipped-left="clipped" fixed app color="purple-darken-2">
            <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
            <v-toolbar-title @click.prevent="goHome" class="cursor-grab">
                {{ title }}
            </v-toolbar-title>
            <v-spacer />
        </v-app-bar>
        <v-main>
            <v-container>
                <slot />
            </v-container>
        </v-main>
        <v-footer :absolute="!fixed" app>
            <span>&copy; {{ new Date().getFullYear() }}</span>
        </v-footer>
    </v-app>
</template>

<script>
export default {
    name: 'DefaultLayout',
    data() {
        return {
            clipped: false,
            drawer: false,
            fixed: true,
            items: [
                {
                    icon: 'mdi-ballot-outline',
                    title: 'Categorias',
                    to: '/categorias'
                },
                {
                    icon: 'mdi-train-car-container',
                    title: 'Produtos',
                    to: '/produtos'
                }
            ],
            miniVariant: false,
            title: 'Controle de Estoque'
        }
    },
    methods: {
        goHome() {
            this.$router.push('/')
        }
    }
}
</script>

<style></style>