using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetReposList.Data
{
    public class RepositoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Owner { get; set; }
    }
}
