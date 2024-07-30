import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const useUsuarios = () => {
    const secciones = ref([]);
    const usuarios = ref([]);
    const vuetifyTheme = useTheme();

    const currentTheme = computed(() => {
        return ref(vuetifyTheme.current.value.colors);
    });

    const obtenerUsuarios = async () => {
        try {
            await axios
                .get('/Usuario/GetAllUsuarios')
                .then(response => {
                    usuarios.value = response.data;
                })
                .catch(() => {
                    algoSalioMalError(currentTheme.value);
                });
            return usuarios.value;
        } catch (error) {
            algoSalioMalError(currentTheme.value);
        }
    };

    const obtenerSecciones = async () => {
        try {
            await axios
                .get('/Seccion/GetAllSecciones')
                .then(response => {
                    secciones.value = response.data;
                })
                .catch(() => {
                    algoSalioMalError(currentTheme.value);
                });
            return usuarios.value;
        } catch (error) {
            algoSalioMalError(currentTheme.value);
        }
    }

    obtenerSecciones();

    return {
        secciones,
        obtenerUsuarios,
        obtenerSecciones
    };
};

export default useUsuarios;
