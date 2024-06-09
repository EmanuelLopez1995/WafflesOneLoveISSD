
export function obtenerHoraActualUTC() {

    let fechaActual = new Date();

    let horaActual = String(fechaActual.getHours()).padStart(2, '0');
    let minutoActual = String(fechaActual.getMinutes()).padStart(2, '0');
    let segundoActual = String(fechaActual.getSeconds()).padStart(2, '0');
    let milisegundoActual = String(fechaActual.getMilliseconds()).padStart(3, '0');

    return `T${horaActual}:${minutoActual}:${segundoActual}.${milisegundoActual}Z`;
}

export function formatearHoraUTC(hora) {
    return `T${hora}:00.000Z`;
}

export function obtenerFechaYHoraActualUTC() {

    let fechaActual = new Date();

    let anio = fechaActual.getFullYear();
    let mes = String(fechaActual.getMonth() + 1).padStart(2, '0');
    let dia = String(fechaActual.getDate()).padStart(2, '0');

    let horaActual = String(fechaActual.getHours()).padStart(2, '0');
    let minutoActual = String(fechaActual.getMinutes()).padStart(2, '0');
    let segundoActual = String(fechaActual.getSeconds()).padStart(2, '0');
    let milisegundoActual = String(fechaActual.getMilliseconds()).padStart(3, '0');

    return `${anio}-${mes}-${dia}T${horaActual}:${minutoActual}:${segundoActual}.${milisegundoActual}Z`;
}


export function obtenerHoraActualHHMMSS() {
    const now = new Date();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');
    
    return `${hours}:${minutes}:${seconds}`;
}