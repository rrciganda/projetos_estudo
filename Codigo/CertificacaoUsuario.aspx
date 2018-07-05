<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificacaoUsuario.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.CertificacaoUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>

            <%--<script src="Scripts/bootstrap-combobox-master/js/bootstrap-combobox.js"></script>--%>
            <div class="col-sm-12 form-group">
                <div class="text-uppercase">
                    <h1 class="text-bold">Certificações do Usuário</h1>
                    <span>Aqui você associa usuários à certificados</span>
                    <hr>
                </div>

            </div>
            <div class="col-sm-4 form-group">
                <asp:RadioButton ID="RadioButtonUsuario" runat="server" Checked="true" GroupName="tipotela" AutoPostBack="true" OnCheckedChanged="RadioButtonUsuario_CheckedChanged" Text="&nbsp; Informar certificações por usuário" />
            </div>
            <div class="col-sm-8 form-group">
                <asp:RadioButton ID="RadioButtonCertificacao" runat="server" Checked="false" GroupName="tipotela" AutoPostBack="true" OnCheckedChanged="RadioButtonCertificacao_CheckedChanged" Text="&nbsp; Informar usuarios por certificação" />
            </div>
            <div class="col-sm-12 form-group">
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
            <div runat="server" class="row" id="DivTelaUsuario" visible="true">

                <div class="col-sm-12">

                    <div class="col-sm-8 form-group">
                        <label for="type-db">Colaborador</label>
                        <asp:DropDownList runat="server" ID="ddlColaborador" CssClass="combobox form-control" Width="100%" DataTextField="dscombo" DataValueField="idusuario"></asp:DropDownList>

                    </div>

                </div>


                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <hr>
                        <label class="control-label" for="filterBase">Certificações</label>
                        <div class="table table-responsive">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdcertificacao" CssClass="table table-striped table-responsive" DataKeyNames="id"
                                    runat="server" AutoGenerateColumns="false" OnRowCommand="grdcertificacao_RowCommand"
                                    OnRowDataBound="grdcertificacao_RowDataBound"
                                    GridLines="Horizontal" BorderStyle="none" CellSpacing="-1">
                                    <Columns>
                                        <asp:BoundField HeaderText="#" DataField="id" ItemStyle-Width="30px" />
                                        <asp:TemplateField HeaderText="Certificação" ItemStyle-Width="360px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlgridcertificacao" runat="server" DataValueField="idcertificado" CssClass="form-control" DataTextField="nome" Width="100%"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hdngrididcertificacao" Value='<%# DataBinder.Eval (Container.DataItem, "idcertificacao") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Regulador" ItemStyle-Width="240px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlgridregulador" runat="server" DataValueField="idregulador" CssClass="form-control" DataTextField="nome" Width="100%"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hdngrididregulador" Value='<%# DataBinder.Eval (Container.DataItem, "idregulador") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Certificação" ItemStyle-Width="140px">
                                            <ItemTemplate>
                                                <div class='js-container-datepicker'>
                                                   
                                                        <asp:TextBox ID="txtgriddtcertificacao" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" Text='<%# DataBinder.Eval (Container.DataItem, "dtcertificacao") %>'></asp:TextBox>
                                                   
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Validade" ItemStyle-Width="140px">
                                            <ItemTemplate>
                                                <div class='js-container-datepicker'>
                                                        <asp:TextBox ID="txtgriddtvalidade" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" Text='<%# DataBinder.Eval (Container.DataItem, "dtvalidade") %>'></asp:TextBox>

                                                </div>
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
                            <div class="col-sm-12 text-right">
                                <asp:LinkButton ID="lnkAdicionarCertificacao" CssClass="btn btn-primary" runat="server" OnClick="lnkAdicionarCertificacao_Click">
                            <i class="icon-plus"></i>Adicionar Certificação
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" class="row" id="DivTelaCertificacao" visible="false">

                <div class="col-sm-12">
                    <div class="col-sm-6 form-group">
                        <label for="type-db">Certificação</label>
                        <asp:DropDownList ID="ddlcertificacao" CssClass="form-control" runat="server" DataValueField="idcertificado" DataTextField="nome" Width="100%"></asp:DropDownList>
                    </div>

                </div>

                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <hr>
                        <label class="control-label" for="filterBase">Colaboradores</label>
                        <div class="table table-responsive">
                            <div class="col-sm-12">
                                <asp:GridView ID="grdusuario" CssClass="table table-striped table-responsive" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" PageSize="10"
                                    DataKeyNames="id" GridLines="Horizontal" BorderStyle="none" CellSpacing="-1" OnRowCommand="grdusuario_RowCommand" OnRowDataBound="grdusuario_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="#" DataField="id" ItemStyle-Width="30px" />
                                        <asp:TemplateField HeaderText="Colaborador" ItemStyle-Width="360px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlgridcolaborador" runat="server" CssClass="combobox form-control" DataValueField="idusuario" DataTextField="dscombo" Width="100%"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hdngrididusuario" Value='<%# DataBinder.Eval (Container.DataItem, "idusuario") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Regulador" ItemStyle-Width="240px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlgridregulador" runat="server" CssClass="form-control" DataValueField="idregulador" DataTextField="nome" Width="100%"></asp:DropDownList>
                                                <asp:HiddenField runat="server" ID="hdngrididregulador" Value='<%# DataBinder.Eval (Container.DataItem, "idregulador") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Certificação" ItemStyle-Width="140px">
                                            <ItemTemplate>
                                                <div class='js-container-datepicker'>
                                                        <asp:TextBox ID="txtgriddtcertificacao" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" Text='<%# DataBinder.Eval (Container.DataItem, "dtcertificacao") %>'></asp:TextBox>
                                                    
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Validade" ItemStyle-Width="140px">
                                            <ItemTemplate>
                                                <div class='js-container-datepicker'>
                                                    
                                                        <asp:TextBox ID="txtgriddtvalidade" CssClass="form-control js-is-datepicker" placeholder="DD/MM/AAAA" runat="server" MaxLength="10" Text='<%# DataBinder.Eval (Container.DataItem, "dtvalidade") %>'></asp:TextBox>
                                                    
                                                </div>
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
                            <div class="col-sm-12 text-right">
                                <asp:LinkButton ID="lnkAdicionarUsuario" CssClass="btn btn-primary" runat="server" OnClick="lnkAdicionarUsuario_Click">
                            <i class="icon-plus"></i>Adicionar Usuario
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-sm-12 text-right">
                <hr>
                <asp:LinkButton ID="lnkSalvar" CssClass="btn btn-primary" runat="server" OnClick="lnkSalvar_Click">
                            <i class="icon-floppy"></i>Salvar
                </asp:LinkButton>
            </div>
            <script type="text/javascript">
                //$(document).ready(function () {
                //    alert('a');
                //    $('.combobox').combobox();
                //});
            </script>
            <script type="text/javascript">

                function comboautocomplete() {
                    $(document).ready(function () {
                        $('.combobox').combobox();
                    });
                }
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
                            onClose: function (selectedDate) {
                                $("#to").datepicker("option", "minDate", "monthNames", selectedDate);
                            }
                        });
                        $.datepicker.setDefaults($.datepicker.regional['pt-BR']);
                        $('.datepicker').datepicker();
                    });
                    
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

