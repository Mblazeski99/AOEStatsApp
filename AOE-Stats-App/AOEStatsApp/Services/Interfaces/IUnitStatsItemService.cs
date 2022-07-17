using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOEStatsApp.Services.Interfaces
{
    public interface IUnitStatsItemService
    {
        Task<IEnumerable<UnitStatsItem>> GetAllItems();

        Task CreateUnitStatsItem(UnitStatsItem item);

        Task UpdateUnitStatsItem(UnitStatsItem item);

        Task DeleteUnitStatsItem(UnitStatsItem item);
    }
}
