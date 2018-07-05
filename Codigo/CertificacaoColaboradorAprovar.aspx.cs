using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class CertificacaoColaboradorAprovar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAprovarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                grdpesquisa.PageIndex = 0;
                CarregarGrid();
            }
        }
        private void CarregarGrid()
        {
            try
            {
                CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();
                CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                eo.aprovacao = EOConst.CodStatusAprovacao.Pendente;

                bool valido = true;

                if (valido)
                {
                    //DataTable lista = blcertuser.Consultar(idgrupo, idcertificado, idregulador, idusuario, status);
                    //DataTable lista = blcertuser.ConsultarPendentesAprovacao(eo);
                    DataTable lista = blcertuser.ConsultarPendentesTelaAprovacao(eo);
                    
                    if (lista.Rows.Count == 0)
                    {
                        divInfo.Visible = true;
                        lblmsInfo.Text = "Não existe registro para filtro informado!";
                    }
                    else
                    {
                        divInfo.Visible = false;
                    }

                    grdpesquisa.DataSource = lista;
                    grdpesquisa.DataBind();

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void grdpesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            diverro.Visible = false;
            try
            {
                if (e.CommandName == "cmdVisualizar" || e.CommandName == "cmdAprovar" || e.CommandName == "cmdReprovar" || e.CommandName == "cmdAlterar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdpesquisa.DataKeys[index];
                    //if (e.CommandName == "cmdVisualizar")
                    //{
                    //    CertficadoDigitalizadoBusinessLayer bl = new CertficadoDigitalizadoBusinessLayer();
                    //    CertficadoDigitalizadoEntity eo = new CertficadoDigitalizadoEntity();
                    //    eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                    //    DataTable dt = new DataTable();
                    //    dt = bl.ConsultarArquivo(eo);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        Session["ArquivoDigitalizado"] = (byte[])dt.Rows[0]["arquivodigitalizado"];
                    //        Session["NomeArquivoDigitalizado"] = dt.Rows[0]["arquivo"].ToString();
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "visualizar", "window.open('VisualizarCertificado.aspx');", true);
                    //    }
                    //    else
                    //    {
                    //        diverro.Visible = true;
                    //        lblmsgerro.Text = "Mensagem do Sistema: Não há arquivo a ser exibido para esse registro";
                    //    }
                    //}
                    if (e.CommandName == "cmdAlterar")
                    {
                        Session.Remove("Origem");
                        Session["Aprovacao"] = "Aprovacao";
                        Session["eoMinhaCertificacaousuario"] = data.Values["idcertificacao"].ToString();
                        Response.Redirect("CertificacaoColaboradorOperacao.aspx");
                    }

                    //else if (e.CommandName == "cmdAprovar")
                    //{
                    //    CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                    //    CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                    //    eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                    //    eo.aprovacao = EOConst.CodStatusAprovacao.Aprovado;
                    //    bl.AlterarStatusAprovacao(eo);
                    //    CarregarGrid();
                    //}
                    //else if (e.CommandName == "cmdReprovar")
                    //{
                    //    CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                    //    CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                    //    eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                    //    eo.aprovacao = EOConst.CodStatusAprovacao.Reprovado;
                    //    bl.AlterarStatusAprovacao(eo);
                    //    CarregarGrid();
                    //}
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void grdpesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdpesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}