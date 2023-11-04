import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './css/styles.scss'

import 'vuetify/styles'
import '@mdi/font/css/materialdesignicons.css'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

import axios from 'axios';


const customAxios = axios.create({
    baseURL: process.env.VUE_APP_API_URL,
    timeout: 10000,
});

const app = createApp(App);
app.config.globalProperties.$http = customAxios;

const vuetify = createVuetify({
    components,
    directives,
    theme: {
      defaultTheme: 'dark'
    },
    icons: {
      defaultSet: 'mdi',
    },
  })

app.use(store).use(router).use(vuetify).mount('#app')
