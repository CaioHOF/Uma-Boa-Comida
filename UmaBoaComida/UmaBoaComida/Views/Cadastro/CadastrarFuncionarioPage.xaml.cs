using Microsoft.Maui.Controls;

namespace UmaBoaComida.Views.Cadastro
{
    public partial class CadastrarFuncionarioPage : ContentPage
    {
        public CadastrarFuncionarioPage()
        {
            InitializeComponent();

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };

            CadastrarButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Cadastro", "Funcionário cadastrado com sucesso!", "OK");
            };
        }
    }
}
