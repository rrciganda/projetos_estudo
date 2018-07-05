using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Entity;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Session["eoUs"] == null || Session["eoFuncs"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                lnkLogin.InnerText = ((UsuarioEntity)(Session["eoUs"])).nome;


                //Menu certificacoes

                mnminhascertificacoes.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.PesquisarMinhascertificacoes, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                mncertificacoesaprovar.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAprovarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                mncertificacoescolaborador.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAlterarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoExcluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoIncluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoPesquisarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );
                mnCertificacoes.Visible = (mncertificacoescolaborador.Visible || mnminhascertificacoes.Visible);

                //Menu cadastro
                //mnGrupoCadastro.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroGrupoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                //mnReguladorCadastro.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroReguladorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                //mnCertificacoesCadastro.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroCertificacaoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                //mnCadastroCertificacao.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoIncluircertificacoesdocolaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                //mnCadastros.Visible = (mnGrupoCadastro.Visible || mnReguladorCadastro.Visible || mnCertificacoesCadastro.Visible || mnCadastroCertificacao.Visible);

                //Menu alteracao
                mnFornecedorPesquisar.Visible = (
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoFornecedorPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoFornecedorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoFornecedorExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroFornecedorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                   );
                mnGrupoPesquisar.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroGrupoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );
                mnReguladorPesquisar.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroReguladorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );
                mnCertificacoesPesquisar.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroCertificacaoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );

                mnprova.Visible = (
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                   ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroProvaIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                   );


                mnAlteracoes.Visible = (mnReguladorPesquisar.Visible || mnprova.Visible || mnGrupoPesquisar.Visible || mnCertificacoesPesquisar.Visible || mnFornecedorPesquisar.Visible);

                //Menu relatorios
                mnRelDashbard.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.RelatriosDashboard, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));

                mnRelCertificado.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.RelatriosCertificacoes, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                mnRelatorios.Visible = (mnRelDashbard.Visible || mnRelCertificado.Visible);

                //Menu Segurança
                mnUsuario.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosColaboradorPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosColaboradorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosColaboradorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosColaboradorExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );

                mnPerfil.Visible = (
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]) ||
                    ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])
                    );
                mnLogs.Visible = (ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosLogsPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]));
                mnSeguranca.Visible = (mnUsuario.Visible || mnPerfil.Visible || mnLogs.Visible);

            }

        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();

            Response.Redirect("Login.aspx");
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }

}