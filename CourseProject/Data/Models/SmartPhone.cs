namespace CourseProject.Data.Models
{
    public class SmartPhone : Phone
    {
        public decimal Memory { get; set; }
        public decimal Ram { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public int CoreNumber { get; set; }
        public decimal Frequency { get; set; }

    }
}
