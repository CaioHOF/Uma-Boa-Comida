using Microsoft.Maui.Controls;
using UmaBoaComida.Models;
using AtendenteModel = UmaBoaComida.Models.UsersModels.Atendente;

namespace UmaBoaComida.Views.Atendente
{
    public partial class AtendenteMainPage : ContentPage
    {
        public AtendenteMainPage()
        {
            InitializeComponent();

            var atendenteLogado = new AtendenteModel("Caio", "caio@email.com", "12345678900", "senha123");

            CriarPedidoButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new GerarPedidoPage(atendenteLogado));
            };

            AdicionarItensButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Info", "Funcionalidade ainda não implementada.", "OK");
            };

            GerenciarEstoqueButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Info", "Funcionalidade ainda não implementada.", "OK");
            };

            EntregarPedidosButton.Clicked += async (s, e) =>
            {
                var pedidos = new List<Pedido>
                {
                    new Pedido("João", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Aceito },
                    new Pedido("Maria", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Preparando },
                    new Pedido("Ana", new List<Receita>(), new List<StockItem>()) { Status = PedidoStatus.Entregue }
                };

                await Navigation.PushAsync(new EntregarPedidosPage(atendenteLogado, pedidos));
            };

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
