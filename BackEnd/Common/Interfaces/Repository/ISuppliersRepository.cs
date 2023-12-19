﻿using Common.Entities;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Repository
{
    public interface ISuppliersRepository : IRepository<Suppliers>
    {
        Suppliers  Obtener(int id);
       
    }
}