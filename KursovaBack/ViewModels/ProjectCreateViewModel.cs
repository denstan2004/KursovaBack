using KursovaBack.Models.Enums;

namespace KursovaBack.ViewModels
{
    public class ProjectCreateViewModel
    {
      
        public string Category { get; set; }
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Analog { get; set; }
        public int InvestmentAmount { get; set; }
        public int InvestmentMoney { get; set; }
        public IFormFile fromFile { get; set; }
        public string ImageBase64 { get; set; }
        public byte[] Image { get; set; }

    }
}
