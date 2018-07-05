<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>Gestão de Conhecimento</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/css" />

    <link rel="SHORTCUT ICON" href="images/favicon.ico">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="Content/bootstrap-multiselect.css">
    <link rel="stylesheet" type="text/css" href="Content/alertas.css">
    <link rel="stylesheet" type="text/css" href="Content/estilo.css">
    <link rel="stylesheet" type="text/css" href="Content/Site.css">
</head>
<body>
    <form id="formLogin" runat="server" defaultbutton="btnLogin">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />

                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="loginPanel" runat="server">
            <ContentTemplate>
                <div class="backgroundlogin">
                    <div class="login-form col-xs-10 col-sm-5 col-lg-3">
                        <div class="img-logo-login"></div>
                        <div class="col-sm-12 form-group">
                            <div class="input-group">
                                <div class="input-group-addon"><i class="icon-user"></i></div>
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" MaxLength="40" placeholder="Usuário"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-12 form-group">
                            <div class="input-group">
                                <div class="input-group-addon"><i class="icon-lock"></i></div>
                                <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" MaxLength="40" placeholder="Senha" TextMode="Password" />
                            </div>
                        </div>
                        <asp:Panel ID="panel1" CssClass="col-sm-12 form-group" runat="server" DefaultButton="btnLogin">
                            <asp:LinkButton Text="Entrar" CssClass="btn btn-default col-sm-12 col-xs-12 entrarlogin" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />
                        </asp:Panel>

                        <div id="divErro" runat="server" style="padding-bottom: 10px;" visible="false">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="EstudoMsg EstudoMsg-error EstudoMsg-small">
                                        <div class="EstudoMsg-icon">
                                            Erro!
                                        </div>
                                        <p class="EstudoMsg-title">
                                            <asp:Label ID="lblMsgRetorno" runat="server" Text=""></asp:Label>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="logo-Estudo-diveo-medio"></div>
                    <div class="simbolo-Estudo-login float-left"></div>
                </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript" src="Scripts/jquery-2.1.4.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="Scripts/highcharts-graficos.js"></script>
    <script type="text/javascript" src="Scripts/highcharts.js"></script>
    <script type="text/javascript" src="Scripts/highcharts-3d.js"></script>
    <script type="text/javascript" src="Scripts/exporting.js"></script>
    <script type="text/javascript" src="Scripts/main.js"></script>
</body>
</html>
