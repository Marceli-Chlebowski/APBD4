using WebApplication1.Database;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Endpoints
{
    public interface IAnimalEndpointService
    {
        IActionResult GetAnimals(MockDb db);
        IActionResult GetAnimal(int id, MockDb db);
        IActionResult AddAnimal(Animal animal, MockDb db);
        IActionResult UpdateAnimal(int id, Animal updatedAnimal, MockDb db);
        IActionResult DeleteAnimal(int id, MockDb db);
    }

    public class AnimalEndpointService : IAnimalEndpointService
    {
        public IActionResult GetAnimals(MockDb db)
        {
            return (IActionResult)Results.Ok(db.Animals);
        }

        public IActionResult GetAnimal(int id, MockDb db)
        {
            var animal = db.Animals.FirstOrDefault(a => a.Id == id);
            return (IActionResult)(animal != null ? Results.Ok(animal) : Results.NotFound());
        }

        public IActionResult AddAnimal(Animal animal, MockDb db)
        {
            db.Animals.Add(animal);
            return (IActionResult)Results.Created($"/animals/{animal.Id}", animal);
        }

        public IActionResult UpdateAnimal(int id, Animal updatedAnimal, MockDb db)
        {
            var animal = db.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return (IActionResult)Results.NotFound();
            animal.Name = updatedAnimal.Name;
            animal.Category = updatedAnimal.Category;
            animal.Weight = updatedAnimal.Weight;
            animal.FurColor = updatedAnimal.FurColor;
            return (IActionResult)Results.Ok(animal);
        }

        public IActionResult DeleteAnimal(int id, MockDb db)
        {
            var animal = db.Animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return (IActionResult)Results.NotFound();
            db.Animals.Remove(animal);
            return (IActionResult)Results.NoContent();
        }
    }

    public static class AnimalEndpoints
    {
        public static void MapAnimalEndpoints(this WebApplication app, IAnimalEndpointService animalService, MockDb db)
        {
            app.MapGet("/animals", () => animalService.GetAnimals(db));
            app.MapGet("/animals/{id}", (int id) => animalService.GetAnimal(id, db));
            app.MapPost("/animals", (Animal animal) => animalService.AddAnimal(animal, db));
            app.MapPut("/animals", (int id, Animal updatedAnimal) => animalService.UpdateAnimal(id, updatedAnimal, db));
            app.MapDelete("/animals", (int id) => animalService.DeleteAnimal(id, db));
        }
    }
}
