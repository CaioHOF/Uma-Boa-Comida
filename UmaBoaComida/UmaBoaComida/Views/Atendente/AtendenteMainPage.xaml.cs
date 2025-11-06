using Microsoft.Maui.Controls;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Views.Pedidos;

namespace UmaBoaComida.Views.Atendente {
    public partial class AtendenteMainPage : ContentPage {
        private Models.UsersModels.Atendente loggedAte;

        public AtendenteMainPage(Models.UsersModels.Atendente loggedAte) {
            InitializeComponent();
            loggedAte = loggedAte;

            ConfigButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new Views.Funcionario.FuncionarioMainPage(loggedAte));
            };

            GerarPedidoButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new GerarPedidoPage(loggedAte));
            };

            VerPedidosButton.Clicked += async (s, e) => {
                var pedidos = loggedAte.VerPedidos();
                await Navigation.PushAsync(new PedidosPage(loggedAte, pedidos));
            };

            VoltarButton.Clicked += async (s, e) => {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
