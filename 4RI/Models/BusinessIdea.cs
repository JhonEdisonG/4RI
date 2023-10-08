namespace _4RI.Models
{
    public class BusinessIdea
    {
        public Guid Id { get; set; }  // Using GUID to ensure uniqueness
        public string Name { get; set; }
        public string Impact { get; set; }
        public List<Department> BenefitedDepartments { get; set; }
        public decimal InvestmentValue { get; set; }
        public decimal TotalIncomeIn3Years { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<string> Tools4IR { get; set; }

        public BusinessIdea()
        {
            this.Id = Guid.NewGuid();  // Auto-generate the ID upon creating a new idea
            this.BenefitedDepartments = new List<Department>();
            this.TeamMembers = new List<TeamMember>();
            this.Tools4IR = new List<string>();
        }
    }
}
