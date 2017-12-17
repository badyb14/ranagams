using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AngramApi.Models
{
  public class AnagramUsage : DbContext
  {
    public AnagramUsage(DbContextOptions<AnagramUsage> options) : base(options)
    {

    }

    public DbSet<AnagramItem> AngramItems { get; set; }

    public void RecordUsage(AnagramItem item)
    {
      //TO to validate parameters.
      //TODO execute this method as task
      //var ar =_context.anagramItems.FirstOrDefault(a => string.Compare(a.Word, word, StringComparison.OrdinalIgnoreCase) == 0);
      //if (ar == null)
      //{
      //  _context.Add(new anagramItem() { Word = word, Language = "en", Requested = 1 });
      //}
      //else
      //{
      //  ar.Requested++;
      //}
      //_context.SaveChangesAsync();
    }
  }
}
