<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificadoOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.CertificadoOperacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">CERTIFICADO</h1>
                            <span>AQUI VOCÊ INCLUI/ALTERA O CERTIFICADO.</span>
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
                                <label class="control-label" for="filterBase">Certificado</label>
                                <asp:TextBox ID="txtCertificado" CssClass="form-control" runat="server" MaxLength="300"></asp:TextBox>
                            </div>

                            <div class="col-sm-4 form-group">
                                <label for="ddlGrupo">Grupo</label>
                                <asp:DropDownList ID="ddlGrupo" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-sm-4 form-group">
                                <label for="ddlGrupo">Fornecedor</label>
                                <asp:DropDownList ID="ddlfornecedor" CssClass="form-control" runat="server" Enabled="False"></asp:DropDownList>
                            </div>

                            <div class="col-sm-4 form-group">
                                <label class="control-label" for="txtVersao">Nº Versão</label>
                                <asp:TextBox ID="txtVersao" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                            </div>

                            <div class="col-sm-4" style="display: none;">
                                <label class="control-label" for="filterBase">Teste Certificador</label>
                                <asp:TextBox ID="txtTesteCertificador" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>

                            <div class="col-sm-4 form-group">
                                <label class="control-label" for="ddlStatus">Status</label>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="row">
                            <label class="control-label col-sm-12" for="filterBase">Validade do Certificado</label>
                            <div class="form-group">

                                <div class="col-sm-1 form-group">
                                    <label class="control-label" for="filterBase">Qtd Dia</label>
                                    <asp:TextBox ID="txtqtddiavalidade" CssClass="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                </div>
                                <div class="col-sm-1 form-group">
                                    <label class="control-label" for="filterBase">Qtd Mes</label>
                                    <asp:TextBox ID="txtqtdmesvalidade" CssClass="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                </div>

                                <div class="col-sm-1 form-group">
                                    <label class="control-label" for="filterBase">Qtd Ano</label>
                                    <asp:TextBox ID="txtqtdanovalidade" CssClass="form-control" runat="server" MaxLength="3"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-sm-offset-1 form-group">
                                    <label class="control-label" for="filterBase">&nbsp;</label><br />
                                    <asp:CheckBox runat="server" ID="chknaoexpira" AutoPostBack="true" OnCheckedChanged="chknaoexpira_CheckedChanged" /><label class="control-label" for="filterBase">&nbsp; Não Expira</label>
                                </div>
                                <div class="col-sm-2 form-group">
                                    <label class="control-label" for="filterBase">&nbsp;</label><br />
                                    <asp:CheckBox runat="server" ID="chkvoucher" AutoPostBack="true" OnCheckedChanged="chkvoucher_CheckedChanged" /><label class="control-label" for="filterBase">&nbsp; Voucher</label>
                                </div>
                                <div class="col-sm-2 form-group">
                                    <label class="control-label" for="filterBase">Qtd Voucher</label>
                                    <asp:TextBox ID="txtqtdevoucher" CssClass="form-control" runat="server" Enabled="false" MaxLength="3"></asp:TextBox>
                                </div>
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
                            <h3><span class="text-bold"><i class="icon-minus" id="iconeaprovador"></i>PROVAS DO CERTIFICADO</h3>
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
                                                        <label for="type-db">Prova</label>
                                                        <asp:DropDownList ID="ddlprova" CssClass="combobox form-control" runat="server" DataValueField="idprova" DataTextField="dscombo" Width="100%"></asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-sm-2">
                                                        <asp:LinkButton ID="lnkIncluir" CssClass="btn btn-primary btn-align-bottom" runat="server" OnClick="lnkIncluir_Click"><i class="icon-plus"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" runat="server" id="dvprovas" visible="false">
                                                <div class="col-sm-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-body">
                                                            <asp:GridView ID="grdcertificacao" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                                                                AllowPaging="false" PageSize="10" OnRowCommand="grdcertificacao_RowCommand"
                                                                DataKeyNames="idprova, nome, tipo" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                                                                <Columns>
                                                                    <asp:BoundField Visible="True" DataField="nome" HeaderText="PROVA"></asp:BoundField>
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
