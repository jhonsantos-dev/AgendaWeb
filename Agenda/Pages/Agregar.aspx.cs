using Agenda.DAL;
using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agenda.Pages
{
    public partial class Agregar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear objeto con los datos del formulario
                Contacto nuevoContacto = new Contacto
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(), 
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };

                // Guardar en la BD
                ContactoDAL dal = new ContactoDAL();
                dal.Agregar(nuevoContacto);

                // Mostrar alerta de éxito con SweetAlert
                string script = @"
                                    Swal.fire({
                                        title: '✅ Contacto agregado',
                                        text: 'El contacto se guardó correctamente',
                                        icon: 'success',
                                        confirmButtonText: 'Aceptar'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            window.location = 'Default.aspx';
                                        }
                                    });
                                ";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaAgregar", script, true);


            }
            catch (Exception ex)
            {
                // Alerta de error con SweetAlert
                string scriptError = $@"
            Swal.fire({{
                title: '❌ Error',
                text: '{ex.Message.Replace("'", " ")}',
                icon: 'error',
                confirmButtonText: 'Aceptar'
            }});
        ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaError", scriptError, true);
            }
        }
    }
}