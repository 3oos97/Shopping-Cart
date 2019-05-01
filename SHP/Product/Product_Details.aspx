<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPages/Master_Page.master" AutoEventWireup="true" CodeFile="Product_Details.aspx.cs" Inherits="Product_Product_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <link href="../App_Themes/Product_Details.css" rel="stylesheet" />

    <asp:Image ID="_Image" runat="server" CssClass="IMG" Height="600px" Width="500px" />

    <div class="Data" runat="server">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="_Name" runat="server" Font-Bold="true"></asp:Label> <br /> <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="_Price" runat="server" Font-Bold="true"></asp:Label> <br /> <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="_Supplier" runat="server" Font-Bold="true"></asp:Label> <br /> <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="_Add" runat="server" Text="Add to Cart" Font-Bold="true" BorderStyle="Solid" style="margin-left: 0px" OnClick="_Add_Click" /> <br />
    &nbsp;&nbsp;&nbsp;<br />
    
    </div>
</asp:Content>

