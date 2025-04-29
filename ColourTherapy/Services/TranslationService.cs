using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ColourTherapy.Services
{
    public class TranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;        private Dictionary<string, Dictionary<string, string>>? _translations;
        private string _currentLanguage = "en"; // Default language

        public event Action OnLanguageChanged;

        public TranslationService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
        }        private Task? _initTask = null;
        private readonly object _lockObject = new object();
        
        public Task InitializeAsync()
        {
            // Use a single initialization task to prevent multiple concurrent initializations
            lock (_lockObject)
            {
                if (_initTask == null)
                {
                    _initTask = InitializeInternalAsync();
                }
                
                return _initTask;
            }
        }
        
        private async Task InitializeInternalAsync()
        {
            if (_translations == null)
            {
                // Load translations from JSON file
                string baseUri = _navigationManager.BaseUri;                var response = await _httpClient.GetFromJsonAsync<Dictionary<string, Dictionary<string, string>>>(
                    $"{baseUri}data/translations.json");
                
                if (response != null)
                {
                    _translations = response;
                }
                  // Try to load saved language preference
                try
                {
                    var savedLanguage = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "preferredLanguage");
                    if (!string.IsNullOrEmpty(savedLanguage) && _translations != null && _translations.ContainsKey(savedLanguage))
                    {
                        _currentLanguage = savedLanguage;
                    }
                }
                catch
                {
                    // Default to English if localStorage is not available
                }
            }
        }        
          public async Task SetLanguageAsync(string languageCode)
        {
            if (_translations != null && _translations.ContainsKey(languageCode))
            {
                _currentLanguage = languageCode;
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "preferredLanguage", languageCode);
                // Notify all subscribers that language has changed
                OnLanguageChanged?.Invoke();
                
                // Note: Page reload now handled by the LanguageSelector component
            }
        }

        public string GetTranslation(string key)
        {
            if (_translations == null || !_translations.ContainsKey(_currentLanguage) || 
                !_translations[_currentLanguage].ContainsKey(key))
            {
                return key; // Return the key itself as a fallback
            }

            return _translations[_currentLanguage][key];
        }

        public string CurrentLanguage => _currentLanguage;
        
        public List<string> AvailableLanguages => _translations?.Keys.ToList() ?? new List<string> { "en" };
    }
}
