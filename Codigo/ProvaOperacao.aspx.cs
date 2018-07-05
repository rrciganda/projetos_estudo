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
    public partial class ProvaOperacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["dtcertificacaoprova"] = null;
                if (
                    (Session["eoProva"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroProvaIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                    (Session["eoProva"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoProvaAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                CarregarComboCertificacao();

                if (Session["eoProva"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoProva");
                }
                dvcertificados.Visible = (grdcertificacao.Rows.Count > 0);

            }
         //   ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
        }

        public void CriarDTCertificadoProva()
        {
            if (Session["dtcertificacaoprova"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idcertificado");
                dt.Columns.Add("nomecertificado");
                dt.Columns.Add("tipo");
                Session["dtcertificacaoprova"] = dt;
            }
        }

        public DataTable ObterDTCertificadoProva()
        {
            if (Session["dtcertificacaoprova"] == null)
            {
                CriarDTCertificadoProva();

            }
            return (DataTable)Session["dtcertificacaoprova"];
        }

        public void AdicionarLinhaCertificado(string id, string nome, string tipo)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            if (Session["dtcertificacaoprova"] == null)
            {
                CriarDTCertificadoProva();
            }
            DataTable dt = (DataTable)Session["dtcertificacaoprova"];
            bool valido = true;
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["idcertificado"].ToString() == id)
                    {
                        valido = false;
                        lblmsgerro.Text = "Certificado já associado a essa prova!";
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
                dr["idcertificado"] = id;
                dr["nomecertificado"] = nome;
                dr["tipo"] = tipo;
                dt.Rows.Add(dr);

                Session["dtcertificacaoprova"] = dt;
                grdcertificacao.DataSource = (DataTable)Session["dtcertificacaoprova"];
                grdcertificacao.DataBind();
                dvcertificados.Visible = (grdcertificacao.Rows.Count > 0);

                //foreach (GridViewRow row in grdcertificacao.Rows)
                //{
                //    if (tipo == "Requerida")
                //    {
                //        ((RadioButton)row.FindControl("rdoRequerida")).Checked = true;
                //    }
                //    else if (tipo == "Obrigatória")
                //    {
                //        ((RadioButton)row.FindControl("rdoObrigatoria")).Checked = true;
                //    }
                //}
            }
        }

        public void CarregarComboCertificacao()
        {
            ddlcertificacao.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            eoCertificado.status = EOConst.CodStatus.Ativo;

            //ddlcertificacao.DataSource = blCertificado.Consultar(eoCertificado);
            DataTable dtCertificacao = blCertificado.Consultar(eoCertificado);

            ddlcertificacao.Items.Clear();
            ListItem itemCliente = new ListItem();

            Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
            radComboBoxItem.Value = "0";
            radComboBoxItem.Text = "";
            ddlcertificacao.Items.Add(radComboBoxItem);

            if (dtCertificacao != null)
            {
                if (dtCertificacao.Rows != null)
                {
                    if (dtCertificacao.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtCertificacao.Rows)
                        {
                            radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
                            radComboBoxItem.Value = dr["idcertificado"].ToString();
                            radComboBoxItem.Text = dr["dscombo"].ToString();
                            ddlcertificacao.Items.Add(radComboBoxItem);
                        }

                    }
                }
            }

            ddlcertificacao.AllowCustomText = true;
            ddlcertificacao.MarkFirstMatch = true;
            ddlcertificacao.SelectedValue = "0";

            //ddlcertificacao.DataTextField = "dscombo";
            //ddlcertificacao.DataValueField = "idcertificado";
            //ddlcertificacao.DataBind();
            //if (ddlcertificacao.Items.Count > 0)
            //{
            //    ddlcertificacao.Items.Insert(0, new ListItem("", ""));
            //}
            //else
            //{
            //    ddlcertificacao.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            //}
        }

        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoProva");
            Response.Redirect("ProvaPesquisar.aspx");
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
                    lblmsgerro.Text = "Nome da prova obrigatório;";
                }



                if (valido)
                {
                    ProvaBusinessLayer bl = new ProvaBusinessLayer();
                    ProvaEntity eo = new ProvaEntity();
                    if (txtnome.Text.Trim() != "")
                        eo.nome = txtnome.Text;
                    if (txtalias.Text.Trim() != "")
                        eo.alias = txtalias.Text;
                    if (ddlStatus.SelectedValue != "")
                        eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    if (hdnId.Value != "")
                    {
                        VerificaTipoProva();
                        eo.idprova = Convert.ToInt32(hdnId.Value);
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        bl.Alterar(eo);
                        SalvarProvaCertificado(eo.idprova);


                        Response.Redirect("ProvaPesquisar.aspx");
                    }
                    else
                    {
                        VerificaTipoProva();
                        eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eo.idprova = bl.Incluir(eo);
                        SalvarProvaCertificado(eo.idprova);


                        divsucesso.Visible = true;
                        lblsucesso.Text = "Inclusão realizada com sucesso!";
                        txtnome.Text = "";
                        txtalias.Text = "";
                        ddlStatus.SelectedValue = EOConst.CodStatus.Ativo.ToString();
                        hdnId.Value = "";
                        Session["dtcertificacaoprova"] = null;
                        grdcertificacao.DataSource = null;
                        grdcertificacao.DataBind();
                        dvcertificados.Visible = (grdcertificacao.Rows.Count > 0);
                    }

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void SalvarProvaCertificado(int idprova)
        {
            ProvaCertificadoBusinessLayer bl = new ProvaCertificadoBusinessLayer();
            int linha = 0;
            int contadorRequeridas = 0;
            //inativa todos os certificados vinculados a prova, pois os próximos passos irá ativar os registros válidos
            ProvaCertificadoEntity eo = new ProvaCertificadoEntity();
            eo.idprova = idprova;
            bl.Excluir(eo);

            //le os certificados vinculados a prova
            foreach (DataRow item in ObterDTCertificadoProva().Rows)
            {
                eo = new ProvaCertificadoEntity();
                eo.idprova = idprova;
                eo.idcertificado = Convert.ToInt32(item["idcertificado"]);
                DataTable dt = bl.Consultar(eo);

                GridView grd = grdcertificacao;

                GridViewRow linhaGrid = ((GridViewRow)(System.Web.UI.Control)grdcertificacao.Rows[linha]);

                if (((RadioButton)linhaGrid.FindControl("rdoRequerida")).Checked == true)
                {
                    eo.requerida = EOConst.CodStatus.Ativo;
                    contadorRequeridas++;
                }
                else
                {
                    eo.obrigatoria = EOConst.CodStatus.Ativo;
                }
                //Solicitado que possa existir mais de uma prova como requerida por certificado. Assim, qualquer prova requerida aprovada, já conclui a certificação.
                //if (contadorRequeridas > 1)
                //{
                //    throw new Exception("Não é permitido selecionar mais que uma prova como REQUERIDA, favor verificar as informações!");
                //}

                //se existir ativa
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    eo.idprova_certificado = Convert.ToInt32(dt.Rows[0]["idprova_certificado"]);
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
                ProvaBusinessLayer bl = new ProvaBusinessLayer();
                ProvaEntity eo = bl.Obter(Convert.ToInt32(Session["eoProva"]));
                hdnId.Value = eo.idprova.ToString();
                txtnome.Text = eo.nome.ToString();
                txtalias.Text = eo.alias.ToString();
                ddlStatus.SelectedValue = eo.status.ToString();


                ProvaCertificadoBusinessLayer blcertprova = new ProvaCertificadoBusinessLayer();
                ProvaCertificadoEntity eocertprova = new ProvaCertificadoEntity();
                eocertprova.idprova = Convert.ToInt32(hdnId.Value);
                eocertprova.status = EOConst.CodStatus.Ativo;
                DataTable dt = blcertprova.Consultar(eocertprova);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        AdicionarLinhaCertificado(item["idcertificado"].ToString(), item["nomecertificado"].ToString(), item["tipo"].ToString());
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
            if (ddlcertificacao.SelectedValue != "")
            {
                AdicionarLinhaCertificado(ddlcertificacao.SelectedValue, ddlcertificacao.SelectedItem.Text, "");
                ddlcertificacao.SelectedValue = "";
            }
        }

        private void RemoverLinhaDtCertificacao(string id)
        {
            DataTable dt = ObterDTCertificadoProva();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["idcertificado"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }

            Session["dtcertificacaoprova"] = dt;
            grdcertificacao.DataSource = dt;
            grdcertificacao.DataBind();
            dvcertificados.Visible = (grdcertificacao.Rows.Count > 0);
        }

        protected void grdcertificacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdcertificacao.DataKeys[index];
                    RemoverLinhaDtCertificacao(data.Values["idcertificado"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }

        }

        protected void VerificaTipoProva()
        {
            string msgvalidacao = string.Empty;
            if (grdcertificacao.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdcertificacao.Rows)
                {
                    RadioButton rdoRequerida = (RadioButton)row.FindControl("rdoRequerida");
                    RadioButton rdoObrigatoria = (RadioButton)row.FindControl("rdoObrigatoria");

                    if (rdoObrigatoria.Checked == false && rdoRequerida.Checked == false)
                    {
                        throw new Exception(msgvalidacao = msgvalidacao + "É obrigatório selecionar o tipo da prova! <br />");
                    }
                }
            }
        }
    }
}