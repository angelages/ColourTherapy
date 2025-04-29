using System.Collections.Generic;

namespace ColourTherapy.Models
{
    public class Translation
    {
        public string? En { get; set; }
        public string? Ko { get; set; }
    }
    
    public class Mood
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Translation? NameTranslations { get; set; }
        public Translation? DescriptionTranslations { get; set; }
        
        public string GetLocalizedName(string language)
        {
            if (NameTranslations == null)
                return Name ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                NameTranslations.Ko ?? Name ?? string.Empty : 
                NameTranslations.En ?? Name ?? string.Empty;
        }
        
        public string GetLocalizedDescription(string language)
        {
            if (DescriptionTranslations == null)
                return Description ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                DescriptionTranslations.Ko ?? Description ?? string.Empty : 
                DescriptionTranslations.En ?? Description ?? string.Empty;
        }
    }

    public class TherapyColour
    {
        public string? Name { get; set; }
        public string? HexCode { get; set; }
        public string? Effect { get; set; }
        public Translation? NameTranslations { get; set; }
        public Translation? EffectTranslations { get; set; }
        
        public string GetLocalizedName(string language)
        {
            if (NameTranslations == null)
                return Name ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                NameTranslations.Ko ?? Name ?? string.Empty : 
                NameTranslations.En ?? Name ?? string.Empty;
        }
        
        public string GetLocalizedEffect(string language)
        {
            if (EffectTranslations == null)
                return Effect ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                EffectTranslations.Ko ?? Effect ?? string.Empty : 
                EffectTranslations.En ?? Effect ?? string.Empty;
        }
    }

    public class Flower
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? ColourMatch { get; set; }
        public string? SymbolicMeaning { get; set; }
        public Translation? NameTranslations { get; set; }
        public Translation? DescriptionTranslations { get; set; }
        public Translation? SymbolicMeaningTranslations { get; set; }
        
        public string GetLocalizedName(string language)
        {
            if (NameTranslations == null)
                return Name ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                NameTranslations.Ko ?? Name ?? string.Empty : 
                NameTranslations.En ?? Name ?? string.Empty;
        }
        
        public string GetLocalizedDescription(string language)
        {
            if (DescriptionTranslations == null)
                return Description ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                DescriptionTranslations.Ko ?? Description ?? string.Empty : 
                DescriptionTranslations.En ?? Description ?? string.Empty;
        }
        
        public string GetLocalizedSymbolicMeaning(string language)
        {
            if (SymbolicMeaningTranslations == null)
                return SymbolicMeaning ?? string.Empty;
                
            return language.ToLower() == "ko" ? 
                SymbolicMeaningTranslations.Ko ?? SymbolicMeaning ?? string.Empty : 
                SymbolicMeaningTranslations.En ?? SymbolicMeaning ?? string.Empty;
        }
    }

    public class TherapyResult
    {
        public Mood? SelectedMood { get; set; }
        public List<TherapyColour>? RecommendedColours { get; set; }
        public List<Flower>? RecommendedFlowers { get; set; }
        public string? Date { get; set; }
    }
}
