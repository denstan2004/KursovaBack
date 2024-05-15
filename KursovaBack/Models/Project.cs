using KursovaBack.Models.Enums;

namespace KursovaBack.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Analog { get; set; }
        public int InvestmentAmount { get; set; }
        public int InvestmentMoney    { get; set; }

        public Project(Guid id, string category, Guid creatorId, string name, string description, string analog, int investmentAmount, int investmentMoney)
        {
            Id = id;
            Category = category;
            CreatorId = creatorId;
            Name = name;
            Description = description;
            Analog = analog;
            InvestmentAmount = investmentAmount;
            InvestmentMoney = investmentMoney;
        }

        public Project()
        {
        }
    }
}
