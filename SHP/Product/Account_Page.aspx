<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPages/Master_Page.master" AutoEventWireup="true" CodeFile="Account_Page.aspx.cs" Inherits="Product_Account_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/Account_Page.css" rel="stylesheet" />
    
    <div class="Data" runat="server">
         <asp:Label ID="Label1" runat="server" Text="Name: " Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp;<asp:Label ID="_Name" runat="server"></asp:Label> <br /> <br /> <br />
         <asp:Label ID="Label3" runat="server" Text="Email: " Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp;<asp:Label ID="_Email" runat="server" ></asp:Label> <br /> <br /> <br />
         <asp:Label ID="Label5" runat="server" Text="User-Name: " Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp;<asp:Label ID="_UserName" runat="server" ></asp:Label> <br /> <br /> <br />
         <asp:Label ID="Label7" runat="server" Text="Mobile: " Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp;<asp:Label ID="_Mobile" runat="server" ></asp:Label> <br /> <br /> <br />
         <asp:Label ID="Label9" runat="server" Text="Country: " Font-Bold="true" Font-Size="X-Large"></asp:Label>&nbsp;<asp:Label ID="_Country" runat="server" ></asp:Label> <br /> <br /> <br /> <br /> <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="_Edit" runat="server" Text="Edit Profile" Font-Bold="true" Font-Size="X-Large" OnClick="_Edit_Click"/> 
        
    </div>
</asp:Content>

