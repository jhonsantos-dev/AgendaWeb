using Agenda.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Agenda.Models;

namespace Agenda.Pages
{
    public partial class Editar : System.Web.UI.Page
    {
        private ContactoDAL ContactoDAL = new ContactoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                { 
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    CargarContacto(id);
                }
            }

        }

        private void CargarContacto(int id)
        { 
            Contacto c = ContactoDAL.ObtenerPorId(id);
            if (c != null)
            { 
                txtNombre.Text = c.Nombre;
                txtApellido.Text = c.Apellido;
                txtTelefono.Text = c.Telefono;
                txtEmail.Text = c.Email;
                txtDireccion.Text = c.Direccion;
                ViewState["IdContacto"] = c.Id_Contacto; //Se guarda el ID para el postback
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ViewState["IdContacto"] != null)
            {
                Contacto c = new Contacto
                {
                    Id_Contacto = (int)ViewState["IdContacto"],
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text,
                    Direccion = txtDireccion.Text
                };

                ContactoDAL.Actualizar(c);


                //Muestra un mensaje y redirige a la lista
                // Script con SweetAlert
                string script = @"Swal.fire({
                            title: '✅ Contacto actualizado correctamente',
                            icon: 'success',
                            confirmButtonText: 'Aceptar'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = 'Default.aspx';
                            }
                        });";

                ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
            }
        }
    }
}