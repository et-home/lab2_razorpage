#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using razor_ef.Model;

    public class GameStoreContext : DbContext
    {
        public GameStoreContext (DbContextOptions<GameStoreContext> options)
            : base(options)
        {
        }

        public DbSet<razor_ef.Model.Game> Game { get; set; }
    }
