import Swal from 'sweetalert2';

export function algoSalioMalError(){
    Swal.fire({
        title: "Algo salió mal!",
        text: "Vuelve a intentar mas tarde",
        icon: "error",
        color: "#fff",
        background: "#212121",
        confirmButtonColor: "#3085d6",
    });
}

//Recibe como parametro que es lo que se está registrando
export function registroExitosoMensaje(item) {
    Swal.fire({
        title: "Registrado!",
        text: `Se registró el ${item} correctamente`, 
        icon: "success",
        color: "#fff",
        background: "#212121",
        confirmButtonColor: "#3085d6",
    });
}