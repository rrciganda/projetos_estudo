<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogsPesquisar.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.LogsPesquisar" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>--%>
    <div class="col-sm-12">
        <h1 class="text-bold">LOGS</h1>
        <span class="text-uppercase">Aqui você consegue visualizar e emitir relatório de logs</span>
        <hr>
    </div>
    <div class="col-sm-12 form-group">
        <label class="control-label">Pesquisar Logs</label>
    </div>
    <div id="divErro" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12">
                <div class="EstudoMsg EstudoMsg-error EstudoMsg-small">
                    <div class="EstudoMsg-icon">Erro!</div>
                    <p class="EstudoMsg-title">
                        <asp:Label ID="lblMsgErro" runat="server" Text=""></asp:Label>
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
                    <asp:ListItem Text="Regulador" Value="Regulador"></asp:ListItem>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">

            <div class="col-sm-4 form-group">
                <label for="sel2">Funcionalidades</label>
                <asp:DropDownList ID="ddlFuncionalidades" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Certificação do Colaborador" Value="CertificacaoColaborador"></asp:ListItem>
                    <asp:ListItem Text="Grupo" Value="Grupo"></asp:ListItem>
                    <asp:ListItem Text="Regulador" Value="Regulador"></asp:ListItem>
                    <asp:ListItem Text="Certificado" Value="Certificado"></asp:ListItem>
                    <asp:ListItem Text="Colaborador" Value="Colaborador"></asp:ListItem>
                    <asp:ListItem Text="Perfil de Acesso" Value="PerfilAcesso"></asp:ListItem>
                    <asp:ListItem Text="Prova" Value="Prova"></asp:ListItem>

                </asp:DropDownList>
            </div>

            <div class="col-sm-4 form-group">
                <label for="type-db">Colaborador</label>
                <%--<asp:DropDownList runat="server" ID="ddlUsuario" CssClass="combobox form-control" Width="100%" DataTextField="dscombo" DataValueField="idusuario"></asp:DropDownList>--%>
                <telerik:RadComboBox RenderMode="Classic" CssClass="dsp-block combobox form-control" Skin="Metro" ID="ddlUsuario" AutoPostBack="false" runat="server" Width="100%" Font-Names="Estudotxtregular" Font-Size="14px" ForeColor="#6d6e71">
                </telerik:RadComboBox>
                <asp:HiddenField runat="server" ID="hdnStatusOrigilnal" />
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-4 form-group">
                        <label for="sel2">Ação</label>

                        <asp:DropDownList ID="ddlAcao" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Alterou"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Excluiu"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Incluiu"></asp:ListItem>
                        </asp:DropDownList>

                    </div>

                    <div class='col-sm-2 js-container-datepicker'>
                        <div class="form-group">
                            <label for="lblDataInicio">Data Inicial</label>
                            <asp:TextBox ID="txtDataInicio" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class='col-sm-2 js-container-datepicker'>
                        <div class="form-group">
                            <label for="lblDataFinal">Data Final</label>
                            <asp:TextBox ID="txtDataFinal" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-1 form-group">
                        <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="btn btn-primary btn-align-bottom icon-search" OnClick="btnPesquisar_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>

            <hr class="col-sm-12">

            <div class="col-sm-12">
                <label class="control-label" for="filterBase">Resultado da Pesquisa</label>
                <div class="table table-responsive">
                    <div class="col-sm-12">
                        <asp:GridView ID="grdpesquisa" CssClass="table table-striped table-responsive" OnPageIndexChanging="grdpesquisa_PageIndexChanging"
                            runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                            <Columns>
                                <asp:BoundField Visible="True" DataField="FUNCIONALIDADE" HeaderText="FUNCIONALIDADE"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="COLABORADOR" HeaderText="COLABORADOR"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="acao" HeaderText="AÇÃO"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="data" HeaderText="DATA"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="registroxml" HeaderText="REGISTRO XML"></asp:BoundField>
                            </Columns>
                            <PagerStyle CssClass="paginacao" />
                            <PagerSettings Position="Bottom" Mode="Numeric" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 text-right">
        <asp:LinkButton ID="lnkExportar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkExportar_Click">
                     <i class="icon-file-excel"></i>Exportar
        </asp:LinkButton>
    </div>

    <script type="text/javascript">
        function calendario() {
            $(document).ready(function () {
                if ($('.js-container-datepicker').length > 0) $.datepicker.regional['pt-BR'] = {
                    closeText: 'Fechar',
                    prevText: '&#x3c;Anterior',
                    nextText: 'Pr&oacute;ximo&#x3e;',
                    currentText: 'Hoje',
                    monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
                    'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
                    'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                    dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                    weekHeader: 'Sm',
                    dateFormat: 'dd/mm/yy',
                    firstDay: 0,
                    isRTL: false,
                    showMonthAfterYear: false,
                    yearSuffix: ''
                };
                $.datepicker.setDefaults($.datepicker.regional['pt-BR']);

                $('.js-is-datepicker').datepicker({

                    buttonText: "",
                    defaultDate: "+1w",
                    changeMonth: true,
                    numberOfMonths: 1,
                    onClose: function (selectedDate) {
                        $("#to").datepicker("option", "minDate", "monthNames", selectedDate);
                    }
                });
                $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
                $('.datepicker').datepicker();
            });
        }
    </script>
    <script type="text/javascript">
        function comboautocomplete() {
            $(document).ready(function () {
                $('.combobox').combobox();
            });
        }
    </script>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
