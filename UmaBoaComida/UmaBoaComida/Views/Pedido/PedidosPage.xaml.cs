using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;

namespace UmaBoaComida.Views.Pedidos {
    public partial class PedidosPage : ContentPage {
        private readonly Models.UsersModels.Funcionario loggedFun;
        private readonly List<Models.Pedido> pedidos;

        public PedidosPage(Models.UsersModels.Funcionario loggedFun, IEnumerable<Models.Pedido> pedidos) {
            InitializeComponent();
            this.loggedFun = loggedFun;
            this.pedidos = pedidos?.ToList() ?? new();
            Title = "Pedidos";
            BackgroundColor = Color.FromArgb("#EDF2F4");
            MontarPedidos();
        }

        private void MontarPedidos() {
            PedidosStack.Children.Clear();

            if (!pedidos.Any()) {
                PedidosStack.Children.Add(new Label {
                    Text = "Nenhum pedido encontrado.",
                    FontAttributes = FontAttributes.Italic,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#2B2D42")
                });
                return;
            }

            foreach (var p in pedidos.OrderByDescending(p => p.CreatedAt)) {
                var layout = new VerticalStackLayout { Spacing = 6 };

                layout.Children.Add(new Label { Text = $"Pedido #{p.Id}", FontAttributes = FontAttributes.Bold, FontSize = 16, TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(new Label { Text = $"Cliente: {p.NomeCliente ?? "Desconhecido"}", TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(new Label { Text = $"Data: {p.CreatedAt:G}", TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(new Label { Text = $"Status: {p.Status}", TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(new Label { Text = $"Valor Total: R$ {p.ValorTotal:F2}", TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(new Label { Text = "Itens:", FontAttributes = FontAttributes.Bold, TextColor = Color.FromArgb("#2B2D42") });
                layout.Children.Add(CriarItensStack(p));

                // ADMINISTRADOR
                if (loggedFun is Models.UsersModels.Administrador adm) {
                    var btnMudarStatus = new Button {
                        Text = "Mudar Status",
                        BackgroundColor = Color.FromArgb("#2B2D42"),
                        TextColor = Colors.White,
                        CornerRadius = 10,
                        Margin = new Thickness(0, 5, 0, 0)
                    };

                    btnMudarStatus.Clicked += async (s, e) => {
                        var opcoes = Enum.GetNames(typeof(Models.PedidoStatus)).ToList();
                        string novoStatus = await DisplayActionSheet("Selecionar novo status", "Cancelar", null, opcoes.ToArray());
                        if (Enum.TryParse<Models.PedidoStatus>(novoStatus, out var statusSelecionado)) {
                            try {
                                adm.MudarEstatusPedido(p, statusSelecionado);
                                await DisplayAlert("Sucesso", $"Pedido #{p.Id} alterado para '{statusSelecionado}'", "OK");
                                MontarPedidos();
                            } catch (Exception ex) {
                                await DisplayAlert("Erro", ex.Message, "OK");
                            }
                        }
                    };
                    layout.Children.Add(btnMudarStatus);
                }
                // ATENDENTE
                else if (loggedFun is Models.UsersModels.Atendente atendente) {
                    if (p.Status == Models.PedidoStatus.Analise) {
                        var aceitarBtn = new Button {
                            Text = "Aceitar Pedido",
                            BackgroundColor = Color.FromArgb("#2B2D42"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };
                        var recusarBtn = new Button {
                            Text = "Recusar Pedido",
                            BackgroundColor = Color.FromArgb("#EF233C"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };

                        aceitarBtn.Clicked += async (s, e) => {
                            atendente.AceitarPedido(p);
                            await DisplayAlert("Sucesso", $"Pedido #{p.Id} aceito.", "OK");
                            MontarPedidos();
                        };
                        recusarBtn.Clicked += async (s, e) => {
                            atendente.RecusarPedido(p);
                            await DisplayAlert("Pedido Recusado", $"Pedido #{p.Id} recusado.", "OK");
                            MontarPedidos();
                        };

                        layout.Children.Add(aceitarBtn);
                        layout.Children.Add(recusarBtn);
                    } else if (p.Status == Models.PedidoStatus.Finalizado) {
                        var entregarBtn = new Button {
                            Text = "Entregar Pedido",
                            BackgroundColor = Color.FromArgb("#8D99AE"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };
                        entregarBtn.Clicked += async (s, e) => {
                            atendente.EntregarPedido(p);
                            await DisplayAlert("Entregue", $"Pedido #{p.Id} entregue ao cliente.", "OK");
                            MontarPedidos();
                        };
                        layout.Children.Add(entregarBtn);
                    }
                }
                // COZINHEIRO
                else if (loggedFun is Models.UsersModels.Cozinheiro cozinheiro) {
                    if (p.Status == Models.PedidoStatus.Aceito) {
                        var prepararBtn = new Button {
                            Text = "Iniciar Preparo",
                            BackgroundColor = Color.FromArgb("#D90429"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };

                        prepararBtn.Clicked += async (s, e) => {
                            cozinheiro.PrepararPedido(p);
                            await DisplayAlert("Em preparo", $"Pedido #{p.Id} está sendo preparado.", "OK");
                            MontarPedidos();
                        };

                        layout.Children.Add(prepararBtn);
                    } else if (p.Status == Models.PedidoStatus.Preparando) {
                        var finalizarBtn = new Button {
                            Text = "Finalizar Pedido",
                            BackgroundColor = Color.FromArgb("#2B2D42"),
                            TextColor = Colors.White,
                            CornerRadius = 10
                        };

                        finalizarBtn.Clicked += async (s, e) => {
                            cozinheiro.FinalizarPedido(p);
                            await DisplayAlert("Finalizado", $"Pedido #{p.Id} foi finalizado com sucesso.", "OK");
                            MontarPedidos();
                        };

                        layout.Children.Add(finalizarBtn);
                    }
                }

                var border = new Border {
                    StrokeShape = new RoundRectangle { CornerRadius = 8 },
                    StrokeThickness = 1,
                    Padding = 10,
                    BackgroundColor = Colors.White,
                    Stroke = Color.FromArgb("#8D99AE"),
                    Margin = new Thickness(0, 0, 0, 10),
                    Content = layout
                };

                PedidosStack.Children.Add(border);
            }
        }

        private static View CriarItensStack(Models.Pedido pedido) {
            var stack = new VerticalStackLayout { Spacing = 4 };
            bool temReceitas = pedido.Receitas?.Count > 0;
            bool temAlimentos = pedido.Alimento?.Count > 0;

            if (!temReceitas && !temAlimentos)
                return new Label {
                    Text = "Nenhum item neste pedido.",
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 14,
                    TextColor = Color.FromArgb("#2B2D42")
                };

            if (temReceitas) {
                stack.Children.Add(new Label { Text = "Receitas:", FontAttributes = FontAttributes.Bold, FontSize = 15, TextColor = Color.FromArgb("#2B2D42") });
                foreach (var receita in pedido.Receitas)
                    stack.Children.Add(new Label { Text = $"- {receita.Nome} (R$ {receita.CalcularCusto():F2})", FontSize = 14, TextColor = Color.FromArgb("#2B2D42") });
            }

            if (temAlimentos) {
                stack.Children.Add(new Label { Text = "Itens de Estoque:", FontAttributes = FontAttributes.Bold, FontSize = 15, TextColor = Color.FromArgb("#2B2D42") });
                foreach (var item in pedido.Alimento)
                    stack.Children.Add(new Label { Text = $"- {item.Quantidade}x {item.Nome} (R$ {item.CustoAproximadoPorUnidade:F2} un.)", FontSize = 14, TextColor = Color.FromArgb("#2B2D42") });
            }

            return stack;
        }
    }
}
