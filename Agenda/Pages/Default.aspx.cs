using Agenda.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Agenda.Pages
{
    public partial class Default : System.Web.UI.Page
    {

        private ContactoDAL contactoDAL = new ContactoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarContactos();
            
        }

        private void CargarContactos(string filtro = "")
        {
            gvContactos.DataSource = contactoDAL.Listar(filtro);
            gvContactos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarContactos(txtBuscar.Text);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            contactoDAL.Eliminar(id);
            CargarContactos();
            // Mostramos alerta SweetAlert2
            string script = @"
                    setTimeout(function() {
                        Swal.fire({
                            title: '✅ Contacto eliminado',
                            text: 'El contacto se eliminó correctamente.',
                            icon: 'success',
                            confirmButtonText: 'Aceptar'
                        });
                    }, 300);
                ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaEliminar", script, true);
        }

        protected void gvContactos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnEliminar");
                if (btn != null)
                {
                    btn.Attributes["data-uniqueid"] = btn.UniqueID;
                }
            }
        }
    }
}