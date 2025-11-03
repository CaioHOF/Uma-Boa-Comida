using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models.UsersModels {
    [Serializable]
    public class Estoque {
        public static readonly string Path = System.IO.Path.Combine(
            FileSystem.AppDataDirectory,
            "Data",
            "Estoque.bin"
        );

        public List<StockItem> Itens { get; set; } = new();

        public void AdicionarItem(StockItem item) {
            var existente = Itens.FirstOrDefault(i => i.Nome.Equals(item.Nome, StringComparison.OrdinalIgnoreCase));
            if (existente != null) {
                existente.Quantidade += item.Quantidade;
                existente.CustoAproximadoPorUnidade = item.CustoAproximadoPorUnidade;
            } else {
                Itens.Add(item);
            }
        }


        public void ConsumirItem(string nome, int qtd) {
            var item = Itens.FirstOrDefault(i => i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            if (item != null) {
                item.Quantidade -= qtd;
                if (item.Quantidade < 0) item.Quantidade = 0;
            }
        }


        public double ObterQuantidade(string nome) {
            var item = Itens.FirstOrDefault(i => i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            return item?.Quantidade ?? 0;
        }

        public double CalcularCustoTotal() {
            return Itens.Sum(i => i.CustoAproximadoPorUnidade * i.Quantidade);
        }
    }
}
