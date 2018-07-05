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
    public partial class GrupoPesquisar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) 
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }

                if (
                  !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroGrupoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                   )
                {
                    lnkNovo.Visible = false;
                }
                ddlStatus.SelectedValue = EOConst.CodStatus.Ativo.ToString();
                CarregarGrid();
                
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
            Session["eoGrupo"] = null;
            Response.Redirect("GrupoOperacao.aspx");
        }

        private void CarregarGrid()
        {
            try
            {
                GrupoBusinessLayer bl = new GrupoBusinessLayer();
                GrupoEntity eo = new GrupoEntity();
                if (txtNome.Text.Trim() != "")
                    eo.nome = txtNome.Text;                
                if (ddlStatus.SelectedValue != "")
                    eo.status = Convert.ToInt32(ddlStatus.SelectedValue);

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

                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[2].Visible = false;
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[3].Visible = false;
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
                        Session["eoGrupo"] = data.Values["idgrupo"].ToString();
                        Response.Redirect("GrupoOperacao.aspx");
                    }
                    else if (e.CommandName == "cmdExcluir")
                    {
                        GrupoBusinessLayer bl = new GrupoBusinessLayer();
                        GrupoEntity eo = new GrupoEntity();
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idgrupo = Convert.ToInt32(data.Values["idgrupo"].ToString());
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
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
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