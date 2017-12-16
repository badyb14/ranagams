using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngramApi.Models
{
  public class AngramItem
  {
    public long Id { get; set; }

    public string Word { get; set; }

    public string Language { get; set; }

    public long Requested { get; set; }
  }
}
