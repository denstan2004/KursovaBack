using KursovaBack.Models.Enums;

namespace KursovaBack.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Roles role { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string skills { get; set; }
        public string education { get; set; }
        public string expirience { get; set; }
        public string investment_info { get; set; }



    }
}
