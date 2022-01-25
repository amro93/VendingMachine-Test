using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Products;

namespace VendingMachine.Application.Services
{
    public interface IDBSeedService
    {
        void Seed();
    }
}
