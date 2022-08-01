namespace JuliRennen.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string ProfilePic { get; set; }
        public string SetPassword { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public bool StaminaPref { get; set; }
        public bool SpeedPref { get; set; }
        public bool StrengthPref { get; set; }
        public bool StretchPref { get; set; }

    }
}
