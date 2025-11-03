using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;

namespace UmaBoaComida.Views.Cozinheiro
{
    public partial class FinalizarPedidoPage : ContentPage
    {
        private ObservableCollection<Pedido> pedidos;

        public FinalizarPedidoPage(ObservableCollection<Pedido> pedidosPreparando)
        {
            InitializeComponent();

            pedidos = pedidosPreparando;
            PedidosCollectionView.ItemsSource = pedidos;
        }

        private void FinalizarButton_Clicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Pedido pedido)
            {
                pedido.Status = PedidoStatus.Finalizado;
                pedidos.Remove(pedido);

                DisplayAlert("Sucesso", $"Pedido de {pedido.NomeCliente} finalizado!", "OK");
            }
        }

        private async void VoltarButton_Clicked(object sender, EventArgs e)
        {
            if (Navigation.NavigationStack.Count > 1)
                await Navigation.PopAsync();
        }
    }
}
