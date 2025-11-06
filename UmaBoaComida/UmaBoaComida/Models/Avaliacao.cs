using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UmaBoaComida.Models {
    
    public class Avaliacao {

        public int NotaComida { get; set; }
        public int NotaCozinheiro { get; set; }
        public int NotaAtendimento { get; set; }
        public string Comentario { get; set; } = "";

        public Avaliacao(int comida, int coz, int atend, string com) {
            NotaComida = comida;
            NotaCozinheiro = coz;
            NotaAtendimento = atend;
            Comentario = com;
        }
    }
}
