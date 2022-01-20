using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain.Entities
{
    public interface IBaseEntity<PK> : IBaseEntity
    {
        PK Id { get; set; }
    }

    public interface IBaseEntity
    {

    }
}
