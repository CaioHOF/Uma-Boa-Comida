using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace UmaBoaComida.Models.UsersModels {
    
    public class Atendente : Funcionario {

        public Atendente(string nome, string cpf, string email, string senha, double salario)
            : base(nome, cpf, email, senha, salario, TipoFuncionario.Atendente) {
        }
        public Atendente(Funcionario novoFuncionario)
            : base(novoFuncionario.Nome, novoFuncionario.Cpf, novoFuncionario.Email, novoFuncionario.Password, novoFuncionario.Salario, TipoFuncionario.Atendente) {
        }

        public List<Pedido> VerPedidos() {
            return Globais.Pedidos;
        }
        public void AceitarPedido( Pedido pedido ) {
            Globais.AtualizarPedido(p => (p.Id == pedido.Id), p => p.Status = PedidoStatus.Aceito);
            RegistrarAcao($"Aceitou o pedido: (Id: {pedido.Id})");
        }
        public void RecusarPedido( Pedido pedido ) {
            Globais.AtualizarPedido(p => (p.Id == pedido.Id), p => p.Status = PedidoStatus.Recusado);
            RegistrarAcao($"Recusou o pedido: (Id: {pedido.Id})");
        }
        public void EntregarPedido(Pedido pedido) {
            Globais.AtualizarPedido(p => (p.Id == pedido.Id), p => p.Status = PedidoStatus.Entregue);
            RegistrarAcao($"Entregou o pedido: (Id: {pedido.Id})");
        }

        public Pedido CriarPedido(string nomeCliente, List<Receita> receitas, List<StockItem> alimentos) {
            var pedido = new Pedido(nomeCliente, receitas, alimentos);
            Globais.AdicionarPedido(pedido);
            RegistrarAcao($"Criou pedido para {nomeCliente} (Id: {pedido.Id})");
            return pedido;
        }
    }
}
