using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HospiPlus.ModeloMedico
{
    public class ConsultaPorFechaModel
    {
        public int ConsultaID { get; set; }
        public int CitaID { get; set; }
        public int PacienteID { get; set; }
        public string NombreCompletoPaciente { get; set; } // Nombre completo del paciente
        public string NombreCompletoMedico { get; set; } // Nombre completo del médico
        public string EspecialidadMedico { get; set; } // Especialidad del médico
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public string Alergia { get; set; }
        public string Sintomas { get; set; }
        public string Diagnostico { get; set; } 
        public string Observaciones { get; set; }
        public DateTime FechaConsulta { get; set; }
        public DateTime FechaCita { get; set; } // Fecha de la cita
        public string EstadoCita { get; set; } // Estado de la cita

        /*
            Fecha Cita: 
                        Es el momento específico en el que un paciente tiene programada una cita con un médico o un especialista.
                        Esta fecha se establece con antelación y puede ser reprogramada o cancelada.
         */


        /*
            Fecha Consulta:
                        Se refiere a la fecha en que efectivamente se lleva a cabo la consulta médica.
                        Puede coincidir con la fecha de cita, pero también puede ser diferente si el paciente necesita 
                        reprogramar o si hay un cambio en la disponibilidad del médico.
         */


        public ConsultaPorFechaModel() { }
    }
}