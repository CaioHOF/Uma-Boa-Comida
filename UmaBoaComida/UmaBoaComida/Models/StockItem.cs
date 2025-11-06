using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    
    public class StockItem {
        public string Nome { get; set; }
        public double Quantidade { get; set; }
        public double CustoAproximadoPorUnidade { get; set; }

        [JsonConstructor]
        public StockItem(string nome, double quantidade, double custo) {
            Nome = nome;
            Quantidade = quantidade;
            CustoAproximadoPorUnidade = custo;
        }
    }

}
