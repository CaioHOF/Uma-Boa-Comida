using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    public enum PedidoStatus { Aceito, Preparando, Finalizado, Entregue }

    [Serializable]
    public class Pedido {
        private static int rOContador = 1;
        public int Id { get; }
        public string NomeCliente { get; set; }
        public List<Receita> Receitas { get; set; }
        public List<StockItem> Alimento { get; set; }
        public PedidoStatus Status { get; set; }
        public Avaliacao? Avaliacao { get; set; }

        public Pedido(string cliente, List<Receita> receitas, List<StockItem> alimento) {
            Id = rOContador++;
            NomeCliente = cliente;
            Receitas = receitas;
            Alimento = alimento;
            Status = PedidoStatus.Aceito;
        }
    }
}
