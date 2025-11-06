using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    public enum PedidoStatus { Analise, Recusado, Aceito, Preparando, Finalizado, Entregue }

    
    public class Pedido {
        private static int rOContador = 1;
        public int Id { get; }
        public double ValorTotal { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
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
            Status = PedidoStatus.Analise;
            CreatedAt = DateTime.Now;
            ValorTotal = Receitas.Sum(r => r.CalcularCusto()) +
                         Alimento.Sum(a => a.CustoAproximadoPorUnidade * a.Quantidade);
        }
    }
}
