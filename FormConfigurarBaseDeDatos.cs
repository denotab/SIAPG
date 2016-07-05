using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAPG
{
    public partial class FormConfigurarBaseDeDatos : Form
    {
        Properties.Settings LogginBd = new Properties.Settings();
        ClassBaseDeDatos ClassConexionBaseDeDatos = new ClassBaseDeDatos(); 
        public FormConfigurarBaseDeDatos()
        {
            InitializeComponent();
        }

        private void FormConfigurarBaseDeDatos_Load(object sender, EventArgs e)
        {
            AsignarConfiguracionPrevia(); 
        }

        private void AsignarConfiguracionPrevia()
        {
            textBoxServidor.Text = LogginBd.Login_Servidor;
            textBoxBaseDeDatos.Text = LogginBd.Login_Bd;
            textBoxUsuario.Text = LogginBd.Login_Usuario;
            textBoxContraseña.Text = LogginBd.Login_Contraseña;

        }
        private Boolean GuardarConfiguracion()
        {
            Boolean ConfiguracionAlmacenada = true;
            try
            {

                LogginBd.Login_Servidor = textBoxServidor.Text.ToString();
                LogginBd.Login_Bd = textBoxBaseDeDatos.Text.ToString();
                LogginBd.Login_Usuario = textBoxUsuario.Text.ToString();
                LogginBd.Login_Contraseña = textBoxContraseña.Text.ToString();
                LogginBd.Save();

            }
            catch (Exception ControlException)
            {
                MessageBox.Show(this, "No ha sido posible guardar la configuración de la base de datos; verifique el siguiente error: " +
                    "\n\r" + ControlException.Message.ToString(), "Cuidado", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                ConfiguracionAlmacenada = false;
            }
            return ConfiguracionAlmacenada; 
        }

        private bool ParametrosIngresados()
        {
            Boolean ParametrosCorrectos = true;
            string Alerta = "";
            if (textBoxServidor.Text.ToString().Trim() == "")
            {
                textBoxServidor.Focus();
                ParametrosCorrectos = false;
                Alerta = "Es necesario que especifíque un valor para el nombre del servidor o instancia";
            }
            else if (textBoxBaseDeDatos.Text.ToString().Trim() == "")
            {
                textBoxBaseDeDatos.Focus();
                ParametrosCorrectos = false;
                Alerta = "Es necesario que especifíque un valor para el nombre de la base de datos";
            }
            else if (textBoxUsuario.Text.ToString().Trim() == "")
            {
                textBoxUsuario.Focus();
                ParametrosCorrectos = false;
                Alerta = "Es necesario que especifíque un valor para el nombre de usuario";
            }

            else if (textBoxContraseña.Text.ToString().Trim() == "")
            {
                textBoxContraseña.Focus();
                ParametrosCorrectos = false;
                Alerta = "Es necesario que especifíque un valor para la clave de acceso";
            }
            if (!ParametrosCorrectos)
            {
                MessageBox.Show(this, Alerta, "Dato sin especificar", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            return ParametrosCorrectos;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            buttonGuardar.Enabled = false;
            if (ParametrosIngresados())
            {
                if (GuardarConfiguracion())
                {
                    if (ConexionBdEstablecida())
                    {
                        MessageBox.Show(this, "Configuración establecida correctamente.", "Proceso correcto", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                    {

                    }
                }
            }
            buttonGuardar.Enabled = true;
        }

        private bool ConexionBdEstablecida()
        {
            // Barra progresiva de proceso en ejecucion
            progressBarConexion.Enabled = true;
            progressBarConexion.Visible = true;
            this.Cursor = Cursors.WaitCursor;

            ClassConexionBaseDeDatos.ConectarBaseDeDatos();
            // Inhabilitar Barra progresiva de proceso en ejecucion

            progressBarConexion.Enabled = false;
            progressBarConexion.Visible = false;
            this.Cursor = Cursors.Default;

            return ClassConexionBaseDeDatos.ConexionEstablecida;

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
