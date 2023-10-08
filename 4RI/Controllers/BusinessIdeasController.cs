using _4RI.Models;
using Microsoft.AspNetCore.Mvc;

namespace _4RI.Controllers
{
    public class BusinessIdeasController : Controller
    {
        private static List<BusinessIdea> BusinessIdeas = new List<BusinessIdea>();

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BusinessIdea newIdea)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessIdeas.Add(newIdea);
                    return RedirectToAction("Index");
                }
                return View(newIdea);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al agregar la idea de negocio.");
                return View(newIdea);
            }
        }

        public IActionResult Index()
        {
            try
            {
                return View(BusinessIdeas);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al cargar la lista de ideas de negocio.");
                return View(new List<BusinessIdea>());
            }
        }

        [HttpGet]
        public IActionResult AddTeamMember(Guid businessIdeaId)
        {
            ViewBag.BusinessIdeaId = businessIdeaId;
            return View(new TeamMember());
        }

        [HttpPost]
        public IActionResult AddTeamMember(Guid businessIdeaId, TeamMember member)
        {
            var businessIdea = BusinessIdeas.FirstOrDefault(b => b.Id == businessIdeaId);
            if (businessIdea != null)
            {
                businessIdea.TeamMembers.Add(member);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult RemoveTeamMember(Guid businessIdeaId)
        {
            var businessIdea = BusinessIdeas.FirstOrDefault(b => b.Id == businessIdeaId);
            if (businessIdea != null)
            {
                ViewBag.BusinessIdeaId = businessIdeaId;
                return View(businessIdea.TeamMembers);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult DeleteTeamMember(Guid businessIdeaId, string memberId)
        {
            var businessIdea = BusinessIdeas.FirstOrDefault(b => b.Id == businessIdeaId);
            if (businessIdea != null)
            {
                var member = businessIdea.TeamMembers.FirstOrDefault(m => m.Identification == memberId);
                if (member != null)
                {
                    businessIdea.TeamMembers.Remove(member);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult UpdateFinancials(Guid businessIdeaId)
        {
            var businessIdea = BusinessIdeas.FirstOrDefault(b => b.Id == businessIdeaId);
            if (businessIdea != null)
            {
                return View(businessIdea);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateFinancials(BusinessIdea updatedIdea)
        {
            var businessIdea = BusinessIdeas.FirstOrDefault(b => b.Id == updatedIdea.Id);
            if (businessIdea != null)
            {
                businessIdea.InvestmentValue = updatedIdea.InvestmentValue;
                businessIdea.TotalIncomeIn3Years = updatedIdea.TotalIncomeIn3Years;
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult BusinessStatistics()
        {
            try
            {
                var viewModel = new BusinessStatisticsViewModel();
                // Logica de obtención de las estadísticas
                viewModel.IdeaWithMostDepartments = BusinessIdeas.OrderByDescending(b => b.BenefitedDepartments.Count).FirstOrDefault();
                viewModel.IdeaWithHighestIncome = BusinessIdeas.OrderByDescending(b => b.TotalIncomeIn3Years).FirstOrDefault();
                viewModel.Top3ProfitableIdeas = BusinessIdeas.OrderByDescending(b => b.TotalIncomeIn3Years - b.InvestmentValue).Take(3).ToList();
                viewModel.IdeasImpactingMoreThan3Depts = BusinessIdeas.Where(b => b.BenefitedDepartments.Count > 3).ToList();
                viewModel.TotalIncome = BusinessIdeas.Sum(b => b.TotalIncomeIn3Years);
                viewModel.TotalInvestment = BusinessIdeas.Sum(b => b.InvestmentValue);
                viewModel.IdeaWithMost4RITools = BusinessIdeas.OrderByDescending(b => b.Tools4IR.Count).FirstOrDefault();
                viewModel.CountOfAIUse = BusinessIdeas.Count(b => b.Tools4IR.Contains("Inteligencia Artificial"));
                viewModel.IdeasWithSustainableDevelopment = BusinessIdeas.Where(b => b.Impact.Contains("Desarrollo sostenible")).ToList();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al calcular las estadísticas.");
                return View(new BusinessStatisticsViewModel());
            }
        }
    }
}