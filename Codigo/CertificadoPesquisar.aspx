<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificadoPesquisar.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.CertificadoPesquisar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="col-sm-12 text-uppercase">
                <h1 class="text-bold text-uppercase">CERTIFICADO</h1>
                <span class="text-uppercase"><span>AQUI VOCÊ PESQUISA OS CERTIFICADOS</span></span>
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


            <div class="col-sm-12 form-group text-uppercase">
                <label class="control-label">Pesquisar certificações por:</label>
            </div>

            <div class="col-sm-4 form-group">
                <label for="ddlGrupo">Grupo</label>
                <asp:DropDownList ID="ddlGrupo" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

                <div class="col-sm-4 form-group">
                <label for="ddlGrupo">Fornecedor</label>
                <asp:DropDownList ID="ddlfornecedor" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-sm-4 form-group">
                <label for="txtNomeCertificado">Certificado</label>
                <asp:TextBox ID="txtNomeCertificado" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
                        
            <div class="col-sm-4 form-group">
                <label for="ddlStatus">Status</label>
                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-4 form-group">
                <label for="txtVersao">Nº Versão</label>
                <asp:TextBox ID="txtVersao" CssClass="form-control" runat="server"></asp:TextBox>
            </div>          

            <div class="col-sm-1 form-group">
                <asp:LinkButton ID="lnkPesquisar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkPesquisar_Click">
                                    <i class="icon-search"></i>
                </asp:LinkButton>
            </div>            

            <hr class="col-sm-12">
            <div class="col-sm-12">
                <asp:GridView ID="grdpesquisa" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                    OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idcertificado, nome, status" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                    <Columns>

                        <asp:BoundField Visible="True" DataField="nome" HeaderText="Certificado"></asp:BoundField>
                        <asp:BoundField Visible="True" DataField="nomeGrupo" HeaderText="Grupo"></asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <%# Entity.EOUtil.RetornarStatusPadrao((Int32) Eval("status")) %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField Visible="True" DataField="versao" HeaderText="Nº Versão"></asp:BoundField>
                        <asp:BoundField Visible="True" DataField="dsvalidade" HeaderText="Validade"></asp:BoundField>
                        
                        <asp:ButtonField CommandName="cmdAlterar" ControlStyle-CssClass="icon-pencil editarexcluir">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="cmdExcluir" ControlStyle-CssClass="icon-trash editarexcluir">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:ButtonField>
                    </Columns>
                    <PagerStyle CssClass="paginacao" />
                    <PagerSettings Position="Bottom" Mode="Numeric" />
                </asp:GridView>
            </div>
             <div class="col-sm-12 text-right">
                <asp:LinkButton ID="lnkNovo" CssClass="btn btn-primary" runat="server" OnClick="lnkNovo_Click">
                            <i class="icon-plus"></i>Novo
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
