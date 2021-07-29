using System.Collections.Generic;
using AutoMapper;
using CommanderAPI.Data;
using CommanderAPI.Dtos;
using CommanderAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto cmd)
        {
            var commandModel = _mapper.Map<Command>(cmd);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(CommandCreateDto cmd, int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
                return NotFound();
            _mapper.Map(cmd, commandItem);

            _repository.UpdateCommand(commandItem);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(JsonPatchDocument<CommandUpdateDto> patchCommandDoc, int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
                return NotFound();

            var commandtoPatch = _mapper.Map<CommandUpdateDto>(commandItem);

            patchCommandDoc.ApplyTo(commandtoPatch, ModelState);

            if (!TryValidateModel(commandtoPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandtoPatch, commandItem);

            _repository.UpdateCommand(commandItem);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            _repository.DeleteCommand(id);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}