using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace UmaBoaComida.Models {
    public static class Storage {
        public static void Save<T>(string path, T data) {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using FileStream fs = new(path, FileMode.Create);
#pragma warning disable SYSLIB0011
            BinaryFormatter bf = new();
            bf.Serialize(fs, data);
#pragma warning restore SYSLIB0011
        }

        public static T? Load<T>(string path) {
            if (!File.Exists(path)) return default;
            using FileStream fs = new(path, FileMode.Open);
#pragma warning disable SYSLIB0011
            BinaryFormatter bf = new();
            return (T?)bf.Deserialize(fs);
#pragma warning restore SYSLIB0011
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
