using AOEStatsApp.ViewModels.DomainViewModels;
using Domain.Enums;
using Domain.Models;

namespace AOEStatsApp.ViewModels
{
    public class UnitStatsItemViewModel : DomainViewModelBase
    {
        private readonly UnitStatsItem _unitStatsItem;

        public Unit UnitType => _unitStatsItem.UnitType;
        public Civilization Civilization => _unitStatsItem.Civilization;
        public string? Attributes => _unitStatsItem.Attributes;
        public double FoodCost => _unitStatsItem.FoodCost;
        public double WoodCost => _unitStatsItem.WoodCost;
        public double GoldCost => _unitStatsItem.GoldCost;
        public double StoneCost => _unitStatsItem.StoneCost;
        public bool IsDone => _unitStatsItem.IsDone;
        public string? VideoLink => _unitStatsItem.VideoLink;

        public UnitStatsItemViewModel(UnitStatsItem unitStatsItem) : base(unitStatsItem)
        {
            _unitStatsItem = unitStatsItem;
        }
    }
}
