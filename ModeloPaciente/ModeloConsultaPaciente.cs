using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloPaciente
{
    public class ModeloConsultaPaciente
    {
        public int ConsultaID { get; set; }
        public int CitaID { get; set; }
        public string PacienteID { get; set; }
        public string MedicoID { get; set; }
        public decimal Altura { get; set; }
        public string EspecialidadM {  get; set; }
        public decimal Peso { get; set; }
        public string Alergia { get; set; }
        public string Sintomas { get; set; }
        public string Diagnostico { get; set; }
        public string Observaciones { get; set; }
        public DateTime  FechaConsulta { get; set; }
        public DateTime FechaCita { get; set; }
        public string EstadoCita { get; set; }
    }
}
