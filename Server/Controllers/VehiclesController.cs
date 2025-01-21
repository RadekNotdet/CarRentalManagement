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
    public class VehiclesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<string> includes = new List<string>() { "Model", "Brand", "Color" };

        public VehiclesController(IUnitOfWork uof)
        {
            _unitOfWork = uof;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = _unitOfWork.ModelsRepository.GetAllAsync(includes: includes);
            if (vehicles == null)
                return NotFound();

            return Ok(vehicles);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var includesForSingle = includes.Append("Bookings").ToList();
            var vehicle = await _unitOfWork.VehiclesRepository.GetAsync(expression: c => c.Id == id,includes: includesForSingle);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _unitOfWork.VehiclesRepository.Update(vehicle);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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
        public async Task<IActionResult> PostVehicle(Vehicle vehicle)
        {
            await _unitOfWork.VehiclesRepository.InsertAsync(vehicle);
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.Id }, vehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var model = await _unitOfWork.VehiclesRepository.GetAsync(x => x.Id == id);

            if (model != null)
            {
                await _unitOfWork.VehiclesRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _unitOfWork.VehiclesRepository.AnyAsync(x => x.Id == id).Result;
        }
    }
}
