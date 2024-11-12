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
            CargarPacientes();
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

                            // Obteniendo valores de MedicoID y PacienteID
                            int medicoID = (int)cmbMedicoID.SelectedValue;
                            int pacienteID = (int)cmbPacienteID.SelectedValue;

                            // Añadiendo los parámetros para guardar la receta
                            command.Parameters.AddWithValue("@PacienteID", pacienteID);
                            command.Parameters.AddWithValue("@MedicoID", medicoID);
                            command.Parameters.AddWithValue("@FechaEmision", DateTime.Parse(datePickerFechaEmision.Text));

                            command.Parameters.AddWithValue("@Medicamento", txtMedicamRecMedic.Text);
                            command.Parameters.AddWithValue("@Dosis", txtDosisRecMedic.Text);
                            command.Parameters.AddWithValue("@Frecuencia", txtFrecRecMedic.Text);
                            command.Parameters.AddWithValue("@Duracion", txtDuraRecMedic.Text);
                            command.Parameters.AddWithValue("@Instrucciones", txtInstrucRecMedic.Text);


                            command.ExecuteNonQuery();
                            limpiarcampos();
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


        //
        private bool ValidarCampos()
        {
            if (
                string.IsNullOrEmpty(txtMedicamRecMedic.Text) ||
                string.IsNullOrEmpty(txtDosisRecMedic.Text) ||
                string.IsNullOrEmpty(txtFrecRecMedic.Text) ||
                string.IsNullOrEmpty(txtDuraRecMedic.Text) ||
                cmbMedicoID.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }

        private void gridGestorRecetaMedico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridGestorRecetaMedico.SelectedItem != null)
            {
                ModelReceta recetaSeleccionada = (ModelReceta)gridGestorRecetaMedico.SelectedItem;

                // Asignando los valores de los campos de texto con los datos del registro seleccionado
                txtMedicamRecMedic.Text = recetaSeleccionada.Medicamento;
                txtDosisRecMedic.Text = recetaSeleccionada.Dosis;
                txtFrecRecMedic.Text = recetaSeleccionada.Frecuencia;
                txtDuraRecMedic.Text = recetaSeleccionada.Duracion;
                txtInstrucRecMedic.Text = recetaSeleccionada.Instrucciones;
            }
        }

        private void btnModificarRecetMedic_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    // Obtener el ID de la receta seleccionada en el DataGrid
                    if (gridGestorRecetaMedico.SelectedItem != null)
                    {
                        ModelReceta recetaSeleccionada = (ModelReceta)gridGestorRecetaMedico.SelectedItem;
                        int recetaID = recetaSeleccionada.RecetaID; // RecetaID de la receta seleccionada

                        using (var conexion = ConexionDB.ObtenerCnx())
                        {
                            ConexionDB.AbrirConexion(conexion);
                            using (var command = conexion.CreateCommand())
                            {
                                command.CommandType = System.Data.CommandType.StoredProcedure;
                                command.CommandText = "EditarReceta";

                                // Obteniendo valores de los campos de texto y ComboBox
                                int pacienteID = (int)cmbPacienteID.SelectedValue;
                                int medicoID = (int)cmbMedicoID.SelectedValue;
                                DateTime fechaEmision = DateTime.Parse(datePickerFechaEmision.Text);
                                int consultaID = recetaSeleccionada.ConsultaID;

                                // Añadiendo los parámetros
                                command.Parameters.AddWithValue("@RecetaID", recetaID);
                                command.Parameters.AddWithValue("@PacienteID", pacienteID);
                                command.Parameters.AddWithValue("@MedicoID", medicoID);
                                command.Parameters.AddWithValue("@FechaEmision", fechaEmision);
                                command.Parameters.AddWithValue("@ConsultaID", consultaID);
                                command.Parameters.AddWithValue("@Medicamento", txtMedicamRecMedic.Text);
                                command.Parameters.AddWithValue("@Dosis", txtDosisRecMedic.Text);
                                command.Parameters.AddWithValue("@Frecuencia", txtFrecRecMedic.Text);
                                command.Parameters.AddWithValue("@Duracion", txtDuraRecMedic.Text);
                                command.Parameters.AddWithValue("@Instrucciones", txtInstrucRecMedic.Text);

                                command.ExecuteNonQuery();

                                limpiarcampos();
                            }
                        }
                        MessageBox.Show("Receta modificada exitosamente.");
                        CargarRecetas(); // Recargar la lista de recetas después de la modificación
                    }
                    else
                    {
                        MessageBox.Show("Por favor, seleccione una receta para modificar.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar la receta: " + ex.Message);
                }
            }
        }


        private void CargarPacientes()
        {
            try
            {
                List<KeyValuePair<int, string>> pacientes = new List<KeyValuePair<int, string>>();
                using (var conexion = ConexionDB.ObtenerCnx())
                {
                    ConexionDB.AbrirConexion(conexion);
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "SELECT PacienteID, NombrePaciente FROM Pacientes";

                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                pacientes.Add(new KeyValuePair<int, string>(Convert.ToInt32(reader["PacienteID"]), reader["NombrePaciente"].ToString()));
                            }
                        }
                    }
                }

                // Configurar el ComboBox para mostrar el nombre y usar el ID como valor
                cmbPacienteID.ItemsSource = pacientes;
                cmbPacienteID.DisplayMemberPath = "Value";  // Mostrar el nombre del paciente
                cmbPacienteID.SelectedValuePath = "Key";   // Usar el ID del paciente como valor
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message);
            }
        }

        private void limpiarcampos()
        {
            cmbMedicoID.SelectedIndex = -1;
            cmbPacienteID.SelectedIndex = -1;
            txtMedicamRecMedic.Clear();
            txtDosisRecMedic.Clear();
            txtDuraRecMedic.Clear();
            txtFrecRecMedic.Clear();
            txtInstrucRecMedic.Clear();
        }

        private void btnCancelarRecetMedic_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult respuesta = MessageBox.Show("Seguro que desea cancelar la acción?.", "??", MessageBoxButton.OKCancel);

            if (respuesta == MessageBoxResult.OK)
            {
                limpiarcampos();
                CargarRecetas();

            }
        }


    }

}