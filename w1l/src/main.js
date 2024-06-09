import { createApp } from 'vue'
import App from '@/App.vue'
import { registerPlugins } from '@core/utils/plugins'
// import axiosConfig from './axiosConfig.js';
import axios from 'axios';


// Styles
import '@core/scss/template/index.scss'
import '@layouts/styles/index.scss'

// Create vue app
const app = createApp(App)

axios.defaults.baseURL = 'https://localhost:7037/api';
app.config.globalProperties.$axios = axios;

// Register plugins
registerPlugins(app)

// Mount vue app
app.mount('#app')
