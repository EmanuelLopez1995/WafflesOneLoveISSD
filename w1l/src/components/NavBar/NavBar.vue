<template>
    <div id="navBar">
        <nav>
            <router-link to="/">Inicio</router-link>
            <router-link to="/stock">Stock</router-link>
            <router-link to="/proveedores">Proveedores</router-link>
            <router-link to="/compras">Compras</router-link>
            <router-link to="/listados">Listados</router-link>
            <router-link to="/empleados">Empleados</router-link>
        </nav>
        <div class="seccionUsuarioNav">
            <img src="@/assets/img/imagenPerfil.png" alt="Usuario" class="imagenUsuario">
            <!-- <div class="datosUsuario">
                <span class="nombreUsuario">Super Mila</span>
                <span class="rolUsuario">Super Admin</span> 
            </div> -->
        </div>
    </div>
</template>

<script>
import './NavBar.scss'

export default {
    
}
</script>