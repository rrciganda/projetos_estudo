<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SemAcesso.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.SemAcesso" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
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
    <form id="formLogin" runat="server">
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
                    <div class="login-form col-xs-10 col-sm-6 col-lg-6">
                        <div class="img-logo-login"></div>
                        
                        <div class="col-sm-5 form-group">
                            <div class="input-group">
                                <div class="input-group-addon"><i class="icon-lock"></i><font color="red">Sua sessão expirou ou seu usuário não possui acesso a essa funcionalidade.</font></div>
                                
                            </div>
                        </div>
                        <asp:LinkButton Text="Ir para o Login" CssClass="btn btn-default col-sm-12 col-xs-12 entrarlogin"  OnClientClick="window.location.href='login.aspx';" runat="server" />
                    </div>
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

                    <div class="logo-Estudo-diveo-medio"></div>
                    <div class="simbolo-Estudo-login float-left"></div>
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
