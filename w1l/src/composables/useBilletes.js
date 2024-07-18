import axios from 'axios';
import { ref, computed } from 'vue';
import { useTheme } from 'vuetify';
import { algoSalioMalError, registroExitosoMensaje } from '@/components/SwalCustom.js';

const useBilletes = () => {
    const vuetifyTheme = useTheme();

    const currentTheme = computed(() => vuetifyTheme.current.value.colors);

    const data = ref([]);
    const loading = ref(false);
    const error = ref(null);

    const fetchBilletes = async () => {
        loading.value = true;
        error.value = null;

        try {
            const response = await axios.get('/Billetes');
            data.value = response.data;
            data.value.sort((a, b) => a.valorBillete - b.valorBillete);
        } catch (err) {
            error.value = err;
            algoSalioMalError(currentTheme.value);
        } finally {
            loading.value = false;
        }

        return data.value; // Asegúrate de devolver los datos
    };

    return {
        data,
        loading,
        error,
        fetchBilletes
    };
};

export default useBilletes;