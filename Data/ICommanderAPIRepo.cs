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
        void UpdateCommand(Command cmd);
        void PartialCommandUpdate(Command cmd);
        void DeleteCommand(int id);
    }
}