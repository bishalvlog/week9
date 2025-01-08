using System.Text.Json;

namespace week9.Abstraction
{
    public abstract class UserBase<T>
    {
        private readonly string FilePath;

        protected UserBase(string fileName)
        {
            FilePath = Path.Combine(FileSystem.AppDataDirectory, fileName); 
            EnsureDirectoryExists();
        }

        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(FilePath); 
            if (!Directory.Exists(directory)) 
            { 
                Directory.CreateDirectory(directory);
            } 
        }

        protected List<T> LoadItems() 
        { 
            if (!File.Exists(FilePath)) return new List<T>();
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
        protected void SaveItems(List<T> items)
        { 
            var json = JsonSerializer.Serialize(items);
            File.WriteAllText(FilePath, json);
        }
    }
}
