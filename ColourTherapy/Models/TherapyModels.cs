using System.Collections.Generic;

namespace ColourTherapy.Models
{
    public class Mood
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TherapyColour
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
        public string Effect { get; set; }
    }

    public class Flower
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ColourMatch { get; set; }
        public string SymbolicMeaning { get; set; }
    }

    public class TherapyResult
    {
        public Mood SelectedMood { get; set; }
        public List<TherapyColour> RecommendedColours { get; set; }
        public List<Flower> RecommendedFlowers { get; set; }
        public string Date { get; set; }
    }
}
