using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface IEmployeeService : IBaseService<EmployeeVM>, IWithOwnerService<EmployeeVM>, IRangeFinderService<EmployeeVM>
    {
    }
}
