using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;
using System.Collections.Generic;

namespace UmaBoaComida.Views.Receitas {
    public partial class ReceitasPage : ContentPage {

        private readonly List<Receita> receitas;
        private readonly Models.UsersModels.Funcionario loggedFun;

        public ReceitasPage(Models.UsersModels.Funcionario loggedFun, List<Receita> receitas) {
            InitializeComponent();
            this.loggedFun = loggedFun;
            this.receitas = receitas ?? new List<Receita>();
            BackgroundColor = Color.FromArgb("#EDF2F4");
            MontarReceitas();
        }

        private void MontarReceitas() {
            ReceitasStack.Children.Clear();

            if (receitas.Count == 0) {
                ReceitasStack.Children.Add(new Label {
                    Text = "Nenhuma receita disponível.",
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2B2D42"),
                    HorizontalOptions = LayoutOptions.Center
                });
                return;
            }

            foreach (var receita in receitas) {
                var layout = new VerticalStackLayout { Spacing = 4 };

                layout.Children.Add(new Label {
                    Text = receita.Nome,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 18,
                    TextColor = Color.FromArgb("#2B2D42")
                });

                layout.Children.Add(new Label {
                    Text = $"Tempo de preparo: {receita.tempoMedioPreparo} min",
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2B2D42")
                });

                layout.Children.Add(new Label {
                    Text = $"Custo: R$ {receita.CalcularCusto():F2}",
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2B2D42")
                });

                layout.Children.Add(CriarIngredientesStack(receita));

                layout.Children.Add(new Label {
                    Text = receita.Aprovada ? "Aprovada" : "Não aprovada",
                    TextColor = receita.Aprovada
                        ? Color.FromArgb("#4CAF50")
                        : Color.FromArgb("#EF233C"),
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 14
                });

                if (loggedFun is Administrador adm) {
                    var button = new Button {
                        Text = receita.Aprovada ? "Desaprovar Receita" : "Aprovar Receita",
                        BackgroundColor = receita.Aprovada
                            ? Color.FromArgb("#EF233C")
                            : Color.FromArgb("#2B2D42"),
                        TextColor = Colors.White,
                        CornerRadius = 10,
                        HeightRequest = 45,
                        Margin = new Thickness(0, 5, 0, 0)
                    };

                    button.Clicked += async (s, e) => {
                        try {
                            if (receita.Aprovada) {
                                adm.DesaprovarReceita(receita);
                                await DisplayAlert("Sucesso", $"Receita '{receita.Nome}' desaprovada.", "OK");
                            } else {
                                adm.AprovarReceita(receita);
                                await DisplayAlert("Sucesso", $"Receita '{receita.Nome}' aprovada.", "OK");
                            }

                            MontarReceitas();
                        } catch (System.Exception ex) {
                            await DisplayAlert("Erro", ex.Message, "OK");
                        }
                    };

                    layout.Children.Add(button);
                }

                var border = new Border {
                    Stroke = Color.FromArgb("#8D99AE"),
                    StrokeThickness = 1,
                    Padding = 10,
                    BackgroundColor = Colors.White,
                    StrokeShape = new RoundRectangle { CornerRadius = 12 },
                    Margin = new Thickness(0, 0, 0, 10),
                    Content = layout
                };

                ReceitasStack.Children.Add(border);
            }
        }

        private static View CriarIngredientesStack(Receita receita) {
            if (receita.Ingredientes == null || receita.Ingredientes.Count == 0) {
                return new Label {
                    Text = "Sem ingredientes.",
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2B2D42")
                };
            }

            var stack = new VerticalStackLayout { Spacing = 2 };
            stack.Children.Add(new Label {
                Text = "Ingredientes:",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                TextColor = Color.FromArgb("#2B2D42")
            });

            foreach (var ingrediente in receita.Ingredientes) {
                stack.Children.Add(new Label {
                    Text = $"- {ingrediente.Quantidade}x {ingrediente.Nome} (R$ {ingrediente.CustoAproximadoPorUnidade:F2} un.)",
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap,
                    TextColor = Color.FromArgb("#2B2D42")
                });
            }

            return stack;
        }
    }
}
