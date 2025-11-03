using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    [Serializable]
    public class Ingrediente {
        public string Nome { get; set; }
        public double Quantidade { get; set; }

        public Ingrediente(string nome, double quantidade) {
            Nome = nome;
            Quantidade = quantidade;
        }
    }
}
