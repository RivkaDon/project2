using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationRank.Models;

namespace WebApplicationRank.Data
{
    public class WebApplicationRankContext : DbContext
    {
        public WebApplicationRankContext (DbContextOptions<WebApplicationRankContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationRank.Models.Review>? Review { get; set; }
    }
}
