<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masterpages/Master_Page.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Product_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <link href="../App_Themes/Home.css" rel="stylesheet" />
    <nav>
        <h3>Category</h3>
        <asp:DataList ID="_CategoryList" runat="server" Width="193px" OnItemDataBound="_Category_ItemDataBound">
            <ItemTemplate>
                <asp:HyperLink ID="_Category" runat="server" CssClass="CAT" TextAlign="Right" Text='<%# Eval("Name") %>' Font-Underline="false"></asp:HyperLink>
            </ItemTemplate>
            
        </asp:DataList>
    </nav>

    <div class="items">
                <asp:DataList ID="_items" runat="server" Height="146px" Width="321px" RepeatColumns="3"  BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnItemDataBound="_items_ItemDataBound">
                    <AlternatingItemStyle BackColor="#DCDCDC" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <ItemTemplate>
                        <asp:Image ID="_image" runat="server" CssClass="_IMG" Height="250px" Width="200px" ImageUrl='<%#"~/App_Resources/Images/Small/"+Eval("ID")+".jpg" %>' ImageAlign="Bottom"  />
                        <asp:HyperLink ID="_Link" runat="server" Text='<%# Eval("Name") %>' Font-Underline="false"></asp:HyperLink>
                    </ItemTemplate>
          
                    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
          
                </asp:DataList>


       

    </div>
</asp:Content>

