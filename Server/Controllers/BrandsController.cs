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
    public class BrandsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Brands
        [HttpGet("all")]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _unitOfWork.BrandsRepository.GetAllAsync();

            if (brands == null)
            {
                return NotFound();
            }
            return Ok(brands);
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id)
        {
            var brand = await _unitOfWork.BrandsRepository.GetAsync(x => x.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            _unitOfWork.BrandsRepository.Update(brand);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(brand);
        }

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostBrand(Brand brand)
        {
            await _unitOfWork.BrandsRepository.InsertAsync(brand);
          
            await _unitOfWork.SaveAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brandToDelete = await _unitOfWork.BrandsRepository.GetAsync(b=>b.Id == id);
            if (brandToDelete != null)
            {
                await _unitOfWork.BrandsRepository.DeleteAsync(id);
                await _unitOfWork.SaveAsync();
            }
           

            return NoContent();
        }

        private async Task<bool> BrandExists(int id)
        {
           var brand = await _unitOfWork.BrandsRepository.GetAsync(b => b.Id == id);
            if(brand != null)
                return true;
            else return false;
        }
    }
}
