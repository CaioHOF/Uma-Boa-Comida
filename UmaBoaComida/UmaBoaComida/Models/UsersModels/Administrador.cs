using System;
using System.Collections.Generic;
using System.Linq;

namespace UmaBoaComida.Models.UsersModels {

    public class Administrador : Funcionario {

        public Administrador(string nome, string cpf, string email, string senha, double salario)
            : base(nome, cpf, email, senha, salario, TipoFuncionario.Administrador) {
        }
        public Administrador(Funcionario novoFuncionario)
            : base(novoFuncionario.Nome, novoFuncionario.Cpf, novoFuncionario.Email, novoFuncionario.Password, novoFuncionario.Salario, TipoFuncionario.Administrador) {
        }

        public void MudarEstatusPedido( Pedido pedido, PedidoStatus pedidoStatus) {
            Globais.AtualizarPedido(p => (p.Id == pedido.Id), p => p.Status = pedidoStatus);
            RegistrarAcao($"Mudou o Status do pedido: (Id: {pedido.Id}) (Status: {pedido.Status})"); 
        }

        public void AprovarReceita(Receita receita) {
            Globais.AtualizarReceita( r => ( r.Id == receita.Id), r => r.Aprovada = true );
            RegistrarAcao($"Aprovou receita: {receita.Nome} (Id: {receita.Id})");
        }
        
        public void DesaprovarReceita(Receita receita) {
            Globais.AtualizarReceita(r => (r.Id == receita.Id), r => r.Aprovada = false);
            RegistrarAcao($"desaprovou receita: {receita.Nome} (Id: {receita.Id})");
        }

        public void ContratarFuncionario( Funcionario novoFuncionario) {
            switch (novoFuncionario.Tipo) {
                case TipoFuncionario.Cozinheiro:
                    Globais.AdicionarFuncionario(new Cozinheiro(novoFuncionario));
                    break;
                case TipoFuncionario.Atendente:
                    Globais.AdicionarFuncionario(new Atendente(novoFuncionario));
                    break;
                case TipoFuncionario.Administrador:
                    Globais.AdicionarFuncionario(new Administrador(novoFuncionario));
                    break;
                case TipoFuncionario.Generico:
                    Globais.AdicionarFuncionario(novoFuncionario);
                    break;
                default:
                    break;
            }
            
            RegistrarAcao($"Contratou funcionário: {novoFuncionario.Nome} ({novoFuncionario.Tipo})");
        }

        public void DemitirFuncionario(Funcionario funcionario) {
            if (Globais.Funcionarios.RemoveAll(f => f.Id == funcionario.Id) > 0) {
                RegistrarAcao($"Demitiu funcionário: {funcionario.Nome} ({funcionario.Tipo})");
            }
        }

        public void AtualizarFuncionario(Predicate<Funcionario> match, Action<Funcionario> updateAction) {
            var f = Globais.AtualizarFuncionario(match, updateAction);
            RegistrarAcao($"Atualizou o(s) funcionário(s): {string.Join(", ", f.Select(f => $"({f.Nome}, {f.Tipo})"))})");
        }

        public Funcionario? ConsultarFuncionarioGlobal(int id) {
            return Globais.Funcionarios.FirstOrDefault(f => f.Id == id);
        }

        public List<Funcionario> VerFuncionarios() {
            return Globais.Funcionarios;
        }
        public List<Pedido> VerPedidos() {
            return Globais.Pedidos;
        }
        public List<Historico> VerHistoricos() {
            return Globais.Historicos;
        }
        public List<Receita> VerReceitas() {
            return Globais.Receitas;
        }
        public List<StockItem> VerEstoque() {
            return Estoque.Instance.Itens;
        }
    }
}
