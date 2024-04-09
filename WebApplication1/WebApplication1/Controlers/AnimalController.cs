using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controlers;
[ApiController]
[Route("/animals-controller")]
public class AnimalController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok();
    }
    [HttpGet("{id}")]
    public IActionResult GetAnimal(int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult AddAnimals()
    {
        return Ok();
    }
}