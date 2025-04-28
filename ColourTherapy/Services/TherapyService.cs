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
        
        public TherapyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Mood>> GetMoodsAsync()
        {
            string json = await _httpClient.GetStringAsync("data/moods.json");
            return JsonConvert.DeserializeObject<List<Mood>>(json) ?? new List<Mood>();
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
            return JsonConvert.DeserializeObject<List<Flower>>(json) ?? new List<Flower>();
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
                        
                        colours.Add(new TherapyColour
                        {
                            Name = name,
                            HexCode = hexCode,
                            Effect = effect
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
            return new TherapyResult
            {
                SelectedMood = mood,
                RecommendedColours = colours,
                RecommendedFlowers = flowers,
                Date = DateTime.Now.ToString("MMMM dd, yyyy")
            };
        }
    }
}
