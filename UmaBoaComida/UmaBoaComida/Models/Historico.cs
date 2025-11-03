using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    [Serializable]
    public class Historico {
        public static readonly string Path = System.IO.Path.Combine(
            FileSystem.AppDataDirectory,
            "Data",
            "HistoricoTrabalhos.bin"
        );

        public DateTime EntradaDoDia { get; set; }
        public DateTime SaidaDodia { get; set; }
        public List<string> Acoes { get; set; } = new();
        public string descricaoMetaDia { get; set; } = string.Empty;

        public void Registrar(string descricao) {
            string log = $"{DateTime.Now:HH:mm:ss} - {descricao}";
            Acoes.Add(log);
        }
    }
}
