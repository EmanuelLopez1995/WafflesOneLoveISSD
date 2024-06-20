
import Swal from 'sweetalert2';

export function algoSalioMalError(theme){
    Swal.fire({
        title: "Algo salió mal!",
        text: "Vuelve a intentar mas tarde",
        icon: "error",
        color: theme['letras'],
        background: theme['background'],
        confirmButtonColor: theme['primary'],
    });
}


export function registroExitosoMensaje(item, theme) {
    Swal.fire({
        title: "Registrado!",
        text: `Se registró el ${item} correctamente`, 
        icon: "success",
        color: theme['letras'],
        background: theme['background'],
        confirmButtonColor: theme['primary'],
    });
}

export function eliminarRegistro(eliminarFuncion, id, nombre, theme) {
    Swal.fire({
        title: "Estas seguro?",
        text: " No podrás revertir esta acción!",
        icon: "warning",
        color: theme['letras'],
        background: theme['background'],
        showCancelButton: true,
        confirmButtonColor: theme['primary'],
        cancelButtonColor: theme['error-darken-1'],
        confirmButtonText: "Si, eliminar!",
        cancelButtonText: "Cancelar",
        }).then(async (result) => {
            if (result.isConfirmed) {
                eliminarFuncion(id);
                Swal.fire({
                    title: "Eliminado!",
                    text: `El registro con nombre ${nombre} fue eliminado`,
                    icon: "success",
                    color: theme['letras'],
                    background: theme['background'],
                    confirmButtonColor: theme['primary']
                });
            }
    });
}

export function warningMessage(mensaje, theme) {
    Swal.fire({
        title: "Atención!",
        text: mensaje, 
        icon: "warning",
        color: theme['letras'],
        background: theme['background'],
        confirmButtonColor: theme['primary'],
    });
}

export function mensajeConVerificacion(mensajeEstaSeguro, mensajeListo, funcion, theme) {
    Swal.fire({
        title: "Estas seguro?",
        text: mensajeEstaSeguro,
        icon: "warning",
        color: theme['letras'],
        background: theme['background'],
        showCancelButton: true,
        confirmButtonColor: theme['primary'],
        cancelButtonColor: theme['error-darken-1'],
        confirmButtonText: "Continuar",
        cancelButtonText: "Cancelar",
        }).then(async (result) => {
            if (result.isConfirmed) {
                funcion();
                Swal.fire({
                    title: "Listo!",
                    text: mensajeListo,
                    icon: "success",
                    color: theme['letras'],
                    background: theme['background'],
                    confirmButtonColor: theme['primary']
                });
            }
    });
}