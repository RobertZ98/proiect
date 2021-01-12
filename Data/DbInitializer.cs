using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zagrean_Robert_project;
using Zagrean_Robert_project.Models;

namespace Zagrean_Robert_project.Data
{
    public class DbInitializer
    {
        public static void Initialize(SuplimenteContext context)
        {
            context.Database.EnsureCreated();


            if (context.Suplimentes.Any())
            {
                return; //db a fost creata anterior
            }
            var suplimentes = new Suplimente[]
            {
                new Suplimente { Tip = "Proteine", Producator = "Protein Inc", Pret = Decimal.Parse("22") },
                new Suplimente { Tip = "Aminoacizi", Producator = "Food Life", Pret = Decimal.Parse("18") },
                new Suplimente { Tip = "Energizante", Producator = "Healthy Food", Pret = Decimal.Parse("47") },
                new Suplimente { Tip = "Creatina", Producator = "Gym Foods", Pret = Decimal.Parse("14") },
                new Suplimente { Tip = "Vitamine", Producator = "Protein Life", Pret = Decimal.Parse("20") },
                new Suplimente { Tip = "Mass Gainer", Producator = "Dedicated", Pret = Decimal.Parse("19") }
            };
            foreach (Suplimente s in suplimentes)
            {
                context.Suplimentes.Add(s);
            }
            context.SaveChanges();

            var clienti = new Client[]
            {
                new Client{ClientID=1050,Nume="Alexandru",DataNasterii=DateTime.Parse("1990-09-01")},
                new Client{ClientID=1045,Nume="Razvan",DataNasterii=DateTime.Parse("1990-07-08")},
            };

            foreach (Client c in clienti)
            {
                context.Clienti.Add(c);
            }
            context.SaveChanges();

            var comenzis = new Comenzi[]
            {
                new Comenzi{SuplimentID=1,ClientID=1050, DataComanda=DateTime.Parse("02-24-2020")},
                new Comenzi{SuplimentID=3,ClientID=1045, DataComanda=DateTime.Parse("03-23-2010")},
                new Comenzi{SuplimentID=1,ClientID=1045, DataComanda=DateTime.Parse("04-22-2020")},
                new Comenzi{SuplimentID=2,ClientID=1050, DataComanda=DateTime.Parse("05-21-2010")},
                
            };
            foreach (Comenzi e in comenzis)
            {
                context.Comenzis.Add(e);
            }
            context.SaveChanges();

            var publishers = new Publisher[]
 {

             new Publisher{PublisherName="Nume 1",Adresa="Str. Aviatorilor, nr. 40, Bucuresti"},
             new Publisher{PublisherName="Nume 2",Adresa="Str. Plopilor, nr. 35, Ploiesti"},
             new Publisher{PublisherName="Nume 3",Adresa="Str. Cascadelor, nr. 22, Cluj-Napoca"},
             };
            foreach (Publisher p in publishers)
            {
                context.Publishers.Add(p);
            }
            context.SaveChanges();
            var publishedsups = new PublishedSuplements[]
            {
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Proteine" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Protein Food").ID
                 },
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Aminoacizi" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Healthy Food").ID
                 },
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Creatina" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Protein Food").ID
                 },
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Energizante" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Healthy Food").ID
                 },
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Vitamine" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Protein Food").ID
                 },
                 new PublishedSuplements {
                 SuplementeID = suplimentes.Single(c => c.Tip == "Mass Gainer" ).ID, PublisherID = publishers.Single(i => i.PublisherName == "Healthy Food").ID
                 },
            };
            foreach (PublishedSuplements ps in publishedsups)
            {
                context.PublishedSuplements.Add(ps);
            }
            context.SaveChanges();
        }
    }

}

