using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmaBoaComida.Models.UsersModels {
    [Serializable]
    public class Administrador : Funcionario {
        public Administrador(string nome, string email, string cpf, string senha)
            : base(nome, cpf, email, senha, TipoFuncionario.Administrador) {
        }

        public void AprovarReceita(Receita receita) {
            receita.Aprovada = true;
            RegistrarAcao($"Aprovou receita: {receita.Nome} (Id: {receita.Id})");
        }

        public void GerenciarFuncionario(string nome) {
            RegistrarAcao($"Gerenciou funcionário: {nome}");
        }

        public void ContratarFuncionario(Funcionario funcionario) {
            var funcionarios = Storage.Load<List<Funcionario>>(funcionario.GetPath()) ?? new List<Funcionario>();

            funcionarios.Add(funcionario);

            Storage.Save(funcionario.GetPath(), funcionarios);

            RegistrarAcao($"Contratou funcionário: {funcionario.Nome} ({funcionario.Tipo})");
        }

        public void DemitirFuncionario(Funcionario funcionario) {
            var funcionarios = Storage.Load<List<Funcionario>>(funcionario.GetPath()) ?? new List<Funcionario>();

            if (funcionarios.RemoveAll(f => f.Id == funcionario.Id) > 0) {
                Storage.Save(funcionario.GetPath(), funcionarios);
                RegistrarAcao($"Demitiu funcionário: {funcionario.Nome} ({funcionario.Tipo})");
            }
        }

        public Funcionario? ConsultarFuncionario(Guid id, TipoFuncionario tipo) {
            var path = tipo.GetPath();
            var funcionarios = Storage.Load<List<Funcionario>>(path) ?? new List<Funcionario>();
            return funcionarios.FirstOrDefault(f => f.Id == id);
        }
    }
}
