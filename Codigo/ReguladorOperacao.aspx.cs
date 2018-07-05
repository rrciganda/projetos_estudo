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
    public partial class ReguladorOperacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtObservacao.Attributes.Add("maxlength", txtObservacao.MaxLength.ToString());
                if (
                   (Session["eoRegulador"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroReguladorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                   (Session["eoRegulador"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                   )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                if (Session["eoRegulador"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoRegulador");
                }
                //else
                //{
                //    if (
                //  !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                //  !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                //  !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                //  )
                //    {
                //        lnkvoltar.Visible = false;
                //    }
                //    else {
                //        lnkvoltar.Text = "<i class=\"icon-search\"></i> Ir para pesquisa";
                //    }
                //}
            }
        }

        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoRegulador");
            Response.Redirect("ReguladorPesquisar.aspx");
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {
                 diverro.Visible = false;
                lblmsgerro.Text = "";

                bool valido = true;
                if (txtRegulador.Text == "")
                {
                    valido = false;
                    diverro.Visible = true;
                    lblmsgerro.Text = "Nome do Regulador obrigatório;";
                }

                if (valido)
                {
                    ReguladorBusinessLayer bl = new ReguladorBusinessLayer();
                    ReguladorEntity eo = new ReguladorEntity();
                    if (txtRegulador.Text.Trim() != "")
                        eo.nome = txtRegulador.Text;
                    if (ddlStatus.SelectedValue != "")
                        eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (txtObservacao.Text.Trim() != "")
                        eo.observacao = txtObservacao.Text;                    
                    if (hdnId.Value != "")
                    {
                        eo.idregulador = Convert.ToInt32(hdnId.Value);
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        bl.Alterar(eo);
                        Response.Redirect("ReguladorPesquisar.aspx");
                    }
                    else
                    {
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idregulador = bl.Incluir(eo);
                        divsucesso.Visible = true;
                        lblsucesso.Text = "Inclusão realizada com sucesso!";
                        txtRegulador.Text = "";
                        txtObservacao.Text = "";
                        ddlStatus.SelectedValue = EOConst.CodStatus.Ativo.ToString();
                        hdnId.Value = "";
                    }
                    
                    
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
                ReguladorBusinessLayer bl = new ReguladorBusinessLayer();
                ReguladorEntity eo = bl.Obter(Convert.ToInt32(Session["eoRegulador"]));
                hdnId.Value = eo.idregulador.ToString();
                txtRegulador.Text = eo.nome.ToString();
                txtObservacao.Text = eo.observacao.ToString();
                ddlStatus.SelectedValue = eo.status.ToString();
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }
    }
}