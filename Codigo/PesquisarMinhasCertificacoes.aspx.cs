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
    public partial class PesquisarMinhasCertificacoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.PesquisarMinhascertificacoes, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                grdpesquisa.PageIndex = 0;
                CarregarGrid();
                grdpesquisa.Columns[10].Visible = false;
                grdpesquisa.Columns[11].Visible = false;
            }
        }


        private void CarregarGrid()
        {
            try
            {
                CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();

                int idusuario = int.MinValue;
                int idgrupo = int.MinValue;
                int idfornecedor = int.MinValue;
                int idcertificado = int.MinValue;
                int idregulador = int.MinValue;
                int status = int.MinValue;
                int certificadoexpirado = int.MinValue;
                string departamento = "";
                int aprovacao = int.MinValue;
                DateTime dtinicertificacao = DateTime.MinValue;
                DateTime dtfimcertificacao = DateTime.MinValue;
                DateTime dtinivalidade = DateTime.MinValue;
                DateTime dtfimvalidade = DateTime.MinValue;
                bool valido = true;

                idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;
                if (valido)
                {
                    //DataTable lista = blcertuser.Consultar(idgrupo, idcertificado, idregulador, idusuario, status);
                   // DataTable lista = blcertuser.ConsultarMinhasCertificacoes(idusuario, idgrupo, idfornecedor, idcertificado, idregulador, status, certificadoexpirado, departamento, dtinicertificacao, dtfimcertificacao, dtinivalidade, dtfimvalidade, aprovacao);
                    DataTable lista = blcertuser.ConsultarMinhasCertificacoesLista(idusuario, idgrupo, idfornecedor, idcertificado, idregulador, status, certificadoexpirado, departamento, dtinicertificacao, dtfimcertificacao, dtinivalidade, dtfimvalidade, aprovacao);
                    
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
            try
            {
                if (e.CommandName == "cmdAlterar" || e.CommandName == "cmdExcluir" || e.CommandName == "cmdAbrir")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdpesquisa.DataKeys[index];
                    if (e.CommandName == "cmdAlterar")
                    {
                        Session["eoMinhaCertificacaousuario"] = data.Values["idcertificacao"].ToString();
                        Session["Origem"] = "MinhasCertificacoes";
                        Response.Redirect("CertificacaoColaboradorOperacao.aspx");
                    }
                    //else if (e.CommandName == "cmdExcluir")
                    //{
                    //    CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                    //    CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                    //    eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                    //    bl.Excluir(eo);
                    //    CarregarGrid();
                    //}
                    //else 
                    else if (e.CommandName == "cmdAbrir")
                    {
                        CertficadoDigitalizadoBusinessLayer bl = new CertficadoDigitalizadoBusinessLayer();
                        CertficadoDigitalizadoEntity eo = new CertficadoDigitalizadoEntity();
                        eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                        DataTable dt = new DataTable();
                        dt = bl.ConsultarArquivo(eo);
                        if (dt.Rows.Count > 0)
                        {
                            DownloadPDF(dt);
                        }
                        else
                        {
                            diverro.Visible = true;
                            lblmsgerro.Text = "Mensagem do Sistema: Não há arquivo a ser exibido para esse registro";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void DownloadPDF(DataTable dt)
        {
            byte[] dbbyte;
            dbbyte = (byte[])dt.Rows[0]["arquivodigitalizado"];
            string nomeArquivo = dt.Rows[0]["arquivo"].ToString();
            Response.Clear();
            MemoryStream ms = new MemoryStream(dbbyte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }
        protected void grdpesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[10].Text;
                string aprovacao = e.Row.Cells[11].Text;

                if(status == "1" && aprovacao == "3")
                {
                    if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroCertificacaoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    {
                        e.Row.Cells[5].Text = Entity.EOUtil.RetornarStatusAprovacaoCertificacaoPesquisa(0, -1);
                    }
                }
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                //LinkButton btn = e.Row.Cells[grdpesquisa.Columns.Count - 2].Controls[0] as LinkButton;
                //btn.OnClientClick = "if (confirm('Confirma a exclusão desse registro?') == false) return false;";
            }
        }
        protected void grdpesquisa_PageIndexChanging(object source, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex > -1)
            {
                grdpesquisa.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
        }

        protected void lnkNovaCertificacao_Click(object sender, EventArgs e)
        {
            Session["Origem"] = "MinhasCertificacoes";
            Session["recarregarSessao"] = null;
            Response.Redirect("CertificacaoColaboradorOperacao.aspx");
        }
    }
}