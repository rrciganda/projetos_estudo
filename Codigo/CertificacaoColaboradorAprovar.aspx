<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificacaoColaboradorAprovar.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.CertificacaoColaboradorAprovar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12">
        <h1 class="text-bold">APROVAR CERTIFICAÇÕES</h1>
        <span>AQUI VOCÊ APROVA AS CERTIFICAÇÕES PENDENTES</span>
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
                OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="30" OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idcertificacao, idcertificado, idregulador, idusuario, status, observacao,  validade, descricao, dtcertificacao">
                <Columns>
                    <asp:BoundField Visible="True" DataField="nomecolaborador" HeaderText="Profissional"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="nomecertificado" HeaderText="Certificado"></asp:BoundField>
                    <asp:BoundField Visible="True" DataField="dtinclusao" HeaderText="Data Inclusão"></asp:BoundField>
                 <%--   <asp:ButtonField CommandName="cmdAbrir" ControlStyle-CssClass="icon-download editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>--%>
                    <asp:ButtonField CommandName="cmdAlterar" ControlStyle-CssClass="icon-pencil editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
            <%--        <asp:ButtonField CommandName="cmdAprovar" ControlStyle-CssClass="icon-check editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="cmdReprovar" ControlStyle-CssClass="icon-cancel editarexcluir">
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:ButtonField>--%>
                </Columns>
                <PagerStyle CssClass="paginacao" />
                <PagerSettings Position="Bottom" Mode="Numeric" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

