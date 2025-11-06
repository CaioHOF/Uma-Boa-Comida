// HistoricoPage.xaml.cs
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UmaBoaComida.Models;

namespace UmaBoaComida.Views.Historico {
    public partial class HistoricoPage : ContentPage {
        public HistoricoPage(IEnumerable<Models.Historico> historicos, string titulo) {
            InitializeComponent();
            Title = titulo ?? "Histórico";

            HistoricoStack.Children.Clear();

            foreach (var h in historicos.OrderByDescending(x => x.EntradaDoDia)) {
                var border = new Border {
                    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(8) },
                    StrokeThickness = 1,
                    Padding = 10,
                    Content = new VerticalStackLayout {
                        Spacing = 6,
                        Children =
                        {
                            new Label { Text = $"Entrada: {h.EntradaDoDia:G}", FontAttributes = FontAttributes.Bold },
                            new Label { Text = $"Saída: {(h.SaidaDodia == default ? "—" : h.SaidaDodia.ToString("G"))}" },
                            new Label { Text = "Ações:", FontAttributes = FontAttributes.Bold },
                            CreateAcoesStack(h)
                        }
                    }
                };

                HistoricoStack.Children.Add(border);
            }
        }

        private static View CreateAcoesStack(Models.Historico h) {
            if (h.Acoes == null || h.Acoes.Count == 0) {
                return new Label { Text = "Nenhuma ação registrada.", FontAttributes = FontAttributes.Italic };
            }

            var stack = new VerticalStackLayout { Spacing = 2 };
            foreach (var a in h.Acoes) {
                stack.Children.Add(new Label { Text = a, LineBreakMode = LineBreakMode.WordWrap, FontSize = 14 });
            }

            return stack;
        }
    }
}