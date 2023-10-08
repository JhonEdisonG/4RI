namespace _4RI.Models
{
    public class BusinessStatisticsViewModel
    {
        public BusinessIdea IdeaWithMostDepartments { get; set; }
        public BusinessIdea IdeaWithHighestIncome { get; set; }
        public List<BusinessIdea> Top3ProfitableIdeas { get; set; }
        public List<BusinessIdea> IdeasImpactingMoreThan3Depts { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalInvestment { get; set; }
        public BusinessIdea IdeaWithMost4RITools { get; set; }
        public int CountOfAIUse { get; set; }
        public List<BusinessIdea> IdeasWithSustainableDevelopment { get; set; }
    }
}
