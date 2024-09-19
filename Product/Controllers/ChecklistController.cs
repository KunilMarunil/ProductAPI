using Microsoft.AspNetCore.Mvc;
using Product.Models;
using Product.Views;

namespace Product.Controllers
{
    [Route("checklist")]
    [ApiController]
    [Authorize]
    public class ChecklistController : Controller
    {
        [HttpGet]
        public IActionResult GetAllChecklists()
        {
            try
            {
                List<Checklist> data = new List<Checklist>();
                data = ChecklistView.SelectChecklist();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateChecklist([FromBody] Checklist checklist)
        {
            try
            {
                ChecklistView.InsertChecklist(checklist);
                return StatusCode(201, "Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{checklistId}")]
        public IActionResult DeleteChecklist(int checklistId)
        {
            try
            {
                ChecklistView.DeleteChecklist(checklistId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
