namespace Zagrean_Robert_project.Models
{
    public class PublishedSuplements
    {
        public int PublisherID { get; set; }
        public int SuplementeID { get; set; }
        public Publisher Publisher { get; set; }
        public Suplimente Suplimente { get; set; }
    }
}