using System.ComponentModel.DataAnnotations.Schema;

namespace RatingSystem.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int RateDegree { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Rate_Session")]
        public int Session_Id { get; set; }
        public Session Rate_Session { get; set; }
        [ForeignKey("Rate_User")]
        public int User_Id { get; set; }
        public User Rate_User { get; set; }
    }
}
