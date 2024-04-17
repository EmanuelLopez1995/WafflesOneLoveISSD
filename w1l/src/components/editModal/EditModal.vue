<template>
    <VDialog
        v-model="props.dialog"
        width="50%"
    >
        <VCard
            prepend-icon="ri-edit-2-fill"
            title="Editar"
            class="px-5"
        >
            <VForm @submit.prevent="confirmarEdicion" ref="form">
                <slot></slot>
                <VCol             
                    cols="12"
                    class="d-flex gap-4 justify-end mt-3"
                >
                    <VBtn
                        color="secondary"
                        variant="outlined"
                        @click="closeDialog"
                    >
                        CERRAR
                    </VBtn>
                    <VBtn
                        color="primary"
                        variant="outlined"
                        type="submit"
                    >
                        CONFIRMAR
                    </VBtn>
                </VCol>
            </VForm>
        </VCard>
    </VDialog>
</template>

<script setup>

const props = defineProps(['dialog']);
const emit = defineEmits(['cerrarDialogo', 'confirmarDialogo']);

const closeDialog = () => {
  emit('cerrarDialogo');
};

const confirmarEdicion = () => {
    emit('confirmarDialogo')
}
</script>