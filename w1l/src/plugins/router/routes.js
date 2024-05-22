export const routes = [
  { path: '/', redirect: '/dashboard' },
  {
    path: '/',
    component: () => import('@/layouts/default.vue'),
    children: [
      {
        path: 'dashboard',
        component: () => import('@/pages/dashboard.vue'),
      },
      {
        path: 'account-settings',
        component: () => import('@/pages/account-settings.vue'),
      },
      {
        path: 'typography',
        component: () => import('@/pages/typography.vue'),
      },
      {
        path: 'icons',
        component: () => import('@/pages/icons.vue'),
      },
      {
        path: 'cards',
        component: () => import('@/pages/cards.vue'),
      },
      {
        path: 'tables',
        component: () => import('@/pages/tables.vue'),
      },
      {
        path: 'form-layouts',
        component: () => import('@/pages/form-layouts.vue'),
      },
      {
        path: 'listadoProveedores',
        component: () => import('@/pages/listadoProveedores.vue'),
      },
      {
        path: 'registrarProveedor',
        component: () => import('@/pages/registrarProveedor.vue'),
      },
      {
        path: 'registrarStock',
        component: () => import('@/pages/registrarStock.vue'),
      },
      {
        path: 'registrarEmpleado',
        component: () => import('@/pages/registrarEmpleado.vue'),
      },
      {
        path: 'listadoEmpleados',
        component: () => import('@/pages/listadoEmpleados.vue'),
      },
      {
        path: 'inicioTurnoYcaja',
        component: () => import('@/pages/inicioTurnoYcaja.vue'),
      },
      {
        path: 'admin',
        component: () => import('@/pages/admin.vue'),
      },
      {
        path: 'registrarCompra',
        component: () => import('@/pages/registrarCompra.vue'),
      },
      {
        path: 'recetas',
        component: () => import('@/pages/recetas.vue'),
      },
      {
        path: 'listadoStock',
        component: () => import('@/pages/listadoStock.vue'),
      },
    ],
  },
  {
    path: '/',
    component: () => import('@/layouts/blank.vue'),
    children: [
      {
        path: 'login',
        component: () => import('@/pages/login.vue'),
      },
      {
        path: 'register',
        component: () => import('@/pages/register.vue'),
      },
      {
        path: '/:pathMatch(.*)*',
        component: () => import('@/pages/[...error].vue'),
      },
    ],
  },
]
