using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zagrean_Robert_project.Models
{
    public class Suplimente
    {
        public int ID { get; set; }
        public string Tip { get; set; }
        public string Producator { get; set; }

        [Column(TypeName = "decimal(6, 2)")]

        public decimal Pret { get; set; }
        public ICollection<Comenzi> Comenzis { get; set; }

        public ICollection<PublishedSuplements> PublishedSuplements { get; set; }
    }
}
