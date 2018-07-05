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
    public partial class CertificadoOperacao : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["dtcertificacaoprova"] = null;
                //txtObservacao.Attributes.Add("maxlength", txtObservacao.MaxLength.ToString());
                if (
                   (Session["eoCertificado"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CadastroCertificacaoIncluir, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))) ||
                   (Session["eoCertificado"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.AlteracaoCertificacaoAlterar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                   )
                {
                    Response.Redirect("SemAcesso.aspx");
                }

                if (Session["eoCertificado"] != null)
                {
                    CarregarComboProva();
                    CarregarDados();
                    ddlfornecedor.Enabled = true;
                    Session.Remove("eoCertificado");
                }
                else
                {
                    CarregarGrupoAtivos();
                    CarregarComboProva();



                }

                dvprovas.Visible = (grdcertificacao.Rows.Count > 0);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
        }

        public void CarregarComboFornecedor(int idGrupo)
        {
            ddlfornecedor.Enabled = true;
            ddlfornecedor.Items.Clear();
            int status = int.MinValue;
            DataTable dt = new DataTable();
            FornecedorBusinessLayer bl = new FornecedorBusinessLayer();
            //FornecedorEntity eo = new FornecedorEntity();
            status = EOConst.CodStatus.Ativo;
            //dt = bl.ConsultarAtivoPorGrupo(status, idGrupo);
            ddlfornecedor.DataSource = bl.ConsultarAtivoPorGrupo(status, idGrupo);
            ddlfornecedor.DataTextField = "nome";
            ddlfornecedor.DataValueField = "idfornecedor";
            ddlfornecedor.DataBind();
            if (ddlfornecedor.Items.Count > 0)
            {
                ddlfornecedor.Items.Insert(0, new ListItem("Selecione o fornecedor", ""));
            }
            else
            {
                ddlfornecedor.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }

        public void CarregarComboFornecedorSelecinado(int idGrupo, int idfornecedor)
        {
            ddlfornecedor.Items.Clear();
            int status = int.MinValue;
            DataTable dt = new DataTable();
            FornecedorBusinessLayer bl = new FornecedorBusinessLayer();
            //FornecedorEntity eo = new FornecedorEntity();
            status = EOConst.CodStatus.Ativo;
            //dt = bl.ConsultarAtivoPorGrupo(status, idGrupo);
            ddlfornecedor.DataSource = bl.ConsultarAtivoSelecionado(status, idGrupo, idfornecedor);
            ddlfornecedor.DataTextField = "nome";
            ddlfornecedor.DataValueField = "idfornecedor";
            ddlfornecedor.DataBind();
            if (ddlfornecedor.Items.Count > 0)
            {
                ddlfornecedor.Items.Insert(0, new ListItem("Selecione o fornecedor", ""));
            }
            else
            {
                ddlfornecedor.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }
        public void CriarDTCertificadoProva()
        {
            if (Session["dtcertificacaoprova"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idprova");
                dt.Columns.Add("nome");
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
                    if (item["idprova"].ToString() == id)
                    {
                        valido = false;
                        lblmsgerro.Text = "Prova já associada a esse certificado!";
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
                dr["idprova"] = id;
                dr["nome"] = nome;
                dr["tipo"] = tipo;
                dt.Rows.Add(dr);
                Session["dtcertificacaoprova"] = dt;
                grdcertificacao.DataSource = (DataTable)Session["dtcertificacaoprova"];
                grdcertificacao.DataBind();

                dvprovas.Visible = (grdcertificacao.Rows.Count > 0);
            }
        }

        public void CarregarComboProva()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idprova");
            dt.Columns.Add("nome");
            ProvaBusinessLayer bl = new ProvaBusinessLayer();
            ProvaEntity eo = new ProvaEntity();
            eo.status = EOConst.CodStatus.Ativo;
            dt = bl.ConsultarAtivas(eo);
            ddlprova.DataSource = dt;
            ddlprova.DataBind();
            if (ddlprova.Items.Count > 0)
            {
                ddlprova.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                ddlprova.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }
        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            Session.Remove("eoCertificado");
            Response.Redirect("CertificadoPesquisar.aspx");
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {
                diverro.Visible = false;
                lblmsgerro.Text = "";

                divsucesso.Visible = false;
                lblsucesso.Text = "";


                bool valido = true;
                string msgvalidacao = "";
                if (ddlGrupo.SelectedValue.Trim() == "")
                {
                    msgvalidacao = msgvalidacao + "Grupo obrigatório! <br />";
                    valido = false;
                }
                if (ddlfornecedor.SelectedValue.Trim() == "")
                {
                    msgvalidacao = msgvalidacao + "Fornecedor obrigatório! <br />";
                    valido = false;
                }
                if (txtCertificado.Text.Trim() == "")
                {
                    msgvalidacao = msgvalidacao + "Certificado obrigatório! <br />";
                    valido = false;
                }
                if (ddlStatus.SelectedValue.Trim() == "")
                {
                    msgvalidacao = msgvalidacao + "Status obrigatório! <br />";
                    valido = false;
                }
                if (!chknaoexpira.Checked && txtqtddiavalidade.Text == "" && txtqtdmesvalidade.Text == "" && txtqtdanovalidade.Text == "")
                {
                    msgvalidacao = msgvalidacao + "Informação de expiração ou informações de período de validade (dia/mes/ano) obrigatório! <br />";
                    valido = false;
                }
                if (chkvoucher.Checked && txtqtdevoucher.Text == "")
                {
                    msgvalidacao = msgvalidacao + "A quantidade de tentativas para o voucher deve ser informado! <br />";
                    valido = false;
                }

                if (!valido)
                {
                    diverro.Visible = true;
                    lblmsgerro.Text = msgvalidacao;
                    return;
                }
                CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                CertificadoEntity eo = new CertificadoEntity();
                if (ddlGrupo.SelectedValue.Trim() != "")
                    eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
                if (ddlfornecedor.SelectedValue.Trim() != "")
                    eo.idfornecedor = Convert.ToInt32(ddlfornecedor.SelectedValue);
                if (txtCertificado.Text.Trim() != "")
                    eo.nome = txtCertificado.Text;
                if (ddlStatus.Text.Trim() != "")
                    eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
                if (txtVersao.Text.Trim() != "")
                    eo.versao = txtVersao.Text;

                if (txtqtddiavalidade.Text.Trim() != "")
                    eo.qtddiavalidade = Convert.ToInt32(txtqtddiavalidade.Text);
                if (txtqtdmesvalidade.Text.Trim() != "")
                    eo.qtdmesvalidade = Convert.ToInt32(txtqtdmesvalidade.Text);
                if (txtqtdanovalidade.Text.Trim() != "")
                    eo.qtdanovalidade = Convert.ToInt32(txtqtdanovalidade.Text);
                if (chknaoexpira.Checked)
                    eo.naoexpira = EOConst.CodStatusExpirado.Naoexpira;
                else
                    eo.naoexpira = EOConst.CodStatusExpirado.Expira;
                if (chkvoucher.Checked)
                {
                    eo.voucher = EOConst.CodStatusVoucher.ComVoucher;
                    eo.qtdvoucher = Convert.ToInt32(txtqtdevoucher.Text);
                }

                else
                    eo.voucher = EOConst.CodStatusVoucher.SemVoucher;
                //if (txtObservacao.Text.Trim() != "")
                //    eo.descricao = txtObservacao.Text;
                if (txtTesteCertificador.Text.Trim() != "")
                    eo.certificador = txtTesteCertificador.Text;
                if (hdnId.Value != "")
                {
                    VerificaTipoProva();
                    eo.idcertificado = Convert.ToInt32(hdnId.Value);
                    eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                    bl.Alterar(eo);
                    SalvarProvaCertificado(eo.idcertificado);
                    Response.Redirect("CertificadoPesquisar.aspx");
                }
                else
                {
                    VerificaTipoProva();
                    eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                    eo.idcertificado = bl.Incluir(eo);

                    SalvarProvaCertificado(eo.idcertificado);
                    divsucesso.Visible = true;
                    lblsucesso.Text = "Inclusão realizada com sucesso!";


                    hdnId.Value = "";
                    ddlGrupo.SelectedValue = "";
                    ddlfornecedor.SelectedValue = "";
                    txtCertificado.Text = "";
                    ddlStatus.Text = "";
                    txtVersao.Text = "";
                    txtqtddiavalidade.Text = "";
                    txtqtdmesvalidade.Text = "";
                    txtqtdanovalidade.Text = "";
                    chknaoexpira.Checked = false;
                    chkvoucher.Checked = false;
                    txtqtddiavalidade.Enabled = true;
                    txtqtdmesvalidade.Enabled = true;
                    txtqtdanovalidade.Enabled = true;
                    txtqtdevoucher.Text = "";
                    txtqtdevoucher.Enabled = false;

                    //txtObservacao.Text = "";
                    txtTesteCertificador.Text = "";
                    Session["dtcertificacaoprova"] = null;
                    Session["tipoProva"] = null;
                    grdcertificacao.DataSource = null;
                    grdcertificacao.DataBind();
                    dvprovas.Visible = (grdcertificacao.Rows.Count > 0);

                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void SalvarProvaCertificado(int idcertificado)
        {
            ProvaCertificadoBusinessLayer bl = new ProvaCertificadoBusinessLayer();

            int linha = 0;
            int contadorRequeridas = 0;
            //inativa todos os certificados vinculados a prova, pois os próximos passos irá ativar os registros válidos
            ProvaCertificadoEntity eo = new ProvaCertificadoEntity();
            eo.idcertificado = idcertificado;
            bl.Excluir(eo);

            //le as provas vinculadas aos certificados
            foreach (DataRow item in ObterDTCertificadoProva().Rows)
            {
                eo = new ProvaCertificadoEntity();
                eo.idcertificado = idcertificado;
                eo.idprova = Convert.ToInt32(item["idprova"]);
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
                CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                CertificadoEntity eo = bl.Obter(Convert.ToInt32(Session["eoCertificado"]));
                hdnId.Value = eo.idcertificado.ToString();
                CarregarGrupoSelecionadosEativos(eo.idgrupo);
                ddlGrupo.SelectedValue = eo.idgrupo.ToString();
                CarregarComboFornecedorSelecinado(eo.idgrupo, eo.idfornecedor);
                ddlfornecedor.SelectedValue = eo.idfornecedor.ToString();
                txtCertificado.Text = eo.nome.ToString();
                ddlStatus.Text = eo.status.ToString();
                txtVersao.Text = eo.versao.ToString();
                //txtObservacao.Text = eo.descricao.ToString();
                txtTesteCertificador.Text = eo.certificador.ToString();
                if (eo.qtddiavalidade != int.MinValue)
                {
                    txtqtddiavalidade.Text = eo.qtddiavalidade.ToString();
                }
                if (eo.qtdmesvalidade != int.MinValue)
                {
                    txtqtdmesvalidade.Text = eo.qtdmesvalidade.ToString();
                }
                if (eo.qtdanovalidade != int.MinValue)
                {
                    txtqtdanovalidade.Text = eo.qtdanovalidade.ToString();
                }
                if (eo.naoexpira == EOConst.CodStatusExpirado.Naoexpira)
                    chknaoexpira.Checked = true;
                else
                    chknaoexpira.Checked = false;
                if (eo.voucher == EOConst.CodStatusVoucher.ComVoucher)
                    chkvoucher.Checked = true;
                else
                    chkvoucher.Checked = false;
                if (eo.qtdvoucher != int.MinValue)
                {
                    txtqtdevoucher.Text = eo.qtdvoucher.ToString();
                }

                chknaoexpira_CheckedChanged(null, null);

                ProvaCertificadoBusinessLayer blcertprova = new ProvaCertificadoBusinessLayer();
                ProvaCertificadoEntity eocertprova = new ProvaCertificadoEntity();
                eocertprova.idcertificado = Convert.ToInt32(hdnId.Value);
                eocertprova.status = EOConst.CodStatus.Ativo;
                DataTable dt = blcertprova.Consultar(eocertprova);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        AdicionarLinhaCertificado(item["idprova"].ToString(), item["nomeprova"].ToString(), item["tipo"].ToString());
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
            if (ddlprova.SelectedValue != "")
            {
                AdicionarLinhaCertificado(ddlprova.SelectedValue, ddlprova.SelectedItem.Text, "");
                ddlprova.SelectedValue = "";
            }
        }

        private void RemoverLinhaDtCertificacao(string id)
        {
            DataTable dt = ObterDTCertificadoProva();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["idprova"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }

            Session["dtcertificacaoprova"] = dt;
            grdcertificacao.DataSource = dt;
            grdcertificacao.DataBind();
            dvprovas.Visible = (grdcertificacao.Rows.Count > 0);
        }

        protected void grdcertificacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdcertificacao.DataKeys[index];
                    RemoverLinhaDtCertificacao(data.Values["idprova"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }
        }

        //protected void lnksalvar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        diverro.Visible = false;
        //        lblmsgerro.Text = "";

        //        bool valido = true;
        //        string msgvalidacao = "";
        //        if (ddlGrupo.SelectedValue.Trim() == "")
        //        {
        //            msgvalidacao = msgvalidacao + "Grupo obrigatório! <br />";
        //            valido = false;
        //        }
        //        if (txtCertificado.Text.Trim() == "")
        //        {
        //            msgvalidacao = msgvalidacao + "Certificado obrigatório! <br />";
        //            valido = false;
        //        }
        //        if (ddlStatus.SelectedValue.Trim() == "")
        //        {
        //            msgvalidacao = msgvalidacao + "Status obrigatório! <br />";
        //            valido = false;
        //        }
        //        //if (txtVersao.Text.Trim() != "")
        //        //{
        //        //    msgvalidacao = msgvalidacao + "Versão obrigatório";
        //        //    valido = false;
        //        //}
        //        //if (txtObservacao.Text.Trim() != "")
        //        //{
        //        //    msgvalidacao = msgvalidacao + "Observação obrigatório";
        //        //    valido = false;
        //        //}
        //        //if (txtTesteCertificador.Text.Trim() != "")
        //        //{
        //        //    msgvalidacao = msgvalidacao + "Teste Certificador obrigatório";
        //        //    valido = false;
        //        //}

        //        if (!valido)
        //        {
        //            diverro.Visible = true;
        //            lblmsgerro.Text = msgvalidacao;
        //            return;
        //        }
        //        CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
        //        CertificadoEntity eo = new CertificadoEntity();
        //        if (ddlGrupo.SelectedValue.Trim() != "")
        //            eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
        //        if (txtCertificado.Text.Trim() != "")
        //            eo.nome = txtCertificado.Text;
        //        if (ddlStatus.Text.Trim() != "")
        //            eo.status = Convert.ToInt32(ddlStatus.SelectedValue);
        //        if (txtVersao.Text.Trim() != "")
        //            eo.versao = txtVersao.Text;
        //        if (txtObservacao.Text.Trim() != "")
        //            eo.descricao = txtObservacao.Text;
        //        if (txtTesteCertificador.Text.Trim() != "")
        //            eo.certificador = txtTesteCertificador.Text;
        //        if (hdnId.Value != "")
        //        {
        //            eo.idcertificado = Convert.ToInt32(hdnId.Value);
        //            eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
        //            bl.Alterar(eo);
        //            Response.Redirect("CertificadoPesquisar.aspx");
        //        }
        //        else
        //        {
        //            eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
        //            eo.idcertificado = bl.Incluir(eo);

        //            divsucesso.Visible = true;
        //            lblsucesso.Text = "Inclusão realizada com sucesso!";

        //            hdnId.Value = "";
        //            ddlGrupo.SelectedValue = "";
        //            txtCertificado.Text = "";
        //            ddlStatus.Text = "";
        //            txtVersao.Text = "";
        //            txtObservacao.Text = "";
        //            txtTesteCertificador.Text = "";


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        diverro.Visible = true;
        //        lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
        //    }
        //}

        private void CarregarGrupoAtivos()
        {
            ddlGrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            eoGrupo.status = EOConst.CodStatus.Ativo;
            ddlGrupo.DataSource = blGrupo.ConsultarGruposAtivos(eoGrupo);
            ddlGrupo.DataTextField = "nome";
            ddlGrupo.DataValueField = "idgrupo";
            ddlGrupo.DataBind();
            if (ddlGrupo.Items.Count > 0)
                ddlGrupo.Items.Insert(0, new ListItem("Selecione o grupo", ""));
            else
                ddlGrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        private void CarregarGrupoSelecionadosEativos(int idGrupo)
        {
            ddlGrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            eoGrupo.idgrupo = idGrupo;
            ddlGrupo.DataSource = blGrupo.ConsultarGruposSelecionadoseAtivos(eoGrupo);
            ddlGrupo.DataTextField = "nome";
            ddlGrupo.DataValueField = "idgrupo";
            ddlGrupo.DataBind();
            if (ddlGrupo.Items.Count > 0)
                ddlGrupo.Items.Insert(0, new ListItem("Selecione o grupo", ""));
            else
                ddlGrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }


        private void CarregarTodosgrupos()
        {
            ddlGrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            ddlGrupo.DataSource = blGrupo.Consultar(eoGrupo);
            ddlGrupo.DataTextField = "nome";
            ddlGrupo.DataValueField = "idgrupo";
            ddlGrupo.DataBind();
            if (ddlGrupo.Items.Count > 0)
                ddlGrupo.Items.Insert(0, new ListItem("Selecione o grupo", ""));
            else
                ddlGrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        protected void chknaoexpira_CheckedChanged(object sender, EventArgs e)
        {
            if (chknaoexpira.Checked)
            {
                txtqtddiavalidade.Text = "";
                txtqtdmesvalidade.Text = "";
                txtqtdanovalidade.Text = "";
                txtqtddiavalidade.Enabled = false;
                txtqtdmesvalidade.Enabled = false;
                txtqtdanovalidade.Enabled = false;
            }
            else
            {
                txtqtddiavalidade.Enabled = true;
                txtqtdmesvalidade.Enabled = true;
                txtqtdanovalidade.Enabled = true;
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

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(ddlGrupo.SelectedValue))
            {
                CarregarComboFornecedor(Convert.ToInt32(ddlGrupo.SelectedValue));
            }

        }

        protected void chkvoucher_CheckedChanged(object sender, EventArgs e)
        {
            if (chkvoucher.Checked)
            {
                txtqtdevoucher.Enabled = true;
            }
            else
            {
                txtqtdevoucher.Enabled = false;
                txtqtdevoucher.Text = "";
            }
        }
    }
}