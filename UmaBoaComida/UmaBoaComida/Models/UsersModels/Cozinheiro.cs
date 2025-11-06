using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models.UsersModels {

    public class Cozinheiro : Funcionario {
        public List<Pedido> PedidosPreparando { get; } = new();

        // Removido [JsonConstructor]
        public Cozinheiro(string nome, string cpf, string email, string senha, double salario)
            : base(nome, cpf, email, senha, salario, TipoFuncionario.Cozinheiro) {
        }

        public Cozinheiro(Funcionario novoFuncionario)
            : base(novoFuncionario.Nome, novoFuncionario.Cpf, novoFuncionario.Email, novoFuncionario.Password, novoFuncionario.Salario, TipoFuncionario.Cozinheiro) {
        }

        public List<Pedido> VerPedidos() {
            return Globais.Pedidos;
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


        public List<Receita> VerReceitas() {
            return Globais.Receitas;
        }
        public void RequisitarNovaReceita(Receita receita) {
            Globais.AdicionarReceita(receita);
            RegistrarAcao($"Requisição de nova receita: {receita.Nome} - {receita.Descricao} (Id: {receita.Id})");
        }
    }
}
