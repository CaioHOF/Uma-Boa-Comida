using Microsoft.Maui.Controls;

namespace UmaBoaComida.Views.Cliente
{
    public partial class ClienteMainPage : ContentPage
    {
        public ClienteMainPage()
        {
            InitializeComponent();

            VerCardapioButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new ClienteMainPage()); 
            };

            CriarPedidoButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new ClienteMainPage()); 
            };

            HistoricoPedidosButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new ClienteMainPage()); 
            };

            AvaliarPedidoButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new ClienteMainPage()); 
            };

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
