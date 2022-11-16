using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Observability.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Models.Dtos;
using Models.Entities;
using DataAccess.Context;
using Repository.Core;
using Infrastructure.Interfaces;
using Models.Email;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.JsonPatch;

namespace SuppliersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<SupplierCreateOrUpdateDto> _validator;

        public SupplierController(ILoggerManager logger,
            IMapper mapper,IUnitOfWork unitOfWork,
            IEmailService emailService, IValidator<SupplierCreateOrUpdateDto> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                _logger.LogInfo("Fetching all suppliers from the database");

                var suppliers = await _unitOfWork.Suppliers.GetAllSuppliersAsync();

                var mappedSuppliers = _mapper.Map<IEnumerable<SupplierReadDto>>(suppliers);
                foreach (var item in mappedSuppliers)
                {
                    var country = await _unitOfWork.Countries.GetByIdAsync(item.CountryId);
                    var category = await _unitOfWork.Categories.GetByIdAsync(item.CategoryId);
                    item.CountryName = country.Country_Name;
                    item.CategoryName = category.Category_Name;
                }
                _logger.LogInfo($"Returning {mappedSuppliers.Count()}");

                return Ok(mappedSuppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in GetListOfAllSuppliers method: {ex}");
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailsOfSupplier(int id)
        {
            try
            {
                _logger.LogInfo("Bringing details of supplier");
                var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
                if(supplier is null)
                {
                    return NotFound();
                }
                var mappedSupplier = _mapper.Map<SupplierReadDto>(supplier);
                var country = await _unitOfWork.Countries.GetByIdAsync(mappedSupplier.CountryId);
                var category = await _unitOfWork.Categories.GetByIdAsync(mappedSupplier.CategoryId);
                mappedSupplier.CountryName = country.Country_Name;
                mappedSupplier.CategoryName = category.Category_Name;

                return Ok(mappedSupplier);  

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in GetDetailsOfSupplier method: {ex}");
                return BadRequest();
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierCreateOrUpdateDto supplierDto)
        {
            try
            {
                var validationResults = await _validator.ValidateAsync(supplierDto);
                if (!validationResults.IsValid)
                {

                    validationResults.AddToModelState(this.ModelState);
                    return ValidationProblem();
                }

                var supplierTobeInserted =  _mapper.Map<Supplier>(supplierDto);

                supplierTobeInserted.Country = await _unitOfWork.Countries.GetByIdAsync(supplierTobeInserted.CountryId);
                if (supplierTobeInserted.Country is null)
                    return ValidationProblem();

                supplierTobeInserted.Category = await _unitOfWork.Categories.GetByIdAsync(supplierTobeInserted.CategoryId);
                if (supplierTobeInserted.Category is null)
                    return ValidationProblem("Category not found");

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Data");     
                }
                _unitOfWork.Suppliers.Insert(supplierTobeInserted);
                _emailService.SendEmailToNewSupplier(supplierTobeInserted);

                _logger.LogInfo($"Supplier with ID {supplierTobeInserted.Id} inserted succesfully");

                var supplierReadDto = _mapper.Map<SupplierReadDto>(supplierTobeInserted);

                return CreatedAtAction(nameof(GetDetailsOfSupplier),
                    new { Id = supplierReadDto.ID }, supplierReadDto);

                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in CreateSupplier method: {ex}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, SupplierCreateOrUpdateDto supplierDto)
        {
            try
            {
                var validationResults = await _validator.ValidateAsync(supplierDto);
                if (!validationResults.IsValid)
                {

                    validationResults.AddToModelState(this.ModelState);
                    return ValidationProblem();
                }

                var supplierFromRepo = await _unitOfWork.Suppliers.GetByIdAsync(id);
                if (supplierFromRepo is null)
                {
                    return NotFound();
                }
                
                var supplierToBeUpdated = _mapper.Map(supplierDto, supplierFromRepo);

                supplierToBeUpdated.Country = await _unitOfWork.Countries.GetByIdAsync(supplierToBeUpdated.CountryId);
                if (supplierToBeUpdated.Country is null)
                    return ValidationProblem();

                supplierToBeUpdated.Category = await _unitOfWork.Categories.GetByIdAsync(supplierToBeUpdated.CategoryId);
                if (supplierToBeUpdated.Category is null)
                    return ValidationProblem();

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Data");
                }

                _unitOfWork.Suppliers.Update(supplierToBeUpdated);
                _logger.LogInfo($"Supplier with ID {supplierFromRepo.Id} updated succesfully");

                return NoContent();
            }
            catch (Exception ex)
            {  
                _logger.LogError($"Something went wrong in UpdateSupplierMethod: {ex}");
                return BadRequest();
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EnableOrDisableSupplier(int id, JsonPatchDocument<SupplierCreateOrUpdateDto> patchDoc)
        {
            try
            {
                var existedSupplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
                if(existedSupplier is null)
                    return NotFound();

                var enabledOrDisabledSupplier = _mapper.Map<SupplierCreateOrUpdateDto>(existedSupplier);
                patchDoc.ApplyTo(enabledOrDisabledSupplier, ModelState);
                if (!TryValidateModel(enabledOrDisabledSupplier))
                    return ValidationProblem();

                _mapper.Map(enabledOrDisabledSupplier, existedSupplier);

                _unitOfWork.Suppliers.Update(existedSupplier);

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in EnableOrDisableSupplier method: {ex}");
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                var supplierFromRepo = await _unitOfWork.Suppliers.GetByIdAsync(id);
                if (supplierFromRepo is null)
                {
                    return NotFound();
                }
                _unitOfWork.Suppliers.Delete(id);
                _logger.LogInfo($"Supplier with ID {supplierFromRepo.Id} deleted succesfully");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in DeleteSupplier method: {ex}");
                return BadRequest();
            }
        }

        
    }
}
