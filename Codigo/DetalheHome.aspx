<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalheHome.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.DetalheHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="SHORTCUT ICON" href="images/favicon.ico" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap-multiselect.css" />
    <link rel="stylesheet" type="text/css" href="Content/alertas.css" />
    <link rel="stylesheet" type="text/css" href="Content/estilo.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui.theme.css" />
    <link href="Scripts/bootstrap-combobox-master/css/bootstrap-combobox.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row content">
            <div class="col-sm-12">
                <div class="col-sm-12 form-group">
                    <div class="text-uppercase">
                        <asp:Label runat="server" ID="lbltitulo"></asp:Label>
                        <hr>
                    </div>

                    <div id="diverro" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="EstudoMsg EstudoMsg-error EstudoMsg-small">
                                    <div class="EstudoMsg-icon">Erro!</div>
                                    <p class="EstudoMsg-title">
                                        <asp:Label ID="lblmsgerro" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="divInfo" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="EstudoMsg EstudoMsg-info EstudoMsg-small">
                                    <div class="EstudoMsg-icon">Info!</div>
                                    <p class="EstudoMsg-title">
                                        <asp:Label ID="lblmsInfo" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-sm-12">
                            <asp:GridView ID="grdpesquisa" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="true"
                                AllowPaging="true" PageSize="10"
                                OnPageIndexChanging="grdpesquisa_PageIndexChanging" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                                <Columns>

                                </Columns>
                                <PagerStyle CssClass="paginacao" />
                                <PagerSettings Position="Bottom" Mode="Numeric" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <%--div abaixo que fecha o container da master page--%>
        </div>
    </form>
</body>
</html>
