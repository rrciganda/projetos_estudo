<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FornecedorPesquisar.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.FornecedorPesquisar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">FORNECEDOR</h1>
                            <span>AQUI VOCÊ PESQUISA OS FORNECEDOR DE CERTIFICADOS</span>
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

                            <div class="col-sm-4 form-group">
                                <label for="txtFornecedor">Fornecedor</label>
                                <asp:TextBox ID="txtFornecedor" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 form-group">
                                <label for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Todos" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>


                            <div class="col-sm-1 form-group">
                                <asp:LinkButton ID="lnkPesquisar" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkPesquisar_Click">
                                    <i class="icon-search"></i>
                                </asp:LinkButton>                        
                            </div>


                            <div class="col-sm-12">
                                <asp:GridView ID="grdpesquisa" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                                    OnRowCommand="grdpesquisa_RowCommand" OnRowDataBound="grdpesquisa_RowDataBound" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idfornecedor, nome, status" GridLines="None" BorderStyle="none" CellSpacing="-1" >
                                    <Columns>

                                        <asp:BoundField Visible="True" DataField="nome" HeaderText="NOME"></asp:BoundField>
                                        <asp:TemplateField HeaderText="STATUS">
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
                </div>
                <%--div abaixo que fecha o container da master page--%>
            </div>      
             <div class="col-sm-12 text-right">
                <asp:LinkButton ID="lnkNovo" CssClass="btn btn-primary" runat="server" OnClick="lnkNovo_Click">
                            <i class="icon-plus"></i>Novo
                </asp:LinkButton>
            </div>      
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
