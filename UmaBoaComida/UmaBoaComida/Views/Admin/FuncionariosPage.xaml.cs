using Microsoft.Maui.Controls;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;
using UmaBoaComida.Views.Cadastro;
using System.Linq;
using System.Collections.Generic;

namespace UmaBoaComida.Views.Admin {
    public partial class FuncionariosPage : ContentPage {
        private readonly Models.UsersModels.Funcionario loggedFun;

        public FuncionariosPage(Models.UsersModels.Funcionario loggedFun, IEnumerable<Models.UsersModels.Funcionario> funcionarios) {
            InitializeComponent();
            this.loggedFun = loggedFun;

            if (loggedFun is Administrador) {
                AdicionarFuncionarioButton.IsVisible = true;
                AdicionarFuncionarioButton.Clicked += async (s, e) => {
                    await Navigation.PushAsync(new CadastrarFuncionarioPage(loggedFun as Administrador));
                };
            }

            MontarLista(funcionarios ?? Globais.Funcionarios);
        }

        private void MontarLista(IEnumerable<Models.UsersModels.Funcionario> funcionarios) {
            FuncionariosStack.Children.Clear();

            if (funcionarios == null || !funcionarios.Any()) {
                FuncionariosStack.Children.Add(new Label {
                    Text = "Nenhum funcionário cadastrado.",
                    FontAttributes = FontAttributes.Italic,
                    HorizontalOptions = LayoutOptions.Center
                });
                return;
            }

            foreach (var f in funcionarios) {
                var contentLayout = new VerticalStackLayout { Spacing = 6 };

                contentLayout.Children.Add(new Label { Text = $"Id: {f.Id}" });
                contentLayout.Children.Add(new Label { Text = $"{f.Nome} - {f.Tipo}", FontAttributes = FontAttributes.Bold });
                contentLayout.Children.Add(new Label { Text = $"Email: {f.Email}" });
                contentLayout.Children.Add(new Label { Text = $"CPF: {f.Cpf}" });
                contentLayout.Children.Add(new Label { Text = $"Salário: R$ {f.Salario:F2}" });

                if (loggedFun is Administrador adm) {
                    var demitirBtn = new Button {
                        Text = "Demitir",
                        BackgroundColor = Colors.Transparent,
                        TextColor = Colors.Red,
                        BorderColor = Colors.Red,
                        CornerRadius = 8,
                        HorizontalOptions = LayoutOptions.End
                    };

                    demitirBtn.Clicked += async (s, e) => {
                        bool confirmar = await DisplayAlert("Confirmar demissão",
                            $"Deseja demitir {f.Nome} (Id: {f.Id})?", "Sim", "Não");

                        if (!confirmar) return;

                        try {
                            adm.DemitirFuncionario(f);

                            MontarLista(adm.VerFuncionarios());
                            await DisplayAlert("Sucesso", $"Funcionário {f.Nome} demitido.", "OK");
                        } catch (System.Exception ex) {
                            await DisplayAlert("Erro", $"Falha ao demitir: {ex.Message}", "OK");
                        }
                    };

                    contentLayout.Children.Add(demitirBtn);
                }

                var border = new Border {
                    StrokeThickness = 1,
                    Padding = 10,
                    Content = contentLayout
                };

                FuncionariosStack.Children.Add(border);
            }
        }
    }
}
