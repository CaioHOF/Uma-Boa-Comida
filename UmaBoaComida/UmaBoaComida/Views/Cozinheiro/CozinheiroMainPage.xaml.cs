using Microsoft.Maui.Controls;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Views.Pedidos;

namespace UmaBoaComida.Views.Cozinheiro {
    public partial class CozinheiroMainPage : ContentPage {
        private Models.UsersModels.Cozinheiro loggedCoz;

        public CozinheiroMainPage(Models.UsersModels.Cozinheiro loggedCoz) {
            InitializeComponent();
            this.loggedCoz = loggedCoz;

            ConfigButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new Views.Funcionario.FuncionarioMainPage(loggedCoz));
            };

            VerPedidosButton.Clicked += async (s, e) => {
                var pedidos = loggedCoz.VerPedidos();
                await Navigation.PushAsync(new PedidosPage(loggedCoz, pedidos));
            };

            VerReceitasButton.Clicked += async (s, e) => {
                var receitas = loggedCoz.VerReceitas();
                await Navigation.PushAsync(new Views.Receitas.ReceitasPage(loggedCoz, receitas));
            };

            GerarReceitaButton.Clicked += async (s, e) =>
                await Navigation.PushAsync(new GerarReceitaPage(loggedCoz));

            VoltarButton.Clicked += async (s, e) => {
                await Navigation.PopAsync();
            };
        }
    }
}
