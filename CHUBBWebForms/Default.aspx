<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CHUBBWebForms._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Crear Usuario</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <asp:Label ID="lblIdentificacion" runat="server"></asp:Label>
    Identificacion:
    <br />
    <asp:TextBox ID="txtIdentificacion" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacion"
        ErrorMessage="Campo nombre es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />
    <br />

    <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label>
    Nombre Completo:
    <br />
    <asp:TextBox ID="txtNombreCompleto" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
        ControlToValidate="txtNombreCompleto"
        ErrorMessage="Campo nombre completo es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblNombre" runat="server"></asp:Label>
    Primer Nombre:
    <br />
    <asp:TextBox ID="txtPrimerNombre" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
        ControlToValidate="txtPrimerNombre"
        ErrorMessage="Campo primer nombre es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblSegundoNombre" runat="server"></asp:Label>
    Segundo Nombre:
    <br />
    <asp:TextBox ID="txtSegundoNombre" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
        ControlToValidate="txtSegundoNombre"
        ErrorMessage="Campo segundo nombre es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblPrimerApellido" runat="server"></asp:Label>
    Primer Apellido:
     <br />
    <asp:TextBox ID="txtPrimerApellido" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
        ControlToValidate="txtPrimerApellido"
        ErrorMessage="Campo primer apellido es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblSegundoApellido" runat="server"></asp:Label>
    Segundo Apellido:
    <br />
    <asp:TextBox ID="txtSegundoApellido" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
        ControlToValidate="txtSegundoApellido"
        ErrorMessage="Campo segundo apellido es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblFechaNacimiento" runat="server"></asp:Label>
    Fecha Nacimiento:
     <br />
    <asp:TextBox ID="txtFechaNacimiento" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
        ControlToValidate="txtFechaNacimiento"
        ErrorMessage="Campo fecha nacimiento es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblDireccion" runat="server"></asp:Label>
    Direccion:
    <br />
    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
        ControlToValidate="txtDireccion"
        ErrorMessage="Campo direccion es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    
    <asp:Label ID="lblNumeroCelular" runat="server"></asp:Label>
    Numero Celular:
    <br />
    <asp:TextBox ID="txtNumeroCelular" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
        ControlToValidate="txtNumeroCelular"
        ErrorMessage="Campo numero celular es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblCorreoElectronico" runat="server"></asp:Label>
    Correo Electronico:
    <br />
    <asp:TextBox ID="txtCorreoElectronico" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
        ControlToValidate="txtCorreoElectronico"
        ErrorMessage="Campo correo electronico es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="lblfechaDeCreacion" runat="server"></asp:Label>
    Fecha de creacion:
    <br />
    <asp:TextBox ID="txtFechaDeCreacion" runat="server"></asp:TextBox><br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
        ControlToValidate="txtFechaDeCreacion"
        ErrorMessage="Campo fecha de creacion es obligatorio" ValidationGroup="CrearUsuario">
    </asp:RequiredFieldValidator>
    <br />

    
    <asp:Label ID="lblEstado" runat="server"></asp:Label>
    Estado:
    <br />
    <asp:checkBox ID="ckEstado" runat="server" text="Activo" />
    <br />
    <asp:CustomValidator
        runat="server"
        ControlToValidator="ckEstado"
        ErrorMessage="Error al aceptar al estado" 
        ValidationGroup="CrearUsuario">
    </asp:CustomValidator>
    <br />

    <asp:Button ID="btnGuardar" runat="server" Text="Insertar" OnClick="InsertUser"
        ValidationGroup="CrearUsuario" />
    <br />
    <br />
    <asp:Button ID="btnDescargarTxt" runat="server" Text="Descargar txt" OnClick="DownloadFile" />
    <br />
    <br />
    <asp:Button ID="btnDescargarXls" runat="server" Text="Descargar xls" OnClick="DownloadFileXls" />

   
</asp:Content>

<asp:Content ID="ContentSecondary" ContentPlaceHolderID="ContentSecondary" runat="server" >
    <h2>Lista de Usuario</h2>
    <asp:GridView ID="gvUsuarios" runat="Server" AutoGenerateColumns="false"
    
        OnRowEditing="EditingUser"
        OnRowCancelingEdit="CancellingEditionUser"
        OnRowUpdating="UpdatingUser">
        <Columns>
            <asp:TemplateField  HeaderText="Identificacion" SortExpression="Identificacion">
                <ItemTemplate>
                    <%#Eval("Identificacion") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIdentificacionGrid" runat="server" Text='<%# Bind("Identificacion") %>' Enabled="false"></asp:TextBox>

                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField  HeaderText="Nombre_Completo" SortExpression="NombreCompleto">
                <ItemTemplate>
                    <%#Eval("NombreCompleto") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombreCompleto" runat="server" Text='<%# Bind("NombreCompleto") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Primer_nombre" SortExpression="PrimerNombre">
                <ItemTemplate>
                    <%# Eval("PrimerNombre") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPrimerNombre" runat="server" Text='<%# Bind("PrimerNombre") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Segundo_nombre" SortExpression="segundoNombre">
                <ItemTemplate>
                    <%# Eval("segundoNombre") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSegundoNombre" runat="server" Text='<%# Bind("segundoNombre") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField  HeaderText="Primer_apellido" SortExpression="primerApellido">
                <ItemTemplate>
                    <%# Eval("primerApellido") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPrimerApellido" runat="server" Text='<%# Bind("primerApellido") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Segundo_apellido" SortExpression="segundoApellido">
                <ItemTemplate>
                    <%# Eval("segundoApellido") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSegundoApellido" runat="server" Text='<%# Bind("segundoApellido") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha_Nacimiento" SortExpression="fechaNacimiento">
                <ItemTemplate>
                    <%# Eval("fechaNacimiento", "{0:yyyy/MM/dd}") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFechaNacimiento" runat="server" Text='<%# Bind("fechaNacimiento") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Direccion" SortExpression="direccion">
                <ItemTemplate>
                    <%# Eval("direccion") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDireccion" runat="server" Text='<%# Bind("direccion") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Numero_Celular" SortExpression="numeroCelular">
                <ItemTemplate>
                    <%# Eval("numeroCelular") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNumeroCelular" runat="server" Text='<%# Bind("numeroCelular") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Correo_electronico" SortExpression="correoElectronico">
                <ItemTemplate>
                    <%# Eval("correoElectronico") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCorreoElectronico" runat="server" Text='<%# Bind("correoElectronico") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha_Creacion" SortExpression="fechaCreacion">
                <ItemTemplate>
                    <%# Eval("fechaDeCreacion", "{0:yyyy/MM/dd}") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFechaCreacion" runat="server" Text='<%# Bind("fechaDeCreacion") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Estado" SortExpression="estado">
                <ItemTemplate>
                    <%# Eval("estado").Equals(true)? "Activo": "Inactivo" %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEstado" runat="server" Text='<%# Bind("estado") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" Text="Editar" runat="server" CommandName="Edit" CausesValidation="false" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="lnkActualizar" Text="Actualizar" runat="server" CommandName="Update" CausesValidation="false"/>
                    <asp:LinkButton ID="lnkCancelar" Text="Cancelar" runat="server" CommandName="Cancel" CausesValidation="false" />
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
