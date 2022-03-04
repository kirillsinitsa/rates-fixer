using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;
using RatesFixer.DAL.EF;
using System.Collections.Generic;
using System.Linq;

namespace RatesFixer.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, RatesContext>, IRepository<Employee>
    {
        public EmployeeRepository(RatesContext context) : base(context)
        {
            dbset = context.Employees;
        }

        public override void DeleteRange(IEnumerable<int> idRange)
        {
            var entities = dbset.Where(o => idRange.Contains(o.EmployeeId));

            dbset.RemoveRange(entities);
        }
    }
}
