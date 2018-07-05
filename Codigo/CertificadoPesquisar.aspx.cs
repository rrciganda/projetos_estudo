using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.Data;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class CertificadoPesquisar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                   !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                   !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                   !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                   )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                CarregarCombo();
                CarregarComboFornecedor();
                if (
                 !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroCertificacaoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                  )
                {
                    lnkNovo.Visible = false;
                }
            }
        }

        protected void lnkPesquisar_Click(object sender, EventArgs e)
        {
            diverro.Visible = false;
            grdpesquisa.PageIndex = 0;
            CarregarGrid();
        }

        protected void lnkNovo_Click(object sender, EventArgs e)
        {
            Session["eoCertificado"] = null;
            Response.Redirect("CertificadoOperacao.aspx");
        }

        private void CarregarGrid()
        {
            try
            {
                CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                CertificadoEntity eo = new CertificadoEntity();

                if (ddlGrupo.SelectedValue.ToString().Trim() != "")
                    eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue.ToString());
                if (ddlfornecedor.SelectedValue.ToString().Trim() != "")
                    eo.idfornecedor = Convert.ToInt32(ddlfornecedor.SelectedValue.ToString());
                if (txtNomeCertificado.Text.Trim() != "")
                    eo.nome = txtNomeCertificado.Text;
                if (ddlStatus.SelectedValue.Trim() != "")
                    eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                if (txtVersao.Text.Trim() != "")
                    eo.versao = txtVersao.Text;

                DataTable lista = bl.Consultar(eo);

                if (lista.Rows.Count == 0)
                {
                    divInfo.Visible = true;
                    lblmsInfo.Text = "Não existe registro para filtro informado!";
                }
                else
                {
                    lista.DefaultView.Sort = "nome";
                    lista = lista.DefaultView.ToTable();
                    divInfo.Visible = false;
                }

                grdpesquisa.DataSource = lista;
                grdpesquisa.DataBind();
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[5].Visible = false;
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[6].Visible = false;
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
                if (e.CommandName == "cmdAlterar" || e.CommandName == "cmdExcluir")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdpesquisa.DataKeys[index];
                    if (e.CommandName == "cmdAlterar")
                    {
                        Session["eoCertificado"] = data.Values["idcertificado"].ToString();
                        Response.Redirect("CertificadoOperacao.aspx");
                    }
                    else if (e.CommandName == "cmdExcluir")
                    {
                        CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                        CertificadoEntity eo = new CertificadoEntity();
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idcertificado = Convert.ToInt32(data.Values["idcertificado"].ToString());
                        bl.Excluir(eo);
                        CarregarGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void grdpesquisa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    LinkButton btn = e.Row.Cells[grdpesquisa.Columns.Count - 1].Controls[0] as LinkButton;
                    btn.OnClientClick = "if (confirm('Confirma a exclusão desse registro?') == false) return false;";
                }
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

        private void CarregarCombo()
        {
            ddlGrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            ddlGrupo.DataSource = blGrupo.Consultar(eoGrupo);
            ddlGrupo.DataTextField = "nome";
            ddlGrupo.DataValueField = "idgrupo";
            ddlGrupo.DataBind();
            if (ddlGrupo.Items.Count > 0)
                ddlGrupo.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlGrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        private void CarregarComboFornecedor() {
            ddlfornecedor.Items.Clear();
            FornecedorBusinessLayer blFornecedor = new FornecedorBusinessLayer();
            FornecedorEntity eoFornecedor = new FornecedorEntity();
            ddlfornecedor.DataSource = blFornecedor.Consultar(eoFornecedor);
            ddlfornecedor.DataTextField = "nome";
            ddlfornecedor.DataValueField = "idfornecedor";
            ddlfornecedor.DataBind();
            if (ddlfornecedor.Items.Count > 0)
                ddlfornecedor.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlfornecedor.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }
    }
}