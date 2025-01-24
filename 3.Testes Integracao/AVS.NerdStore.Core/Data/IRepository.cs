using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.NerdStore.Core.DomainObjects;

namespace AVS.NerdStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}