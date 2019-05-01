<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sign_Up.aspx.cs" Inherits="Product_Sign_Up" %>

<!DOCTYPE html>
<html>
<head>
<title>Client Signup</title>
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="Client Signup Form template Responsive, Login form web template,Flat Pricing tables,Flat Drop downs  Sign up Web Templates, Flat Web Templates, Login sign up Responsive web template, SmartPhone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- Custom Theme files -->
<link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
<!-- //Custom Theme files -->
<!-- web font -->
<link href="//fonts.googleapis.com/css?family=Old+Standard+TT:400,400i,700" rel="stylesheet">
<link href='//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700,800' rel='stylesheet' type='text/css'><!--web font-->
<link href="../App_Themes/Sign_Up.css" rel="stylesheet" />
<!-- //web font -->
</head>
<body>
	<!-- main -->
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
	    
    <div class="main main-agileits">
		<h1>Client Signup Form</h1>
		<div class="main-agilerow"> 
			<div class="signup-wthreetop">
				<h2>Welcome to Registration</h2>
			</div>	
			<div class="contact-wthree">

				<form id="form1" runat="server" onsubmit="return checkForm(this);">
					<h3>Step 1 :</h3>
					<div class="form-w3step1">
						<asp:TextBox type="text" name="Name" id="_Name" runat="server" placeholder="Your Name" required=""></asp:TextBox> 
						<asp:TextBox type="email" ID="_Email" runat="server" class="email agileits-btm" name="Email" placeholder="Email" required="" /> 
					</div> 
					<h3>Step 2 :</h3>
					<div class="form-w3step1">  
						<asp:TextBox ID="_UserName" runat="server" type="text" name="User Name" placeholder="User Name" required=""></asp:TextBox>
						<asp:TextBox ID="_Password" runat="server" type="password" name="Password" placeholder="Password" required="" onkeyup='check();'></asp:TextBox>
						<asp:TextBox ID="_ConfirmPassword" runat="server" type="password" class="agileits-btm" name="confirm password" placeholder="Confirm Password" required="" onkeyup='check();'></asp:TextBox>
                        <span id="message" runat="server"></span>
					</div>
					<h3>Step 3 :</h3>
					<div class="form-w3step1 w3ls-formrow">
						<asp:TextBox ID="_Mobile" runat="server" type="text" name="Number" placeholder="Mobile number" required=""></asp:TextBox>
                        <asp:DropDownList ID="_Country" runat="server" DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="Name" Height="35px" style="margin-left: 0px" Width="319px" BackColor="#666666" ForeColor="White" CssClass="temp"></asp:DropDownList>
						<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Name] FROM [SHP_Country_VIW]"></asp:SqlDataSource>
						<%--<input type="text" class="agileits-btm" name="Your Address" placeholder="Your Address" required="">--%>
					</div>
					<%--<div class="agileits-row2 w3ls-formrow2">
						<input type="checkbox" id="brand2" value="">
						<label for="brand2"><span></span>I accept the terms of Use</label> 
					</div> --%> 
					<asp:Button ID="_SignUp" runat="server" Text="Sign UP" OnClick="_SignUp_Click" />
				</form>
			</div>  
		</div>	
	</div>	
	<!-- //main -->
	<!-- copyright -->
	<%--<div class="w3copyright-agile">
		<p>© 2017 Client Signup Form. All rights reserved | Design by <a href="http://w3layouts.com/" target="_blank">W3layouts</a></p>
	</div>--%>
	<!-- //copyright --> 
</body>
</html>
