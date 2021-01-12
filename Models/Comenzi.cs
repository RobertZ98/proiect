using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zagrean_Robert_project.Models
{
    public class Comenzi
    {
        public int ComenziID { get; set; }
        public int ClientID { get; set; }
        public int SuplimentID { get; set; }

        public DateTime DataComanda { get; set; }

        public Client Client { get; set; }
        public Suplimente Suplimente { get; set; }
    }
}
