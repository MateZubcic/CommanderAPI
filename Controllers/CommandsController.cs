using System.Collections.Generic;
using AutoMapper;
using CommanderAPI.Data;
using CommanderAPI.Dtos;
using CommanderAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommanderAPI.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommanderAPIRepo _repository;

        public CommandsController(ICommanderAPIRepo repository, IMapper mapper)
        {   
            _mapper = mapper;
            _repository = repository;
        }
        
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            else
                return NotFound();
        }
    }
}