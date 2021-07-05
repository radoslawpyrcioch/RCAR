using RCAR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Car> Car { get; }
        IGenericRepository<Member> Member { get; }
        IGenericRepository<PaymentRecord> PaymentRecord { get; }
        IGenericRepository<Service> Service { get; }
        IGenericRepository<User> User { get; }

        Task<bool> SaveChangesAsync();
    }
}
