using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloPaciente
{
    public class PacientesModel
    {
        public int PacienteID { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public string ApellidoDeCasada {  get; set; }
        public DateTime FechaNacimientoPaciente { get; set; }
        public string SexoPaciente { get; set; }
        public string EstadoCivilPaciente { get; set; }
        public string DUIPaciente { get; set; }
        public string TelefonoPaciente { get; set; }
        public string CorreoPaciente { get; set; }
        public string DepartamentosPaciente { get; set; }
        public string MunicipioPaciente { get; set; }
        public string EstadoPaciente { get; set; }


        public PacientesModel() { }
    }
}
