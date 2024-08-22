import { eliminarRegistro, algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';
import { ref } from 'vue';
import { useTheme } from 'vuetify';
import axios from 'axios';

const useProveedores = () => {
    const proveedores = ref([]);
    const vuetifyTheme = useTheme();

    const currentTheme = computed(() => {
        return ref(vuetifyTheme.current.value.colors);
    });

    const obtenerProveedores = async () => {
        try {
            await axios
                .get('/Proveedor/GetAllProveedores')
                .then(response => {
                    proveedores.value = response.data;
                })
                .catch(() => {
                    algoSalioMalError(currentTheme.value);
                });
                return proveedores.value;
        } catch (error) {
            algoSalioMalError(currentTheme.value);
        }
    }

    obtenerProveedores();

    return {
        proveedores,
        obtenerProveedores
    };
};

export default useProveedores;
