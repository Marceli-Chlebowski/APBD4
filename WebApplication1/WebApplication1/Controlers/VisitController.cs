using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/visits")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        public IActionResult GetVisits()
        {
            var visits = _visitService.GetVisits();
            return Ok(visits);
        }

        [HttpPost]
        public IActionResult AddVisit([FromBody] Visit visit)
        {
            var addedVisit = _visitService.AddVisit(visit);
            return Created($"/visits/{addedVisit.Id}", addedVisit);
        }

        [HttpGet("{id}")]
        public IActionResult GetVisit(int id)
        {
            var visit = _visitService.GetVisit(id);
            if (visit == null)
            {
                return NotFound($"Visit with ID {id} not found.");
            }

            return Ok(visit);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVisit(int id, [FromBody] Visit updatedVisit)
        {
            var visit = _visitService.GetVisit(id);
            if (visit == null)
            {
                return NotFound();
            }

            _visitService.UpdateVisit(id, updatedVisit);
            return Ok(updatedVisit);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVisit(int id)
        {
            var visit = _visitService.GetVisit(id);
            if (visit == null)
            {
                return NotFound();
            }

            _visitService.DeleteVisit(id);
            return NoContent();
        }
    }
}