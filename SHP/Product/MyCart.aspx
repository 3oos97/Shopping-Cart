<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPages/Master_Page.master" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="Product_MyCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/MyCart.css" rel="stylesheet" />
   
  <%--  <script>
         var check = function () {

             var total = parseFloat(document.getElementById("_Quantity").innerText) * parseFloat(document.getElementById("_UnitPrice").innerText);
             document.getElementById("_Total").innerHTML = total;
        }
    </script>--%>
      <asp:DataList ID="_List" runat="server" Height="206px" Width="1301px" CssClass="List"  CellPadding="4" ForeColor="#333333" style="margin-right: 217px" OnItemDataBound="_List_ItemDataBound">
         <AlternatingItemStyle BackColor="White" />
         <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
         <ItemStyle BackColor="#E3EAEB" />
         <ItemTemplate>
             <asp:Image ID="_Image" runat="server" CssClass="img" ImageUrl='<%# Eval("ID") %>' />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
           <div class="details" runat="server">
             Product Name:
             <asp:Label ID="_ProductName" runat="server" Text='<%# Eval("Name") %>' />
             <br />
             
             Supplier:
             <asp:Label ID="_Supplier" runat="server" Text='<%# Eval("Supplier") %>' />
             <br />
             
              Unit Price:
             <asp:Label ID="_UnitPrice" runat="server" Text='<%# Eval("UnitPrice")+"$" %>' />
               <br />
              
              Quantity:
             <asp:DropDownList ID="_Quantity" runat="server" Height="19px" Width="46px">
                 <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem  Value="4"></asp:ListItem>
            </asp:DropDownList>  
             
             <br />
             
             Total Price:
             <asp:Label ID="_Total" runat="server" />
             
             <br />
             
              Shipping Address:
             <asp:TextBox ID="_ShippingAddress" runat="server" ></asp:TextBox>
               
               <br />

               Shipping Date:
               <asp:TextBox ID="_ShippingDate" runat="server" TextMode="Date"></asp:TextBox>
        
           </div>
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
         </ItemTemplate>
         
         <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
         
     </asp:DataList>
          <br />
        <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="_Save" runat="server" Text="Save" Font-Bold="true" Font-Size="XX-Large" Height="34px" OnClick="_Save_Click" Width="96px" />
    </asp:Content>

