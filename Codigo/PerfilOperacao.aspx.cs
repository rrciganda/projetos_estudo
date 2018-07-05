using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class PerfilOperacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                  (Session["eoPerfil"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                  (Session["eoPerfil"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                  )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                CarregarFuncionalidade();
                if (Session["eoPerfil"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoPerfil");
                }

               if(Session["PerfilAdm"].ToString() == "1")
                {
                    lnksalvar.Visible = false;
                    lnkDesmarcarTodas.Visible = false;
                    lnkTodas.Visible = false;
                }
            }
        }

        private void CarregarFuncionalidade()
        {
            try
            {
                if (Session["PerfilAdm"].ToString() == "1")
                {
                    chkFuncionalidades.Items.Clear();
                    FuncionalidadeBusinessLayer blFunc = new FuncionalidadeBusinessLayer();
                    FuncionalidadeEntity eoFunc = new FuncionalidadeEntity();
                    eoFunc.status = 1;
                    chkFuncionalidades.DataSource = blFunc.ConsultarList(eoFunc);
                    chkFuncionalidades.DataTextField = "nome";
                    chkFuncionalidades.DataValueField = "idfuncionalidade";
                    chkFuncionalidades.Enabled = false;
                    chkFuncionalidades.DataBind();
                }else
                {
                    chkFuncionalidades.Items.Clear();
                    FuncionalidadeBusinessLayer blFunc = new FuncionalidadeBusinessLayer();
                    FuncionalidadeEntity eoFunc = new FuncionalidadeEntity();
                    eoFunc.status = 1;
                    chkFuncionalidades.DataSource = blFunc.ConsultarList(eoFunc);
                    chkFuncionalidades.DataTextField = "nome";
                    chkFuncionalidades.DataValueField = "idfuncionalidade";
                    chkFuncionalidades.DataBind();
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Erro ao carregar dados: " + ex.Message;

            }

        }


        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoPerfil");
            Response.Redirect("PerfilPesquisar.aspx");
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {
                diverro.Visible = false;
                lblmsgerro.Text = "";

                bool valido = true;
                if (txtnome.Text == "")
                {
                    valido = false;
                    diverro.Visible = true;
                    lblmsgerro.Text = "Nome do Perfil obrigatório;";
                }
                if (chkFuncionalidades.SelectedValue == "")
                {
                    valido = false;
                    diverro.Visible = true;
                    lblmsgerro.Text = lblmsgerro.Text + "Selecione ao menos uma funcionalidade para o perfil;";
                }
                if (valido)
                {
                    PerfilBusinessLayer bl = new PerfilBusinessLayer();
                    PerfilEntity eo = new PerfilEntity();
                    if (txtnome.Text.Trim() != "")
                        eo.nome = txtnome.Text;
                    if (rdostatus.SelectedValue != "")
                        eo.status = Convert.ToInt32(rdostatus.SelectedValue);
                    if (hdnId.Value != "")
                    {
                        eo.idperfil = Convert.ToInt32(hdnId.Value);
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        bl.Alterar(eo);
                    }
                    else
                    {
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idperfil = bl.Incluir(eo);
                    }

                    PerfilfuncionalidadeBusinessLayer blperfilfunc = new PerfilfuncionalidadeBusinessLayer();
                    PerfilfuncionalidadeEntity eoperfilfunc = new PerfilfuncionalidadeEntity();
                    eoperfilfunc.idperfil = eo.idperfil;
                    eoperfilfunc.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                    blperfilfunc.ExcluirPorPerfil(eoperfilfunc);

                    foreach (ListItem item in chkFuncionalidades.Items)
                    {
                        if (item.Selected)
                        {
                            eoperfilfunc = new PerfilfuncionalidadeEntity();
                            eoperfilfunc.idperfil = eo.idperfil;
                            eoperfilfunc.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                            eoperfilfunc.idfuncionalidade = Convert.ToInt32(item.Value);
                            blperfilfunc.Incluir(eoperfilfunc);
                        }
                    }

                    divsucesso.Visible = true;
                    lblsucesso.Text = "Operação realizada com sucesso!";
                    Response.Redirect("PerfilPesquisar.aspx");
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }
        private void CarregarDados()
        {
            try
            {
                PerfilBusinessLayer bl = new PerfilBusinessLayer();
                PerfilEntity eo = bl.Obter(Convert.ToInt32(Session["eoPerfil"]));
                hdnId.Value = eo.idperfil.ToString();
                txtnome.Text = eo.nome.ToString();
                rdostatus.SelectedValue = eo.status.ToString();

                PerfilfuncionalidadeBusinessLayer blperfilfunc = new PerfilfuncionalidadeBusinessLayer();
                PerfilfuncionalidadeEntity eoperfilfunc = new PerfilfuncionalidadeEntity();
                eoperfilfunc.idperfil = eo.idperfil;
                List<PerfilfuncionalidadeEntity> lstperfilFunc = blperfilfunc.ConsultarList(eoperfilfunc);

                foreach (PerfilfuncionalidadeEntity item in lstperfilFunc)
                {
                    chkFuncionalidades.Items.FindByValue(item.idfuncionalidade.ToString()).Selected = true;
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void lnkDesmarcarTodas_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < chkFuncionalidades.Items.Count; i++)
            {
                chkFuncionalidades.Items[i].Selected = false;
            }
        }

        protected void lnkTodas_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < chkFuncionalidades.Items.Count; i++)
            {
                chkFuncionalidades.Items[i].Selected = true;
            }
        }
    }
}