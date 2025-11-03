using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                TipoFuncionario.Cozinheiro => Path.Combine(baseDir, "Cozinheiros.bin"),
                TipoFuncionario.Atendente => Path.Combine(baseDir, "Atendentes.bin"),
                TipoFuncionario.Administrador => Path.Combine(baseDir, "Administradores.bin"),
                _ => Path.Combine(baseDir, "FuncionariosGenericos.bin")
            };
        }
    }

    [Serializable]
    public class Funcionario : UmaBoaComida.Models.Usuario {
        private TipoFuncionario atendente;

        public double Salario { get; set; }
        public TipoFuncionario Tipo { get; set; }
        public List<Historico> Historicos { get; set; } = new();

        public Funcionario(string nome, string cpf, string email, string password, double salario, TipoFuncionario tipo)
            : base(nome, email, cpf, password) {
            Tipo = tipo;
            Salario = salario;
        }

        public Funcionario(string nome, string email, string cpf, string password, TipoFuncionario atendente)
            : base(nome, email, cpf, password) {
            this.atendente = atendente;
        }

        public override string GetPath() => Tipo.GetPath();

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
            return historico;
        }

        public void IniciarDia(string descricaoMetaDia = "") {
            var historico = CriarNovoHistorico(descricaoMetaDia);
            historico.Registrar("Início do dia");
        }

        protected void RegistrarAcao(string descricao) {
            var historico = CriarNovoHistorico();
            historico.Registrar(descricao);
        }

        public void FinalizarDia() {
            if (Historicos.Count == 0)
                return;

            Historicos[^1].SaidaDodia = DateTime.Now;
            RegistrarAcao("Fim do dia");
        }
    }
}
