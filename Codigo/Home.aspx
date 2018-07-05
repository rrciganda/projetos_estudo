<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <script src="Scripts/showModalDialog.js"></script>
            <script>
                function OpenModalDetalhe(tipo) {
                    var ret = window.showModalDialog("DetalheHome.aspx?Acao=" + tipo, "Anexo", "dialogWidth:550px;dialogHeight:340px");

                }
            </script>
            <div class="col-sm-12">
                <h1 class="text-bold">HOME</h1>
                <span>SEJA BEM VINDO AO SISTEMA MATRIZ DE CONHECIMENTO</span>
                <hr>
                <asp:HiddenField ID="hdnId" runat="server" />
                <div class="row" runat="server" id="divindicadores">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-4 col-lg-2 form-group">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações Registradas<br />
                                            &nbsp;</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacaoRegistrada')">
                                            <asp:Label ID="lbCertificacaoregistrada" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações Concluidas<br />
                                            &nbsp;</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacaoConcluida')">
                                            <asp:Label ID="lbCertificacoesConcluidas" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações em Andamento<br />
                                            &nbsp;</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacaoEmAndamento')">
                                            <asp:Label ID="lbCertificacoesEmAndamento" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">

                                        <h5 class="panel-title titulo-indicadores">Certificações<br />
                                            Expiradas<br />
                                            &nbsp;</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacaoExpirada')">
                                            <asp:Label ID="lblCertificacoesExpiradas" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group" id ="divColCadastrosECertificados" runat="server">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Profissionais cadastrados e certificados</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('ColaboradorCertificado')">
                                            <asp:Label ID="lbColCadastrosECertificados" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group" id ="divSemCertificado" runat="server">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações cadastrados e Sem certificados</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('SemCertificado')">
                                            <asp:Label ID="lblSemCertificado" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-lg-2 form-group">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Últ. certificação cadastrada em:
                                            <br />
                                            &nbsp;</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores-sx">
                                        <a href="javascript:OpenModalDetalhe('DataUltimaCertificacao')">
                                        <asp:Label ID="lbUltCertificacaoCadastrada" runat="server" Text=""></asp:Label>
                                            </a>
                                    </div>
                                </div>
                            </div>

                               <div class="col-sm-4 col-lg-2 form-group" id ="divCertificacoesPendentesEnvio" runat="server">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações Pendentes de Envio para Aprovação</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacoesPendentesEnvio')">
                                            <asp:Label ID="lblCertificacoesPendentesEnvio" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>

                               <div class="col-sm-4 col-lg-2 form-group" id ="divCertificacoesPendentesAprovacao" runat="server">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações Pendentes de Aprovação</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacoesPendentesAprovacao')">
                                            <asp:Label ID="lblCertificacoesPendentesAprovacao" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>

                             <div class="col-sm-4 col-lg-2 form-group" id ="divCertificacoesReprovadas" runat="server">
                                <div class="panel panel-info bottom-zero">
                                    <div class="panel-heading">
                                        <h5 class="panel-title titulo-indicadores">Certificações com erro de preenchimento</h5>
                                    </div>
                                    <div class="panel-body text-center numero-indicadores">
                                        <a href="javascript:OpenModalDetalhe('CertificacoesReprovadas')">
                                            <asp:Label ID="lblCertificacoesReprovadas" runat="server" Text=""></asp:Label>
                                        </a>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <hr>
            </div>

            <div class="col-sm-6 form-group" runat="server" id="divTopCertificacoes">
                <label class="control-label text-uppercase" for="filterBase"><i class="icon-star star-top20"></i>Top 20 Certificações</label>

                <div class="table table-responsive">

                    <asp:GridView ID="topcertificacoes" runat="server" AllowPaging="false" GridLines="None" AllowSorting="false" DataKeyNames=""
                        AutoGenerateColumns="False" CssClass="table table-striped" Width="100%" Visible="true">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ordem" />
                            <asp:BoundField HeaderText="Quantidade" DataField="count" />
                            <asp:BoundField HeaderText="Certificação" DataField="nome" />
                            <asp:BoundField HeaderText="Versão" DataField="versao" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-sm-6 form-group" runat="server" id="divTopCertGrupo">
                <label class="control-label text-uppercase text-responsive" for="filterBase"><i class="icon-star star-top20"></i>TOP 20 CERTIFICADOS POR GRUPOS</label>
                <div class="table table-responsive">
                    <asp:GridView ID="topporgrupo" runat="server" AllowPaging="false" GridLines="None" AllowSorting="false" DataKeyNames=""
                        AutoGenerateColumns="False" CssClass="table table-striped" Width="100%" Visible="true">
                        <Columns>
                            <asp:BoundField HeaderText="#" DataField="ordem" />
                            <asp:BoundField HeaderText="Quantidade" DataField="qtd" />
                            <asp:BoundField HeaderText="Grupos" DataField="nome" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



