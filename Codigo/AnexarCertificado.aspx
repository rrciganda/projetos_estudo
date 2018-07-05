<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnexarCertificado.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.AnexarCertificado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>Anexar Certificado</title>

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
<body  style="overflow: hidden; background-color:white">
    <form id="formLogin" runat="server" style="overflow: hidden;">
        <div style="margin-right:10px">
            <div class="col-sm-12">
                <h1 class="text-bold">Anexar Certificado</h1>
                <hr>
            </div>
            <div id="diverro" runat="server" visible="false">
                <div class="row">
                    <div class="col-sm-12 form-group">
                        <div class="EstudoMsg EstudoMsg-error EstudoMsg-small">
                            <div class="EstudoMsg-icon">Erro!</div>
                            <p class="EstudoMsg-title">
                                <asp:Label ID="lblmsgerro" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-6 form-group">
                        <label for="type-db">Anexo</label>
                        <asp:FileUpload ID="UploadCertificacao" runat="server" />
                    </div>
                </div>
            </div>
<div class="row">
                <div class="col-sm-12 text-right pull-right">

                    <asp:LinkButton ID="lnkvoltar" CssClass="btn btn-default" runat="server" OnClick="lnkvoltar_Click">
                                    <i class="icon-ccw"></i>
                                    Voltar
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnksalvar" CssClass="btn btn-primary" runat="server" OnClick="lnksalvar_Click">
                                    <i class="icon-floppy"></i>
                                    Salvar
                    </asp:LinkButton>

                </div>
            </div>
        </div>
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
