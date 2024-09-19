using Microsoft.AspNetCore.Mvc;
using Product.Models;

namespace Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistIDController : Controller
    {
        [HttpGet]
        [Route("checklist/{checklistId}/item")]
        public IActionResult GetAllChecklistItems(int checklistId)
        {
            return Ok(new { message = $"All items for checklist {checklistId} returned." });
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
