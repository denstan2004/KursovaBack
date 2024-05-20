using KursovaBack.Models;

namespace KursovaBack.ResponceModels
{
    public class VideoChatStatistics
    {
        public int totalCalls { get; set; }

        public double avgMinutes { get; set; }

        public int totalMinutes { get; set; }

        public List<VideoChatHub>? calls { get; set; }
    }
}
