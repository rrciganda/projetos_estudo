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
    public partial class GrupoOperacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtObservacao.Attributes.Add("maxlength", txtObservacao.MaxLength.ToString());
                Session["dtfornecedorgrupo"] = null;
                if (
                    (Session["eoGrupo"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroGrupoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                    (Session["eoGrupo"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoGrupoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                if (Session["eoGrupo"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoGrupo");
                }

                CarregarComboFornecedor();
                dvfornecedores.Visible = (grdfornecedor.Rows.Count > 0);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
        }

        public void CriarDTFornecedorGrupo()
        {
            if (Session["dtfornecedorgrupo"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idfornecedor");
                dt.Columns.Add("nomefornecedor");
                dt.Columns.Add("status");
                Session["dtfornecedorgrupo"] = dt;
            }
        }

        public DataTable ObterDTGrupoFornecedor()
        {
            if (Session["dtfornecedorgrupo"] == null)
            {
                CriarDTFornecedorGrupo();

            }
            return (DataTable)Session["dtfornecedorgrupo"];
        }

        public void AdicionarLinhaFornecedor(string id, string nome, string status)
        {

            diverro.Visible = false;
            lblmsgerro.Text = "";
            if (Session["dtfornecedorgrupo"] == null)
            {
                CriarDTFornecedorGrupo();
            }
            DataTable dt = (DataTable)Session["dtfornecedorgrupo"];
            bool valido = true;
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["idfornecedor"].ToString() == id)
                    {
                        valido = false;
                        lblmsgerro.Text = "Fornecedor já associada a esse grupo!";
                    }
                }
            }
            if (!valido)
            {
                diverro.Visible = true;
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["idfornecedor"] = id;
                dr["nomefornecedor"] = nome;
                dr["status"] = status;
                dt.Rows.Add(dr);
                Session["dtfornecedorgrupo"] = dt;
                grdfornecedor.DataSource = (DataTable)Session["dtfornecedorgrupo"];
                grdfornecedor.DataBind();

                dvfornecedores.Visible = (grdfornecedor.Rows.Count > 0);
            }
        }
        public void CarregarComboFornecedor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idfornecedor");
            dt.Columns.Add("nome");
            FornecedorBusinessLayer bl = new FornecedorBusinessLayer();
            FornecedorEntity eo = new FornecedorEntity();
            eo.status = EOConst.CodStatus.Ativo;
            dt = bl.ConsultarAtivo(eo);
            ddlfornecedor.DataSource = dt;
            ddlfornecedor.DataBind();
            if (ddlfornecedor.Items.Count > 0)
            {
                ddlfornecedor.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                ddlfornecedor.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }

        private void SalvarGrupoFornecedor(int idgrupo)
        {
            GrupoFornecedorBusinessLayer bl = new GrupoFornecedorBusinessLayer();

            int linha = 0;
            //int contadorRequeridas = 0;
            //inativa todos os grupos vinculados ao fornecedor, pois os próximos passos irá ativar os registros válidos
            GrupoFornecedorEntity eo = new GrupoFornecedorEntity();
            eo.idgrupo = idgrupo;
            bl.Excluir(eo);

            //le os fornecedores vinculadas aos grupos
            foreach (DataRow item in ObterDTGrupoFornecedor().Rows)
            {
                eo = new GrupoFornecedorEntity();
                eo.idgrupo = idgrupo;
                eo.idfornecedor = Convert.ToInt32(item["idfornecedor"]);
                DataTable dt = bl.Consultar(eo);

                GridView grd = grdfornecedor;

                GridViewRow linhaGrid = ((GridViewRow)(System.Web.UI.Control)grdfornecedor.Rows[linha]);


                //se existir ativa
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    eo.idgrupo_fornecedor = Convert.ToInt32(dt.Rows[0]["idgrupo_fornecedor"]);
                    eo.status = EOConst.CodStatus.Ativo;
                    bl.Alterar(eo);

                    linha++;
                }
                else
                {
                    //senão existir, inclui
                    eo.status = EOConst.CodStatus.Ativo;
                    bl.Incluir(eo);
                    linha++;
                }
            }
        }

        private void CarregarDados()
        {
            try
            {
                GrupoBusinessLayer bl = new GrupoBusinessLayer();
                GrupoEntity eo = bl.Obter(Convert.ToInt32(Session["eoGrupo"]));
                hdnId.Value = eo.idgrupo.ToString();
                txtNome.Text = eo.nome.ToString();
                txtObservacao.Text = eo.observacao.ToString();
                ddlStatus.SelectedValue = eo.status.ToString();

                GrupoFornecedorBusinessLayer blgrupofornecedor = new GrupoFornecedorBusinessLayer();
                GrupoFornecedorEntity eogrupofornecedor = new GrupoFornecedorEntity();
                eogrupofornecedor.idgrupo = Convert.ToInt32(hdnId.Value);
                eogrupofornecedor.status = EOConst.CodStatus.Ativo;
                DataTable dt = blgrupofornecedor.Consultar(eogrupofornecedor);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        AdicionarLinhaFornecedor(item["idfornecedor"].ToString(), item["nomefornecedor"].ToString(), item["status"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }


        protected void lnkIncluir_Click(object sender, EventArgs e)
        {
            if (ddlfornecedor.SelectedValue != "")
            {
                AdicionarLinhaFornecedor(ddlfornecedor.SelectedValue, ddlfornecedor.SelectedItem.Text, "");
                ddlfornecedor.SelectedValue = "";
            }
        }

        private void RemoverLinhaDtFornecedor(string id)
        {
            DataTable dt = ObterDTGrupoFornecedor();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["idfornecedor"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }

            Session["dtgrupofornecedor"] = dt;
            grdfornecedor.DataSource = dt;
            grdfornecedor.DataBind();
            dvfornecedores.Visible = (grdfornecedor.Rows.Count > 0);
        }

        protected void grdfornecedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdfornecedor.DataKeys[index];
                    RemoverLinhaDtFornecedor(data.Values["idfornecedor"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }
        }

        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoGrupo");
            Response.Redirect("GrupoPesquisar.aspx");
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {
                diverro.Visible = false;
                lblmsgerro.Text = "";

                bool valido = true;
                if (txtNome.Text == "")
                {
                    valido = false;
                    diverro.Visible = true;
                    lblmsgerro.Text = "Nome do Grupo obrigatório;";
                }

                if (valido)
                {
                    GrupoBusinessLayer bl = new GrupoBusinessLayer();
                    GrupoEntity eo = new GrupoEntity();
                    if (txtNome.Text.Trim() != "")
                        eo.nome = txtNome.Text;
                    if (txtObservacao.Text.Trim() != "")
                        eo.observacao = txtObservacao.Text;
                    if (ddlStatus.SelectedValue != "")
                        eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (hdnId.Value != "")
                    {
                        eo.idgrupo = Convert.ToInt32(hdnId.Value);
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        bl.Alterar(eo);
                        SalvarGrupoFornecedor(eo.idgrupo);
                        Response.Redirect("GrupoPesquisar.aspx");
                    }
                    else
                    {
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idgrupo = bl.Incluir(eo);

                        SalvarGrupoFornecedor(eo.idgrupo);

                        divsucesso.Visible = true;
                        lblsucesso.Text = "Inclusão realizada com sucesso!";
                        txtNome.Text = "";
                        txtObservacao.Text = "";
                        ddlStatus.SelectedValue = EOConst.CodStatus.Ativo.ToString();
                        hdnId.Value = "";
                        Session["dtfornecedorgrupo"] = null;
                        Session["dtcertificacaoprova"] = null;

                        grdfornecedor.DataSource = null;
                        grdfornecedor.DataBind();
                        dvfornecedores.Visible = (grdfornecedor.Rows.Count > 0);
                    }

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }


    }
}