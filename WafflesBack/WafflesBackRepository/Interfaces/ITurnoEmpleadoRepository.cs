﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafflesBackCommon.Models;

namespace WafflesBackRepository.Interfaces
{
    public interface ITurnoEmpleadoRepository
    {
        Task RegistrarEmpleadoTurno(TurnoEmpleadoModel empleado);
    }
}
