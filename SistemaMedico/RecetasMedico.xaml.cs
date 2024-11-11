using HospiPlus.DataAcces;
using HospiPlus.ModeloReceta;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HospiPlus.ModeloMedico;

namespace HospiPlus.SistemaMedico
{
    public partial class RecetasMedico : Page
    {
        public RecetasMedico()
        {
            InitializeComponent();
            CargarMedicos();
            CargarRecetas();
        }

        private void CargarRecetas()
        {
            try
            {
                List<ModelReceta> recetas = new List<ModelReceta>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "MostrarRecetas";

                        using (DbDataReader leertabla = command.ExecuteReader())
                        {
                            while (leertabla.Read())
                            {
                                ModelReceta receta = new ModelReceta()
                                {
                                    RecetaID = int.Parse(leertabla["RecetaID"].ToString()),
                                    PacienteID = leertabla["NombreCompletoPaciente"].ToString(),
                                    MedicoID = leertabla["NombreCompletoMedico"].ToString(),
                                    FechaEmision = DateTime.Parse(leertabla["FechaEmision"].ToString()),
                                    ConsultaID = int.Parse(leertabla["ConsultaID"].ToString()),
                                    Medicamento = leertabla["Medicamento"].ToString(),
                                    Dosis = leertabla["Dosis"].ToString(),
                                    Frecuencia = leertabla["Frecuencia"].ToString(),
                                    Duracion = leertabla["Duracion"].ToString(),
                                    Instrucciones = leertabla["Instrucciones"].ToString()
                                };

                                recetas.Add(receta);
                            }
                        }
                    }
                }
                gridGestorRecetaMedico.ItemsSource = recetas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las recetas: " + ex.Message);
            }
        }


        
        private void CargarMedicos()
        {
            try
            {
                List<MedicosModel> medicos = new List<MedicosModel>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT MedicoID, NombreMedico FROM Medicos";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicosModel medico = new MedicosModel()
                                {
                                    MedicoID = int.Parse(reader["MedicoID"].ToString()),
                                    NombreMedico = reader["NombreMedico"].ToString()
                                };

                                medicos.Add(medico);
                            }
                        }
                    }
                }

                
                cmbMedicoID.ItemsSource = medicos;
                cmbMedicoID.DisplayMemberPath = "NombreMedico"; 
                cmbMedicoID.SelectedValuePath = "MedicoID"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar médicos: " + ex.Message);
            }
        }

        // Evento para guardar la receta
        private void btnGuardarRecetMedic_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    using (var conexion = ConexionDB.ObtenerCnx())
                    {
                        ConexionDB.AbrirConexion(conexion);
                        using (var command = conexion.CreateCommand())
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.CommandText = "InsertarReceta";

                            // Obtener el MedicoID desde el ComboBox
                            int medicoID = (int)cmbMedicoID.SelectedValue; // Asegúrate de que el valor seleccionado sea un MedicoID

                            command.Parameters.AddWithValue("@PacienteID", int.Parse(txtPacRecMedic.Text));
                            command.Parameters.AddWithValue("@MedicoID", medicoID);
                            command.Parameters.AddWithValue("@FechaEmision", DateTime.Parse(datePickerFechaEmision.Text));
                            command.Parameters.AddWithValue("@Medicamento", txtMedicamRecMedic.Text);
                            command.Parameters.AddWithValue("@Dosis", txtDosisRecMedic.Text);
                            command.Parameters.AddWithValue("@Frecuencia", txtFrecRecMedic.Text);
                            command.Parameters.AddWithValue("@Duracion", txtDuraRecMedic.Text);
                            command.Parameters.AddWithValue("@Instrucciones", txtInstrucRecMedic.Text);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Receta guardada exitosamente.");
                    CargarRecetas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar la receta: " + ex.Message);
                }
            }
        }

        // Método para validar campos (ejemplo de validación)
        private bool ValidarCampos()
        {
            if (string.IsNullOrEmpty(txtPacRecMedic.Text) ||
                string.IsNullOrEmpty(txtMedicamRecMedic.Text) ||
                string.IsNullOrEmpty(txtDosisRecMedic.Text) ||
                string.IsNullOrEmpty(txtFrecRecMedic.Text) ||
                string.IsNullOrEmpty(txtDuraRecMedic.Text) ||
                cmbMedicoID.SelectedItem == null) // Asegúrate de que un médico esté seleccionado
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }

        private void gridGestorRecetaMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
