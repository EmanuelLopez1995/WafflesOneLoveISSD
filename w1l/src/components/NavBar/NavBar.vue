<template>
    <div id="navBar">
        <nav>
            <router-link to="/">Inicio</router-link>
            <router-link to="/stock">Stock</router-link>
            <router-link to="/proveedores">Proveedores</router-link>
            <router-link to="/compras">Compras</router-link>
            <router-link to="/empleados">Empleados</router-link>
        </nav>
        <div class="seccionUsuarioNav">
            <span>Nombre de usuario</span>
            
            <v-menu
                transition="slide-y-transition"
                >
                <template v-slot:activator="{ props }">
                    <img v-bind="props" src="@/assets/img/imagenPerfil.png" alt="Usuario" class="imagenUsuario">
                    <router-link to="/configuraciones" class="iconGearLink"><span class="gearIcon"></span></router-link>
                </template>
                <v-list>
                    <v-list-item
                    class="listItem"
                    v-for="(item, i) in items"
                    :key="i"
                    @click="handleFunciones(i)"
                    >
                        <span v-html="item.icon"></span>
                        <v-list-item-title>{{ item.title }}</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </div>
    </div>
</template>

<script>
import './NavBar.scss'

export default {
    name: 'navBar',
    components: {
    },
    data() {
        return {
            items: [
                { 
                    title: 'Mi cuenta',
                    icon: `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-person-fill" viewBox="0 0 16 16">
                            <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1H3Zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6Z"/>
                            </svg>`
                },
                { 
                    title: 'Cerrar sesión',
                    icon: `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-box-arrow-left" viewBox="0 0 16 16">
                            <path fill-rule="evenodd" d="M6 12.5a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5h-8a.5.5 0 0 0-.5.5v2a.5.5 0 0 1-1 0v-2A1.5 1.5 0 0 1 6.5 2h8A1.5 1.5 0 0 1 16 3.5v9a1.5 1.5 0 0 1-1.5 1.5h-8A1.5 1.5 0 0 1 5 12.5v-2a.5.5 0 0 1 1 0v2z"/>
                            <path fill-rule="evenodd" d="M.146 8.354a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L1.707 7.5H10.5a.5.5 0 0 1 0 1H1.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3z"/>
                            </svg>`
                }
            ],
        }
    },
    methods: {
        handleFunciones(index) {
            switch(index){
                case 0 : this.mostrarDatosCuenta();
                    break;
                case 1 : this.cerrarSesión();
                    break;
            }
        },
        cerrarSesión() {
            localStorage.removeItem('usuarioAutenticado'); // modificar con el back
            this.$router.push('/Login')
        },
        mostrarDatosCuenta() {
            
        }
    }
}
</script>