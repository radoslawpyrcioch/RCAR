using RCAR.Domain.Context;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private bool disposed = false;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Car = new GenericRepository<Car>(_applicationDbContext);
            Member = new GenericRepository<Member>(_applicationDbContext);
            PaymentRecord = new GenericRepository<PaymentRecord>(_applicationDbContext);
            Service = new GenericRepository<Service>(_applicationDbContext);
            User = new GenericRepository<User>(_applicationDbContext);
        }
        public IGenericRepository<Car> Car { get; private set; }
        public IGenericRepository<Member> Member { get; private set; }
        public IGenericRepository<PaymentRecord> PaymentRecord { get; private set; }
        public IGenericRepository<Service> Service { get; private set; }
        public IGenericRepository<User> User { get; private set; }

        public void Dispose()
        {
            if(!disposed)
            {
                _applicationDbContext.Dispose();
            }
            disposed = true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _applicationDbContext.SaveChangesAsync() > 0);
        }
    }
}
