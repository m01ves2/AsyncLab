namespace Ex15.FinalSynthesis.Dtos
{
    public class DashboardResponse
    {
        public Profile Profile { get; set; }
        public List<Order> Orders { get; set; }
        public Statistics Statistics { get; set; }
        public List<Recommendation> Recommendations { get; set; }
    }
}
