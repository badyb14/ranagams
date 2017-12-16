using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AngramApi.Models
{
  public class AngramContext : DbContext
  {
    public AngramContext(DbContextOptions<AngramContext> options) : base(options)
    {

    }

    public DbSet<AngramItem> AngramItems { get; set; }
  }
}
