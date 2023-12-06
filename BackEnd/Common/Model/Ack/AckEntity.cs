using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Ack
{
    public class AckEntity<T> : Ack
    {
        public T Entity { get; set; }
    }
}
