using Microsoft.Maui.Controls;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Models;

namespace UmaBoaComida.Views.Cozinheiro {
    public partial class GerarReceitaPage : ContentPage {
        private Models.UsersModels.Cozinheiro loggedCoz;

        public GerarReceitaPage(Models.UsersModels.Cozinheiro loggedCoz) {
            InitializeComponent();
            this.loggedCoz = loggedCoz;

            SalvarButton.Clicked += OnSalvarClicked;
            VoltarButton.Clicked += async (s, e) => await Navigation.PopAsync();
        }

        private async void OnSalvarClicked(object sender, EventArgs e) {
            string nome = NomeEntry.Text?.Trim();
            string descricao = DescricaoEditor.Text?.Trim();
            string precoStr = PrecoEntry.Text?.Trim();
            string tempoStr = TempoPreparoEntry.Text?.Trim();

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(descricao) ||
                string.IsNullOrEmpty(precoStr) || string.IsNullOrEmpty(tempoStr)) {
                await DisplayAlert("Erro", "Preencha todos os campos!", "OK");
                return;
            }

            if (!double.TryParse(precoStr, out double preco) || preco <= 0) {
                await DisplayAlert("Erro", "Preço inválido!", "OK");
                return;
            }

            if (!int.TryParse(tempoStr, out int tempoPreparo) || tempoPreparo <= 0) {
                await DisplayAlert("Erro", "Tempo de preparo inválido!", "OK");
                return;
            }

            loggedCoz.RequisitarNovaReceita(new Receita(nome, descricao, "", preco, tempoPreparo));

            await DisplayAlert("Sucesso", "Receita gerada com sucesso!", "OK");
            await Navigation.PopAsync();
        }
    }
}
