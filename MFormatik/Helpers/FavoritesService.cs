using MFormatik.Core.Models;
using Newtonsoft.Json;
using System.IO;

public static class FavoritesService
{
    private static readonly string _configPath = "FavoritesConfig.json";

    public static List<FavoriteView> LoadFavorites()
    {
        if (!File.Exists(_configPath))
            return new List<FavoriteView>();

        string json = File.ReadAllText(_configPath);
        var config = JsonConvert.DeserializeObject<FavoritesConfig>(json);
        return config?.Favorites ?? new List<FavoriteView>();
    }

    public static void SaveFavorites(List<FavoriteView> favorites)
    {
        var config = new FavoritesConfig { Favorites = favorites };
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText(_configPath, json);
    }

    public static void AddFavorite(FavoriteView window)
    {
        var favorites = LoadFavorites();

        // تجنب إضافة مكرر
        if (!favorites.Any(f => f.ViewName == window.ViewName))
        {
            favorites.Add(window);
            SaveFavorites(favorites);
        }
    }

    public static void RemoveFavorite(string windowName)
    {
        var favorites = LoadFavorites();
        var item = favorites.FirstOrDefault(f => f.ViewName == windowName);
        if (item != null)
        {
            favorites.Remove(item);
            SaveFavorites(favorites);
        }
    }
}
