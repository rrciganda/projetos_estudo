<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProvaOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.ProvaOperacao" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">PROVAS</h1>
                            <span>Aqui você gerencia as provas</span>
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
                                <label class="control-label" for="txtNome">Prova</label>
                                <asp:TextBox ID="txtnome" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 form-group">
                                <label class="control-label" for="txtNome">Alias Prova</label>
                                <asp:TextBox ID="txtalias" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 form-group">
                                <label class="control-label" for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr>

                        <%--   <h3 class="text-bold">APROVADORES</h3>--%>

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
                            <h3><span class="text-bold"><i class="icon-minus" id="iconeaprovador"></i>CERTIFICADOS</h3>
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
                                                        <label for="type-db">Certificação</label>
                                                        <%--<asp:DropDownList ID="ddlcertificacao" CssClass="combobox form-control" runat="server" DataValueField="idcertificado" DataTextField="dscombo" Width="100%"></asp:DropDownList>
                                                        <label for="type-db">Profissional</label>--%>
                                                        <telerik:RadComboBox RenderMode="Classic" CssClass="dsp-block combobox form-control" Skin="Metro" ID="ddlcertificacao" AutoPostBack="false" runat="server" Width="100%" Font-Names="Estudotxtregular" Font-Size="14px" ForeColor="#6d6e71">
                                                        </telerik:RadComboBox>
                                                    </div>

                                                    <div class="form-group col-sm-2">
                                                        <asp:LinkButton ID="lnkIncluir" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkIncluir_Click"><i class="icon-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" id="dvcertificados" visible="false">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">

                                                            <asp:GridView ID="grdcertificacao" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                                                                AllowPaging="true" PageSize="10" OnRowCommand="grdcertificacao_RowCommand"
                                                                DataKeyNames="idcertificado, nomecertificado" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                                                                <Columns>
                                                                    <%--<asp:BoundField Visible="True" DataField="idcertificado" HeaderText="id"></asp:BoundField>--%>
                                                                    <asp:BoundField Visible="True" DataField="nomecertificado" HeaderText="NOME DO CERTIFICADO"></asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Requerida">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButton ID="rdoRequerida" GroupName="TipoProva" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean((Eval("tipo").ToString()=="Requerida"?"true":"false")) %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Obrigatória">
                                                                        <ItemTemplate>
                                                                            <asp:RadioButton ID="rdoObrigatoria" GroupName="TipoProva" runat="server" AutoPostBack="true" Checked='<%# Convert.ToBoolean((Eval("tipo").ToString()=="Obrigatória"?"true":"false")) %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

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
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <label class="control-label col-sm-12" for="filterBase">As certificações assumirão o status de concluído, para as seguintes situações:</label>
                            <label class="control-label col-sm-12" for="filterBase">•	Quando houver uma ou mais provas Requeridas cuja TODAS OU apenas UMA delas possuir(em) status = “Aprovado”</label>
                            <label class="control-label col-sm-12" for="filterBase">•	Quando houver uma ou mais provas Obrigatórias cuja TODAS possuírem status = “Aprovado”</label>
                            <label class="control-label col-sm-12" for="filterBase">•	Quando houver uma ou mais provas Obrigatórias e Requeridas cuja TODAS (Obrigatórias + Requeridas) OU apenas UMA Requerida possuir(em) status = “Aprovado”</label>
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
