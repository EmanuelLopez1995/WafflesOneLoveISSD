<script setup>
import { ref, computed } from 'vue';
import { useTheme } from 'vuetify';
import { useGeneralStore } from '@/store/store.js';
import authV1MaskDark from '@images/pages/auth-v1-mask-dark.png';
import authV1MaskLight from '@images/pages/auth-v1-mask-light.png';
import authV1Tree2 from '@images/pages/auth-v1-tree-2.png';
import authV1Tree from '@images/pages/auth-v1-tree.png';
import { useRouter } from 'vue-router';

const form = ref({
    usuario: '',
    password: '',
    remember: false
});

const vuetifyTheme = useTheme();
const router = useRouter();
const authStore = useGeneralStore();

const valoresIncorrectos = ref(false);

const authThemeMask = computed(() => {
    return vuetifyTheme.global.name.value === 'light' ? authV1MaskLight : authV1MaskDark;
});

const theme = computed(() => {
    return vuetifyTheme.global.name.value;
});

const isPasswordVisible = ref(false);

const login = async () => {
    const { usuario, password } = form.value;
    try {
        let resp = await authStore.login({ Username: usuario, Password: password });

        //Si hay respuesta es que falló la auth 
        if(resp && resp.response.status == 401) {
            valoresIncorrectos.value = true;
        }else {
            valoresIncorrectos.value = false;
        }
        router.push('/dashboard');
    } catch (error) {
        console.error('Login failed', error);
    }
};
</script>

<template>
    <!-- eslint-disable vue/no-v-html -->

    <div class="auth-wrapper d-flex align-center justify-center pa-4">
        <VCard
            class="auth-card pa-4 pt-7"
            max-width="448"
        >
            <VCardItem class="justify-center">
                <VCardTitle class="d-flex justify-center">
                    <img
                        v-if="theme === 'light'"
                        src="../assets/images/logoFondoBlanco.png"
                        width="70%"
                    />
                    <img
                        v-else
                        src="../assets/images/logoOscuroFondoClaro.png"
                        width="70%"
                    />
                </VCardTitle>
            </VCardItem>

            <VCardText class="pt-2">
                <h5 class="text-h5 font-weight-semibold mb-1">Bienvenid@ a SSLIT! 👋🏻</h5>
                <p class="mb-0">Inserte usuario y contraseña para ingresar</p>
            </VCardText>

            <VCardText>
                <VForm @submit.prevent="login">
                    <VRow>
                        <!-- usuario -->
                        <VCol cols="12">
                            <VTextField
                                v-model="form.usuario"
                                label="Usuario"
                            />
                        </VCol>

                        <!-- password -->
                        <VCol cols="12">
                            <VTextField
                                v-model="form.password"
                                label="Contraseña"
                                placeholder="············"
                                :type="isPasswordVisible ? 'text' : 'password'"
                                :append-inner-icon="isPasswordVisible ? 'ri-eye-off-line' : 'ri-eye-line'"
                                @click:append-inner="isPasswordVisible = !isPasswordVisible"
                            />

                            <!-- remember me checkbox -->
                            <!-- <div class="d-flex align-center justify-space-between flex-wrap mt-1 mb-4">
                                <VCheckbox
                                    v-model="form.remember"
                                    label="Recordar"
                                />
                            </div> -->

                            <!-- login button -->
                            <p v-if="valoresIncorrectos" class="mt-5 text-error">Usuario o contraseña incorrecto</p>
                            <VBtn
                                block
                                type="submit"
                                class="mt-10"
                            >
                                Ingresar
                            </VBtn>
                        </VCol>
                    </VRow>
                </VForm>
            </VCardText>
        </VCard>

        <VImg
            class="auth-footer-start-tree d-none d-md-block"
            :src="authV1Tree"
            :width="250"
        />

        <VImg
            :src="authV1Tree2"
            class="auth-footer-end-tree d-none d-md-block"
            :width="350"
        />

        <!-- bg img -->
        <VImg
            class="auth-footer-mask d-none d-md-block"
            :src="authThemeMask"
        />
    </div>
</template>

<style lang="scss">
@use '@core/scss/pages/page-auth.scss';
</style>