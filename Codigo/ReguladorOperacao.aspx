<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReguladorOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.ReguladorOperacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>            
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">REGULADOR</h1>
                            <span>AQUI VOCÊ INCLUI/ALTERA O REGULADOR DO CERTIFICADO.</span>
                            <hr>
                        </div>
                        <asp:HiddenField ID="hdnId" runat="server" />
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
                        <div class="row">

                            <div class="col-sm-4 form-group">
                                <label class="control-label" for="txtNome">Regulador</label>
                                <asp:TextBox ID="txtRegulador" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 form-group">
                                <label class="control-label" for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                           
                        </div>


                        <label class="control-label" for="txtObservacao">Observações:</label>
                        <asp:TextBox ID="txtObservacao" CssClass="form-control form-group" runat="server" MaxLength="300" TextMode="MultiLine" Rows="10"></asp:TextBox>


                        <div class="col-sm-12 text-right">
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
            </div>
            <%--div abaixo que fecha o container da master page--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
