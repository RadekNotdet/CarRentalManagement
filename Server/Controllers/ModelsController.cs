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
    public class ModelsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelsController(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetModels()
        {
            var allModels = _unitOfWork.ModelsRepository.GetAllAsync();
            if (allModels == null)
                return NotFound();

            return Ok(allModels);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel(int id)
        {
            var model = await _unitOfWork.ModelsRepository.GetAsync(c => c.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(int id, Model model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _unitOfWork.ModelsRepository.Update(model);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

       
        [HttpPost]
        public async Task<ActionResult<Color>> PostModel(Model model)
        {
            await _unitOfWork.ModelsRepository.InsertAsync(model);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetColor", new { id = model.Id }, model);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _unitOfWork.ModelsRepository.GetAsync(x => x.Id == id);

            if (model != null)
            {
                await _unitOfWork.ModelsRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }

            return NoContent();
        }

        private bool ModelExists(int id)
        {
            return _unitOfWork.ModelsRepository.AnyAsync(x => x.Id == id).Result;
        }
    }
}
