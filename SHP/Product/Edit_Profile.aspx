<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterPages/Master_Page.master" AutoEventWireup="true" CodeFile="Edit_Profile.aspx.cs" Inherits="Product_Edit_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script>
        var check = function () {
            if (document.getElementById('_Password').value == document.getElementById('_ConfirmPassword').value) {
                document.getElementById('message').style.color = 'black';
                document.getElementById('message').innerHTML = 'matching';
            }
            else {
                document.getElementById('message').style.color = 'red';
                document.getElementById('message').innerHTML = 'not matching';
            }
  
        }
    </script>
    <div style="position:absolute; top:200px; left:50px;">
    <asp:Label runat="server" Text="Name:     " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:TextBox type="text" name="Name" id="_Name" runat="server" placeholder="Your Name" required=""></asp:TextBox> <br /> <br />
	<asp:Label runat="server" Text="Email:    " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:TextBox type="email" ID="_Email" runat="server" class="email agileits-btm" name="Email" placeholder="Email" required="" /> <br /><br />
    <asp:Label runat="server" Text="User Name:   " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:TextBox ID="_UserName" runat="server" type="text" name="User Name" placeholder="User Name" required=""></asp:TextBox> <br /><br />
	<asp:Label runat="server" Text="Password:   " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:TextBox ID="_Password" runat="server" type="password" name="Password" placeholder="Password" required="" onkeyup='check();'></asp:TextBox> <br /><br />
    <asp:Label runat="server" Text="Mobile:   " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:TextBox ID="_Mobile" runat="server" type="text" name="Number" placeholder="Mobile number" required=""></asp:TextBox> <br /><br />
    <asp:Label runat="server" Text="County:   " Font-Bold="true" Font-Size="X-Large"></asp:Label>
    <asp:DropDownList ID="_Country" runat="server" Height="16px" Width="129px" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name"></asp:DropDownList><br /><br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Name] FROM [SHP_LKP_Country]"></asp:SqlDataSource>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="_Save" runat="server" Text="Save" Font-Bold="true" Font-Size="X-Large" OnClick="_Save_Click"/>
    </div>
</asp:Content>

