const validacionesStore = {
    namespaced: true,
    state: () => ({
        reglas: {
          // Validacion formulario de registro de empleado
          nombre: [
            value => {
              if (value) return true
              return 'El nombre es obligatorio'
            }
          ],
          apellido: [
            value => {
              if (value) return true 
              return 'El apellido es obligatorio'
            }
          ],
          dni: [
            value => {
              if (value) return true
              return 'El DNI es obligatorio'
            }
          ],
          telefono: [
            value => {
              if (value) return true
              return 'El telefono es obligatorio'
            }
          ],
          direccion: [
            value => {
              if (value) return true
              return 'La direccion es obligatoria'
            }
          ],
          email: [
            value => {
              if (value) return true
              return 'El email es obligatorio'
            },
            value => {
              if (/^[a-z.-]+@[a-z.-]+\.[a-z]+$/i.test(value)) return true
              return 'Email inválido' 
            },
          ],
          sueldoNormal: [
            value => {
              if (value) return true
              return 'Debe colocar el valor de la hora normal'
            }
          ],
        
          sueldoFeriado: [
            value => {
              if (value) return true
              return 'Debe colocar el valor de la hora feriado'
            }
          ],
        
          sueldoDomingo: [
            value => {
              if (value) return true
              return 'Debe colocar el valor de la hora domingo'
            }
          ],
        
          puestos: [
            value => {
              if (value) return true
              return 'Debe seleccionar al menos un puesto'
            }
          ],
          confirmarDatos: [
            value => {
              if (value) return true
              return 'Debe confirmar los datos'
            }
          ],
        
      // fin validaciones formulario de registro de empleado
    
      
        // Validacion formulario adelanto de sueldo
            empleados: [
            value => {
                if (value) return true
                return 'Debe seleccionar un empleado'
            }
            ],

            adelantoSueldo: [
            value => {
                if (value) return true
                return 'Debe ingresar el adelanto para el empleado seleccionado'
            }
            ],
            confirmarAdelanto: [
            value => {
                if (value) return true
                return 'Debe confirmar el adelanto'
            }
            ],  

        //fin validaciones formulario de registro adelanto de empleados

            password: [
            value => {
                if (value) return true
                return 'Debe ingresar la contraseña'
            }
            ],  
        }
    }),
    getters: {
      reglas(state) {
        return state.reglas
      }
    },
    mutations: {
    },
    actions: {
    },
    modules: {
    }
}

export default validacionesStore;