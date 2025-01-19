using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagement.Server.Data;
using CarRentalManagement.Shared.Models;
using CarRentalManagement.Server.Interfaces.IRepository;

namespace CarRentalManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ColorsController(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }

        // GET: api/Colors
        [HttpGet("all")]
        public async Task<IActionResult> GetColors()
        {
            var allColors = _unitOfWork.ColorsRepository.GetAllAsync();
            if (allColors == null)
                return NotFound();

            return Ok(allColors);
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetColor(int id)
        {
            var color = await _unitOfWork.ColorsRepository.GetAsync(c=>c.Id == id);

            if (color == null)
            {
                return NotFound();
            }

            return Ok(color);
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, Color color)
        {
            if (id != color.Id)
            {
                return BadRequest();
            }

            _unitOfWork.ColorsRepository.Update(color);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Colors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
          await _unitOfWork.ColorsRepository.InsertAsync(color);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetColor", new { id = color.Id }, color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
          var colorToDelete = await _unitOfWork.ColorsRepository.GetAsync(x=>x.Id == id);

            if (colorToDelete != null)
            {
                await _unitOfWork.ColorsRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }

            return NoContent();
        }

        private bool ColorExists(int id)
        {
            return _unitOfWork.ColorsRepository.AnyAsync(x => x.Id == id).Result;
        }
    }
}
