using Common.Entities;
using Common.Model;
using Common.Model.Ack;
using Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Service
{
    public interface IShiftService
    {
       
        Ack CloseShift(ShiftModel model);
        Ack Create(ShiftModel model);
    }
}
