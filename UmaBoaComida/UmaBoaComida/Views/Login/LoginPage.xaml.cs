using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace UmaBoaComida.Views.Login
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            LoginButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new Views.Admin.AdminMainPage());
            };

            CadastrarClienteButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new UmaBoaComida.Views.Cadastro.CadastrarFuncionarioPage());
            };
        }
    }
}
