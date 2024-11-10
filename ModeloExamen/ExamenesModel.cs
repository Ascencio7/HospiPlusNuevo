using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloExamen
{
    public class ExamenesModel
    {
        public int ID { get; set; }
        //public int PacienteID { get; set; }
        public string Pacientes { get; set; }
        //public int? ConsultaID { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string TipoExamen { get; set; }
        public DateTime FechaExamen { get; set; }
        public string Resultado { get; set; }
        public string Observaciones { get; set; }

        public ExamenesModel() { }
    }

}
