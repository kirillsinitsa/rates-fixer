using RatesFixer.BLL.Models;

namespace RatesFixer.BLL.Interfaces
{
    public interface ITeamService : IBaseService<TeamVM>, IWithOwnerService<TeamVM>
    {
    }
}
