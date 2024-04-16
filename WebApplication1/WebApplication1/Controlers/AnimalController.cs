using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public IActionResult GetAnimals()
        {
            var animals = _animalService.GetAnimals();
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = _animalService.GetAnimal(id);
            if (animal == null)
            {
                return NotFound($"Animal with ID {id} not found.");
            }

            return Ok(animal);
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal? animal)
        {
            if (animal == null || string.IsNullOrEmpty(animal.Name))
            {
                return BadRequest("Animal data is incomplete.");
            }

            var addedAnimal = _animalService.AddAnimal(animal);
            return CreatedAtAction(nameof(GetAnimal), new { id = addedAnimal.Id }, addedAnimal);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
        {
            var animal = _animalService.GetAnimal(id);
            if (animal == null)
            {
                return NotFound($"Animal with ID {id} not found.");
            }

            _animalService.UpdateAnimal(id, updatedAnimal);
            return Ok(updatedAnimal);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _animalService.GetAnimal(id);
            if (animal == null)
            {
                return NotFound($"Animal with ID {id} not found.");
            }

            _animalService.DeleteAnimal(id);
            return NoContent();
        }
    }
}
