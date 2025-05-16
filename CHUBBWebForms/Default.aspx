<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CHUBBWebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" AutoGenerateColumn="false">
    <asp:GridView ID="gvUsuarios" runat="Server">
        <Columns>
            <asp:BoundField Datafield="identificacion" HeaderText="Identificacion" />
            <asp:BoundField Datafield="nombreCompleto" HeaderText="Nombre_Completo" />
            <asp:BoundField Datafield="primerNombre" HeaderText="Primer_nombre" />
            <asp:BoundField Datafield="segundoNombre" HeaderText="Segundo_nombre" />
            <asp:BoundField Datafield="primerApellido" HeaderText="Primer_apellido" />
            <asp:BoundField Datafield="segundoApellido" HeaderText="Segundo_Apellido" />
            <asp:BoundField Datafield="fechaNacimiento" HeaderText="Fecha_Nacimiento" />
            <asp:BoundField Datafield="direccion" HeaderText="Direccion" />
            <asp:BoundField Datafield="numeroCelular" HeaderText="Numero_celular" />
            <asp:BoundField Datafield="correoElectronico" HeaderText="correo_Electronico" />
            <asp:BoundField Datafield="fechaCreacion" HeaderText="Fecha_creacion" />
            <asp:BoundField Datafield="estado" HeaderText="estado" /> 
        </Columns>

    </asp:GridView>
        
</asp:Content>
