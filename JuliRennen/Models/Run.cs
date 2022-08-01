namespace JuliRennen.Models
{
    public class Run
    {
        public int ID { get; set; }
        public DateTime DateAndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public char Rating { get; set; }
        public User? User { get; set; }
        public Route? Route { get; set; }
    }
}
