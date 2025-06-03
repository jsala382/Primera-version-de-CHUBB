<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="CHUBBWebForms.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentSecondary" runat="server">
    <div style="text-align: center; margin-top= 50px;">
        <asp:FileUpload ID="tuArchivo" runat="server" />
         <asp:Button ID ="btnEnviarXls" runat="server" Text ="EnviarXls" OnClick="btnEnviarClick" />
        <asp:Label ID="lblResultado" runat="server" ForeColor="Green"></asp:Label>
    </div>
   
</asp:Content>
