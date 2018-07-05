<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GrupoOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.GrupoOperacao" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">GRUPO</h1>
                            <span>AQUI VOCÊ INCLUI/ALTERA O GRUPO DE CERTIFICAÇÃO.</span>
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
                                <label class="control-label" for="txtNome">Grupo</label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 form-group">
                                <label class="control-label" for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-12">
                                <label class="control-label" for="txtObservacao">Observações:</label>
                                <asp:TextBox ID="txtObservacao" CssClass="form-control form-group" runat="server" MaxLength="300" TextMode="MultiLine" Rows="10"></asp:TextBox>
                            </div>
                        </div>

                        <script>
                            function ForceToggle(divdestino, iconeorigem) {
                                //alert(divdestino + " - " + $('#' + divdestino).is(':visible'));
                                if ($('#' + divdestino).is(':visible')) {
                                    $('#' + iconeorigem).removeClass("icon-minus");
                                    $('#' + iconeorigem).addClass("icon-plus");
                                    $('#' + divdestino).hide(1000);
                                }
                                else {
                                    $('#' + iconeorigem).removeClass("icon-plus");
                                    $('#' + iconeorigem).addClass("icon-minus");
                                    $('#' + divdestino).show(1000);
                                }
                            }
                        </script>
                        <div role="button" onclick="javascript:ForceToggle('<%=collapseOne.ClientID%>','iconeaprovador')">
                            <h3><span class="text-bold"><i class="icon-minus" id="iconeaprovador"></i>FORNECEDORES</h3>
                        </div>
                        <div runat="server" id="collapseOne" class="row">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="col-sm-12">
                                            <hr>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-10 form-group">
                                                        <label for="type-db">Fornecedor</label>
                                                        <asp:DropDownList ID="ddlfornecedor" CssClass="combobox form-control" runat="server" DataValueField="idfornecedor" DataTextField="nome" Width="100%"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-sm-2">
                                                        <asp:LinkButton ID="lnkIncluir" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkIncluir_Click"><i class="icon-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" id="dvfornecedores" visible="false">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <asp:GridView ID="grdfornecedor" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                                                                OnRowCommand="grdfornecedor_RowCommand"
                                                                DataKeyNames="idfornecedor, nomefornecedor, status" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                                                                <Columns>
                                                                    <asp:BoundField Visible="True" DataField="nomefornecedor" HeaderText="Nome"></asp:BoundField>
                                                                    <%--<asp:BoundField Visible="True" DataField="status" HeaderText="Status"></asp:BoundField>--%>
                                                                    <asp:ButtonField CommandName="cmdExcluir" ControlStyle-CssClass="icon-trash editarexcluir">
                                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                    </asp:ButtonField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <br />
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
            </span>
             <script type="text/javascript">

                 function comboautocomplete() {
                     $(document).ready(function () {
                         $('.combobox').combobox();
                     });
                 }
             </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
