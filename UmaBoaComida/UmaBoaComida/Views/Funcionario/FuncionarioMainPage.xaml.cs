using Microsoft.Maui.Controls;

namespace UmaBoaComida.Views.Funcionario
{
    public partial class FuncionarioMainPage : ContentPage
    {
        public FuncionarioMainPage()
        {
            InitializeComponent();

            VoltarButton.Clicked += async (s, e) =>
            {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };

            IniciarExpedienteButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Expediente", "Expediente iniciado com sucesso!", "OK");
            };

            EncerrarExpedienteButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Expediente", "Expediente encerrado com sucesso!", "OK");
            };

            GerarRelatorioButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Relatório", "Relatório diário gerado com sucesso!", "OK");
            };

            HistoricoDiaButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Histórico do Dia", "Exibindo histórico do dia.", "OK");
            };

            HistoricoCompletoButton.Clicked += async (s, e) =>
            {
                await DisplayAlert("Histórico Completo", "Exibindo histórico completo.", "OK");
            };
        }
    }
}
