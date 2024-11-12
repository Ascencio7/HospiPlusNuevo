﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospiPlus.ModeloCita
{
    public class ModelCita
    {
        public ModelCita() { }
        public int CitaID { get; set; }
        public string Paciente { get; set; }
        public string DUIPaciente { get; set; }
        public string Medico { get; set; }
        public string Especialidad { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraCita { get; set; }
        public string EstadoCita { get; set; }
    }
}