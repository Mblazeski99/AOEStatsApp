using Domain.Enums;

namespace Domain.Models
{
    public class UnitStatsItem : BaseEntity
    {
        public Unit UnitType { get; set; }
        public Civilization Civilization { get; set; }
        public string? Attributes { get; set; }
        public double FoodCost { get; set; }
        public double WoodCost { get; set; }
        public double GoldCost { get; set; }
        public double StoneCost { get; set; }
        public bool IsDone { get; set; }
        public string? VideoLink { get; set; }
    }
}
