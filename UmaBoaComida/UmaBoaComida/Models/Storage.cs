using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    public static class Storage {
        public static void Save<T>(string path, T data) {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions {
                WriteIndented = true,
                IncludeFields = true
            });

            File.WriteAllText(path, json);
        }

        public static T? Load<T>(string path) {
            if (!File.Exists(path) || new FileInfo(path).Length == 0)
                return default;

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions {
                IncludeFields = true
            });
        }

        public static void Remove<T>(string path, Predicate<T> match) {
            var list = Load<List<T>>(path) ?? new List<T>();
            list.RemoveAll(match);
            Save(path, list);
        }

        public static void Update<T>(string path, Predicate<T> match, Action<T> updateAction) {
            var list = Load<List<T>>(path) ?? new List<T>();
            bool changed = false;

            foreach (var item in list) {
                if (match(item)) {
                    updateAction(item);
                    changed = true;
                }
            }

            if (changed)
                Save(path, list);
        }
    }
}
