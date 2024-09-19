using Microsoft.AspNetCore.Mvc;
using Product.Models;
using Product.Views;

namespace Product.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class ChecklistIDController : Controller
    {
        [HttpGet]
        [Route("checklist/{checklistId}/item")]
        public IActionResult GetAllChecklistItems(int checklistId)
        {
            try
            {
                List<Checklist> data = new List<Checklist>();
                data = ChecklistItemView.SelectChecklistItem(checklistId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("checklist/{checklistId}/item")]
        public IActionResult CreateChecklistItem(int checklistId, [FromBody] ChecklistItem checklistItem)
        {
            return Ok(new { message = $"Item added to checklist {checklistId}." });
        }

        [HttpGet]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult GetChecklistItem(int checklistId, int checklistItemId)
        {
            return Ok(new { message = $"Checklist item {checklistItemId} from checklist {checklistId} returned." });
        }

        [HttpPut]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult UpdateChecklistItemStatus(int checklistId, int checklistItemId)
        {
            return Ok(new { message = $"Checklist item {checklistItemId} updated." });
        }

        [HttpDelete]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult DeleteChecklistItem(int checklistId, int checklistItemId)
        {
            return Ok(new { message = $"Checklist item {checklistItemId} deleted from checklist {checklistId}." });
        }

        [HttpPut]
        [Route("checklist/{checklistId}/item/rename/{checklistItemId}")]
        public IActionResult RenameChecklistItem(int checklistId, int checklistItemId, [FromBody] ChecklistItem checklistItem)
        {
            return Ok(new { message = $"Checklist item {checklistItemId} renamed in checklist {checklistId}." });
        }
    }
}
