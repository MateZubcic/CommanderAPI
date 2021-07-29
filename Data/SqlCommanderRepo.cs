using System;
using System.Collections.Generic;
using System.Linq;
using CommanderAPI.Models;

namespace CommanderAPI.Data
{
    public class SqlCommanderRepo : ICommanderAPIRepo
    {
        private readonly CommanderAPIContext _context;

        public SqlCommanderRepo(CommanderAPIContext context)
        {
            _context = context;    
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
             return _context.Commands.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}