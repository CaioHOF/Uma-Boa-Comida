using Microsoft.Maui.Controls;
using UmaBoaComida.Views.Atendente;
using UmaBoaComida.Views.Cozinheiro;
using UmaBoaComida.Views.Funcionario;

namespace UmaBoaComida.Views.Admin
{
    public partial class AdminMainPage : ContentPage
    {
        public AdminMainPage()
        {
            InitializeComponent();

            GerenciarFuncionariosButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new FuncionarioMainPage());
            };

            GerenciarPedidosButton.Clicked += async (s, e) =>
            {
                

                await Navigation.PushAsync(new AtendenteMainPage());
            };

            GerenciarEstoqueButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new FuncionarioMainPage());
            };

            AprovarReceitasButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new CozinheiroMainPage());
            };

            VerHistoricoButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new FuncionarioMainPage());
            };

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
