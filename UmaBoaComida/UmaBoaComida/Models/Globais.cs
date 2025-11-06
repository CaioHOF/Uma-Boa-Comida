using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using UmaBoaComida.Models.UsersModels;

namespace UmaBoaComida.Models {
    public static class Globais {

        public static int rOProximoUserId { get; private set; } = 0;
        public static int rOProximoPedidoId { get; private set; } = 0;
        public static int rOProximoHistoricoId { get; private set; } = 0;
        public static int rOProximoReceitaId { get; private set; } = 0;
        public static List<Pedido> Pedidos { get; private set; } = new List<Pedido>();
        public static List<Receita> Receitas { get; private set; } = new List<Receita>();
        public static List<Historico> Historicos { get; private set; } = new List<Historico>();
        public static List<Funcionario> Funcionarios { get; private set; } = new List<Funcionario>();

        // PEDIDOS
        public static void AdicionarPedido(Pedido pedido) {
            Pedidos.Add(pedido);
            rOProximoPedidoId++;
        }

        public static bool RemoverPedido(Pedido pedido) {
            return Pedidos.Remove(pedido);
        }

        public static List<Pedido>? AtualizarPedido(Predicate<Pedido> match, Action<Pedido> updateAction) {
            var p = Pedidos.FindAll(match);
            if (p != null) p.ForEach(updateAction);
            return p?.ToList();
        }

        // RECEITAS
        public static void AdicionarReceita(Receita receita) {
            Receitas.Add(receita);
            rOProximoReceitaId++;
        }

        public static int RemoverReceita(int receitaId) {
            //devolve o numero de receitas removidas
            return Receitas.RemoveAll(r => r.Id == receitaId);
        }

        public static List<Receita>? AtualizarReceita(Predicate<Receita> match, Action<Receita> updateAction) {
            var r = Receitas.FindAll(match);
            if (r != null) r.ForEach(updateAction);
            return r?.ToList();
        }

        // HISTÓRICOS
        public static void AdicionarHistorico(Historico historico) {
            Historicos.Add(historico);
            rOProximoHistoricoId++;
        }

        public static List<Historico>? AtualizarHistorico(Predicate<Historico> match, Action<Historico> updateAction) {
            var h = Historicos.FindAll(match);
            if (h != null) h.ForEach(updateAction);
            return h?.ToList();
        }

        // FUNCIONÁRIOS
        public static void AdicionarFuncionario(Funcionario funcionario) {
            Funcionarios.Add(funcionario);
            rOProximoUserId++;
        }

        public static int RemoverFuncionario(int funcionarioId) {
            //devolve o numero de funcionarios removidos
            return Funcionarios.RemoveAll(f => f.Id == funcionarioId);
        }

        public static List<Funcionario>? AtualizarFuncionario(Predicate<Funcionario> match, Action<Funcionario> updateAction) {
            var f = Funcionarios.FindAll(match);
            if (f != null) f.ForEach(updateAction);
            return f?.ToList();
        }
    }
}
