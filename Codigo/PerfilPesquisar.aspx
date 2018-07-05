<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilPesquisar.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.PerfilPesquisar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>

            <div class="col-sm-12 form-group">
                <div class="text-uppercase">
                    <h1 class="text-bold">Perfil de Acesso</h1>
                    <span>AQUI VOCÊ GERENCIA OS PERFIS DE ACESSO</span>
                    <hr>
                </div>

            </div>
            <div class="col-sm-12 form-group">
                <label class="control-label">PESQUISAR PERFIL</label>
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
                    <div class="col-sm-4 form-group" style="display: none;">
                        <label for="type-db">idperfil</label>
                        <asp:TextBox ID="txtidperfil" CssClass="form-control" runat="server" MaxLength="8"></asp:TextBox>
                    </div>
                    <div class="col-sm-4 form-group">
                        <label for="type-db">Nome</label>
                        <asp:TextBox ID="txtnome" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 form-group">
                        <label for="type-db">Status</label>
                        <asp:DropDownList ID="rdostatus" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                            <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-1 form-group">
                        <asp:LinkButton ID="lnkPesquisar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkPesquisar_Click">
                            <i class="icon-search"></i>
                        </asp:LinkButton>

                    </div>
                </div>
            </div>

            <hr class="col-sm-12">

            <div class="col-sm-12">
                <label class="control-label" for="filterBase">Resultado da Pesquisa</label>
                <div class="table table-responsive">
                    <div class="col-sm-12">
                        <asp:GridView ID="grdpesquisa" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                            OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="10"
                            OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idperfil, nome, status" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                            <Columns>
                               <%-- <asp:BoundField Visible="false" DataField="idperfil" HeaderText="idperfil"></asp:BoundField>--%>
                                <asp:BoundField Visible="True" DataField="nome" HeaderText="Nome"></asp:BoundField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Entity.EOUtil.RetornarStatusPadrao((Int32) Eval("status")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                </div>
            </div>
            <div class="col-sm-12 text-right">
                <asp:LinkButton ID="lnkNovo" CssClass="btn btn-primary" runat="server" OnClick="lnkNovo_Click">
                            <i class="icon-plus"></i>Novo
                </asp:LinkButton>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
