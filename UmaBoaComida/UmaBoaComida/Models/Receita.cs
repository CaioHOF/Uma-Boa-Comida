using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    [Serializable]
    public class Receita {
        private static int rOContador = 1;
        public int Id { get; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<StockItem> Ingredientes { get; set; } = new();
        public string ListaNutricional { get; set; }
        public double Preco { get; set; }
        public bool Aprovada { get; set; }

        public double CalcularCusto()
            => Ingredientes.Sum(i => i.CustoAproximadoPorUnidade * i.Quantidade);

        public Receita(string nome, string desc, string nutri, double preco) {
            Id = rOContador++;
            Nome = nome;
            Descricao = desc;
            ListaNutricional = nutri;
            Preco = preco;
            Aprovada = false;
        }
    }
}
