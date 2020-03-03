using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tamagachi.Models;

namespace Tamagachi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet("pets")]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(p => p.Name);
      return pets.ToList();
    }

    [HttpGet("{id}")]
    public Pet GetOnePet(int id)
    {
      var onePet = db.Pets.FirstOrDefault(i => i.Id == id);
      return onePet;
    }

    [HttpPost]
    public Pet AddPet(Pet pet)
    {
      db.Pets.Add(pet);
      db.SaveChanges();
      return pet;
    }

    [HttpPut("{id}/play")]
    public Pet Play(int id)
    {
      var whichPet = db.Pets.FirstOrDefault(i => i.Id == id);
      whichPet.HappinessLevel += 5;
      whichPet.HungerLevel += 3;
      db.SaveChanges();
      return whichPet;
    }
    [HttpPut("{id}/feed")]
    public Pet Feed(int id)
    {
      var whichPet = db.Pets.FirstOrDefault(i => i.Id == id);
      whichPet.HungerLevel -= 5;
      whichPet.HappinessLevel += 3;
      db.SaveChanges();
      return whichPet;
    }
    [HttpPut("{id}/scold")]
    public Pet Scold(int id)
    {
      var whichPet = db.Pets.FirstOrDefault(i => i.Id == id);
      whichPet.HappinessLevel -= 3;
      db.SaveChanges();
      return whichPet;
    }
    [HttpDelete("{id}")]

    public ActionResult DeleteOne(int id)
    {
      var delete = db.Pets.FirstOrDefault(p => p.Id == id);
      if (delete == null)
      {
        return NotFound();
      }
      db.Pets.Remove(delete);
      db.SaveChanges();
      return Ok();
    }
  }


}
