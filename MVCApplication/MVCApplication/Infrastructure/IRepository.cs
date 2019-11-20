using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplication.Infrastructure
{
    public interface IRepository<TEntity, TIdentity>
    {

        IQueryable<TEntity> GetAll();

        TEntity GetDetails(TIdentity identity);

        void CreateNew(TEntity item);

        void Update(TEntity item);

        void delete(TIdentity identity);

    }
}
