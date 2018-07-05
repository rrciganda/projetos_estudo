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
    public partial class PerfilPesquisar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                if (
                   !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
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
            Session["eoPerfil"] = null;
            Response.Redirect("PerfilOperacao.aspx");
        }

        private void CarregarGrid()
        {
            try
            {
                PerfilBusinessLayer bl = new PerfilBusinessLayer();
                PerfilEntity eo = new PerfilEntity();

                if (txtidperfil.Text.Trim() != "")
                    eo.idperfil = Convert.ToInt32(txtidperfil.Text);
                if (txtnome.Text.Trim() != "")
                    eo.nome = txtnome.Text;
                if (rdostatus.SelectedValue != "")
                    eo.status = Convert.ToInt32(rdostatus.SelectedValue);

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

                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    grdpesquisa.Columns[2].Visible = false;
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
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
                        Session["eoPerfil"] = data.Values["idperfil"].ToString();
                        Session["PerfilAdm"] = data.Values["idperfil"].ToString();
                        Response.Redirect("PerfilOperacao.aspx");
                    }
                    else if (e.CommandName == "cmdExcluir")
                    {
                        PerfilBusinessLayer bl = new PerfilBusinessLayer();
                        PerfilEntity eo = new PerfilEntity();
                        eo.idperfil = Convert.ToInt32(data.Values["idperfil"].ToString());

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
                int index = e.Row.RowIndex;

                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosPerfildeAcessoExcluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    if (index == 0)
                    {
                        LinkButton btn = e.Row.Cells[grdpesquisa.Columns.Count - 1].Controls[0] as LinkButton;
                        btn.OnClientClick = "alert('O perfil Administrador não pode ser excluido');return false;";

                    }else
                    {
                        LinkButton btn = e.Row.Cells[grdpesquisa.Columns.Count - 1].Controls[0] as LinkButton;
                        btn.OnClientClick = "if (confirm('Confirma a exclusão desse registro?') == false) return false;";
                    }
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