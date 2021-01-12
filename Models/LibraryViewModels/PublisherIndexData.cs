using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zagrean_Robert_project.Models.LibraryViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Suplimente> Suplemets { get; set; }
        public IEnumerable<Comenzi> Comenzi { get; set; }
    }
}
