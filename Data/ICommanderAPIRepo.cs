using System.Collections.Generic;
using CommanderAPI.Models;

namespace CommanderAPI.Data
{
    public interface ICommanderAPIRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
    }
}