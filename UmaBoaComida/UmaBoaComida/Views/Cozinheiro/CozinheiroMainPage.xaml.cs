using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using UmaBoaComida.Models;

namespace UmaBoaComida.Views.Cozinheiro
{
    public partial class CozinheiroMainPage : ContentPage
    {
        public CozinheiroMainPage()
        {
            InitializeComponent();

            AceitarPedidoButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new CozinheiroMainPage()); 
            };

            FinalizarPedidoButton.Clicked += async (s, e) =>
            {
                var cozinheiroLogado = new UmaBoaComida.Models.UsersModels.Cozinheiro(
    "Cozinheiro Teste",
    "cozinheiro@email.com",
    "123.456.789-00",
    "123"
);


                // Mock de pedidos
                cozinheiroLogado.PedidosPreparando.Add(new Pedido("João", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Aceito });
                cozinheiroLogado.PedidosPreparando.Add(new Pedido("Maria", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Preparando });
                cozinheiroLogado.PedidosPreparando.Add(new Pedido("Ana", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Aceito });

                await Navigation.PushAsync(new FinalizarPedidoPage(new ObservableCollection<Pedido>(cozinheiroLogado.PedidosPreparando)));
            };


            CriarNovaReceitaButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new CozinheiroMainPage()); 
            };

            GerenciarEstoqueButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new CozinheiroMainPage()); 
            };

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
