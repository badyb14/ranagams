using AngramApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramApi.Data
{
  interface IUsageRepository
  {
    void RecordUsage(AnagramItem item);
  }
}
