namespace JuliRennen.Models
{
    public class Route
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public double Distance { get; set; }
        public double GPSxStart { get; set; }
        public double GPSxEnd { get; set; }
        public double GPSyStart { get; set; }
        public double GPSyEnd { get; set; }
        public User? User { get; set; }

    }
}
