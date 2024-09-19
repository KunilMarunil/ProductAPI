using Microsoft.AspNetCore.Mvc;
using Product.Models;

namespace Product.Controllers
{
    [Route("checklist")]
    [ApiController]
    public class ChecklistController : Controller
    {
        [HttpGet]
        public IActionResult GetAllChecklists()
        {
            return Ok(new { message = "All checklists returned." });
        }

        [HttpPost]
        public IActionResult CreateChecklist([FromBody] Checklist checklist)
        {
            return Ok(new { message = "Checklist created successfully." });
        }

        [HttpDelete]
        [Route("checklist/{checklistId}")]
        public IActionResult DeleteChecklist(int checklistId)
        {
            return Ok(new { message = "Checklist deleted successfully." });
        }
    }
}
