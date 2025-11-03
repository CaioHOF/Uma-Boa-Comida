using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using UmaBoaComida.Models;
using AtendenteModel = UmaBoaComida.Models.UsersModels.Atendente;

namespace UmaBoaComida.Views.Atendente
{
    public partial class EntregarPedidosPage : ContentPage
    {
        private ObservableCollection<Pedido> pedidosEmProcesso;
        private AtendenteModel atendenteLogado;

        public EntregarPedidosPage(AtendenteModel atendente, List<Pedido> pedidos)
        {
            InitializeComponent();

            atendenteLogado = atendente;

            pedidosEmProcesso = new ObservableCollection<Pedido>(
                pedidos.FindAll(p => p.Status == PedidoStatus.Aceito || p.Status == PedidoStatus.Preparando)
            );

            PedidosListView.ItemsSource = pedidosEmProcesso;

            EntregarButton.Clicked += EntregarButton_Clicked;
            VoltarButton.Clicked += async (s, e) => await Navigation.PopAsync();
        }

        private async void EntregarButton_Clicked(object sender, EventArgs e)
        {
            if (PedidosListView.SelectedItem is Pedido pedidoSelecionado)
            {
                atendenteLogado.EntregarPedido(pedidoSelecionado);
                pedidosEmProcesso.Remove(pedidoSelecionado);

                await DisplayAlert("Sucesso", $"Pedido #{pedidoSelecionado.Id} entregue!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Selecione um pedido para entregar.", "OK");
            }
        }
    }
}
