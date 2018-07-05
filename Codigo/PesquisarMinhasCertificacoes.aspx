<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PesquisarMinhasCertificacoes.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.PesquisarMinhasCertificacoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12">
        <h1 class="text-bold">MINHAS CERTIFICAÇÕES</h1>
        <span>AQUI VOCÊ PESQUISA SUAS CERTIFICAÇÕES</span>
    </div>
    <hr class="transparentHR">
    <div id="diverro" runat="server" visible="false">
        <div class="row">
            <div class="col-sm-12 form-group">
                <div class="EstudoMsg EstudoMsg-info EstudoMsg-small">
                    <div class="EstudoMsg-icon">Info!</div>
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


    <%--<div class="row">--%>
    <div class="col-sm-12">
        <label class="control-label" for="filterBase"></label>
        <div class="table table-responsive">
            <asp:GridView ID="grdpesquisa" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="false"
                OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idcertificacao, idcertificado, idregulador, idusuario, status, observacao,  validade, descricao, dtcertificacao, aprovacao">
                <Columns>
                    <asp:BoundField Visible="True" DataField="nomegrupo" HeaderText="Grupo"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomecertificado" HeaderText="Certificado"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomeregulador" HeaderText="Regulador"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomecolaborador" HeaderText="Profissional"></asp:BoundField>
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <%# Entity.EOUtil.RetornarStatusPadraoCertificacao((Int32) Eval("status")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aprovação">
                        <ItemTemplate>
                            <%# Entity.EOUtil.RetornarStatusAprovacaoCertificacaoPesquisa((Int32) Eval("status"),(Int32) Eval("aprovacao")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField Visible="True" DataField="dtcertificacao" HeaderText="Data Certificação" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="validade" HeaderText="Data Validade" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundField>
                    <asp:ButtonField CommandName="cmdAbrir" ControlStyle-CssClass="icon-download editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="cmdAlterar" ControlStyle-CssClass="icon-pencil editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
                      <asp:BoundField Visible="True" DataField="status" HeaderText="status"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="aprovacao" HeaderText="aprovacao"></asp:BoundField>
                </Columns>
                <PagerStyle CssClass="paginacao" />
                <PagerSettings Position="Bottom" Mode="Numeric" />
            </asp:GridView>
        </div>

        <asp:LinkButton ID="lnkNovaCertificacao" CssClass="pull-right btn btn-primary btn-align-bottom" runat="server" OnClick="lnkNovaCertificacao_Click">
                            <i class="icon-plus"></i> Nova Certificação
        </asp:LinkButton>
    </div>


</asp:Content>
