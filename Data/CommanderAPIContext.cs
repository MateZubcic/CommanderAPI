using CommanderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommanderAPI.Data
{
    public class CommanderAPIContext : DbContext
    {
        public CommanderAPIContext(DbContextOptions<CommanderAPIContext> opt) : base(opt)
        {
            
        }

        public DbSet<Command> Commands { get; set; }
    }
}