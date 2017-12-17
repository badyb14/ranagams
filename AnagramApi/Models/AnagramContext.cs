using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AngramApi.Models
{
  public class AnagramContext : DbContext
  {
    public AnagramContext(DbContextOptions<AnagramContext> options) : base(options)
    {

    }

    public DbSet<AnagramItem> AngramItems { get; set; }
  }
}
