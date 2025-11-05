using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;

namespace UmaBoaComida.Views.Pedidos {
    public partial class GerarPedidoPage : ContentPage {
        private readonly Models.UsersModels.Atendente loggedAtendente;
        

        public GerarPedidoPage(Models.UsersModels.Atendente atendente) {
            InitializeComponent();
            loggedAtendente = atendente;
            CarregarDados();
        }

        private void CarregarDados() {
            List<Receita> receitasDisponiveis = Globais.Receitas ?? new();;
            //List<StockItem> alimentosDisponiveis = Models.Estoque.Instance.Itens ?? new();

            ReceitasList.ItemsSource = receitasDisponiveis.Where(r => r.Aprovada).Select(r => new SelecaoItem<Receita>(r)).ToList();
            //EstoqueList.ItemsSource = alimentosDisponiveis.Select(a => new SelecaoItem<StockItem>(a)).ToList(); Depois
        }

        private async void OnGerarPedidoClicked(object sender, EventArgs e) {
            var nomeCliente = ClienteEntry.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nomeCliente)) {
                await DisplayAlert("Aviso", "Informe o nome do cliente.", "OK");
                return;
            }

            var receitasSelecionadas = ((List<SelecaoItem<Receita>>)ReceitasList.ItemsSource)
                .Where(i => i.IsSelected)
                .Select(i => i.Item)
                .ToList();

            if (!receitasSelecionadas.Any()) {
                await DisplayAlert("Aviso", "Selecione pelo menos uma receita ou item de estoque.", "OK");
                return;
            }

            var pedido = loggedAtendente.CriarPedido(nomeCliente, receitasSelecionadas, new());
            await DisplayAlert("Sucesso", $"Pedido #{pedido.Id} criado com sucesso!", "OK");
            await Navigation.PopAsync();
        }

        private class SelecaoItem<T> {//feio mas n precisa ser fora dessa classe, so vo usar nela
            public T Item { get; }
            public string Nome { get; }
            public bool IsSelected { get; set; }

            public SelecaoItem(T item) {
                Item = item;
                Nome = item?.GetType().GetProperty("Nome")?.GetValue(item)?.ToString() ?? "Sem nome";
            }
        }
    }
}
