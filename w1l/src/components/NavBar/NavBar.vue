<template>
    <div id="navBar">
        <nav>
            <router-link to="/">Inicio</router-link>
            <router-link to="/stock">Stock</router-link>
            <router-link to="/proveedores">Proveedores</router-link>
            <router-link to="/compras">Compras</router-link>
            <a to="/listados" class="listadosItem">
                Listados 
                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-chevron-down" viewBox="0 0 16 16">
                    <path fill-rule="evenodd" d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z"/>
                </svg>
            </a>
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