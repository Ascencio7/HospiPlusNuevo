using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloMedico
{
    public class RecetasModel
    {
        public int RecetaID { get; set; }
        public string NombreCompletoPaciente { get; set; }
        public string NombreCompletoMedico { get; set; }
        public string DuiPaciente { get; set; }
        public DateTime FechaEmision {  get; set; }
        public int ConsultaID { get; set; }
        public string Medicamento { get; set; }
        public string Dosis {  get; set; }
        public string Frecuencia { get; set; }
        public string Duracion { get; set; }
        public string Instrucciones { get; set; }

        public RecetasModel() { }
    }
}
