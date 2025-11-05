using Microsoft.Maui.Controls;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Views.Pedidos;
using UmaBoaComida.Views;
using UmaBoaComida.Views.Historico;
using UmaBoaComida.Views.Cadastro;
using UmaBoaComida.Views.Admin;
using UmaBoaComida.Views.Funcionario;
using UmaBoaComida.Views.Estoque;
using System.Linq;

namespace UmaBoaComida.Views.Admin {
    public partial class AdminMainPage : ContentPage {
        private readonly Administrador loggedAdm;

        public AdminMainPage(Administrador loggedAdm) {
            InitializeComponent();
            this.loggedAdm = loggedAdm;

            // Configurações (dados pessoais do adm)
            ConfigButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new FuncionarioMainPage(loggedAdm));
            };

            // VISUALIZAR
            VisualizarPedidosButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new PedidosPage(loggedAdm, loggedAdm.VerPedidos()));
            };

            VisualizarEstoqueButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new EstoquePage(loggedAdm.VerEstoque(), "Estoque"));
            };

            VisualizarReceitasButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new Views.Receitas.ReceitasPage(loggedAdm ,loggedAdm.VerReceitas()));
            };

            VisualizarHistoricosButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new HistoricoPage(loggedAdm.VerHistoricos(), "Histórico do Sistema"));
            };

            VisualizarFuncionariosButton.Clicked += async (s, e) => {
                await Navigation.PushAsync(new FuncionariosPage(loggedAdm, loggedAdm.VerFuncionarios()));
            };

            VoltarButton.Clicked += async (s, e) => {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };
        }
    }
}
