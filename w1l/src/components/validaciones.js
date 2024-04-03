

export const reglaObligatoria = () => (value) => {
    if (Array.isArray(value) && value.length === 0) {
      return 'El campo es obligatorio';
    }
    
    return value ? true : 'El campo es obligatorio';
  };

export const validarEmail = () => (value) => {
    if (!value) return true; // El campo puede estar vacío
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(value) || 'Formato de email incorrecto';
};
