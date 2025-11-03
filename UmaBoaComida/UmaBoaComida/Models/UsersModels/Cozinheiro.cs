using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models.UsersModels {
    [Serializable]
    public class Cozinheiro : Funcionario {
        public List<Pedido> PedidosPreparando { get; } = new();

        public Cozinheiro(string nome, string email, string cpf, string senha)
            : base(nome, cpf, email, senha, TipoFuncionario.Cozinheiro) {
        }

        public void PrepararPedido(Pedido pedido) {
            if (pedido.Status != PedidoStatus.Aceito) return;

            pedido.Status = PedidoStatus.Preparando;
            PedidosPreparando.Add(pedido);

            RegistrarAcao($"Iniciou preparo do pedido #{pedido.Id}");
        }

        public void FinalizarPedido(Pedido pedido) {
            if (!PedidosPreparando.Contains(pedido)) return;

            pedido.Status = PedidoStatus.Finalizado;
            PedidosPreparando.Remove(pedido);

            RegistrarAcao($"Finalizou pedido #{pedido.Id}");
        }

        public void RequisitarNovaReceita(string nome, string descricao) {
            RegistrarAcao($"Requisição de nova receita: {nome} - {descricao}");

        }
    }
}
