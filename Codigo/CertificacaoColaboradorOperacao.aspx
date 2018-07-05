<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificacaoColaboradorOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.MinhaCertificacaoOperacao" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="col-sm-12">
                <h1>
                    <asp:Label runat="server" ID="lbNomePagina" CssClass="text-bold"></asp:Label></h1>
                <span>AQUI VOCÊ INCLUI/ALTERA A CERTIFICAÇÃO DO COLABORADOR.</span>
                <hr>
            </div>

            <div id="diverro" runat="server" visible="false">
                <div class="row">
                    <div class="col-sm-12 form-group">
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
            <div id="divsucesso" runat="server" visible="false">
                <div class="row">
                    <div class="col-sm-12 form-group">
                        <div class="EstudoMsg EstudoMsg-success EstudoMsg-small">
                            <div class="EstudoMsg-icon">Sucesso</div>
                            <p class="EstudoMsg-title">
                                <asp:Label ID="lblsucesso" runat="server" Text=""></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="gridSystemModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="gridSystemModalLabel">
                                <asp:Label ID="lblConfirmacaoEnvio" runat="server" Text="Confirme o envio"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div id="divConfirmaEnvioAprovacao" runat="server">
                                <div class="row">
                                    <div class="col-sm-12 form-group">
                                        <div class="EstudoMsg EstudoMsg-success EstudoMsg-small">

                                            <div id="divErroModal" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-sm-12 form-group">
                                                        <div class="EstudoMsg EstudoMsg-error EstudoMsg-small">
                                                            <div class="EstudoMsg-icon">Erro!</div>
                                                            <p class="EstudoMsg-title">
                                                                <asp:Label ID="lblErroModal" runat="server" Text=""></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <p class="EstudoMsg-title">
                                                <asp:CheckBox ID="chkAceite" runat="server" Visible="true" />
                                                <asp:Label ID="lblAceite" Text="Li e concordo que ao cadastrar uma certificação na Matriz do conhecimento, afirmo que todas as informações apresentadas são verídicas e de minha responsabilidade" runat="server" Visible="true" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>
                            <asp:LinkButton ID="btnEnviarAprovacao" CssClass="btn btn-info" runat="server" OnClick="btnEnviarAprovacao_Click">
                                        <i class="icon-paper-plane-empty"></i>
                                        Confirmar Envio Aprovação
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnSalvarAprovacao" CssClass="btn btn-info" runat="server" OnClick="btnSalvarAprovacao_Click">
                                        <i class="icon-paper-plane-empty"></i>
                                        Não Enviar Agora
                            </asp:LinkButton>

                            <asp:LinkButton ID="btnEnviarSalvarAprovacao" CssClass="btn btn-info" runat="server" OnClick="btnEnviarSalvarAprovacao_Click">
                                        <i class="icon-paper-plane-empty"></i>
                                        Enviar para Aprovação
                            </asp:LinkButton>

                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
            <!-- /.modal -->



            <asp:HiddenField ID="hdnId" runat="server" />
            <script>
                function Closepopup() {
                    $('#myModal').modal('close');

                }


                function FecharModalAnexo(acao) {
                    if (acao == "ALTEROU") {
                        $('#' + '<%=hdnpossuianexo.ClientID%>').val("1");
                        $('#' + '<%=divanexarlinkcertificado.ClientID%>').hide(0);
                        $('#' + '<%=divanexolink.ClientID%>').show(0);
                    } else {
                        if ($('#' + '<%=hdnpossuianexo.ClientID%>').val() == "1") {
                            $('#' + '<%=divanexarlinkcertificado.ClientID%>').hide(0);
                            $('#' + '<%=divanexolink.ClientID%>').show(0);
                        } else {
                            $('#' + '<%=divanexarlinkcertificado.ClientID%>').show(0);
                            $('#' + '<%=divanexolink.ClientID%>').hide(0);
                        }
                        // var dialog = $('dialog');
                        // dialog.trigger('close');
                    }
                    $('#modal').modal('toggle');
                }

                function fecharModelDownload() {
                    $('#modalDonwload').modal('toggle');
                }

                //function OpenModalVisualizar() {
                //    var ret = window.showModalDialog("VisualizarCertificado.aspx", "Visualizar", "dialogWidth:250px;dialogHeight:33px");
                //}
                function OpenModalDownload() {

                    //var element = document.createElement("iframe");
                    //element.setAttribute('id', 'myframedownload');
                    //document.body.appendChild(element);
                    //$('#myframedownload').load("DownloadCertificado.aspx");
                    setTimeout(function () {
                        $('#dialog-close').html("<font style='font-size: 12px;bold:700;'>Efetuando download. Aguarde.&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; x </font>");
                    }, 300);
                    setTimeout(function () {
                        FecharModalAnexo('');
                    }, 2500);

                    var ret = window.showModalDialog("DownloadCertificado.aspx", "Download", "dialogWidth:250px;dialogHeight:33px");
                    //var dialog = $('#dialog-close');
                    //dialog.append("Efetuando download. Aguarde.");

                    //var jan = window.open("DownloadCertificado.aspx", '_blank', 'toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,left=0, top=0, width=40, height=20, visible=none', '');
                    //setTimeout(function () { jan.close(); }, 2000);
                    //jan.close();
                }
                function OpenModalAnexo() {
                    var ret = window.showModalDialog("AnexarCertificado.aspx", "Anexo", "dialogWidth:550px;dialogHeight:340px");
                    //alert("Returned from modal: " + ret);
                    if (ret == "ALTEROU") {
                        //alert("1");
                        $('#' + '<%=hdnpossuianexo.ClientID%>').val("1");
                        $('#' + '<%=divanexarlinkcertificado.ClientID%>').hide(0);
                        $('#' + '<%=divanexolink.ClientID%>').show(0);
                    }
                    else {
                        //alert("2");
                        if ($('#' + '<%=hdnpossuianexo.ClientID%>').val() == "1") {
                            $('#' + '<%=divanexarlinkcertificado.ClientID%>').hide(0);
                            $('#' + '<%=divanexolink.ClientID%>').show(0);
                        }
                        else {
                            //alert("3");
                            $('#' + '<%=divanexarlinkcertificado.ClientID%>').show(0);
                            $('#' + '<%=divanexolink.ClientID%>').hide(0);
                        }
                    }
                  <%-- 
                window.returnValue = undefined;
                 var retValue = window.showModalDialog("AnexarCertificado.aspx", 0, "dialogWidth=450,dialogHeight=280");
                    alert("3");
                    alert(retValue);
                    alert(retValue.returnValue);
                    var retorno = retValue;
                    if (retorno == null || retorno == undefined || retorno == "")
                        retorno = retValue.returnValue;

                    if (retorno == "ALTEROU") {
                        $('#' + '<%=hdnpossuianexo.ClientID%>').val("1");
                    }
                    else
                    {

                    }--%>
                }
                function ForceToggle(divdestino, iconeorigem, campovisivel) {
                    //alert(divdestino + " - " + $('#' + divdestino).is(':visible'));
                    //alert($('#' + campovisivel));
                    //alert($('#' + campovisivel).val());
                    if ($('#' + divdestino).is(':visible')) {
                        $('#' + iconeorigem).removeClass("icon-minus");
                        $('#' + iconeorigem).addClass("icon-plus");
                        $('#' + divdestino).hide(1000);
                        $('#' + campovisivel).val("0");
                    }
                    else {
                        $('#' + iconeorigem).removeClass("icon-plus");
                        $('#' + iconeorigem).addClass("icon-minus");
                        $('#' + divdestino).show(1000);
                        $('#' + campovisivel).val("1");
                    }
                }
            </script>
            <div class="col-sm-12">
                <div runat="server" id="mnCertificacoes">
                    <div role="button" onclick="javascript:ForceToggle('<%=divcertificado.ClientID%>','<%=iconecertificado.ClientID%>','<%=txtcertificadovisivel.ClientID%>')">
                        <h4><span class="text-bold"><i class="icon-minus" id="iconecertificado" runat="server"></i>DADOS CERTIFICAÇÃO</h4>
                        <asp:HiddenField runat="server" Value="1" ID="txtcertificadovisivel"></asp:HiddenField>
                    </div>
                </div>

                <div runat="server" id="divcertificado" class="collapse-dadoscertificacao">
                    <div class="col-sm-12">
                        <%-- <div class="col-sm-4 form-group">
                            <label for="type-db">Profissional</label>
                            <asp:DropDownList runat="server" ID="ddlColaborador" CssClass="combobox form-control" Width="100%" DataTextField="dscombo" DataValueField="idusuario"></asp:DropDownList>
                            <asp:HiddenField runat="server" ID="hdnStatusOrigilnal" />
                        </div>--%>
                        <div class="col-sm-4 form-group">
                            <label for="type-db">Profissional</label>
                            <telerik:RadComboBox RenderMode="Classic" CssClass="dsp-block combobox form-control" Skin="Metro" ID="ddlColaborador" AutoPostBack="false" runat="server" Width="100%" Font-Names="Estudotxtregular" Font-Size="14px" ForeColor="#6d6e71">
                            </telerik:RadComboBox>
                            <asp:HiddenField runat="server" ID="hdnStatusOrigilnal" />
                        </div>

                        <div class="col-sm-8 form-group">
                            <label for="type-db">Grupo / Fornecedor / Certificado / Versão</label>
                            <asp:DropDownList ID="ddlidcertificado" CssClass="combobox form-control" OnSelectedIndexChanged="ddlidcertificado_SelectedIndexChanged" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label for="type-db">Regulador</label>
                            <asp:DropDownList ID="ddlidregulador" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label for="type-db">N. Licença</label>
                            <asp:TextBox ID="txtLicenca" CssClass="form-control" placeholder="Número Licença" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label for="type-db">URL</label>
                            <asp:TextBox ID="txtURL" CssClass="form-control" runat="server" placeholder="URL" MaxLength="200"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 form-group js-container-datepicker">
                            <label for="type-db">Data Certificação</label>
                            <asp:TextBox ID="txtdtcertificacao" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtdtcertificacao_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 form-group js-container-datepicker">
                            <label for="type-db">Data Validade</label>
                            <asp:TextBox ID="txtvalidade" CssClass="form-control js-is-datepicker-validade" placeholder="DD/MM/AAAA" runat="server" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 text-center btn-align-bottom nao-se-aplica">
                            <%-- <asp:RadioButton ID="rdoExpira" Text="Não Expira" runat="server" />--%>
                            <asp:CheckBox ID="chkExpira" Text="Não Expira" runat="server" AutoPostBack="true" OnCheckedChanged="chkExpira_CheckedChanged" />
                        </div>
                        <div class="col-sm-2 text-center btn-align-bottom nao-se-aplica">
                            <%-- <asp:RadioButton ID="rdoExpira" Text="Não Expira" runat="server" />--%>
                            <asp:CheckBox ID="chkVoucher" Text="Voucher" Enabled="false" runat="server" AutoPostBack="true" OnCheckedChanged="chkVoucher_CheckedChanged" />
                        </div>
                        <div class="col-sm-2 form-group">
                            <label for="type-db">Qtd Voucher</label>
                            <asp:TextBox ID="txtqtdvoucher" CssClass="form-control" runat="server" placeholder="Qtd Voucher" Enabled="false" MaxLength="99"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 form-group">
                            <label for="type-db">Status Certificação</label>
                            <asp:DropDownList ID="ddlStatusCertificacao" CssClass="form-control" runat="server" Enabled="false">
                                <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Concluída" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Em Andamento" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Reprovado" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <hr>

            <div class="col-sm-12">
                <div runat="server" id="Div1">
                    <div role="button" onclick="javascript:ForceToggle('<%=divprova.ClientID%>','<%=iconeprova.ClientID%>','<%=txtprovavisivel.ClientID%>')">
                        </i><h4><span class="text-bold"><i class="icon-plus" id="iconeprova" runat="server"></i>PROVAS DO CERTIFICADO</h4>
                        <asp:HiddenField runat="server" Value="0" ID="txtprovavisivel"></asp:HiddenField>
                    </div>
                </div>

                <div runat="server" id="divprova" style="display: none;">
                    <div id="divInfoProva" runat="server" visible="false">
                        <div class="row">
                            <div class="col-sm-12 form-group">
                                <div class="EstudoMsg EstudoMsg-info EstudoMsg-small">
                                    <div class="EstudoMsg-icon">Info!</div>
                                    <p class="EstudoMsg-title">
                                        <asp:Label ID="lblmsInfoProva" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-12">
                            <label class="control-label" for="filterBase"></label>
                            <div class="table table-responsive">
                                <asp:GridView ID="grdpesquisa" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="30" OnRowCommand="grdpesquisa_RowCommand" OnRowDeleting="grdpesquisa_RowDeleting" OnPageIndexChanging="grdpesquisa_PageIndexChanging" DataKeyNames="idprova, Nome, Alias, Tipo">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelecionado" OnCheckedChanged="chkSelecionado_CheckedChanged" runat="server" AutoPostBack="True" Checked='<%# Convert.ToBoolean((Eval("selecionado").ToString()==""?"false":Eval("selecionado").ToString())) %>' Enabled='<%# !(Convert.ToBoolean((Eval("selecionado").ToString()==""?"false":Eval("selecionado").ToString())) && (Convert.ToBoolean((Eval("status").ToString()=="3")) || Convert.ToBoolean((Eval("status").ToString()=="2"))) ) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField Visible="True" DataField="nome" HeaderText="Nome"></asp:BoundField>
                                        <asp:BoundField Visible="True" DataField="alias" HeaderText="Alias"></asp:BoundField>
                                        <asp:BoundField Visible="True" DataField="tipo" HeaderText="Tipo de Prova"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Dt Prova">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDTProva" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" Text='<%# Eval("dtprova") %>' Enabled='<%# Convert.ToBoolean((Eval("selecionado").ToString()==""?"false":Eval("selecionado").ToString())) && (Convert.ToBoolean((Eval("status").ToString()!="3")) && Convert.ToBoolean((Eval("status").ToString()!="2"))) %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resultado">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="True" SelectedValue='<%# Eval("status") %>' Enabled='<%# Convert.ToBoolean((Eval("selecionado").ToString()==""?"false":Eval("selecionado").ToString())) && (Convert.ToBoolean((Eval("status").ToString()!="3")) && Convert.ToBoolean((Eval("status").ToString()!="2"))) %>'>
                                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                                    <asp:ListItem Text="Aprovado" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Reprovado" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Ausência" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnExcluir" CssClass="icon-trash editarexcluir" runat="server" CommandName="cmdExcluir" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <PagerStyle CssClass="paginacao" />
                                    <PagerSettings Position="Bottom" Mode="Numeric" />
                                </asp:GridView>
                            </div>
                            <div>
                                <asp:LinkButton ID="btnPreencherProva" CssClass="btn btn-default pull-right" runat="server" OnClick="btnPreencherProva_Click" Visible="false">
                                    <i class="icon-pencil"></i>
                                    Preencher Novamente a(s) Prova(s)
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12">
                <div runat="server" id="Div2">
                    <div role="button" onclick="javascript:ForceToggle('<%=divanexo.ClientID%>','<%=iconeanexo.ClientID%>','<%=txtanexovisivel.ClientID%>')">
                        </i><h4><span class="text-bold"><i class="icon-plus" id="iconeanexo" runat="server"></i>ANEXO</h4>
                        <asp:HiddenField runat="server" Value="0" ID="txtanexovisivel"></asp:HiddenField>
                    </div>
                </div>

                <div runat="server" id="divanexo" style="display: none;">
                    <div class="col-sm-12">
                        <asp:HiddenField runat="server" ID="hdnpossuianexo" />
                        <div class="col-sm-6 form-group" runat="server" id="divanexarlinkcertificado">
                            <a id="lnkanexararquivo" data-toggle="modal" data-target=".bd-example-modal-lg">Anexar Certificado</a>
                        </div>
                        <div class="col-sm-6 form-group" runat="server" id="divanexolink" style="display: none;">
                            <a id="lnkdownloadarquivo" data-toggle="modal" data-target=".bg-infos" onclick="
                                var iframe = document.getElementById('iframeDownload');iframe.src = 'DownloadCertificado.aspx';setTimeout(function () {
                                fecharModelDownload();}, 2500);">Download Certificado</a>
                            <br />
                            <br />
                            <a id="lnkvisualizararquivo" href="VisualizarCertificado.aspx" target="_blank">Visualizar Certificado</a>
                            <br />
                            <br />
                            <a id="lnkalteraranexararquivo" data-toggle="modal" data-target=".bd-example-modal-lg">Alterar Certificado</a>
                        </div>
                    </div>
                </div>
            </div>

            <hr>
            <div class="col-sm-12">
                <div runat="server" id="Div3">
                    <div role="button" onclick="javascript:ForceToggle('<%=divAprovacao.ClientID%>','<%=iconeAprovacao.ClientID%>','<%=txtAprovacaoVisivel.ClientID%>')">
                        </i><h4><span class="text-bold"><i class="icon-plus" id="iconeAprovacao" runat="server"></i>Dados da Aprovação/Reprovação</h4>
                        <asp:HiddenField runat="server" Value="0" ID="txtAprovacaoVisivel"></asp:HiddenField>
                    </div>
                </div>
                <div runat="server" id="divAprovacao" style="display: none;">
                    <div id="divmotivo" runat="server" visible="false">
                        <div class="col-sm-12">
                            <label for="type-db">Detalhes da Aprovação/Reprovação</label>
                            <asp:TextBox ID="txtmotivo" CssClass="form-control form-group" runat="server" MaxLength="300" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <hr />
                <div id="divPendenteAprovacao" runat="server" visible="false">
                    <div class="col-sm-12 text-left alert alert-danger">
                        <asp:Label ID="lblRegistrosPendentesAprovacao" Text="Todas as provas devem ser concluídas antes de enviar para aprovação!" runat="server" Visible="false" />
                    </div>

                </div>

                <%-- <div class="row">
                    <div class="col-sm-12 text-left btn-align-bottom nao-se-aplica MainContent_chkAceite">

                        <asp:CheckBox ID="chkAceite" runat="server" AutoPostBack="true" Visible="false" />
                        <asp:Label ID="lblAceite" Text="Li e concordo que ao cadastrar uma certificação na Matriz do conhecimento, afirmo que todas as informações apresentadas são verídicas e de minha responsabilidade" runat="server" Visible="false" />
                    </div>
                </div>--%>
                <hr>

                <asp:LinkButton ID="lnkvoltar" CssClass="btn btn-default" runat="server" OnClick="lnkvoltar_Click">
                                    <i class="icon-ccw"></i>
                                    Voltar
                </asp:LinkButton>



                <asp:LinkButton ID="lnksalvar" CssClass="btn btn-primary" runat="server" OnClick="lnksalvar_Click">
                                    <i class="icon-floppy"></i>
                                    Salvar
                </asp:LinkButton>
                <asp:LinkButton ID="lnkEnviarAprovacao" CssClass="btn btn-info" runat="server" OnClick="lnkEnviarAprovacao_Click">
                                    <i class="icon-paper-plane-empty"></i>
                                    Enviar Aprovação
                </asp:LinkButton>
                <asp:LinkButton ID="lnkAprovar" CssClass="btn btn-info" runat="server" OnClick="lnkAprovar_Click" Visible="false">
                                    <i class="icon-check"></i>
                                    Aprovar
                </asp:LinkButton>
                <asp:LinkButton ID="lnkReprovar" CssClass="btn btn-info" runat="server" OnClick="lnkReprovar_Click" Visible="false">
                                    <i class="icon-cancel"></i>
                                    Reprovar
                </asp:LinkButton>




            </div>
            <script type="text/javascript">

                function calendario() {
                    $(document).ready(function () {
                        if ($('.js-container-datepicker').length > 0) $.datepicker.regional['pt-BR'] = {
                            closeText: 'Fechar',
                            prevText: '&#x3c;Anterior',
                            nextText: 'Pr&oacute;ximo&#x3e;',
                            currentText: 'Hoje',
                            monthNames: ['Janeiro', 'Fevereiro', 'Mar&ccedil;o', 'Abril', 'Maio', 'Junho',
                                'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun',
                                'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                            dayNames: ['Domingo', 'Segunda-feira', 'Ter&ccedil;a-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sabado'],
                            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                            dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
                            weekHeader: 'Sm',
                            dateFormat: 'dd/mm/yy',
                            firstDay: 0,
                            isRTL: false,
                            showMonthAfterYear: false,
                            yearSuffix: ''
                        };
                        $.datepicker.setDefaults($.datepicker.regional['pt-BR']);

                        $('.js-is-datepicker').datepicker({

                            buttonText: "",
                            defaultDate: "+1w",
                            changeMonth: true,
                            numberOfMonths: 1,
                            maxDate: "0d",
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);
                                }, 0);
                            },
                            onClose: function (selectedDate) {
                                $("#to").datepicker("option", "minDate", "monthNames", selectedDate);
                            }
                        });
                        $('.js-is-datepicker-validade').datepicker({

                            buttonText: "",
                            defaultDate: "+1w",
                            changeMonth: true,
                            numberOfMonths: 1,
                            //maxDate: "0d",
                            beforeShow: function () {
                                setTimeout(function () {
                                    $('.ui-datepicker').css('z-index', 99999999999999);
                                }, 0);
                            },
                            onClose: function (selectedDate) {
                                $("#to").datepicker("option", "minDate", "monthNames", selectedDate);
                            }
                        });
                        $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
                        $('.datepicker').datepicker();
                    });
                }
            </script>
            <script type="text/javascript">
                function comboautocomplete() {
                    $(document).ready(function () {
                        $('.combobox').combobox();
                    });
                }
            </script>
            </span></span></span>


            		<div id="modal" class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-body" style="height: 350px">
                                    <iframe style="border-width: 0px" src="AnexarCertificado.aspx" width="100%" height="100%"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>

            <div id="modalDonwload" class="modal fade bg-infos" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-body">
                            <h4>Efetuando o download. Aguarde.</h4>
                            <iframe id="iframeDownload" style="visibility: hidden"></iframe>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../media/js/lib/jquery-1.12.0.min.js"></script>
    <script type="text/javascript" src="../media/js/lib/bootstrap.min.js"></script>
    <script type="text/javascript" src="../media/js/lib/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../media/js/lib/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../media/js/lib/jquery.validate.custom.js"></script>
    <script type="text/javascript" src="../media/js/lib/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../media/js/cadastro.js"></script>
</asp:Content>
