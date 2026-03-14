<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Agenda.Pages.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Agenda de Contactos</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="card shadow-sm">
                <div class="card-header bg-primary text-white">
                    <h4 class="mb-0">📒 Agenda de Contactos</h4>
                </div>
                <div class="card-body">

                    <!-- Barra de búsqueda -->
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" 
                                Placeholder="Buscar por nombre o teléfono"></asp:TextBox>
                        </div>
                        <div class="col-md-4 d-flex gap-2">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-50"
                                OnClick="btnBuscar_Click" />
                            <asp:Button ID="btnNuevo" runat="server" Text="➕ Agregar Contacto" 
                                CssClass="btn btn-success w-50"
                                PostBackUrl="~/Pages/Agregar.aspx" />
                        </div>
                    </div>

                    <!-- GridView de contactos -->
                    <asp:GridView ID="gvContactos" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="Id_Contacto" OnRowDataBound="gvContactos_RowDataBound" 
                        CssClass="table table-striped table-hover table-bordered text-center" 
                        HeaderStyle-CssClass="table-dark">
                        <Columns>
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                            <asp:HyperLinkField DataNavigateUrlFields="Id_Contacto" 
                                DataNavigateUrlFormatString="Editar.aspx?id={0}" Text="✏️ Editar" 
                                HeaderText="Acciones" />
                            <asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="btnEliminar" runat="server" 
            CommandArgument='<%# Eval("Id_Contacto") %>' 
            CssClass="btn btn-sm btn-danger eliminar-contacto"
            Text="🗑 Eliminar"
            OnClick="btnEliminar_Click" 
            OnClientClick="return false;" />
    </ItemTemplate>
</asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS (opcional, solo si usas componentes dinámicos) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".eliminar-contacto").forEach(btn => {
                btn.addEventListener("click", function (e) {
                    e.preventDefault(); // Bloquea postback automático

                    let uniqueId = this.getAttribute("data-uniqueid");

                    Swal.fire({
                        title: "⚠️ ¿Estás seguro?",
                        text: "Este contacto será eliminado permanentemente.",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Sí, eliminar",
                        cancelButtonText: "Cancelar",
                        reverseButtons: true
                    }).then((result) => {
                        if (result.isConfirmed) {
                            __doPostBack(uniqueId, ""); // 🚀 ahora sí dispara el OnClick
                        }
                    });
                });
            });
        });
    </script>
</body>
</html>

