<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilOperacao.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.PerfilOperacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ajaxtela" runat="server">
        <ContentTemplate>
            <div class="row content">
                <div class="col-sm-12">
                    <div class="col-sm-12 form-group">
                        <div class="text-uppercase">
                            <h1 class="text-bold">Administrar Perfil</h1>
                            <span>AQUI VOCÊ GERENCIA OS GRUPOS</span>
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

                        <asp:HiddenField ID="hdnId" runat="server" />

                        <div class="row">
                            <div class="col-sm-4 form-group">
                                <label for="type-db">Nome</label>
                                <asp:TextBox ID="txtnome" CssClass="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 form-group">
                                <label for="type-db">Status</label>
                                <asp:DropDownList ID="rdostatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Text="Ativo" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inativo" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <hr class="transparentHR">
                        <div class="row">
                            <div class="col-sm-12 form-group">
                                <label for="">Funcionalidades do Perfil:</label>
                            </div>
                        </div>



                        <div class="col-sm-12-functionality">
                            <div class="has-border-radius">
                                <asp:CheckBoxList ID="chkFuncionalidades" CssClass="chkFuncionalidades col-sm-3 form-group" RepeatDirection="Vertical" RepeatColumns="3" Width="100%" runat="server" Style="background-color: white;"></asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 form-group todas-funcionalidades">
                                <asp:LinkButton ID="lnkTodas" runat="server" OnClick="lnkTodas_Click">Marcar Todas / </asp:LinkButton>
                                 
                        <asp:LinkButton ID="lnkDesmarcarTodas" runat="server" OnClick="lnkDesmarcarTodas_Click">Desmarcar Todas</asp:LinkButton>
                            </div>
                        </div>
                        <hr class="transparentHR">

                        <div class="row">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
