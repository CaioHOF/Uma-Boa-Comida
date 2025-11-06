using System;
using System.Collections.Generic;
using System.IO;

namespace UmaBoaComida.Models.UsersModels {
    public enum TipoFuncionario {
        Cozinheiro,
        Atendente,
        Administrador,
        Generico
    }

    public static class PathFuncionarioExtensions {
        public static string GetPath(this TipoFuncionario tipo) {
            string baseDir = Path.Combine(FileSystem.AppDataDirectory, "usuarios", "funcionarios");
            Directory.CreateDirectory(baseDir);

            return tipo switch {
                TipoFuncionario.Cozinheiro => Path.Combine(baseDir, "Cozinheiros.json"),
                TipoFuncionario.Atendente => Path.Combine(baseDir, "Atendentes.json"),
                TipoFuncionario.Administrador => Path.Combine(baseDir, "Administradores.json"),
                _ => Path.Combine(baseDir, "FuncionariosGenericos.json")
            };
        }
    }

    public class Funcionario : UmaBoaComida.Models.Usuario {
        private TipoFuncionario atendente;

        public double Salario { get; set; }
        public TipoFuncionario Tipo { get; set; }
        public List<Historico> Historicos { get; set; } = new();


        public Funcionario(string nome, string cpf, string email, string password, double salario, TipoFuncionario tipo)
            : base(nome, cpf, email, password) {

            Tipo = tipo;
            Salario = salario;
        }

        protected Historico CriarNovoHistorico(string descricaoMetaDia = "") {
            var hoje = DateTime.Now.Date;

            if (Historicos.Count > 0 && Historicos[^1].EntradaDoDia.Date == hoje) {
                return Historicos[^1];
            }

            var historico = new Historico {
                EntradaDoDia = DateTime.Now,
                descricaoMetaDia = descricaoMetaDia
            };

            Historicos.Add(historico);
            Globais.AdicionarHistorico(historico);

            return historico;
        }

        public void IniciarDia(string descricaoMetaDia = "") {
            var historico = CriarNovoHistorico(descricaoMetaDia);
            historico.Registrar("Início do dia");
        }

        protected void RegistrarAcao(string descricao) {
            var historico = CriarNovoHistorico();
            historico.Registrar(descricao);

            Globais.AtualizarHistorico(
                h => h.EntradaDoDia.Date == historico.EntradaDoDia.Date,
                h => h.Acoes = new List<string>(historico.Acoes)
            );
        }

        public void FinalizarDia() {
            if (Historicos.Count == 0)
                return;

            var historico = Historicos[^1];
            historico.SaidaDodia = DateTime.Now;
            historico.Registrar("Fim do dia");

            if (!Globais.Historicos.Contains(historico))
                Globais.AdicionarHistorico(historico);
            else
                Globais.AtualizarHistorico(
                    h => h.EntradaDoDia == historico.EntradaDoDia,
                    h => h.Acoes = new List<string>(historico.Acoes)
                );
        }
    }
}
