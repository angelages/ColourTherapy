using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Threading.Tasks;
using ColourTherapy.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColourTherapy.Services
{
    public class TherapyService
    {
        private readonly HttpClient _httpClient;
        private readonly TranslationService _translationService;
        
        public TherapyService(HttpClient httpClient, TranslationService translationService)
        {
            _httpClient = httpClient;
            _translationService = translationService;
        }    
        
    public async Task<List<Mood>> GetMoodsAsync()
    {
        string json = await _httpClient.GetStringAsync("data/moods.json");
        var moods = JsonConvert.DeserializeObject<List<Mood>>(json) ?? new List<Mood>();
        
        // Ensure each mood has at least empty translation objects
        foreach (var mood in moods)
        {
            if (mood.NameTranslations == null)
            {
                mood.NameTranslations = new Translation
                {
                    En = mood.Name
                };
            }
            
            if (mood.DescriptionTranslations == null)
            {
                mood.DescriptionTranslations = new Translation
                {
                    En = mood.Description
                };
            }
        }
        
        return moods;
    }
        public async Task<List<JObject>> GetColourDataAsync()
        {
            // Using Newtonsoft.Json to handle the JSON parsing
            string json = await _httpClient.GetStringAsync("data/colours.json");
            return JsonConvert.DeserializeObject<List<JObject>>(json) ?? new List<JObject>();
        }
    public async Task<List<Flower>> GetFlowersAsync()
    {
        string json = await _httpClient.GetStringAsync("data/flowers.json");
        var flowers = JsonConvert.DeserializeObject<List<Flower>>(json) ?? new List<Flower>();
        
        // Ensure each flower has at least empty translation objects
        foreach (var flower in flowers)
        {
            if (flower.NameTranslations == null)
            {
                flower.NameTranslations = new Translation
                {
                    En = flower.Name
                };
            }
            
            if (flower.DescriptionTranslations == null)
            {
                flower.DescriptionTranslations = new Translation
                {
                    En = flower.Description
                };
            }
            
            if (flower.SymbolicMeaningTranslations == null)
            {
                flower.SymbolicMeaningTranslations = new Translation
                {
                    En = flower.SymbolicMeaning
                };
            }
        }
        
        return flowers;
    }
    public async Task<List<TherapyColour>> GetColoursForMoodAsync(string moodId)
    {
        if (string.IsNullOrEmpty(moodId))
            return new List<TherapyColour>();
            
        var colourData = await GetColourDataAsync();
        
        if (colourData.Count == 0)
            return new List<TherapyColour>();
        
        // Find the mood entry that matches the requested moodId
        JObject? matchingMood = colourData.FirstOrDefault(item => 
            item["moodId"]?.ToString() == moodId);
        
        if (matchingMood == null)
            return new List<TherapyColour>();
            
        var colours = new List<TherapyColour>();
        
        // Get the colours array from the matching mood
        JArray? colourArray = matchingMood["colours"] as JArray;
        
        if (colourArray != null)
        {
            foreach (JToken token in colourArray)
            {
                if (token is JObject colourItem)
                {
                    // Extract properties with fallback values
                    string name = colourItem["name"]?.ToString() ?? "Unknown";
                    string hexCode = colourItem["hexCode"]?.ToString() ?? "#000000";
                    string effect = colourItem["effect"]?.ToString() ?? "No description available";
                    
                    // Extract translations if available
                    JObject? nameTranslationsObj = colourItem["nameTranslations"] as JObject;
                    JObject? effectTranslationsObj = colourItem["effectTranslations"] as JObject;
                    
                    var nameTranslations = new Translation();
                    var effectTranslations = new Translation();
                    
                    if (nameTranslationsObj != null)
                    {
                        nameTranslations.En = nameTranslationsObj["en"]?.ToString();
                        nameTranslations.Ko = nameTranslationsObj["ko"]?.ToString();
                    }
                    else
                    {
                        // Default to English if no translations
                        nameTranslations.En = name;
                    }
                    
                    if (effectTranslationsObj != null)
                    {
                        effectTranslations.En = effectTranslationsObj["en"]?.ToString();
                        effectTranslations.Ko = effectTranslationsObj["ko"]?.ToString();
                    }
                    else
                    {
                        // Default to English if no translations
                        effectTranslations.En = effect;
                    }
                    
                    colours.Add(new TherapyColour
                    {
                        Name = name,
                        HexCode = hexCode,
                        Effect = effect,
                        NameTranslations = nameTranslations,
                        EffectTranslations = effectTranslations
                    });
                }
            }
        }
        
        return colours;
    }
        public async Task<List<Flower>> GetFlowersForColoursAsync(List<TherapyColour> colours)
        {
            var allFlowers = await GetFlowersAsync();
            var matchingFlowers = new List<Flower>();
            
            if (colours != null && colours.Count > 0)
            {
                foreach (var colour in colours)
                {
                    if (!string.IsNullOrEmpty(colour.Name))
                    {
                        // Find flowers that match this colour
                        var matches = allFlowers
                            .Where(f => f.ColourMatch == colour.Name)
                            .ToList();
                        
                        matchingFlowers.AddRange(matches);
                    }
                }
            }
            
            // Return distinct flowers by name
            return matchingFlowers
                .DistinctBy(f => f.Name)
                .ToList();
        }
        
    public TherapyResult CreateResult(Mood mood, List<TherapyColour> colours, List<Flower> flowers)
    {
        string currentLanguage = _translationService.CurrentLanguage;
        
        // Format date based on current language
        string dateFormat = currentLanguage.ToLower() == "ko" ? "yyyy년 MM월 dd일" : "MMMM dd, yyyy";
        
        return new TherapyResult
        {
            SelectedMood = mood,
            RecommendedColours = colours,
            RecommendedFlowers = flowers,
            Date = DateTime.Now.ToString(dateFormat)
        };
    }
        public string GetCurrentLanguage()
        {
            return _translationService.CurrentLanguage;
        }
        
        public async Task<List<Flower>> GetLocalizedFlowersAsync()
        {
            var flowers = await GetFlowersAsync();
            string currentLanguage = _translationService.CurrentLanguage;
            
            // No need to transform the data - the Flower model already has GetLocalized methods
            // that will be used in the UI components
            
            return flowers;
        }
        
        public async Task<List<Mood>> GetLocalizedMoodsAsync()
        {
            var moods = await GetMoodsAsync();
            string currentLanguage = _translationService.CurrentLanguage;
            
            // No need to transform the data - the Mood model already has GetLocalized methods
            // that will be used in the UI components
            
            return moods;
        }
    }
}
