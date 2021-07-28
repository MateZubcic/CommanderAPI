using System.Collections.Generic;
using CommanderAPI.Models;

namespace CommanderAPI.Data
{
    public interface ICommanderAPIRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);

    }
}