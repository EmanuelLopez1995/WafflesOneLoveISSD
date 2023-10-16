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
            <img src="@/assets/img/imagenPerfil.png" alt="Usuario" class="imagenUsuario">
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

        }
    },
    methods: {
    }
}
</script>