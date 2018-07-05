<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatoriosGraficos.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.RelatoriosGráficos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12 form-group">
        <h1 class="text-bold text-uppercase">Acompanhamento Gráfico</h1>
        <span class="text-uppercase">Visualize as informacões mais relevantes em forma de gráfico e tenha um melhor acompanhamento dos processos</span>

        <div class="col-sm-12 form-group">
            <hr>

            <label class="control-label text-uppercase form-group">Período Certificação</label>

            <div class="row">
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

                <div class="col-sm-3 form-group">
                    <label for="ddlGrupo">Grupo</label>
                    <asp:DropDownList ID="ddlGrupo" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>

                <div class="col-sm-1 form-group">
                    <asp:LinkButton ID="lnkPesquisar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkPesquisar_Click">
                                    <i class="icon-search"></i>
                    </asp:LinkButton>
                </div>
            </div>
            <%--
            <div class="panel panel-default" style="visibility:hidden;">
                <div class="panel-body">
                    <div id="certificacaomensal">
                        <div id="pd-graficomensal"></div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="grdpesquisaCertificacao" runat="server" AllowPaging="false" GridLines="None" AllowSorting="false"
                            AutoGenerateColumns="False" CssClass="table table-striped bottom-zero" Width="100%" Visible="true" DataKeyNames="colaborador, certificacao, dtcertificacao" OnPageIndexChanging="grdpesquisa_PageIndexChanging">
                            <Columns>
                                <asp:BoundField Visible="True" DataField="colaborador" HeaderText="COLABORADOR"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="certificacao" HeaderText="CERTIFICAÇÃO"></asp:BoundField>
                                <asp:BoundField Visible="True" DataField="dtcertificacao" HeaderText="DATA CERTIFICAÇÃO"></asp:BoundField>
                            </Columns>
                            <PagerStyle CssClass="paginacao" />
                            <PagerSettings Position="Bottom" Mode="Numeric" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            --%>
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
            <br />
        </div>

        <div class="row">
            <div class="col-sm-6">
                <label class="control-label text-uppercase">Certificação por Grupo</label>
                <div id="divsucesso" runat="server" visible="false">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="EstudoMsg EstudoMsg-success EstudoMsg-small">
                                <div class="EstudoMsg-icon">Sucesso</div>
                                <p class="EstudoMsg-title">
                                    <asp:Label ID="lblsucesso" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="certificadosgrupos">
                            <div id="pd-graficogrupo"></div>
                        </div>

                        <div class="table-responsive">
                            <div style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="grdpesquisaGrupo" runat="server" AllowPaging="false" GridLines="None" AllowSorting="false"
                                    AutoGenerateColumns="False" CssClass="table table-striped bottom-zero" Width="100%" Visible="true" DataKeyNames="grupo, certificacoes">
                                    <Columns>
                                        <asp:BoundField Visible="True" DataField="grupo" HeaderText="GRUPO"></asp:BoundField>
                                        <asp:BoundField Visible="True" DataField="certificacoes" HeaderText="CERTIFICAÇÕES"></asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="paginacao" />
                                    <PagerSettings Position="Bottom" Mode="Numeric" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-6">
                <label class="control-label text-uppercase">Certificação por departamento</label>

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div id="certificadosdpto">
                            <div id="pd-graficodepartamento"></div>
                        </div>

                        <div class="table-responsive">
                            <div style="width: 100%; height: 200px; overflow: scroll">
                                <asp:GridView ID="grdpesquisaDepartamento" runat="server" AllowPaging="false" GridLines="None" AllowSorting="false"
                                    AutoGenerateColumns="False" CssClass="table table-striped bottom-zero" Width="100%" Visible="true" DataKeyNames="departamento, certificacoes">
                                    <Columns>
                                        <asp:BoundField Visible="True" DataField="departamento" HeaderText="DEPARTAMENTO"></asp:BoundField>
                                        <asp:BoundField Visible="True" DataField="certificacoes" HeaderText="CERTIFICAÇÕES"></asp:BoundField>
                                    </Columns>
                                    <PagerStyle CssClass="paginacao" />
                                    <PagerSettings Position="Bottom" Mode="Numeric" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
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


    <%--removido a pedido do mustafá - 19/11/2016 para funcionar o calendario--%>
    <%--<script type="text/javascript" src="Scripts/highcharts-graficos.js"></script>--%>
    <script type="text/javascript" src="Scripts/highcharts.js"></script>
    <script type="text/javascript" src="Scripts/highcharts-3d.js"></script>

</asp:Content>
