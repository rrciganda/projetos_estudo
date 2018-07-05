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
    public partial class ProvaPesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) 
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }

                if (
                  !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroProvaIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                   )
                {
                    lnkNovo.Visible = false;
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
            Session["eoProva"] = null;
            Response.Redirect("ProvaOperacao.aspx");
        }

        private void CarregarGrid()
        {
            try
            {
                ProvaBusinessLayer bl = new ProvaBusinessLayer();
                ProvaEntity eo = new ProvaEntity();

                if (txtprova.Text.Trim() != "")
                    eo.nome = txtprova.Text;

                if (ddlStatus.SelectedValue != "")
                    eo.status = Convert.ToInt32(ddlStatus.SelectedValue);

                if (txtaliasprova.Text.Trim() != "")
                    eo.alias = txtaliasprova.Text;

                DataTable lista = bl.ConsultarProvas(eo);

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

                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[5].Visible = false;
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
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
                        Session["eoProva"] = data.Values["idprova"].ToString();
                        Response.Redirect("ProvaOperacao.aspx");
                    }
                    else if (e.CommandName == "cmdExcluir")
                    {
                        ProvaBusinessLayer bl = new ProvaBusinessLayer();
                        ProvaEntity eo = new ProvaEntity();
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idprova = Convert.ToInt32(data.Values["idprova"].ToString());
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
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
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

    }
}

