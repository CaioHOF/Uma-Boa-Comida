using Microsoft.Maui.Controls;
using UmaBoaComida.Models.UsersModels;
using System;

namespace UmaBoaComida.Views.Cadastro {
    public partial class CadastrarFuncionarioPage : ContentPage {
        private readonly Administrador loggedAdm;

        public CadastrarFuncionarioPage(Administrador loggedAdm) {
            InitializeComponent();
            this.loggedAdm = loggedAdm;

            VoltarButton.Clicked += async (s, e) => {
                if (Navigation.NavigationStack.Count > 1)
                    await Navigation.PopAsync();
            };

            CadastrarButton.Clicked += async (s, e) => {
                try {
                    if (string.IsNullOrWhiteSpace(NomeEntry.Text) ||
                        string.IsNullOrWhiteSpace(CpfEntry.Text) ||
                        string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                        string.IsNullOrWhiteSpace(SenhaEntry.Text) ||
                        TipoPicker.SelectedIndex == -1 ||
                        string.IsNullOrWhiteSpace(SalarioEntry.Text)) {
                        await DisplayAlert("Erro", "Preencha todos os campos obrigatórios!", "OK");
                        return;
                    }

                    if (!double.TryParse(SalarioEntry.Text, out double salario)) {
                        await DisplayAlert("Erro", "Salário inválido!", "OK");
                        return;
                    }

                    var tipoSelecionado = TipoPicker.SelectedItem.ToString();
                    TipoFuncionario tipo = tipoSelecionado switch {
                        "Atendente" => TipoFuncionario.Atendente,
                        "Cozinheiro" => TipoFuncionario.Cozinheiro,
                        "Administrador" => TipoFuncionario.Administrador,
                        _ => TipoFuncionario.Generico
                    };

                    var novoFuncionario = new Models.UsersModels.Funcionario(
                        NomeEntry.Text,
                        CpfEntry.Text,
                        EmailEntry.Text,
                        SenhaEntry.Text,
                        salario,
                        tipo
                    );

                    loggedAdm.ContratarFuncionario(novoFuncionario);

                    await DisplayAlert("Sucesso", $"Funcionário {novoFuncionario.Nome} cadastrado!", "OK");
                    await Navigation.PopAsync();
                } catch (Exception ex) {
                    await DisplayAlert("Erro", $"Falha ao cadastrar: {ex.Message}", "OK");
                }
            };
        }
    }
}
