using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Collections.Generic;
using System.Linq;
using UmaBoaComida.Models;

namespace UmaBoaComida.Views.Estoque {
    public partial class EstoquePage : ContentPage {
        public EstoquePage(IEnumerable<StockItem> estoque, string titulo = "Estoque Atual") {
            InitializeComponent();
            Title = titulo;
            EstoqueStack.Children.Clear();

            if (estoque == null || !estoque.Any()) {
                EstoqueStack.Children.Add(new Label {
                    Text = "Nenhum item no estoque.",
                    FontAttributes = FontAttributes.Italic,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                });
                return;
            }

            foreach (var item in estoque.OrderBy(i => i.Nome)) {
                var border = new Border {
                    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
                    StrokeThickness = 1,
                    Padding = 10,
                    Content = new VerticalStackLayout {
                        Spacing = 4,
                        Children =
                        {
                            new Label
                            {
                                Text = item.Nome,
                                FontSize = 16,
                                FontAttributes = FontAttributes.Bold
                            },
                            new Label
                            {
                                Text = $"Quantidade: {item.Quantidade:F2} unidade(s)"
                            },
                            new Label
                            {
                                Text = $"Custo Aprox./Unidade: R$ {item.CustoAproximadoPorUnidade:F2}"
                            },
                            new Label
                            {
                                Text = $"Custo Total: R$ {(item.Quantidade * item.CustoAproximadoPorUnidade):F2}",
                                FontAttributes = FontAttributes.Bold
                            }
                        }
                    }
                };

                EstoqueStack.Children.Add(border);
            }
        }
    }
}
