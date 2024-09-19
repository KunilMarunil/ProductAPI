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
                List<ChecklistItem> data = new List<ChecklistItem>();
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
            try
            {
                ChecklistItemView.InsertChecklistItem(checklistId, checklistItem);
                return StatusCode(201, "Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult GetChecklistItem(int checklistId, int checklistItemId)
        {
            try
            {
                List<ChecklistItem> data = new List<ChecklistItem>();
                data = ChecklistItemView.SelectedChecklistItem(checklistId, checklistItemId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult UpdateChecklistItemStatus(int checklistId, int checklistItemId)
        {
            try
            {
                ChecklistItemView.UpdateChecklistItem(checklistId, checklistItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("checklist/{checklistId}/item/{checklistItemId}")]
        public IActionResult DeleteChecklistItem(int checklistId, int checklistItemId)
        {
            try
            {
                ChecklistItemView.DeleteChecklistItem(checklistId, checklistItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("checklist/{checklistId}/item/rename/{checklistItemId}")]
        public IActionResult RenameChecklistItem(int checklistId, int checklistItemId, [FromBody] ChecklistItem checklistItem)
        {
            try
            {
                ChecklistItemView.RenameChecklistItem(checklistId, checklistItemId, checklistItem);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
