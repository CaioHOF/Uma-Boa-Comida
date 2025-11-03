using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace UmaBoaComida.Models {

    namespace UmaBoaComida.Models {
        [Serializable]
        public class Usuario {
            public Guid Id { get; set; } = Guid.NewGuid();
            public string Nome { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Cpf { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            protected Usuario(string nome, string email, string cpf, string password) {
                Nome = nome;
                Email = email;
                Cpf = cpf;
                Password = password;
            }

            public virtual string GetPath() {
                string path = Path.Combine(FileSystem.AppDataDirectory, "usuarios", "clientes.bin");
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
                return path;
            }
        }
    }
}
