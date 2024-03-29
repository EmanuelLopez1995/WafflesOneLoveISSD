
export const reglaObligatoria = () => (value) => {
    return !!value || `El campo es obligatorio`;
};

export const validarEmail = () => (value) => {
    if (!value) return true; // El campo puede estar vacío
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(value) || 'Formato de email incorrecto';
};
