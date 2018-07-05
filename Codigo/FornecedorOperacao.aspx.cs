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
    public partial class FornecedorOperacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtObservacao.Attributes.Add("maxlength", txtObservacao.MaxLength.ToString());
                Session["dtfornecedorgrupo"] = null;
                if (
                   (Session["eoFornecedor"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroReguladorIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                   (Session["eoFornecedor"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoReguladorAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                   )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                if (Session["eoFornecedor"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoFornecedor");
                }

                CarregarComboGrupo();
                dvgrupos.Visible = (grdgrupo.Rows.Count > 0);
            }
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
        }

        public void CriarDTGrupoFornecedor()
        {
            if (Session["dtfornecedorgrupo"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idgrupo");
                dt.Columns.Add("nomegrupo");
                dt.Columns.Add("status");
                Session["dtfornecedorgrupo"] = dt;
            }
        }

        public DataTable ObterDTGrupoFornecedor()
        {
            if (Session["dtfornecedorgrupo"] == null)
            {
                CriarDTGrupoFornecedor();

            }
            return (DataTable)Session["dtfornecedorgrupo"];
        }

        public void AdicionarLinhaGrupo(string id, string nome, string status)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            if (Session["dtfornecedorgrupo"] == null)
            {
                CriarDTGrupoFornecedor();
            }
            DataTable dt = (DataTable)Session["dtfornecedorgrupo"];
            bool valido = true;
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["idgrupo"].ToString() == id)
                    {
                        valido = false;
                        lblmsgerro.Text = "Grupo já associada a esse fornecedor!";
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
                dr["idgrupo"] = id;
                dr["nomegrupo"] = nome;
                dr["status"] = status;
                dt.Rows.Add(dr);
                Session["dtfornecedorgrupo"] = dt;
                grdgrupo.DataSource = (DataTable)Session["dtfornecedorgrupo"];
                grdgrupo.DataBind();

                dvgrupos.Visible = (grdgrupo.Rows.Count > 0);
            }
        }

        public void CarregarComboGrupo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idfornecedor");
            dt.Columns.Add("nome");
            GrupoBusinessLayer bl = new GrupoBusinessLayer();
            GrupoEntity eo = new GrupoEntity();
            eo.status = EOConst.CodStatus.Ativo;
            dt = bl.ConsultarGruposAtivos(eo);

            ddlgrupo.Items.Clear();
            ListItem itemCliente = new ListItem();

            Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
            radComboBoxItem.Value = "0";
            radComboBoxItem.Text = "";
            ddlgrupo.Items.Add(radComboBoxItem);

            if (dt != null)
            {
                if (dt.Rows != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
                            radComboBoxItem.Value = dr["idgrupo"].ToString();
                            radComboBoxItem.Text = dr["nome"].ToString();
                            ddlgrupo.Items.Add(radComboBoxItem);
                        }

                    }
                }
            }

            ddlgrupo.AllowCustomText = true;
            ddlgrupo.MarkFirstMatch = true;

            ddlgrupo.SelectedValue = "0";

            //ddlgrupo.DataSource = dt;
            //ddlgrupo.DataBind();
            //if (ddlgrupo.Items.Count > 0)
            //{
            //    ddlgrupo.Items.Insert(0, new ListItem("", ""));
            //}
            //else
            //{
            //    ddlgrupo.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            //}
        }
        
        private void SalvarGrupoFornecedor(int idfornecedor)
        {
            GrupoFornecedorBusinessLayer bl = new GrupoFornecedorBusinessLayer();

            int linha = 0;
            //int contadorRequeridas = 0;
            //inativa todos os grupos vinculados ao fornecedor, pois os próximos passos irá ativar os registros válidos
            GrupoFornecedorEntity eo = new GrupoFornecedorEntity();
            eo.idfornecedor = idfornecedor;
            bl.Excluir(eo);

            //le os fornecedores vinculadas aos grupos
            foreach (DataRow item in ObterDTGrupoFornecedor().Rows)
            {
                eo = new GrupoFornecedorEntity();
                eo.idfornecedor = idfornecedor;
                eo.idgrupo = Convert.ToInt32(item["idgrupo"]);
                DataTable dt = bl.Consultar(eo);

                GridView grd = grdgrupo;

                GridViewRow linhaGrid = ((GridViewRow)(System.Web.UI.Control)grdgrupo.Rows[linha]);


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
                FornecedorBusinessLayer bl = new FornecedorBusinessLayer();
                FornecedorEntity eo = bl.Obter(Convert.ToInt32(Session["eoFornecedor"]));
                hdnId.Value = eo.idfornecedor.ToString();
                txtFornecedor.Text = eo.nome.ToString();
                txtObservacao.Text = eo.observacao.ToString();
                ddlStatus.SelectedValue = eo.status.ToString();

                GrupoFornecedorBusinessLayer blgrupofornecedor = new GrupoFornecedorBusinessLayer();
                GrupoFornecedorEntity eogrupofornecedor = new GrupoFornecedorEntity();
                eogrupofornecedor.idfornecedor = Convert.ToInt32(hdnId.Value);
                eogrupofornecedor.status = EOConst.CodStatus.Ativo;
                DataTable dt = blgrupofornecedor.Consultar(eogrupofornecedor);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        AdicionarLinhaGrupo(item["idgrupo"].ToString(), item["nomegrupo"].ToString(), item["status"].ToString());
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
            if (ddlgrupo.SelectedValue != "")
            {
                AdicionarLinhaGrupo(ddlgrupo.SelectedValue, ddlgrupo.SelectedItem.Text, "");
                ddlgrupo.SelectedValue = "";
            }
        }


        private void RemoverLinhaDtFornecedor(string id)
        {
            DataTable dt = ObterDTGrupoFornecedor();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["idGrupo"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }
            Session["dtgrupofornecedor"] = dt;
            grdgrupo.DataSource = dt;
            grdgrupo.DataBind();
            dvgrupos.Visible = (grdgrupo.Rows.Count > 0);
        }

        protected void grdfornecedor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdgrupo.DataKeys[index];
                    RemoverLinhaDtFornecedor(data.Values["idgrupo"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }
        }

        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoFornecedor");
            Response.Redirect("FornecedorPesquisar.aspx");
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {
                diverro.Visible = false;
                lblmsgerro.Text = "";

                bool valido = true;
                if (txtFornecedor.Text == "")
                {
                    valido = false;
                    diverro.Visible = true;
                    lblmsgerro.Text = "Nome do Fornecedor obrigatório;";
                }

                if (valido)
                {
                    FornecedorBusinessLayer bl = new FornecedorBusinessLayer();
                    FornecedorEntity eo = new FornecedorEntity();
                    if (txtFornecedor.Text.Trim() != "")
                        eo.nome = txtFornecedor.Text;
                    if (ddlStatus.SelectedValue != "")
                        eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (txtObservacao.Text.Trim() != "")
                        eo.observacao = txtObservacao.Text;
                    if (hdnId.Value != "")
                    {
                        eo.idfornecedor = Convert.ToInt32(hdnId.Value);
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        bl.Alterar(eo);
                        SalvarGrupoFornecedor(eo.idfornecedor);
                        Response.Redirect("FornecedorPesquisar.aspx");
                    }
                    else
                    {
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idfornecedor = bl.Incluir(eo);

                        SalvarGrupoFornecedor(eo.idfornecedor);
                        divsucesso.Visible = true;
                        lblsucesso.Text = "Inclusão realizada com sucesso!";
                        txtFornecedor.Text = "";
                        txtObservacao.Text = "";
                        ddlStatus.SelectedValue = EOConst.CodStatus.Ativo.ToString();
                        hdnId.Value = "";

                        grdgrupo.DataSource = null;
                        grdgrupo.DataBind();
                        dvgrupos.Visible = (grdgrupo.Rows.Count > 0);
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