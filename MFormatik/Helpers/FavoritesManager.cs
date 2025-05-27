using System.Configuration;
namespace MFormatik.Helpers
{
    public static class FavoritesManager
    {
        private const string Key = "Favorites.PreferredViews";

        // Get the full favorites list
        public static List<string> GetFavoriteScreens()
        {
            var csv = ConfigurationManager.AppSettings[Key];
            if (string.IsNullOrWhiteSpace(csv))
                return new List<string>();

            return csv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => s.Trim())
                      .ToList();
        }

        // Add a single favorite screen (no duplicates)
        public static void AddFavoriteScreen(string screen)
        {
            if (string.IsNullOrWhiteSpace(screen))
                throw new ArgumentException("Screen name cannot be empty.", nameof(screen));

            var favorites = GetFavoriteScreens();

            if (!favorites.Contains(screen.Trim(), StringComparer.OrdinalIgnoreCase))
            {
                favorites.Add(screen.Trim());
                SaveFavoriteScreens(favorites);
            }
        }

        // Remove a single favorite screen (case-insensitive)
        public static void RemoveFavoriteScreen(string screen)
        {
            if (string.IsNullOrWhiteSpace(screen))
                throw new ArgumentException("Screen name cannot be empty.", nameof(screen));

            var favorites = GetFavoriteScreens();

            var toRemove = favorites
                .FirstOrDefault(s => string.Equals(s, screen.Trim(), StringComparison.OrdinalIgnoreCase));

            if (toRemove != null)
            {
                favorites.Remove(toRemove);
                SaveFavoriteScreens(favorites);
            }
        }

        // Save the entire favorites list back to app.config
        private static void SaveFavoriteScreens(List<string> screens)
        {
            string csv = string.Join(",", screens);

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;

            if (settings[Key] == null)
            {
                settings.Add(Key, csv);
            }
            else
            {
                settings[Key].Value = csv;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
