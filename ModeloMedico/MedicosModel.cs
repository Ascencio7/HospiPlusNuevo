using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloMedico
{
    public class MedicosModel
    {
        public int MedicoID { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public string ApellidoCasada { get; set; }
        public DateTime FechaNacimientoMedico { get; set; }
        public string TelefonoMedico { get; set; }
        public string DepartamentosMedicoID { get; set; }
        public string MunicipioMedico { get; set; }
        public string CorreoMedico { get; set; }
        public string DUIMedico { get; set; }
        public string SexoMedico { get; set; } 
        public string EstadoCivilMedico { get; set; }
        public string EspecialidadID { get; set; }
        public string EstadoMedico { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Dias { get; set; }
 

        public MedicosModel() { }


    }
}
