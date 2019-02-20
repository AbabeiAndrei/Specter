using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Data.Repository;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult<IEnumerable<CategoryModel>> Get()
        {
            var result = _categoryRepository.GetAll().ToList();
            
            return Ok(result.Select(_mapper.Map<CategoryModel>));
        }

        [Authorize]
        [HttpGet("{id}")]
        public virtual ActionResult<CategoryModel> Get(Guid id)
        {
            var result = _categoryRepository.GetById(id);
            
            return Ok(_mapper.Map<CategoryModel>(result));
        }

        [AllowAnonymous]
        [HttpPost]
        public virtual ActionResult<CategoryModel> Post([FromBody] CategoryModel model) 
        {
            var entity = _mapper.Map<Data.Entities.Category>(model);

            _categoryRepository.Insert(entity);
            
            return Created("category/" + entity.Id, _mapper.Map<CategoryModel>(entity));
        }
    }
}