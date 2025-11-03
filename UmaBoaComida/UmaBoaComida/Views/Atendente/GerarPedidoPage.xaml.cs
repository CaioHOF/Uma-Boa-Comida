using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;

namespace UmaBoaComida.Views.Atendente
{
    public partial class GerarPedidoPage : ContentPage
    {
        private ObservableCollection<Receita> receitasDisponiveis;
        private ObservableCollection<Receita> receitasSelecionadas;

        private Models.UsersModels.Atendente atendenteLogado;

        public GerarPedidoPage(Models.UsersModels.Atendente atendente)
        {
            InitializeComponent();

            atendenteLogado = atendente;
            receitasDisponiveis = new ObservableCollection<Receita>(GetReceitasMock());
            receitasSelecionadas = new ObservableCollection<Receita>();

            ReceitasPicker.ItemsSource = receitasDisponiveis;
            ReceitasSelecionadasView.ItemsSource = receitasSelecionadas;

            AdicionarReceitaButton.Clicked += AdicionarReceitaButton_Clicked;
            GerarPedidoButton.Clicked += GerarPedidoButton_Clicked;
            VoltarButton.Clicked += async (s, e) => await Navigation.PopAsync();
        }

        private void AdicionarReceitaButton_Clicked(object sender, EventArgs e)
        {
            var receitaSelecionada = ReceitasPicker.SelectedItem as Receita;
            if (receitaSelecionada != null && !receitasSelecionadas.Contains(receitaSelecionada))
                receitasSelecionadas.Add(receitaSelecionada);
        }

        private async void GerarPedidoButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NomeClienteEntry.Text))
            {
                await DisplayAlert("Erro", "Informe o nome do cliente.", "OK");
                return;
            }

            if (receitasSelecionadas.Count == 0)
            {
                await DisplayAlert("Erro", "Adicione pelo menos uma receita.", "OK");
                return;
            }

            var pedido = atendenteLogado.CriarPedido(NomeClienteEntry.Text, receitasSelecionadas.ToList(), new List<StockItem>());
            await DisplayAlert("Sucesso", $"Pedido #{pedido.Id} criado para {pedido.NomeCliente}.", "OK");

            receitasSelecionadas.Clear();
            NomeClienteEntry.Text = string.Empty;
        }

        private List<Receita> GetReceitasMock()
        {
            return new List<Receita>
    {
        new Receita("Feijoada", "Feijão preto com carnes variadas", "Prato Principal", 29.90),
        new Receita("Lasanha", "Lasanha à bolonhesa com queijo gratinado", "Massas", 32.50),
        new Receita("Moqueca", "Peixe cozido no leite de coco com pimentões", "Peixes", 35.00),
        new Receita("Strogonoff", "Carne ao molho cremoso com batata palha", "Carnes", 27.80)
    };
        }

    }
}
