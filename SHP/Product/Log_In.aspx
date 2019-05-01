<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Log_In.aspx.cs" Inherits="Product_Log_In" %>

<!DOCTYPE html>
<html>

<head>

  <meta charset="UTF-8">

  <title>Log-in</title>

  <link rel='stylesheet' href='http://codepen.io/assets/libs/fullpage/jquery-ui.css'>

    <link href="../App_Themes/Log_In.css" rel="stylesheet" media="screen" />

</head>

<body>

  <div class="login-card">
    <h1>Log-in</h1><br>
  <form runat="server">
    <asp:TextBox type="text" ID="_UserName" name="user" runat="server" placeholder="Username" required=""></asp:TextBox>
    <asp:TextBox type="password" ID="_Password" runat="server" name="pass" placeholder="Password" required=""></asp:TextBox>
    <asp:Button type="submit" Text="login" ID="_LogIn" runat="server" class="login login-submit" OnClick="_LogIn_Click"></asp:Button>
  </form>

  <%--<div class="login-help">
    <a href="#">Register</a> • <a href="#">Forgot Password</a>
  </div>--%>
</div>

<!-- <div id="error"><img src="https://dl.dropboxusercontent.com/u/23299152/Delete-icon.png" /> Your caps-lock is on.</div> -->

  <script src='http://codepen.io/assets/libs/fullpage/jquery_and_jqueryui.js'></script>

</body>

</html>
