using WebApplication1.Models;
using System.Collections.Generic;

namespace WebApplication1.Services
{
    public interface IAnimalService
    {
        IEnumerable<Animal> GetAnimals();
        Animal? GetAnimal(int id);
        Animal AddAnimal(Animal animal);
        void UpdateAnimal(int id, Animal updatedAnimal);
        void DeleteAnimal(int id);
    }

    public class AnimalService : IAnimalService
    {
        public IEnumerable<Animal> GetAnimals()
        {
            return StaticData.Animals;
        }

        public Animal? GetAnimal(int id)
        {
            return StaticData.Animals.FirstOrDefault(a => a.Id == id);
        }

        public Animal AddAnimal(Animal animal)
        {
            animal.Id = StaticData.Animals.Max(a => a.Id) + 1;
            StaticData.Animals.Add(animal);
            return animal;
        }

        public void UpdateAnimal(int id, Animal updatedAnimal)
        {
            var animal = StaticData.Animals.FirstOrDefault(a => a.Id == id);
            if (animal != null)
            {
                animal.Name = updatedAnimal.Name;
                animal.Category = updatedAnimal.Category;
                animal.Weight = updatedAnimal.Weight;
                animal.FurColor = updatedAnimal.FurColor;
            }
        }

        public void DeleteAnimal(int id)
        {
            var animal = StaticData.Animals.FirstOrDefault(a => a.Id == id);
            if (animal != null)
            {
                StaticData.Animals.Remove(animal);
            }
        }
    }
}