﻿using Microsoft.AspNetCore.Http;
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
using Models.Validations;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IValidator<ValidatorBase> _validator;

        public SupplierController(ILoggerManager logger,
            IMapper mapper,IUnitOfWork unitOfWork,
            IEmailService emailService, IValidator<ValidatorBase> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _validator = validator;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                _logger.LogInfo("Fetching all suppliers from the database");

                var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
                var mappedSuppliers = _mapper.Map<IEnumerable<SupplierReadDto>>(suppliers);

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
                return Ok(mappedSupplier);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in GetDetailsOfSupplier method: {ex}");
                return BadRequest();
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierCreateDto supplierCreateDto)
        {
            try
            {
                //var validationResults = _validator.Validate(supplierCreateDto)
                var supplierTobeInserted =  _mapper.Map<Supplier>(supplierCreateDto);

                supplierTobeInserted.Country = await _unitOfWork.Countries.GetByIdAsync(supplierTobeInserted.CountryId);
                if (supplierTobeInserted.Country is null)
                    return ValidationProblem();
                supplierTobeInserted.Category = await _unitOfWork.Categories.GetByIdAsync(supplierTobeInserted.CategoryId);
                if (supplierTobeInserted.Category is null)
                    return ValidationProblem();

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Data");     
                }
                _unitOfWork.Suppliers.Insert(supplierTobeInserted);
                //var message = new Message(new string[] { "steveventis@gmail.com" }, "Test", "Test body");
                //_emailService.SendEmail(message);

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
        public async Task<IActionResult> UpdateSupplier(int id,SupplierUpdateDto supplierUpdateDto)
        {
            try
            {
                var supplierFromRepo = await _unitOfWork.Suppliers.GetByIdAsync(id);
                if (supplierFromRepo is null)
                {
                    return NotFound();
                }
                
                var supplierToBeUpdated = _mapper.Map(supplierUpdateDto,supplierFromRepo);

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
