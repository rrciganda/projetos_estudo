<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioCertificados.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.RelatorioCertificados" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-sm-12">
        <h1 class="text-bold">RELATÓRIO DE CERTIFICADOS</h1>
        <span>AQUI VOCÊ GERA RELATÓRIO DE CERTIFICADOS.</span>
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
    <div id="divInfo" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12 form-group">
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
            <div class="col-sm-4 form-group">
                <%--<label for="type-db">Profissional</label>
                <asp:DropDownList runat="server" ID="ddlColaborador" CssClass="combobox form-control" Width="100%" DataTextField="dscombo" DataValueField="idusuario"></asp:DropDownList>--%>
                <label for="type-db">Profissional</label>
                <telerik:RadComboBox RenderMode="Classic" CssClass="dsp-block combobox form-control" Skin="Metro" ID="ddlColaborador" AutoPostBack="false" runat="server" Width="100%" Font-Names="Estudotxtregular" Font-Size="14px" ForeColor="#6d6e71">
                </telerik:RadComboBox>
            </div>
            <div class="col-sm-4 form-group">
                <label for="type-db">Grupo</label>
                <asp:DropDownList ID="ddlgrupo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgrupo_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-sm-4 form-group">
                <label for="type-db">Fornecedor</label>
                <asp:DropDownList ID="ddlfornecedor" CssClass="combobox form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-sm-4 form-group">
                <label for="type-db">Certificado</label>
                <asp:DropDownList ID="ddlcertificado" CssClass="combobox form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-sm-4 form-group">
                <label for="type-db">Área / Departamento</label>
                <asp:DropDownList ID="ddldepartamento" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-sm-4 form-group">
                <label for="type-db">Regulador</label>
                <asp:DropDownList ID="ddlregulador" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <div class="col-sm-2 form-group">
                <label for="type-db">Certificado Expirado</label>
                <asp:DropDownList ID="ddlcertificadoexpirado" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Não" Value="0"></asp:ListItem>

                </asp:DropDownList>
            </div>


            <div class="col-sm-2 form-group">
                <label for="type-db">Status Certificação</label>
                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Concluída" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Em Andamento" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Reprovado" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-3 form-group">
                <label for="type-db">Revisão Preenchimento</label>
                <asp:DropDownList ID="ddlStatusAprovacao" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    <asp:ListItem Text="Aprovado" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Reprovado" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Pendente" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-2 form-group js-container-datepicker">
                <label for="type-db">Data Certificação Inicial</label>
                <asp:TextBox ID="txtdtinicertificacao" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
            </div>

            <div class="col-sm-2 form-group js-container-datepicker">
                <label for="type-db">Data Certificação Final</label>
                <asp:TextBox ID="txtdtfimcertificacao" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-2 form-group js-container-datepicker">
                <label for="type-db">Data Validade Inicial</label>
                <asp:TextBox ID="txtdtinivalidade" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-2 form-group js-container-datepicker">
                <label for="type-db">Data Validade Final</label>
                <asp:TextBox ID="txtdtfimvalidade" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server"></asp:TextBox>
            </div>

            <div class="col-sm-1 form-group">
                <asp:LinkButton ID="lnkPesquisar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkPesquisar_Click">
                            <i class="icon-search"></i>
                </asp:LinkButton>
            </div>
            <div class="col-sm-1 form-group">
                <label for="type-db">&nbsp;</label>
            </div>



        </div>
    </div>

    <%--<div class="row">--%>
    <div class="col-sm-12">
        <hr class="transparentHR">
        <label class="control-label" for="filterBase"></label>
        <div class="table table-responsive">
            <asp:GridView ID="grdpesquisa" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="false"
                OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idcertificacao, idcertificado, idregulador, idusuario, status, observacao,  validade, descricao, dtcertificacao">
                <Columns>
                    <asp:BoundField Visible="True" DataField="nomegrupo" HeaderText="Grupo"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomefornecedor" HeaderText="Fornecedor"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomecertificado" HeaderText="Certificado"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomeprova" HeaderText="Prova"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="dtprova" HeaderText="Data Prova" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                    <asp:TemplateField HeaderText="Status Prova">
                        <ItemTemplate>
                            <%# Entity.EOUtil.RetornoStatusProvaInt((object) Eval("statusprova")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField Visible="True" DataField="versao" HeaderText="Versão"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomeregulador" HeaderText="Regulador"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomecolaborador" HeaderText="Profissional"></asp:BoundField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <%# Entity.EOUtil.RetornarStatusPadraoCertificacao((Int32) Eval("status")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField Visible="True" DataField="dtcertificacao" HeaderText="Data Certificação" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="validade" HeaderText="Data Validade" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                    <asp:TemplateField HeaderText="Aprovação">
                        <ItemTemplate>
                            <%# Entity.EOUtil.RetornarStatusAprovacaoCertificacao((Int32) Eval("aprovacao")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:ButtonField CommandName="cmdAlterar" ControlStyle-CssClass="icon-pencil editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="cmdExcluir" ControlStyle-CssClass="icon-trash editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="cmdAbrir" ControlStyle-CssClass="icon-download editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>--%>
                </Columns>
                <PagerStyle CssClass="paginacao" />
                <PagerSettings Position="Bottom" Mode="Numeric" />
            </asp:GridView>
        </div>
    </div>
    <div class="col-sm-12 text-right">
        <asp:LinkButton ID="lnkExportar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkExportar_Click">
        <i class="icon-export"></i>Exportar
        </asp:LinkButton>
    </div>

    <script type="text/javascript">

        function comboautocomplete() {
            $(document).ready(function () {
                $('.combobox').combobox();
            });
        }
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
                    //maxDate: "0d",
                    beforeShow: function () {
                        setTimeout(function () {
                            $('.ui-datepicker').css('z-index', 99999999999999);
                        }, 0);
                    },
                    onClose: function (selectedDate) {
                        $("#to").datepicker("option", "minDate", "monthNames", selectedDate);
                    }
                });
                $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
                $('.datepicker').datepicker();
            });
        }
    </script>
</asp:Content>
