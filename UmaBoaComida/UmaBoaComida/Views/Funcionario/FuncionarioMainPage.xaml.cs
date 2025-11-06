// FuncionarioMainPage.cs
using Microsoft.Maui.Controls;
using UmaBoaComida.Models;
using UmaBoaComida.Models.UsersModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UmaBoaComida.Views.Funcionario {
    public partial class FuncionarioMainPage : ContentPage {

        private readonly Models.UsersModels.Funcionario loggedFun;

        public FuncionarioMainPage(Models.UsersModels.Funcionario loggedFun) {
            InitializeComponent();
            this.loggedFun = loggedFun ?? throw new ArgumentNullException(nameof(loggedFun));

            VoltarButton.Clicked += OnVoltarClicked;
            IniciarExpedienteButton.Clicked += OnIniciarExpedienteClicked;
            EncerrarExpedienteButton.Clicked += OnEncerrarExpedienteClicked;
            GerarRelatorioButton.Clicked += OnGerarRelatorioClicked;
            HistoricoDiaButton.Clicked += OnHistoricoDiaClicked;
            HistoricoCompletoButton.Clicked += OnHistoricoCompletoClicked;
        }

        private async void OnVoltarClicked(object sender, EventArgs e) {
            if (Navigation.NavigationStack.Count > 1)
                await Navigation.PopAsync();
        }

        private async void OnIniciarExpedienteClicked(object sender, EventArgs e) {
            try {
                loggedFun.IniciarDia($"Iniciado via app em {DateTime.Now:HH:mm}");
                await DisplayAlert("Expediente", $"{loggedFun.Nome}, expediente iniciado com sucesso!", "OK");
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Erro iniciar expediente: {ex}");
                await DisplayAlert("Erro", "Não foi possível iniciar o expediente.", "OK");
            }
        }

        private async void OnEncerrarExpedienteClicked(object sender, EventArgs e) {
            try {
                loggedFun.FinalizarDia();
                await DisplayAlert("Expediente", $"{loggedFun.Nome}, expediente encerrado com sucesso!", "OK");
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Erro encerrar expediente: {ex}");
                await DisplayAlert("Erro", "Não foi possível encerrar o expediente.", "OK");
            }
        }

        private async void OnGerarRelatorioClicked(object sender, EventArgs e) {
            try {
                var historicos = loggedFun.Historicos;
                if (historicos == null || historicos.Count == 0) {
                    await DisplayAlert("Relatório", "Sem histórico para gerar relatório.", "OK");
                    return;
                }

                int diasTrabalhados = historicos.Count;
                int totalAcoes = historicos.Sum(h => h.Acoes?.Count ?? 0);
                var ultimaAcoes = historicos
                    .OrderByDescending(h => h.EntradaDoDia)
                    .SelectMany(h => h.Acoes)
                    .Take(10);

                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"Funcionário: {loggedFun.Nome}");
                sb.AppendLine($"Dias trabalhados registrados: {diasTrabalhados}");
                sb.AppendLine($"Total de ações registradas: {totalAcoes}");
                sb.AppendLine();
                sb.AppendLine("Últimas ações:");
                foreach (var a in ultimaAcoes) sb.AppendLine($"- {a}");

                await DisplayAlert("Relatório", sb.ToString(), "OK");
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Erro gerar relatório: {ex}");
                await DisplayAlert("Erro", "Falha ao gerar relatório.", "OK");
            }
        }

        private async void OnHistoricoDiaClicked(object sender, EventArgs e) {
            try {
                var hoje = DateTime.Now.Date;
                var historicoHoje = loggedFun.Historicos?.FirstOrDefault(h => h.EntradaDoDia.Date == hoje);
                if (historicoHoje == null) {
                    await DisplayAlert("Histórico do Dia", "Nenhum histórico encontrado para hoje.", "OK");
                    return;
                }

                await Navigation.PushAsync(new Views.Historico.HistoricoPage(new List<Models.Historico> { historicoHoje }, $"{loggedFun.Nome} - Histórico do Dia"));
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Erro abrir histórico do dia: {ex}");
                await DisplayAlert("Erro", "Não foi possível abrir o histórico do dia.", "OK");
            }
        }

        private async void OnHistoricoCompletoClicked(object sender, EventArgs e) {
            try {
                var historicos = loggedFun.Historicos ?? new List<Models.Historico>();
                if (historicos.Count == 0) {
                    await DisplayAlert("Histórico Completo", "Nenhum histórico registrado.", "OK");
                    return;
                }

                await Navigation.PushAsync(new Views.Historico.HistoricoPage(historicos, $"{loggedFun.Nome} - Histórico Completo"));
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Erro abrir histórico completo: {ex}");
                await DisplayAlert("Erro", "Não foi possível abrir o histórico completo.", "OK");
            }
        }
    }
}