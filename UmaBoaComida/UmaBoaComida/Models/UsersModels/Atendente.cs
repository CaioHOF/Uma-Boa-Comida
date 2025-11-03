using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UmaBoaComida.Models.UsersModels {
    [Serializable]
    public class Atendente : Funcionario {
        public Atendente(string nome, string email, string cpf, string senha)
            : base(nome, cpf, email, senha, TipoFuncionario.Atendente) {
        }

        public Pedido CriarPedido(string nomeCliente, List<Receita> receitas, List<StockItem> alimentos) {
            var pedido = new Pedido(nomeCliente, receitas, alimentos);
            RegistrarAcao($"Criou pedido para {nomeCliente} (Id: {pedido.Id})");
            return pedido;
        }

        public void EntregarPedido(Pedido pedido) {
            pedido.Status = PedidoStatus.Entregue;
            RegistrarAcao($"Entregou pedido #{pedido.Id}");
        }
    }
}
