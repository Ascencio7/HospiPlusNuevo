using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloReceta
{
    public class ModelReceta
    {
        public int RecetaID { get; set; }
        public string PacienteID { get; set; }
        public string MedicoID { get; set; }
        public DateTime FechaEmision { get; set; }
        public int ConsultaID { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
        public string Duracion { get; set; }
        public string Instrucciones { get; set; }
    }
}
