namespace RatingSystem.Models.ViewModel
{
    public class SessionVM
    {
        public int Id { get; set; }
        public string SessionName { get; set; }
        public string Presenter { get; set; }
        public double RateAverage { get; set; }
    }
}
