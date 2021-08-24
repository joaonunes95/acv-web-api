namespace Application.Models
{
    public class SectionModel
    {
        public string Text { get; set; }
        public double Duration { get; set; }
        public double Start { get; set; }
        public SpeakerModel Speaker { get; set; }
    }
}
