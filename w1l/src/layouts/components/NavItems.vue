<script setup>
import VerticalNavSectionTitle from '@/@layouts/components/VerticalNavSectionTitle.vue'
import VerticalNavGroup from '@layouts/components/VerticalNavGroup.vue'
import VerticalNavLink from '@layouts/components/VerticalNavLink.vue'
import { ref, onMounted, onUnmounted } from 'vue';
import { useGeneralStore } from '@/store/store.js';

const generalStore = useGeneralStore();

const updateTurnoIniciado = () => {
  generalStore.updateTurnoIniciado();
};

const seccionesPermitidas = ref([]);

// Escuchar los cambios en localStorage
onMounted(async () => {
  window.addEventListener('storage', updateTurnoIniciado);
  let usuario = await JSON.parse(localStorage.getItem('user'));
  seccionesPermitidas.value = usuario.sections;
});
</script>

<template>
  <div>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('4')"
      :item="{
        title: 'Turnos',
        icon: 'ri-t-box-line',
      }"
    >
    <VerticalNavLink
      :item="{
        title: 'Listado de turnos',
        to: '/listadoTurnos',
      }"
    />
    <VerticalNavLink
      v-if="!generalStore.turnoIniciado && seccionesPermitidas.includes('4')"
      :item="{
        title: 'Inicio de Turno',
        to: '/inicioTurnoYcaja',
      }"
    />
      <VerticalNavLink
        v-if="generalStore.turnoIniciado"
        :item="{
          title: 'Detalle de Inicio',
          to: '/detalleInicioTurno',
        }"
      />
      <VerticalNavLink
        v-if="generalStore.turnoIniciado"
        :item="{
          title: 'Finalizar Turno',
          to: '/finalizarTurno',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('3')"
      :item="{
        title: 'Stock',
        icon: 'ri-archive-stack-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Listado',
          to: '/listadoStock',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Registrar Stock',
          to: '/registrarStock',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('6')"
      :item="{
        title: 'Proveedores',
        icon: 'ri-account-pin-circle-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Listado',
          to: '/listadoProveedores',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Registrar proveedor',
          to: '/registrarProveedor',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('7')"
      :item="{
        title: 'Empleados',
        icon: 'ri-team-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Listado',
          to: '/listadoEmpleados',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Registrar empleado',
          to: '/registrarEmpleado',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('1')"
      :item="{
        title: 'Compras',
        icon: 'ri-bank-card-2-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Listado',
          to: '/listadoCompras',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Registrar compra',
          to: '/registrarCompra',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      v-if="seccionesPermitidas.includes('10')"
      :item="{
        title: 'Producción',
        icon: 'ri-restaurant-2-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Recetas',
          to: '/recetas',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavGroup
      :item="{
        title: 'Estadísticas',
        icon: 'ri-line-chart-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Compras',
          to: '/comprasEstadisticas',
        }"
      />
    </VerticalNavGroup>
    <VerticalNavLink
      v-if="seccionesPermitidas.includes('5')"
      :item="{
        title: 'Admin',
        icon: 'ri-settings-5-line',
        to: '/admin',
      }"
    />
    <!-- Aca empieza lo otro  -->
      <VerticalNavLink
        :item="{
          title: '----------',
          to: '/',
        }"
      />
      <VerticalNavLink
        :item="{
          title: '----------',
          to: '/',
        }"
      />
  
    <!-- 👉 Dashboards -->
    <VerticalNavGroup
      :item="{
        title: 'Dashboards',
        badgeContent: '5',
        badgeClass: 'bg-error',
        icon: 'ri-home-smile-line',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Analytics',
          to: '/dashboard',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'CRM',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/dashboards/crm',
          target: '_blank',
          badgeContent: 'Pro',
          badgeClass: 'bg-light-primary text-primary',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'ECommerce',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/dashboards/ecommerce',
          target: '_blank',
          badgeContent: 'Pro',
          badgeClass: 'bg-light-primary text-primary',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Academy',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/dashboards/academy',
          target: '_blank',
          badgeContent: 'Pro',
          badgeClass: 'bg-light-primary text-primary',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Logistics',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/dashboards/logistics',
          target: '_blank',
          badgeContent: 'Pro',
          badgeClass: 'bg-light-primary text-primary',
        }"
      />
    </VerticalNavGroup>
  
    <!-- 👉 Front Pages -->
    <VerticalNavGroup
      :item="{
        title: 'Front Pages',
        icon: 'ri-file-copy-line',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    >
      <VerticalNavLink
        :item="{
          title: 'Landing',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/front-pages/landing-page',
          target: '_blank',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Pricing',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/front-pages/pricing',
          target: '_blank',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Payment',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/front-pages/payment',
          target: '_blank',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Checkout',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/front-pages/checkout',
          target: '_blank',
        }"
      />
      <VerticalNavLink
        :item="{
          title: 'Help Center',
          href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/front-pages/help-center',
          target: '_blank',
        }"
      />
    </VerticalNavGroup>
  
    <!-- 👉 Apps & Pages -->
    <VerticalNavSectionTitle
      :item="{
        heading: 'Apps & Pages',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Permissions',
        icon: 'ri-lock-2-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/apps/permissions',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Email',
        icon: 'ri-mail-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/apps/email',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Chat',
        icon: 'ri-wechat-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/apps/chat',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Calendar',
        icon: 'ri-calendar-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/apps/calendar',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
  
    <VerticalNavLink
      :item="{
        title: 'Account Settings',
        icon: 'ri-user-settings-line',
        to: '/account-settings',
      }"
    />
  
    <VerticalNavLink
      :item="{
        title: 'Login',
        icon: 'ri-login-box-line',
        to: '/login',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Register',
        icon: 'ri-user-add-line',
        to: '/register',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Error',
        icon: 'ri-information-line',
        to: '/no-existence',
      }"
    />
  
    <!-- 👉 User Interface -->
    <VerticalNavSectionTitle
      :item="{
        heading: 'User Interface',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Typography',
        icon: 'ri-text',
        to: '/typography',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Icons',
        icon: 'ri-remixicon-line',
        to: '/icons',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Cards',
        icon: 'ri-bar-chart-box-line',
        to: '/cards',
      }"
    />
  
    <!-- 👉 Forms & Tables -->
    <VerticalNavSectionTitle
      :item="{
        heading: 'Forms & Tables',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Form Layouts',
        icon: 'ri-layout-4-line',
        to: '/form-layouts',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Form Validation',
        icon: 'ri-checkbox-multiple-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/forms/form-validation',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Form Wizard',
        icon: 'ri-git-commit-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/forms/form-wizard-numbered',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Tables',
        icon: 'ri-table-alt-line',
        to: '/tables',
      }"
    />
  
    <!-- 👉 Others -->
    <VerticalNavSectionTitle
      :item="{
        heading: 'Others',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Access Control',
        icon: 'ri-shield-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/demo-1/access-control',
        target: '_blank',
        badgeContent: 'Pro',
        badgeClass: 'bg-light-primary text-primary',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Documentation',
        icon: 'ri-article-line',
        href: 'https://demos.themeselection.com/materio-vuetify-vuejs-admin-template/documentation/',
        target: '_blank',
      }"
    />
    <VerticalNavLink
      :item="{
        title: 'Raise Support',
        href: 'https://github.com/themeselection/materio-vuetify-vuejs-admin-template-free/issues',
        icon: 'ri-lifebuoy-line',
        target: '_blank',
      }"
    />

  </div>
</template>
