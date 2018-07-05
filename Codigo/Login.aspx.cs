using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.DirectoryServices;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Runtime.InteropServices;
using System.DirectoryServices.Protocols;
using System.Net;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                divErro.Visible = false;
                lblMsgRetorno.Text = "";
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    divErro.Visible = true;
                    throw new Exception("O campo usuário é obrigatório!");
                }
                if (string.IsNullOrEmpty(txtSenha.Text))
                {
                    divErro.Visible = true;
                    throw new Exception("O campo senha é obrigatório!");
                }
                if (txtUsuario.Text.Length > 40)
                {
                    divErro.Visible = true;
                    throw new Exception("Login do usuário deve conter até 40 caracteres");
                }
                if (txtSenha.Text.Length > 40)
                {
                    divErro.Visible = true;
                    throw new Exception("Login do usuário deve conter até 40 caracteres");
                }
                string login = txtUsuario.Text;
                string senha = txtSenha.Text;
                string ldap = ConfigurationManager.AppSettings["LDAP"].ToString();
                //string domain = ConfigurationManager.AppSettings["DOMAIN"].ToString();

             //   bool Valido = CheckADUserCredentials(login, senha, ldap);

                //pc = new PrincipalContext(ContextType.Domain, domain);
                //bool Valido = pc.ValidateCredentials(login, senha);
                bool Valido = true;

                if (Valido)
                {
                    UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                    UsuarioEntity eo = new UsuarioEntity();

                    eo = bl.Login(txtUsuario.Text);
                    eo.Log.IdUsuario = eo.idusuario;
                    //precisa verificar se vai ter acesso a funcionalidade
                    if (eo != null && !string.IsNullOrEmpty(eo.login))
                    {
                        //Verificar se o perfil está ativo ou não no sistema
                        if (!string.IsNullOrEmpty(eo.idperfil.ToString()))
                        {
                            PerfilBusinessLayer blperfil = new PerfilBusinessLayer();
                            PerfilEntity eoperfil = blperfil.Obter(eo.idperfil);
                            if (eoperfil.status != EOConst.CodStatus.Ativo)
                            {
                                divErro.Visible = true;
                                throw new Exception("Acesso inválido, perfil inativo!");
                            }
                        }
                        else
                        {
                            divErro.Visible = true;
                            throw new Exception("Usuário não possui perfil associado!");
                        }

                        PerfilfuncionalidadeBusinessLayer blperfilfunc = new PerfilfuncionalidadeBusinessLayer();
                        PerfilfuncionalidadeEntity eoperfilfunc = new PerfilfuncionalidadeEntity();
                        eoperfilfunc.idperfil = eo.idperfil;
                        List<PerfilfuncionalidadeEntity> funcionalidades = blperfilfunc.ConsultarList(eoperfilfunc);

                        Session["eoUs"] = eo;
                        Session["eoFuncs"] = funcionalidades;
                    }

                    Response.Redirect("Home.aspx");
                }
                else
                {
                    throw new Exception("Usuário ou senha Inválidos!");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    //string contexto = String.Format(" connectedServer {0} : name {1}", pc.ConnectedServer.ToString(), pc.Name);                         
                    divErro.Visible = true;
                    lblMsgRetorno.Text = "Erro: " + ex.Message;
                }
                else
                {
                    divErro.Visible = true;
                    lblMsgRetorno.Text = ex.InnerException.Message;
                }

            }
        }

        public static Boolean CheckADUserCredentials(String accountName, String password, String domain)
        {
            Boolean result;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(domain, accountName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        String filter = String.Format("(&(objectCategory=user)(sAMAccountName={0}))", accountName);
                        searcher.Filter = filter;
                        searcher.SizeLimit = 9999999;
                        try
                        {
                            SearchResult adsSearchResult = searcher.FindOne();
                            result = true;
                        }
                        catch (DirectoryServicesCOMException)
                        {
                            throw;
                        }
                    }
                }

            }
            catch (DirectoryServicesCOMException)
            {
                throw;
            }
            catch (DirectoryException)
            {
                throw;
            }
            return result;
        }
    }
}