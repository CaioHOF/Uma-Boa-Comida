using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {

    namespace UmaBoaComida.Models {
        
        public class Usuario {
            public int Id { get; private set; }
            public string Nome { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Cpf { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            protected Usuario(string nome, string cpf, string email, string password) {
                Nome = nome;
                Email = email;
                Cpf = cpf;
                Password = password;
                Id = Globais.rOProximoUserId;
            }
        }
    }
}
