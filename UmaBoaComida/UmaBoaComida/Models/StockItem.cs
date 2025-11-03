using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    [Serializable]
    public class StockItem {
        public string Nome { get; set; }
        public double Quantidade { get; set; }
        public double CustoAproximadoPorUnidade { get; set; }

        public StockItem(string nome, double quantidade, double custo) {
            Nome = nome;
            Quantidade = quantidade;
            CustoAproximadoPorUnidade = custo;
        }
    }

}
