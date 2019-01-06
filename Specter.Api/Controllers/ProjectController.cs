using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AutoMapper;

using Specter.Api.Models;
using Specter.Api.Data.Repository;
using Specter.Api.Data.Entities;

namespace Specter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public ProjectController(IMapper mapper, IProjectRepository projectRepository, IDeliveryRepository deliveryRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _deliveryRepository = deliveryRepository;
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult<IEnumerable<ProjectExModel>> Get()
        {
            var result = _projectRepository.GetAll().ToList();
            var models = result.Select(_mapper.Map<ProjectExModel>).ToList();

            foreach(var model in models)
            {
                var deliveries = _deliveryRepository.GetByProject(model.Id)
                                                    .ToList()
                                                    .Select(_mapper.Map<DeliveryExModel>)
                                                    .ToList();
                foreach(var delivery in deliveries)
                    delivery.Project = model.Name;

                model.Deliveries = deliveries;
            }
            
            return Ok(models);
        }

        [Authorize]
        [HttpGet("{id}")]
        public virtual ActionResult<ProjectExModel> Get(Guid id)
        {
            var result = _projectRepository.GetById(id);
            var model = _mapper.Map<ProjectExModel>(result);

            model.Deliveries = _deliveryRepository.GetByProject(id).ToList()
                                                  .Select(_mapper.Map<DeliveryModel>);

            return Ok(model);
        }

        [Authorize]
        [HttpGet("{id}/deliveries")]
        public virtual ActionResult<DeliveryModel> Deliveries(Guid projectId)
        {
            var result = _deliveryRepository.GetByProject(projectId);

            return Ok(result.Select(_mapper.Map<DeliveryModel>));
        }

        [HttpPost]
        [Authorize]
        public virtual ActionResult Post([FromBody] ProjectModel model)
        {
            var entity = _mapper.Map<Project>(model);

            _projectRepository.Insert(entity);

            return Created("project/" + entity.Id, _mapper.Map<ProjectModel>(entity));
        }

        [HttpPost("{projectId}/deliveries")]
        [Authorize]
        public virtual ActionResult Post(Guid projectId, [FromBody] DeliveryModel model)
        {
            var entity = _mapper.Map<Delivery>(model);

            entity.ProjectId = projectId;

            _deliveryRepository.Insert(entity);

            return Created($"project/{projectId}/deliveries/{entity.Id}", _mapper.Map<DeliveryModel>(entity));
        }
    }
}