using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCities.Server.Data;
using WorldCities.Server.Data.Models;

namespace WorldCities.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
   private readonly ApplicationDbContext _context;
   public CitiesController(ApplicationDbContext context)
   {
      _context = context;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<City>>> GetCities()
   {
      return await _context.Cities.ToListAsync();
   }

   [HttpGet("{id}")]
   public async Task<ActionResult<City>> GetCity(int id)
   {
      var city = await _context.Cities.FindAsync(id);
      if (city == null)
      {
         return NotFound();
      }
      
      return city;
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> PutCity(int id, City city)
   {
      if (id != city.Id)
      {
         return BadRequest();
      }
      _context.Entry(city).State = EntityState.Modified;

      try
      {
         await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
         if (!CityExists(id))
         {
            return NotFound();
         }else
         {
            throw;
         }
      }
      return NoContent();
   }

   [HttpPost]
   public async Task<IActionResult> PostCity(City city)
   {
      _context.Cities.Add(city);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction("GetCity", new { id = city.Id }, city);
   }

   [HttpDelete]
   public async Task<IActionResult> DeleteCity(int id)
   {
      var city = await _context.Cities.FindAsync(id);
      if (city == null)
      {
         return NotFound();
      }
      
      _context.Cities.Remove(city);
      await _context.SaveChangesAsync();
      
      return NoContent();
   }
   private bool CityExists(int id)
   {
      return _context.Cities.AsNoTracking().Any(e => e.Id == id);
   }
}

