using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abouca.Domain
{
    public abstract class AggregateRoot
    {
        public int Id { get; protected set; }
    }
}
