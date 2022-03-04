using RatesFixer.BLL.Models;
using System.Collections.Generic;

namespace RatesFixer.BLL.Interfaces
{
    public interface IPartOperationService : IBaseService<PartOperationVM>, IWithOwnerService<PartOperationVM>, IRangeFinderService<PartOperationVM>
    {
        List<int> FindParts(int? divisionId);
    }
}
