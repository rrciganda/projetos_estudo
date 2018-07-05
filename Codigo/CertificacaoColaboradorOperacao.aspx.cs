using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.Data;
using System.IO;
using System.Drawing.Imaging;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class MinhaCertificacaoOperacao : System.Web.UI.Page
    {
        public int qdteProvas = 0;

        #region LoadFormulario

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((String)Session["Origem"] == "MinhasCertificacoes")
            {
                lnkEnviarAprovacao.Visible = true;
                lbNomePagina.Text = "MINHAS CERTIFICAÇÕES";
            }
            else
            {
                lnkEnviarAprovacao.Visible = false;
                lbNomePagina.Text = "Certificações Do Colaborador";
            }


            if (!Page.IsPostBack || Session["recarregarSessao"] != null)
            {
                bool CertificacaoIncluirCertificacoesColaborador = (Session["eoMinhaCertificacaousuario"] == null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoIncluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])));
                bool CertificacaoAlterarCertificacoesColaborador = (Session["eoMinhaCertificacaousuario"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAlterarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])));
                bool CertificacaoIncluirMinhaCertificacao = (Session["Origem"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoIncluirMinhaCertificacao, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])));
                bool CertificacaoAprovarCertificacoesColaborador = (Session["Aprovacao"] != null && !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAprovarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])));
                if (

                        (CertificacaoIncluirCertificacoesColaborador && Session["Origem"] == null && Session["Aprovacao"] == null) ||
                        (CertificacaoAlterarCertificacoesColaborador && Session["Origem"] == null && Session["Aprovacao"] == null) ||
                        (CertificacaoIncluirMinhaCertificacao && Session["eoMinhaCertificacaousuario"] == null && Session["Aprovacao"] == null) ||
                        (CertificacaoAprovarCertificacoesColaborador && Session["eoMinhaCertificacaousuario"] == null && Session["Origem"] == null)

                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }

                Session["AlterouArquivoDigitalizado"] = null;
                Session["NomeArquivoDigitalizado"] = null;
                Session["ArquivoDigitalizado"] = null;


                CarregarComboColaborador();
                CarregarComboRegulador();
                //CarregarComboGrupo();
                CarregarComboCertificado();
                grdpesquisa.DataSource = null;
                grdpesquisa.DataBind();


                if (Session["recarregarSessao"] != null && Session["eoMinhaCertificacaousuario"] == null)
                    Session["eoMinhaCertificacaousuario"] = Session["recarregarSessao"];


                if (Session["eoMinhaCertificacaousuario"] != null)
                {
                    CarregarDados();
                    Session.Remove("eoMinhaCertificacaousuario");
                    //  Session.Remove("Origem");
                    //   Session.Remove("Aprovacao");
                }
                else if ((String)Session["Origem"] == "MinhasCertificacoes")
                {
                    //   chkAceite.Visible = true;
                    //   lblAceite.Visible = true;
                    DesabilitarEnviarAprovacao();
                    int idUsuario = int.MinValue;
                    idUsuario = ((UsuarioEntity)Session["eoUs"]).idusuario;
                    // Session.Remove("Origem");
                  
                    Div3.Visible = false;
                }

                else
                {
                    DesabilitarEnviarAprovacao();
                    Div3.Visible = false;
                    CarregarComboReguladorAtivo();
                    CarregarComboCertificadoAtivo();
                    CarregarGrid();

                }
            }
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);
            if (txtprovavisivel.Value == "0")
            {
                iconeprova.Attributes.Remove("class");
                iconeprova.Attributes.Add("class", "icon-plus");
                divprova.Style.Remove("display");
                divprova.Style.Add("display", "none");
            }
            else
            {
                iconeprova.Attributes.Remove("class");
                iconeprova.Attributes.Add("class", "icon-minus");
                divprova.Style.Remove("display");
            }
            if (txtcertificadovisivel.Value == "0")
            {
                iconecertificado.Attributes.Remove("class");
                iconecertificado.Attributes.Add("class", "icon-plus");
                divcertificado.Style.Remove("display");
                divcertificado.Style.Add("display", "none");
            }
            else
            {
                iconecertificado.Attributes.Remove("class");
                iconecertificado.Attributes.Add("class", "icon-minus");
                divcertificado.Style.Remove("display");
            }

            if (txtanexovisivel.Value == "0")
            {
                iconeanexo.Attributes.Remove("class");
                iconeanexo.Attributes.Add("class", "icon-plus");
                divanexo.Style.Remove("display");
                divanexo.Style.Add("display", "none");
            }
            else
            {
                iconeanexo.Attributes.Remove("class");
                iconeanexo.Attributes.Add("class", "icon-minus");
                divanexo.Style.Remove("display");
            }
            if (hdnpossuianexo.Value == "1")
            {
                divanexolink.Style.Remove("display");
                divanexarlinkcertificado.Style.Remove("display");
                divanexarlinkcertificado.Style.Add("display", "none");
            }
            else
            {
                divanexarlinkcertificado.Style.Remove("display");
                divanexolink.Style.Remove("display");
                divanexolink.Style.Add("display", "none");
            }
            if (txtAprovacaoVisivel.Value == "0")
            {
                iconeAprovacao.Attributes.Remove("class");
                iconeAprovacao.Attributes.Add("class", "icon-plus");
                divanexo.Style.Remove("display");
                divanexo.Style.Add("display", "none");
            }
            else
            {
                iconeAprovacao.Attributes.Remove("class");
                iconeAprovacao.Attributes.Add("class", "icon-minus");
                divanexo.Style.Remove("display");
            }
        }

        #endregion

        #region EventosFormulario

        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            if ((String)Session["Origem"] == "MinhasCertificacoes")
            {
                Session.Remove("Origem");
                Response.Redirect("PesquisarMinhasCertificacoes.aspx");
            }
            else if (Session["Aprovacao"] != null)
            {
                Session.Remove("Aprovacao");
                Response.Redirect("CertificacaoColaboradorAprovar.aspx");
            }
            Session.Remove("eoMinhaCertificacaousuario");
            Response.Redirect("CertificacaoColaboradorPesquisar.aspx");

        }

        protected void ddlgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarComboCertificado();
        }

        protected void lnkdownloadarquivo_Click(object sender, CommandEventArgs e)
        {
            DownloadPDF(e.CommandArgument.ToString());
        }

        protected void grdpesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            diverro.Visible = false;
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdpesquisa.Rows[index];
                bool provaexistente = false;

                TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;
                DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                CheckBox chkSelecionado = row.FindControl("chkSelecionado") as CheckBox;

                if (txtDtProva.Text != "" || ddlStatus.SelectedValue != "" || chkSelecionado.Checked)
                {
                    if (e.CommandName == "cmdExcluir")
                    {
                        int qtdVoucher = int.MinValue;
                        DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                        DataTable dt = new DataTable();
                        dt = (DataTable)Session["Grid"];
                        DataRow linha = dt.NewRow();
                        linha["selecionado"] = "False"; //(row.FindControl("chkSelecionado") as CheckBox).Checked;
                        linha["idprova"] = data["idprova"];
                        linha["Nome"] = data["Nome"].ToString();
                        linha["Alias"] = data["Alias"].ToString();
                        linha["Tipo"] = data["Tipo"].ToString();
                        linha["idlinha"] = grdpesquisa.Rows.Count + 1;

                        int idpro = Convert.ToInt32(data["idprova"]);
                        string prova = "idprova = " + idpro.ToString() + "and (dtprova<>'') or (status='3' or status='2')";
                        string provarepetida = "idprova = " + idpro.ToString() + " and (status<>'3' or status <>'2') or (dtprova='')";

                        DataRow[] find = dt.Select(prova);
                        int numrep = find.Count();


                        if (txtqtdvoucher.Text != "" && txtqtdvoucher.Text != null)
                        {
                            qtdVoucher = Convert.ToInt32(txtqtdvoucher.Text);
                        }


                        DataRow[] findrep = dt.Select(provarepetida);
                        int numprovarep = findrep.Count();
                        if (numprovarep != 0)
                            provaexistente = true;


                        if (numrep < qtdVoucher && !provaexistente)
                        {
                            dt.Rows.Add(linha);
                            Session["Grid"] = dt;
                        }

                        dt.Rows.RemoveAt(index);
                        Session["Grid"] = dt;

                        if (dt.Rows.Count == 0)
                        {
                            divInfoProva.Visible = true;
                            lblmsInfoProva.Text = "Não existe provas para o certificado selecionado!";
                            ddlStatusCertificacao.SelectedValue = EOConst.CodStatusCertificacao.Concluida.ToString();
                        }
                        else
                        {
                            divInfoProva.Visible = false;
                        }

                        grdpesquisa.DataSource = dt;
                        grdpesquisa.DataBind();
                        AtualizarDTSessao();
                        AtualizarStatusCertificacao();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void ddlidcertificado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarGrid();
            txtdtcertificacao_TextChanged(null, null);
        }

        protected void chkSelecionado_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((System.Web.UI.Control)sender).Parent.Parent);
            if (row != null)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                if (chkbox.Checked)
                {

                    ((TextBox)row.FindControl("txtDTProva")).Enabled = true;
                    ((DropDownList)row.FindControl("ddlStatus")).Enabled = true;
                    ((LinkButton)row.FindControl("btnExcluir")).Enabled = true;
                }
                else
                {
                    ((TextBox)row.FindControl("txtDTProva")).Text = "";
                    ((TextBox)row.FindControl("txtDTProva")).Enabled = false;

                    ((DropDownList)row.FindControl("ddlStatus")).SelectedValue = "";
                    ((DropDownList)row.FindControl("ddlStatus")).Enabled = false;

                    ((LinkButton)row.FindControl("btnExcluir")).Enabled = false;



                }
                AtualizarStatusCertificacao();
            }
            AtualizarDTSessao();
        }

        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";

            try
            {
                List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
                List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
                CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
                string msgvalidacao = "";
                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                //Validação dos campos para salvar o formulário
                bool valido = SalvarEnvioAprovacao();

                if (!valido)
                {
                    return;
                }
                else
                {

                    //Verificar se o status da certificação é igual concluída para habilitar o botão de enviar para aprovação
                    if (ddlStatusCertificacao.SelectedValue != null)
                    {
                        if (!string.IsNullOrEmpty(ddlStatusCertificacao.SelectedValue))
                        {
                            if (ddlStatusCertificacao.SelectedValue == "1")
                            {
                                if ((String)Session["Origem"] == "MinhasCertificacoes")
                                {
                                    btnEnviarAprovacao.Visible = false;
                                    btnSalvarAprovacao.Visible = true;
                                    btnEnviarSalvarAprovacao.Visible = true;
                                    lblConfirmacaoEnvio.Text = "A certificação foi concluída, deseja enviar para aprovação?";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
                                }
                                else
                                {
                                    btnEnviarAprovacao.Visible = false;
                                    btnSalvarAprovacao.Visible = false;
                                    btnEnviarSalvarAprovacao.Visible = false;
                                    lblConfirmacaoEnvio.Text = "";

                                    this.Salvar();

                                    divsucesso.Visible = true;
                                    lblsucesso.Text = "Operação realizada com sucesso!";
                                    if (Session["Origem"] != null)
                                    {
                                        if ((String)Session["Origem"] == "MinhasCertificacoes")
                                        {
                                            Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                                        }

                                    }
                                    else if (Session["eoMinhaCertificacaousuario"] == null && Session["Origem"] == null)
                                    {
                                        Response.Redirect("CertificacaoColaboradorPesquisar.aspx");
                                    }
                                    else
                                    {
                                        Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                                    }

                                }

                            }
                            else
                            {
                                this.Salvar();

                                divsucesso.Visible = true;
                                lblsucesso.Text = "Operação realizada com sucesso!";
                                if (Session["Origem"] != null)
                                {
                                    if ((String)Session["Origem"] == "MinhasCertificacoes")
                                    {
                                        Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                                    }
                                }
                                else if (Session["eoMinhaCertificacaousuario"] == null && Session["Origem"] == null)
                                {
                                    Response.Redirect("CertificacaoColaboradorPesquisar.aspx");
                                }
                                else
                                {
                                    Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                                }

                            }
                        }
                        else
                        {
                            this.Salvar();
                            divsucesso.Visible = true;
                            lblsucesso.Text = "Operação realizada com sucesso!";
                            if (Session["Origem"] != null)
                            {
                                if ((String)Session["Origem"] == "MinhasCertificacoes")
                                {
                                    Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                                }
                            }
                            else if (Session["eoMinhaCertificacaousuario"] == null && Session["Origem"] == null)
                            {
                                Response.Redirect("CertificacaoColaboradorPesquisar.aspx");
                            }
                            else
                            {
                                Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                            }
                        }
                    }
                    else
                    {
                        this.Salvar();
                        divsucesso.Visible = true;
                        lblsucesso.Text = "Operação realizada com sucesso!";
                        if (Session["Origem"] != null)
                        {
                            if ((String)Session["Origem"] == "MinhasCertificacoes")
                            {
                                Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                            }
                        }
                        else if (Session["eoMinhaCertificacaousuario"] == null && Session["Origem"] == null)
                        {
                            Response.Redirect("CertificacaoColaboradorPesquisar.aspx");
                        }
                        else
                        {
                            Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                        }
                    }

                    //CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();
                    //CertificacaousuarioEntity eocertuser = new CertificacaousuarioEntity();
                    //CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
                    //CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
                    //if (ddlColaborador.SelectedValue.Trim() != "")
                    //    eocertuser.idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
                    //if (ddlidcertificado.SelectedValue.Trim() != "")
                    //    eocertuser.idcertificado = Convert.ToInt32(ddlidcertificado.SelectedValue);
                    //if (ddlidregulador.SelectedValue.Trim() != "")
                    //    eocertuser.idregulador = Convert.ToInt32(ddlidregulador.SelectedValue);
                    //// eocertuser.idusuario = idusuario;
                    //if (ddlStatusCertificacao.SelectedValue.Trim() != "")
                    //    eocertuser.status = Convert.ToInt32(ddlStatusCertificacao.SelectedValue);
                    //if (txtvalidade.Text.Trim() != "")
                    //    eocertuser.validade = Convert.ToDateTime(txtvalidade.Text);
                    //if (txtLicenca.Text.Trim() != "")
                    //    eocertuser.licenca = txtLicenca.Text;
                    //if (txtURL.Text.Trim() != "")
                    //    eocertuser.url = txtURL.Text;
                    //if (chkExpira.Checked)
                    //    eocertuser.expira = 1;
                    //else
                    //    eocertuser.expira = 0;
                    //if (txtdtcertificacao.Text.Trim() != "")
                    //    eocertuser.dtcertificacao = Convert.ToDateTime(txtdtcertificacao.Text);

                    //if (hdnId.Value != "")
                    //{
                    //    eocertuser.idcertificacao = Convert.ToInt32(hdnId.Value);
                    //    eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                    //    eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                    //    blcertuser.Alterar(eocertuser);
                    //    if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                    //    {
                    //        eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                    //        eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                    //        eocertdigi.idcertificacao = Convert.ToInt32(hdnId.Value);

                    //        bool blretorno = blcertdigi.Consulta(eocertdigi.idcertificacao);
                    //        if (blretorno == false)
                    //        {
                    //            blcertdigi.Incluir(eocertdigi);
                    //            lblsucesso.Text = "Operação realizada com sucesso!";
                    //        }
                    //        else
                    //        {
                    //            blcertdigi.Alterar(eocertdigi);
                    //            lblsucesso.Text = "Operação realizada com sucesso!";
                    //        }
                    //    }
                    //    if (eocertuser.status == 1)
                    //    {
                    //        CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                    //        eodel.idcertificacao = eocertuser.idcertificacao;
                    //        blProva.ExcluirImagem(eodel);
                    //        foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            blProva.Excluir(item);
                    //            item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    //        }
                    //        foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        List<CertificadoUsuarioProvaEntity> lstProvaExistente = new List<CertificadoUsuarioProvaEntity>();
                    //        CertificadoUsuarioProvaEntity eoProvaExistente = new CertificadoUsuarioProvaEntity();
                    //        eoProvaExistente.idcertificacao = eocertuser.idcertificacao;

                    //        lstProvaExistente = blProva.ConsultarList(eoProvaExistente);

                    //        foreach (CertificadoUsuarioProvaEntity item in lstProvaExistente)
                    //        {
                    //            if (!lstprova.Contains(item))
                    //            {
                    //                item.idcertificacao = eocertuser.idcertificacao;
                    //                blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //                blProva.Excluir(item);
                    //            }
                    //        }

                    //        foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            blProva.Excluir(item);
                    //            item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                    //    eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                    //    eocertuser.idcertificacao = blcertuser.Incluir(eocertuser);
                    //    if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                    //    {
                    //        eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                    //        eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                    //        eocertdigi.idcertificacao = eocertuser.idcertificacao;
                    //        eocertdigi.idcertificadodigitalizado = blcertdigi.Incluir(eocertdigi);
                    //    }
                    //    if (eocertuser.status == 1)
                    //    {
                    //        CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                    //        eodel.idcertificacao = eocertuser.idcertificacao;
                    //        blProva.ExcluirImagem(eodel);
                    //        foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            blProva.Excluir(item);
                    //            item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    //        }
                    //        foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    //        {
                    //            item.idcertificacao = eocertuser.idcertificacao;
                    //            blProva = new CertificadoUsuarioProvaBusinessLayer();
                    //            item.idcertificacao_usuario_prova = blProva.Incluir(item);

                    //        }
                    //    }
                    //}
                    //divsucesso.Visible = true;
                    //lblsucesso.Text = "Operação realizada com sucesso!";
                    //if (Session["Origem"] != null)
                    //{
                    //    Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                    //}
                    //Response.Redirect("CertificacaoColaboradorPesquisar.aspx");

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msg = string.Empty;

            diverro.Visible = false;
            lblmsgerro.Text = msg;

            bool validacao = true;

            GridViewRow row = ((GridViewRow)((System.Web.UI.Control)sender).Parent.Parent);

            AtualizarDTSessao();
            DataKey data = grdpesquisa.DataKeys[row.RowIndex];
            int idprova = Convert.ToInt32(data["idprova"]);
            DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
            int idStatus = -1;
            if (ddlStatus.SelectedValue != "")
                idStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;
            string tipoProva = data["Tipo"].ToString();

            if (txtDtProva.Text == "")
            {
                ddlStatus.SelectedValue = "";
                msg = msg + "Informe a data da prova antes de informar o Status; </br>";
                validacao = false;
            }
            else
            {
                try
                {
                    DateTime dtProva = Convert.ToDateTime(txtDtProva.Text);
                }
                catch (Exception)
                {
                    ddlStatus.SelectedValue = "";
                    msg = msg + "Informe uma data de prova válida antes de informar o status.; </br>";
                    validacao = false;
                }
            }
            if (!validacao)
            {
                diverro.Visible = true;
                lblmsgerro.Text = msg;
            }
            else
            {
                if (idStatus == 3 || idStatus == 2)
                {
                    //AtualizarDTSessao();
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["Grid"];
                    DataRow linha = dt.NewRow();
                    linha["selecionado"] = "False"; //(row.FindControl("chkSelecionado") as CheckBox).Checked;
                    linha["idprova"] = data["idprova"];
                    linha["Nome"] = data["Nome"].ToString();
                    linha["Alias"] = data["Alias"].ToString();
                    linha["Tipo"] = data["Tipo"].ToString();
                    linha["idlinha"] = grdpesquisa.Rows.Count + 1;

                    int idpro = Convert.ToInt32(data["idprova"]);
                    string prova = "idprova = " + idpro.ToString();
                    DataRow[] find = dt.Select(prova);

                    //int numLinha = int.MinValue;
                    int qtdVoucher = 1;
                    int numrep = find.Count();
                    if (txtqtdvoucher.Text != "")
                    {
                        qtdVoucher = Convert.ToInt32(txtqtdvoucher.Text);
                    }
                    if (numrep < qtdVoucher || qtdVoucher == int.MinValue)
                    {
                        dt.Rows.Add(linha);
                        Session["Grid"] = dt;
                    }

                    grdpesquisa.DataSource = dt;
                    grdpesquisa.DataBind();

                    AtualizarDTSessao();
                }
            }
            AtualizarStatusCertificacao();
        }

        protected void chkExpira_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExpira.Checked)
            {
                txtvalidade.Text = "";
                txtvalidade.Enabled = false;
            }
            else
            {
                txtvalidade.Enabled = true;
            }
        }

        protected void txtdtcertificacao_TextChanged(object sender, EventArgs e)
        {
            if (txtdtcertificacao.Text == "" || ddlidcertificado.SelectedValue == "")
            {
                txtvalidade.Text = "";
                chkExpira.Checked = false;
            }
            else
            {
                DateTime dtvalidade = DateTime.MinValue;
                try
                {
                    dtvalidade = Convert.ToDateTime(txtdtcertificacao.Text);

                    CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                    CertificadoEntity eo = new CertificadoEntity();

                    eo = bl.Obter(Convert.ToInt32(ddlidcertificado.SelectedValue));
                    if (eo != null)
                    {
                        if (eo.naoexpira == EOConst.CodStatusExpirado.Naoexpira)
                        {
                            chkExpira.Checked = true;
                            txtvalidade.Text = "";
                            txtvalidade.Enabled = false;
                        }
                        else
                        {
                            if (eo.qtddiavalidade != int.MinValue)
                                dtvalidade = dtvalidade.AddDays(Convert.ToInt32(eo.qtddiavalidade));
                            if (eo.qtdmesvalidade != int.MinValue)
                                dtvalidade = dtvalidade.AddMonths(Convert.ToInt32(eo.qtdmesvalidade));
                            if (eo.qtdanovalidade != int.MinValue)
                                dtvalidade = dtvalidade.AddYears(Convert.ToInt32(eo.qtdanovalidade));

                            txtvalidade.Text = dtvalidade.ToString("dd/MM/yyyy");
                            txtvalidade.Enabled = true;
                        }
                        if (eo.voucher == EOConst.CodStatusVoucher.ComVoucher)
                        {
                            chkVoucher.Checked = true;
                            txtqtdvoucher.Text = eo.qtdvoucher.ToString();
                        }
                    }

                }
                catch
                {
                }
            }
        }

        protected void chkVoucher_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void grdpesquisa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(grdpesquisa.DataKeys[e.RowIndex].Value.ToString());

        }

        protected void lnkEnviarAprovacao_Click(object sender, EventArgs e)
        {
            if (ValidarDadosAprovacao() == true)
            {
                btnEnviarAprovacao.Visible = true;
                btnSalvarAprovacao.Visible = false;
                btnEnviarSalvarAprovacao.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
            }
        }

        protected void btnEnviarAprovacao_Click(object sender, EventArgs e)
        {
            try
            {
                List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
                List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
                CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
                string msgvalidacao = "";
                bool valido = true;
                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                if (chkAceite.Checked == false)
                {
                    msgvalidacao = msgvalidacao + "Você deve ler e concordar com o termo de aceite para prosseguir; <br/>";
                    valido = false;
                }

                if (!valido)
                {
                    divErroModal.Visible = true;
                    lblErroModal.Text = msgvalidacao;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
                    return;
                }
                else
                {
                    foreach (GridViewRow row in grdpesquisa.Rows)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                        if (chkbox.Checked)
                        {
                            string msg = "Mensagem: Linha " + (row.RowIndex + 1) + ": </br>";
                            // bool validacao = true;
                            CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                            CertificadoUsuarioProvaEntityImagem eoImagem = new CertificadoUsuarioProvaEntityImagem();

                            DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                            eoProva.idprova_certificado = Convert.ToInt32(data["idprova"]);
                            eoImagem.idprova = Convert.ToInt32(data["idprova"]);
                            TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;

                            eoProva.dtprova = Convert.ToDateTime(txtDtProva.Text);
                            eoImagem.dtprova = Convert.ToDateTime(txtDtProva.Text);

                            DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                            eoProva.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            eoImagem.status = Convert.ToInt32(ddlStatus.SelectedValue);


                            eoProva.dtinclusao = DateTime.Now;

                            string nomeProva = data["Nome"].ToString();
                            string aliasProva = data["Alias"].ToString();
                            string TipoProva = data["Tipo"].ToString();
                            if (TipoProva == "Requerida")
                            {
                                eoImagem.requerida = EOConst.CodStatus.Ativo;
                            }
                            else
                            {
                                eoImagem.obrigatoria = EOConst.CodStatus.Ativo;
                            }
                            eoImagem.nome = nomeProva;
                            eoImagem.alias = aliasProva;

                            lstprova.Add(eoProva);
                            lstImagem.Add(eoImagem);

                        }
                    }

                    CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();
                    CertificacaousuarioEntity eocertuser = new CertificacaousuarioEntity();
                    CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
                    CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
                    eocertuser.aprovacao = EOConst.CodStatusAprovacao.Pendente;
                    eocertuser.motivoreprovacao = "";
                    eocertuser.idreprovador = ((UsuarioEntity)Session["eoUs"]).idusuario;
                    if (ddlColaborador.SelectedValue.Trim() != "")
                        eocertuser.idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
                    if (ddlidcertificado.SelectedValue.Trim() != "")
                        eocertuser.idcertificado = Convert.ToInt32(ddlidcertificado.SelectedValue);
                    if (ddlidregulador.SelectedValue.Trim() != "")
                        eocertuser.idregulador = Convert.ToInt32(ddlidregulador.SelectedValue);
                    // eocertuser.idusuario = idusuario;
                    if (ddlStatusCertificacao.SelectedValue.Trim() != "")
                        eocertuser.status = Convert.ToInt32(ddlStatusCertificacao.SelectedValue);
                    if (txtvalidade.Text.Trim() != "")
                        eocertuser.validade = Convert.ToDateTime(txtvalidade.Text);
                    if (txtLicenca.Text.Trim() != "")
                        eocertuser.licenca = txtLicenca.Text;
                    if (txtURL.Text.Trim() != "")
                        eocertuser.url = txtURL.Text;
                    if (chkExpira.Checked)
                        eocertuser.expira = 1;
                    else
                        eocertuser.expira = 0;
                    if (chkAceite.Checked)
                        eocertuser.aceite = 1;
                    else
                        eocertuser.aceite = 0;
                    if (txtdtcertificacao.Text.Trim() != "")
                        eocertuser.dtcertificacao = Convert.ToDateTime(txtdtcertificacao.Text);

                    if (hdnId.Value != "")
                    {
                        eocertuser.idcertificacao = Convert.ToInt32(hdnId.Value);
                        eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        blcertuser.Alterar(eocertuser);
                        if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                        {
                            eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                            eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                            eocertdigi.idcertificacao = Convert.ToInt32(hdnId.Value);

                            bool blretorno = blcertdigi.Consulta(eocertdigi.idcertificacao);
                            if (blretorno == false)
                            {
                                blcertdigi.Incluir(eocertdigi);
                                lblsucesso.Text = "Operação realizada com sucesso!";
                            }
                            else
                            {
                                blcertdigi.Alterar(eocertdigi);
                                lblsucesso.Text = "Operação realizada com sucesso!";
                            }
                        }
                        if (eocertuser.status == 1)
                        {
                            CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                            eodel.idcertificacao = eocertuser.idcertificacao;
                            blProva.ExcluirImagem(eodel);
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                            foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                            }
                        }
                        else
                        {
                            List<CertificadoUsuarioProvaEntity> lstProvaExistente = new List<CertificadoUsuarioProvaEntity>();
                            CertificadoUsuarioProvaEntity eoProvaExistente = new CertificadoUsuarioProvaEntity();
                            eoProvaExistente.idcertificacao = eocertuser.idcertificacao;

                            lstProvaExistente = blProva.ConsultarList(eoProvaExistente);

                            foreach (CertificadoUsuarioProvaEntity item in lstProvaExistente)
                            {
                                if (!lstprova.Contains(item))
                                {
                                    item.idcertificacao = eocertuser.idcertificacao;
                                    blProva = new CertificadoUsuarioProvaBusinessLayer();
                                    blProva.Excluir(item);
                                }
                            }

                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                        }

                    }
                    else
                    {
                        eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eocertuser.idcertificacao = blcertuser.Incluir(eocertuser);
                        if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                        {
                            eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                            eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                            eocertdigi.idcertificacao = eocertuser.idcertificacao;
                            eocertdigi.idcertificadodigitalizado = blcertdigi.Incluir(eocertdigi);
                        }
                        if (eocertuser.status == 1)
                        {
                            CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                            eodel.idcertificacao = eocertuser.idcertificacao;
                            blProva.ExcluirImagem(eodel);
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                            foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                            }
                        }
                        else
                        {
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);

                            }
                        }
                    }
                    divsucesso.Visible = true;
                    lblsucesso.Text = "Operação realizada com sucesso!";
                    Response.Redirect("PesquisarMinhasCertificacoes.aspx");

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

        }

        protected void btnSalvarAprovacao_Click(object sender, EventArgs e)
        {
            try
            {
                List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
                List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
                CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
                string msgvalidacao = "";
                bool valido = true;
                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                //if (chkAceite.Checked == false)
                //{
                //    msgvalidacao = msgvalidacao + "Você deve ler e concordar com o termo de aceite para prosseguir; <br/>";
                //    valido = false;
                //}

                if (!valido)
                {
                    divErroModal.Visible = true;
                    lblErroModal.Text = msgvalidacao;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
                    return;
                }
                else
                {

                    //Salvar o registro no banco de dados
                    this.Salvar();

                    divsucesso.Visible = true;
                    lblsucesso.Text = "Operação realizada com sucesso!";

                    if (Session["Origem"] != null)
                    {
                        if ((String)Session["Origem"] == "MinhasCertificacoes")
                        {
                            Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                        }
                    }
                    else if (Session["Origem"] == null && Session["eoMinhaCertificacaousuario"] == null)
                    {
                        Response.Redirect("CertificacaoColaboradorPesquisar.aspx");
                    }
                    else
                    {
                        Response.Redirect("PesquisarMinhasCertificacoes.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        protected void btnEnviarSalvarAprovacao_Click(object sender, EventArgs e)
        {

            try
            {
                List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
                List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
                CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
                string msgvalidacao = "";
                bool valido = true;
                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                if (chkAceite.Checked == false)
                {
                    msgvalidacao = msgvalidacao + "Você deve ler e concordar com o termo de aceite para prosseguir; <br/>";
                    valido = false;
                }

                if (!valido)
                {
                    divErroModal.Visible = true;
                    lblErroModal.Text = msgvalidacao;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#myModal').hide();", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
                    return;
                }
                else
                {

                    //Salvar o registro no banco de dados
                    //     this.Salvar();

                    foreach (GridViewRow row in grdpesquisa.Rows)
                    {
                        CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                        if (chkbox.Checked)
                        {
                            string msg = "Mensagem: Linha " + (row.RowIndex + 1) + ": </br>";
                            // bool validacao = true;
                            CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                            CertificadoUsuarioProvaEntityImagem eoImagem = new CertificadoUsuarioProvaEntityImagem();

                            DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                            eoProva.idprova_certificado = Convert.ToInt32(data["idprova"]);
                            eoImagem.idprova = Convert.ToInt32(data["idprova"]);
                            TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;

                            eoProva.dtprova = Convert.ToDateTime(txtDtProva.Text);
                            eoImagem.dtprova = Convert.ToDateTime(txtDtProva.Text);

                            DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                            eoProva.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            eoImagem.status = Convert.ToInt32(ddlStatus.SelectedValue);


                            eoProva.dtinclusao = DateTime.Now;

                            string nomeProva = data["Nome"].ToString();
                            string aliasProva = data["Alias"].ToString();
                            string TipoProva = data["Tipo"].ToString();
                            if (TipoProva == "Requerida")
                            {
                                eoImagem.requerida = EOConst.CodStatus.Ativo;
                            }
                            else
                            {
                                eoImagem.obrigatoria = EOConst.CodStatus.Ativo;
                            }
                            eoImagem.nome = nomeProva;
                            eoImagem.alias = aliasProva;

                            lstprova.Add(eoProva);
                            lstImagem.Add(eoImagem);

                        }
                    }

                    CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();
                    CertificacaousuarioEntity eocertuser = new CertificacaousuarioEntity();
                    CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
                    CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
                    eocertuser.aprovacao = EOConst.CodStatusAprovacao.Pendente;
                    eocertuser.motivoreprovacao = "";
                    eocertuser.idreprovador = ((UsuarioEntity)Session["eoUs"]).idusuario;
                    if (ddlColaborador.SelectedValue.Trim() != "")
                        eocertuser.idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
                    if (ddlidcertificado.SelectedValue.Trim() != "")
                        eocertuser.idcertificado = Convert.ToInt32(ddlidcertificado.SelectedValue);
                    if (ddlidregulador.SelectedValue.Trim() != "")
                        eocertuser.idregulador = Convert.ToInt32(ddlidregulador.SelectedValue);
                    // eocertuser.idusuario = idusuario;
                    if (ddlStatusCertificacao.SelectedValue.Trim() != "")
                        eocertuser.status = Convert.ToInt32(ddlStatusCertificacao.SelectedValue);
                    if (txtvalidade.Text.Trim() != "")
                        eocertuser.validade = Convert.ToDateTime(txtvalidade.Text);
                    if (txtLicenca.Text.Trim() != "")
                        eocertuser.licenca = txtLicenca.Text;
                    if (txtURL.Text.Trim() != "")
                        eocertuser.url = txtURL.Text;
                    if (chkExpira.Checked)
                        eocertuser.expira = 1;
                    else
                        eocertuser.expira = 0;
                    if (chkAceite.Checked)
                        eocertuser.aceite = 1;
                    else
                        eocertuser.aceite = 0;
                    if (txtdtcertificacao.Text.Trim() != "")
                        eocertuser.dtcertificacao = Convert.ToDateTime(txtdtcertificacao.Text);

                    if (hdnId.Value != "")
                    {
                        eocertuser.idcertificacao = Convert.ToInt32(hdnId.Value);
                        eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        blcertuser.Alterar(eocertuser);
                        if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                        {
                            eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                            eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                            eocertdigi.idcertificacao = Convert.ToInt32(hdnId.Value);

                            bool blretorno = blcertdigi.Consulta(eocertdigi.idcertificacao);
                            if (blretorno == false)
                            {
                                blcertdigi.Incluir(eocertdigi);
                                lblsucesso.Text = "Operação realizada com sucesso!";
                            }
                            else
                            {
                                blcertdigi.Alterar(eocertdigi);
                                lblsucesso.Text = "Operação realizada com sucesso!";
                            }
                        }
                        if (eocertuser.status == 1)
                        {
                            CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                            eodel.idcertificacao = eocertuser.idcertificacao;
                            blProva.ExcluirImagem(eodel);
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                            foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                            }
                        }
                        else
                        {
                            List<CertificadoUsuarioProvaEntity> lstProvaExistente = new List<CertificadoUsuarioProvaEntity>();
                            CertificadoUsuarioProvaEntity eoProvaExistente = new CertificadoUsuarioProvaEntity();
                            eoProvaExistente.idcertificacao = eocertuser.idcertificacao;

                            lstProvaExistente = blProva.ConsultarList(eoProvaExistente);

                            foreach (CertificadoUsuarioProvaEntity item in lstProvaExistente)
                            {
                                if (!lstprova.Contains(item))
                                {
                                    item.idcertificacao = eocertuser.idcertificacao;
                                    blProva = new CertificadoUsuarioProvaBusinessLayer();
                                    blProva.Excluir(item);
                                }
                            }

                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                        }

                    }
                    else
                    {
                        eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                        eocertuser.idcertificacao = blcertuser.Incluir(eocertuser);
                        if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                        {
                            eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                            eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                            eocertdigi.idcertificacao = eocertuser.idcertificacao;
                            eocertdigi.idcertificadodigitalizado = blcertdigi.Incluir(eocertdigi);
                        }
                        if (eocertuser.status == 1)
                        {
                            CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                            eodel.idcertificacao = eocertuser.idcertificacao;
                            blProva.ExcluirImagem(eodel);
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                blProva.Excluir(item);
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);
                            }
                            foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                            }
                        }
                        else
                        {
                            foreach (CertificadoUsuarioProvaEntity item in lstprova)
                            {
                                item.idcertificacao = eocertuser.idcertificacao;
                                blProva = new CertificadoUsuarioProvaBusinessLayer();
                                item.idcertificacao_usuario_prova = blProva.Incluir(item);

                            }
                        }
                    }
                    divsucesso.Visible = true;
                    lblsucesso.Text = "Operação realizada com sucesso!";
                    Response.Redirect("PesquisarMinhasCertificacoes.aspx");

                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

        }

        protected void lnkAprovar_Click(object sender, EventArgs e)
        {
            CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
            CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
            eo.idcertificacao = Convert.ToInt32(hdnId.Value);
            eo.idreprovador = ((UsuarioEntity)Session["eoUs"]).idusuario;
            eo.aprovacao = EOConst.CodStatusAprovacao.Aprovado;
            eo.motivoreprovacao = "Aprovado por: " + ((UsuarioEntity)Session["eoUs"]).nome;
            bl.AlterarStatusAprovacao(eo);
            Response.Redirect("CertificacaoColaboradorAprovar.aspx");
        }

        protected void lnkReprovar_Click(object sender, EventArgs e)
        {
            string msgvalidacao = "";
            bool valido = true;
            if (txtmotivo.Text == "")
            {
                msgvalidacao = msgvalidacao + "Motivo da reprovação não informado; <br/>";
                valido = false;
            }
            if (!valido)
            {
                diverro.Visible = true;
                lblmsgerro.Text = msgvalidacao;
            }
            else
            {
                CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                int idUsuario = ((UsuarioEntity)Session["eoUs"]).idusuario;
                eo.idcertificacao = Convert.ToInt32(hdnId.Value);
                eo.aprovacao = EOConst.CodStatusAprovacao.Reprovado;
                eo.motivoreprovacao = "Reprovado por: " + ((UsuarioEntity)Session["eoUs"]).nome + " - " + txtmotivo.Text;
                eo.idreprovador = ((UsuarioEntity)Session["eoUs"]).idusuario;
                bl.AlterarStatusAprovacao(eo);
                Response.Redirect("CertificacaoColaboradorAprovar.aspx");
            }

        }

        #endregion

        #region RotinasFormulario

        private void DesabilitarEnviarAprovacao()
        {
            lnkEnviarAprovacao.Enabled = false;
            lnkEnviarAprovacao.CssClass = "btn btn-info disabled";
        }

        private void HabilitarEnviarAprovacao()
        {
            lnkEnviarAprovacao.Enabled = true;
            lnkEnviarAprovacao.CssClass = "btn btn-info";
        }

        //public void CarregarComboColaborador()
        //{

        //    ddlColaborador.Items.Clear();
        //    DataTable dtcolaborador = new DataTable();
        //    if (Session["dtcombocolaborador"] == null)
        //    {
        //        UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
        //        UsuarioEntity eo = new UsuarioEntity();
        //        eo.status = EOConst.CodStatusUsuario.Ativo;
        //        dtcolaborador = bl.Consultar(eo);
        //        Session["dtcombocolaborador"] = dtcolaborador;
        //    }
        //    else
        //    {
        //        dtcolaborador = ((DataTable)Session["dtcombocolaborador"]);
        //    }
        //    ddlColaborador.DataSource = dtcolaborador;
        //    ddlColaborador.DataBind();
        //    if (ddlColaborador.Items.Count > 0)
        //    {
        //        ddlColaborador.Items.Insert(0, new ListItem("", ""));
        //    }
        //    else
        //    {
        //        ddlColaborador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        //    }

        //}

        public void CarregarComboColaborador()
        {

            // ddlColaborador.Items.Clear();
            DataTable dtcolaborador = new DataTable();
            if (Session["dtcombocolaborador"] == null)
            {
                UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                UsuarioEntity eo = new UsuarioEntity();
                eo.status = EOConst.CodStatusUsuario.Ativo;
                dtcolaborador = bl.Consultar(eo);
                Session["dtcombocolaborador"] = dtcolaborador;
            }
            else
            {
                dtcolaborador = ((DataTable)Session["dtcombocolaborador"]);
            }

            ddlColaborador.Items.Clear();
            ListItem itemCliente = new ListItem();

            Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
            radComboBoxItem.Value = "";
            radComboBoxItem.Text = "";
            ddlColaborador.Items.Add(radComboBoxItem);

            if (dtcolaborador != null)
            {
                if (dtcolaborador.Rows != null)
                {
                    if (dtcolaborador.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtcolaborador.Rows)
                        {
                            radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
                            radComboBoxItem.Value = dr["idusuario"].ToString();
                            radComboBoxItem.Text = dr["dscombo"].ToString();
                            ddlColaborador.Items.Add(radComboBoxItem);
                        }

                    }
                }
            }

            ddlColaborador.AllowCustomText = true;
            ddlColaborador.MarkFirstMatch = true;

            ddlColaborador.SelectedValue = "";

            //ddlColaborador.DataSource = dtcolaborador;
            //ddlColaborador.DataBind();
            //if (ddlColaborador.Items.Count > 0)
            //{
            //    ddlColaborador.Items.Insert(0, new ListItem("", ""));
            //}
            //else
            //{
            //    ddlColaborador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
            //}

        }

        private void CarregarDados()
        {
            try
            {
                CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                CertificacaousuarioEntity eo = bl.Obter(Convert.ToInt32(Session["eoMinhaCertificacaousuario"]));
                hdnId.Value = eo.idcertificacao.ToString();
                CarregarComboCertificadoSelecionadoEativo(eo.idcertificado);
                CertificadoBusinessLayer certbl = new CertificadoBusinessLayer();
                CertificadoEntity eocert = certbl.Obter(eo.idcertificado);

                int idUsuario = int.MinValue;
                int idCertificacao = int.MinValue;

                if (Session["eoUs"] != null)
                {
                    if ((UsuarioEntity)Session["eoUs"] != null)
                    {
                        idUsuario = ((UsuarioEntity)Session["eoUs"]).idusuario;
                    }
                }

                if (eo.idcertificacao != null)
                {
                    idCertificacao = eo.idcertificacao;
                }

                if (idCertificacao > int.MinValue && idUsuario > int.MinValue)
                {
                    bool blnRetorno = VerificarRegistrosAprovacao(idUsuario, idCertificacao);
                }

                if ((String)Session["Origem"] == "MinhasCertificacoes")
                {
                    //chkAceite.Visible = true;
                    //lblAceite.Visible = true;
                    //lnkEnviarAprovacao.Visible = true;
                    txtmotivo.Enabled = false;
                    divmotivo.Visible = true;
                    txtmotivo.Visible = true;
                    //if (eo.motivoreprovacao == null || eo.motivoreprovacao=="")
                    //{
                    //    Div3.Visible = false;
                    //}
                    // Session.Remove("Origem");

                }
                else if ((String)Session["Aprovacao"] == "Aprovacao")
                {
                    DesabilitarEnviarAprovacao();
                    divmotivo.Visible = true;
                    txtmotivo.Visible = true;
                    lnkAprovar.Visible = true;
                    lnkReprovar.Visible = true;
                    lnksalvar.Visible = false;
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = false;
                    txtLicenca.Enabled = false;
                    txtURL.Enabled = false;
                    txtdtcertificacao.Enabled = false;
                    txtvalidade.Enabled = false;
                    chkExpira.Enabled = false;
                    //   DesabilitarRegistrosPendentesAprovacao();
                }
                else
                {
                    divmotivo.Visible = false;
                    txtmotivo.Visible = false;
                    lnkAprovar.Visible = false;
                    lnkReprovar.Visible = false;
                    lnksalvar.Visible = true;
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = false;
                    txtLicenca.Enabled = false;
                    txtURL.Enabled = false;
                    txtdtcertificacao.Enabled = false;
                    txtvalidade.Enabled = false;
                    chkExpira.Enabled = false;
                    Div3.Visible = false;
                }
                if (eocert.voucher != null && eocert.voucher != 0)
                {
                    chkVoucher.Checked = true;
                    txtqtdvoucher.Text = eocert.qtdvoucher.ToString();
                }
                ddlidcertificado.SelectedValue = eo.idcertificado.ToString();
                CarregarComboReguladorSelecionadoEativo(eo.idregulador);
                ddlidregulador.SelectedValue = eo.idregulador.ToString();
                txtLicenca.Text = eo.licenca;
                txtURL.Text = eo.url;
                txtmotivo.Text = eo.motivoreprovacao;
                if (eo.aceite == 1)
                {
                    chkAceite.Checked = true;
                }

                if (eo.expira == 1)
                {
                    chkExpira.Checked = true;
                    txtvalidade.Text = "";
                }
                else
                {
                    chkExpira.Checked = false;
                    if (eo.validade > DateTime.MinValue)
                        txtvalidade.Text = eo.validade.ToString("dd/MM/yyyy");
                }

                UsuarioBusinessLayer blusu = new UsuarioBusinessLayer();
                UsuarioEntity eousu = new UsuarioEntity();
                eousu = blusu.Obter(eo.idusuario);
                ddlColaborador.SelectedValue = eousu.idusuario.ToString();

                if (eousu != null)
                {
                    if (!string.IsNullOrEmpty((eousu.idusuario.ToString())))
                    {
                        if (idCertificacao > int.MinValue)
                        {
                            bool blnRetorno = VerificarRegistrosAprovacao(eousu.idusuario, idCertificacao);
                        }
                    }
                }



                //CertificadoBusinessLayer blcert = new CertificadoBusinessLayer();
                //CertificadoEntity eocert = new CertificadoEntity();
                //eocert = blcert.Obter(eo.idcertificado);
                //ddlgrupo.SelectedValue = eocert.idgrupo.ToString();

                ddlStatusCertificacao.SelectedValue = eo.status.ToString();
                hdnStatusOrigilnal.Value = eo.status.ToString();

                //bloqueio dos campos quando a certificacao for concluida - solicitacao Emerson 09/11/2016

                if (eo.status == 1)
                {
                    //divcertificado
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = true;
                    txtLicenca.Enabled = true;
                    txtURL.Enabled = true;
                    txtdtcertificacao.Enabled = true;
                    txtvalidade.Enabled = true;
                    chkExpira.Enabled = true;
                    HabilitarEnviarAprovacao();

                    //bool blnRetorno = VerificarRegistrosAprovacao(eo.idcertificacao);
                    //if (blnRetorno == false)
                    //{
                    //    DesabilitarEnviarAprovacao();
                    //    DesabilitarRegistrosPendentesAprovacao();
                    //}
                    //else
                    //{
                    //    HabilitarEnviarAprovacao();
                    //}
                }

                if (eo.dtcertificacao > DateTime.MinValue)
                    txtdtcertificacao.Text = eo.dtcertificacao.ToString("dd/MM/yyyy");

                CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
                CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
                eocertdigi.idcertificacao = eo.idcertificacao;
                DataTable dt = new DataTable();
                dt = blcertdigi.ConsultarArquivo(eocertdigi);
                if (dt.Rows.Count > 0)
                {
                    Session["ArquivoDigitalizado"] = (byte[])dt.Rows[0]["arquivodigitalizado"];
                    Session["NomeArquivoDigitalizado"] = dt.Rows[0]["arquivo"].ToString();
                    hdnpossuianexo.Value = "1";


                    divanexolink.Visible = true;
                    divanexarlinkcertificado.Visible = false;
                    //UploadCertificacao.FileName = dt.Rows[0]["arquivo"].ToString();
                }
                else
                {/*
                    hdnpossuianexo.Value = "";
                    divanexolink.Visible = false;
                    divanexarlinkcertificado.Visible = true;
                    */
                }

                if (eo.status == 1)
                {
                    CarregaGridEditarConcluido(eo.idcertificacao);
                }
                else
                {
                    CarregaGridEditar(eo.idcertificacao);
                }

                if (((UsuarioEntity)Session["eoUs"]).idperfil != 1 && ddlStatusCertificacao.SelectedValue == EOConst.CodStatusCertificacao.Concluida.ToString())
                {
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = false;
                    txtLicenca.Enabled = false;
                    txtURL.Enabled = false;
                    txtdtcertificacao.Enabled = false;
                    txtvalidade.Enabled = false;
                    chkExpira.Enabled = false;
                    chkVoucher.Enabled = false;
                    txtqtdvoucher.Enabled = false;
                    ddlStatusCertificacao.Enabled = false;
                    divanexarlinkcertificado.Visible = false;

                }
                else if (((UsuarioEntity)Session["eoUs"]).idperfil != 1 && ddlStatusCertificacao.SelectedValue == EOConst.CodStatusCertificacao.Em_Andamento.ToString())
                {
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = true;
                    txtLicenca.Enabled = true;
                    txtURL.Enabled = true;
                    txtdtcertificacao.Enabled = true;
                    txtvalidade.Enabled = true;
                    chkExpira.Enabled = true;
                    chkVoucher.Enabled = false;
                    txtqtdvoucher.Enabled = false;
                    ddlStatusCertificacao.Enabled = false;
                    divanexarlinkcertificado.Visible = true;
                }


                if (((UsuarioEntity)Session["eoUs"]).idperfil == 1)
                {
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = true;
                    txtLicenca.Enabled = true;
                    txtURL.Enabled = true;
                    txtdtcertificacao.Enabled = true;
                    txtvalidade.Enabled = true;
                    chkExpira.Enabled = true;
                    chkVoucher.Enabled = false;
                    txtqtdvoucher.Enabled = false;
                    ddlStatusCertificacao.Enabled = false;
                    divanexarlinkcertificado.Visible = true;
                    lnksalvar.Visible = true;
                }

                if (eo.status == EOConst.CodStatusCertificacao.Concluida && eo.aprovacao == EOConst.CodStatusAprovacao.Reprovado)
                {
                    ddlColaborador.Enabled = false;
                    ddlidcertificado.Enabled = false;
                    ddlidregulador.Enabled = true;
                    txtLicenca.Enabled = true;
                    txtURL.Enabled = true;
                    txtdtcertificacao.Enabled = true;
                    txtvalidade.Enabled = true;
                    chkExpira.Enabled = true;
                    chkVoucher.Enabled = false;
                    txtqtdvoucher.Enabled = false;
                    ddlStatusCertificacao.Enabled = false;
                    divanexarlinkcertificado.Visible = true;
                    lnksalvar.Visible = true;
                    btnPreencherProva.Visible = true;
                    Session["recarregarSessao"] = Session["eoMinhaCertificacaousuario"];
                }
                else
                {
                    Session["recarregarSessao"] = null;
                }


            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private bool VerificarRegistrosAprovacao(int idUsuario, int idcertificacao)
        {
            CertificacaousuarioBusinessLayer objCertificacaousuarioBusinessLayer = new CertificacaousuarioBusinessLayer();
            DataTable qtdRegistros = objCertificacaousuarioBusinessLayer.ConsultarRegistrosPendenteAprovacao(idUsuario, idcertificacao);

            if (qtdRegistros != null)
            {
                if (qtdRegistros.Rows != null)
                {
                    if (qtdRegistros.Rows.Count > 0)
                    {
                        HabilitarRegistrosPendentesAprovacao();
                        return true;
                    }
                }
            }

            return false;
        }

        private string VerificarRegistrosAprovacao(int idcertificacao)
        {
            CertificacaousuarioBusinessLayer objCertificacaousuarioBusinessLayer = new CertificacaousuarioBusinessLayer();
            DataTable qtdRegistros = objCertificacaousuarioBusinessLayer.ConsultarRegistrosAprovados(idcertificacao);
            string status = string.Empty;

            if (qtdRegistros != null)
            {
                if (qtdRegistros.Rows != null)
                {
                    if (qtdRegistros.Rows.Count > 0)
                    {
                        foreach (DataRow dr in qtdRegistros.Rows)
                        {
                            status = dr["aprovacao"].ToString();
                            return status;
                        }
                    }
                }
            }

            return status;
        }

        private void DownloadPDF(string idcertificacao)
        {
            //CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
            //CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
            //eocertdigi.idcertificacao = Convert.ToInt32(idcertificacao);
            //DataTable dt = new DataTable();
            //dt = blcertdigi.ConsultarArquivo(eocertdigi);

            byte[] dbbyte;
            //dbbyte = (byte[])dt.Rows[0]["arquivodigitalizado"];
            //string nomeArquivo = dt.Rows[0]["arquivo"].ToString();
            dbbyte = (byte[])Session["ArquivoDigitalizado"];
            string nomeArquivo = Session["NomeArquivoDigitalizado"].ToString();

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

        private void CarregarComboRegulador()
        {
            ddlidregulador.Items.Clear();
            ReguladorBusinessLayer blRegulador = new ReguladorBusinessLayer();
            ReguladorEntity eoRegulador = new ReguladorEntity();
            ddlidregulador.DataSource = blRegulador.Consultar(eoRegulador);
            ddlidregulador.DataTextField = "nome";
            ddlidregulador.DataValueField = "idregulador";
            ddlidregulador.DataBind();
            if (ddlidregulador.Items.Count > 0)
                ddlidregulador.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidregulador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregarComboReguladorAtivo()
        {
            ddlidregulador.Items.Clear();
            ReguladorBusinessLayer blRegulador = new ReguladorBusinessLayer();
            ReguladorEntity eoRegulador = new ReguladorEntity();
            eoRegulador.status = EOConst.CodStatus.Ativo;
            ddlidregulador.DataSource = blRegulador.ConsultarAtivo(eoRegulador);
            ddlidregulador.DataTextField = "nome";
            ddlidregulador.DataValueField = "idregulador";
            ddlidregulador.DataBind();
            if (ddlidregulador.Items.Count > 0)
                ddlidregulador.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidregulador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregarComboReguladorSelecionadoEativo(int idRegulador)
        {
            ddlidregulador.Items.Clear();
            ReguladorBusinessLayer blRegulador = new ReguladorBusinessLayer();
            ReguladorEntity eoRegulador = new ReguladorEntity();
            eoRegulador.idregulador = idRegulador;
            ddlidregulador.DataSource = blRegulador.ConsultarReguladorSelecionadoEativo(eoRegulador);
            ddlidregulador.DataTextField = "nome";
            ddlidregulador.DataValueField = "idregulador";
            ddlidregulador.DataBind();
            if (ddlidregulador.Items.Count > 0)
                ddlidregulador.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidregulador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregarComboCertificado()
        {
            ddlidcertificado.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            //if (ddlgrupo.SelectedValue != "")
            //{
            //    eoCertificado.idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
            //}
            eoCertificado.status = EOConst.CodStatus.Ativo;
            ddlidcertificado.DataSource = blCertificado.ConsultarAtivo(eoCertificado);
            ddlidcertificado.DataTextField = "dscombo";
            ddlidcertificado.DataValueField = "idcertificado";
            ddlidcertificado.DataBind();
            if (ddlidcertificado.Items.Count > 0)
                ddlidcertificado.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidcertificado.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregarComboCertificadoAtivo()
        {
            ddlidcertificado.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            //if (ddlgrupo.SelectedValue != "")
            //{
            //    eoCertificado.idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
            //}
            eoCertificado.status = EOConst.CodStatus.Ativo;
            ddlidcertificado.DataSource = blCertificado.ConsultarAtivo(eoCertificado);
            ddlidcertificado.DataTextField = "dscombo";
            ddlidcertificado.DataValueField = "idcertificado";
            ddlidcertificado.DataBind();
            if (ddlidcertificado.Items.Count > 0)
                ddlidcertificado.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidcertificado.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregarComboCertificadoSelecionadoEativo(int idCertificado)
        {
            ddlidcertificado.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            //if (ddlgrupo.SelectedValue != "")
            //{
            //    eoCertificado.idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
            //}
            //eoCertificado.status = EOConst.CodStatus.Ativo;
            eoCertificado.idcertificado = idCertificado;
            ddlidcertificado.DataSource = blCertificado.ConsultarSelecionadoEativo(eoCertificado);
            ddlidcertificado.DataTextField = "dscombo";
            ddlidcertificado.DataValueField = "idcertificado";
            ddlidcertificado.DataBind();
            if (ddlidcertificado.Items.Count > 0)
                ddlidcertificado.Items.Insert(0, new ListItem("Selecione", ""));
            else
                ddlidcertificado.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }

        private void CarregaGridEditar(int idCertificacao)
        {
            try
            {
                //Desabilita o botão de envio para aprovação
                DesabilitarEnviarAprovacao();
                bool btnEnviarCertificacao = false;

                Session.Remove("Grid");
                divInfoProva.Visible = false;

                CertificadoUsuarioProvaBusinessLayer blprova = new CertificadoUsuarioProvaBusinessLayer();
                CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                eoProva.idcertificacao = idCertificacao;


                DataTable lista = blprova.Consultar(eoProva);
                lista.Columns.Add("selecionado");
                lista.Columns.Add("idlinha");
                Session["Grid"] = lista;

                //Verificar se o status da certificação é igual concluída para habilitar o botão de enviar para aprovação
                if (ddlStatusCertificacao.SelectedValue != null)
                {
                    if (!string.IsNullOrEmpty(ddlStatusCertificacao.SelectedValue))
                    {
                        if (ddlStatusCertificacao.SelectedValue == "1")
                        {
                            btnEnviarCertificacao = true;
                            HabilitarEnviarAprovacao();
                        }
                    }
                }

                // ddlStatusCertificacao
                if (lista.Rows.Count == 0)
                {
                    divInfoProva.Visible = true;
                    lblmsInfoProva.Text = "Não existe provas para o certificado selecionado!";
                    grdpesquisa.DataSource = null;
                    grdpesquisa.DataBind();
                }
                else
                {
                    divInfoProva.Visible = false;

                    //Verificar se o botão enviar para aprovação deve estar habilitado ou não
                    if (btnEnviarCertificacao == false)
                    {

                        //Verificar se existe certificação requerida e com status concluida
                        foreach (DataRow row in lista.Rows)
                        {
                            string requerida = row["requerida"].ToString();
                            if (requerida != "" && requerida != null)
                            {
                                if (requerida == "1")
                                {
                                    string status = row["status"].ToString();
                                    if (status == "1")
                                    {
                                        btnEnviarCertificacao = true;
                                        HabilitarEnviarAprovacao();
                                    }
                                }

                            }
                        }

                        if (btnEnviarCertificacao == false)
                        {
                            //Verificar se existe certificação requerida e com status concluida
                            foreach (DataRow row in lista.Select("status <> '3' And obrigatoria = '1'"))
                            {
                                string obrigatoria = row["obrigatoria"].ToString();
                                if (obrigatoria != "" && obrigatoria != null)
                                {
                                    if (obrigatoria == "1")
                                    {
                                        string status = row["status"].ToString();
                                        if (status != "1")
                                        {
                                            DesabilitarEnviarAprovacao();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (DataRow row in lista.Rows)
                {
                    string data = row["dtprova"].ToString();
                    if (data != "" && data != null)
                    {
                        row["selecionado"] = "True";
                    }
                    string teste = row["status"].ToString();
                }

                grdpesquisa.DataSource = lista;
                grdpesquisa.DataBind();

                //bloqueio dos campos quando a certificacao for concluida - solicitacao Emerson 09/11/2016
                string statusCertificacao = hdnStatusOrigilnal.Value;

                foreach (GridViewRow row in grdpesquisa.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelecionado = (CheckBox)row.FindControl("chkSelecionado");
                        TextBox txtDTProva = (TextBox)row.FindControl("txtDTProva");
                        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                        string nomeProva = row.Cells[1].Text;

                        if (ddlStatus.SelectedValue != "")
                        {
                            int idStatus = Convert.ToInt32(ddlStatus.SelectedValue);

                            DataKey data = grdpesquisa.DataKeys[row.RowIndex];

                            if ((idStatus == 3) || (idStatus == 2))
                            {
                                DataTable dt = new DataTable();
                                if (Session["Grid"] != null)
                                    dt = (DataTable)Session["Grid"];
                                DataRow linha = dt.NewRow();
                                linha["selecionado"] = "False"; //(row.FindControl("chkSelecionado") as CheckBox).Checked;
                                linha["idprova"] = data["idprova"];
                                linha["Nome"] = data["Nome"].ToString();
                                linha["Alias"] = data["Alias"].ToString();
                                linha["Tipo"] = data["Tipo"].ToString();
                                linha["idlinha"] = grdpesquisa.Rows.Count + 1;
                                bool repetida = false;
                                foreach (GridViewRow item in grdpesquisa.Rows)
                                {
                                    DataKey dataaux = grdpesquisa.DataKeys[item.RowIndex];
                                    DropDownList ddlStatusAtual = (DropDownList)item.FindControl("ddlStatus");

                                    if ((linha["idprova"].ToString() == dataaux["idprova"].ToString()) && ddlStatusAtual.SelectedValue != "3" && ddlStatusAtual.SelectedValue != "2")
                                    {
                                        repetida = true;
                                        break;
                                    }

                                }
                                if (!repetida)
                                {
                                    dt.Rows.Add(linha);
                                }
                                Session["Grid"] = dt;
                                grdpesquisa.DataSource = dt;
                                grdpesquisa.DataBind();
                            }
                        }

                        if (statusCertificacao == "1")
                        {
                            chkSelecionado.Enabled = false;
                            txtDTProva.Enabled = false;
                            ddlStatus.Enabled = false;
                        }
                    }
                }



            }

            catch (Exception ex)
            {
                throw;
            }
        }

        private void CarregaGridEditarConcluido(int idCertificacao)
        {
            try
            {
                Session.Remove("GridConcluido");
                divInfoProva.Visible = false;

                CertificadoUsuarioProvaBusinessLayer blprova = new CertificadoUsuarioProvaBusinessLayer();
                CertificadoUsuarioProvaEntityImagem eoProva = new CertificadoUsuarioProvaEntityImagem();
                eoProva.idcertificacao = idCertificacao;


                DataTable lista = blprova.ConsultarImagem(eoProva);
                lista.Columns.Add("selecionado");
                lista.Columns.Add("idlinha");
                Session["GridConcluido"] = lista;
                if (lista.Rows.Count == 0)
                {
                    divInfoProva.Visible = true;
                    lblmsInfoProva.Text = "Não existe provas para o certificado selecionado!";
                    grdpesquisa.DataSource = null;
                    grdpesquisa.DataBind();
                }
                else
                {
                    divInfoProva.Visible = false;
                }

                foreach (DataRow row in lista.Rows)
                {
                    string data = row["dtprova"].ToString();
                    if (data != "" && data != null)
                    {
                        row["selecionado"] = "True";
                    }
                    string teste = row["status"].ToString();
                }

                grdpesquisa.DataSource = lista;
                grdpesquisa.DataBind();

                //bloqueio dos campos quando a certificacao for concluida - solicitacao Emerson 09/11/2016
                string statusCertificacao = hdnStatusOrigilnal.Value;

                foreach (GridViewRow row in grdpesquisa.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelecionado = (CheckBox)row.FindControl("chkSelecionado");
                        TextBox txtDTProva = (TextBox)row.FindControl("txtDTProva");
                        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
                        LinkButton btnexcluir = (LinkButton)row.FindControl("btnExcluir");

                        if (ddlStatus.SelectedValue != "")
                        {
                            int idStatus = Convert.ToInt32(ddlStatus.SelectedValue);

                            DataKey data = grdpesquisa.DataKeys[row.RowIndex];

                            //if ((idStatus == 0) || (idStatus == 2))
                            //{
                            //    DataTable dt = new DataTable();
                            //    dt = (DataTable)Session["GridConcluido"];
                            //    DataRow linha = dt.NewRow();
                            //    linha["selecionado"] = "False"; //(row.FindControl("chkSelecionado") as CheckBox).Checked;
                            //    linha["idprova"] = data["idprova"];
                            //    linha["Nome"] = data["Nome"].ToString();
                            //    linha["Alias"] = data["Alias"].ToString();
                            //    linha["Tipo"] = data["Tipo"].ToString();
                            //    linha["idlinha"] = grdpesquisa.Rows.Count + 1;
                            //    dt.Rows.Add(linha);
                            //    Session["GridConcluido"] = dt;
                            //    grdpesquisa.DataSource = dt;
                            //    grdpesquisa.DataBind();
                            //}
                        }

                        //if (statusCertificacao == "1")
                        //{
                        chkSelecionado.Enabled = false;
                        txtDTProva.Enabled = false;
                        ddlStatus.Enabled = false;
                        btnexcluir.Enabled = false;
                        string statusAprovacao = VerificarRegistrosAprovacao(idCertificacao);
                        if (statusAprovacao == "2")
                        {
                            DesabilitarEnviarAprovacao();
                            // DesabilitarRegistrosPendentesAprovacao();
                            RetornaMensagemAprovacao();
                            lnksalvar.Visible = false;
                            lnkEnviarAprovacao.Visible = false;
                        }
                        else if (statusAprovacao == "0")
                        {
                            DesabilitarEnviarAprovacao();
                            lnksalvar.Visible = false;
                            lnkEnviarAprovacao.Visible = false;
                        }
                        else if (statusAprovacao == "3")
                        {
                            HabilitarEnviarAprovacao();
                            HabilitarRegistrosPendentesAprovacao();
                            lnksalvar.Visible = true;
                        }
                    }
                }

            }

            catch (Exception)
            {
                throw;
            }
        }

        private void DesabilitarRegistrosPendentesAprovacao()
        {
            lblRegistrosPendentesAprovacao.Visible = false;
            divPendenteAprovacao.Visible = false;
        }

        private void RetornaMensagemAprovacao()
        {
            lblRegistrosPendentesAprovacao.Text = "Certificação Pendente de Aprovação";
            divPendenteAprovacao.Visible = true;
        }

        private void HabilitarRegistrosPendentesAprovacao()
        {
            lblRegistrosPendentesAprovacao.Visible = true;
            divPendenteAprovacao.Visible = true;
        }

        private void CarregarGrid()
        {
            try
            {
                Session.Remove("Grid");
                divInfoProva.Visible = false;
                CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
                CertificadoEntity eoCertificado = new CertificadoEntity();

                bool valido = false;
                if (ddlidcertificado.SelectedValue != "")
                {
                    eoCertificado.idcertificado = Convert.ToInt32(ddlidcertificado.SelectedValue);
                    valido = true;
                }
                else
                {
                    divInfoProva.Visible = true;
                    lblmsInfoProva.Text = "Não existe provas para o certificado selecionado!";
                    grdpesquisa.DataSource = null;
                    grdpesquisa.DataBind();

                }

                if (valido)
                {
                    DataTable lista = blCertificado.ConsultarProva(eoCertificado);
                    lista.Columns.Add("dtprova");
                    lista.Columns.Add("status");
                    lista.Columns.Add("idlinha");
                    lista.Columns.Add("selecionado");
                    //lista.Columns.Add("tipo");

                    Session["Grid"] = lista;

                    if (lista.Rows.Count == 0)
                    {
                        divInfoProva.Visible = true;
                        lblmsInfoProva.Text = "Não existe provas para o certificado selecionado!";
                        ddlStatusCertificacao.SelectedValue = EOConst.CodStatusCertificacao.Concluida.ToString();
                    }
                    else
                    {
                        divInfoProva.Visible = false;
                    }

                    grdpesquisa.DataSource = lista;
                    grdpesquisa.DataBind();

                    //recupera dados do certificado
                    eoCertificado = blCertificado.Obter(Convert.ToInt32(ddlidcertificado.SelectedValue));
                    if (eoCertificado.voucher != int.MinValue && eoCertificado.voucher != 0)
                    {
                        chkVoucher.Checked = true;
                        txtqtdvoucher.Text = eoCertificado.qtdvoucher.ToString();
                    }
                    else
                    {
                        chkVoucher.Checked = false;
                        txtqtdvoucher.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void AtualizarStatusCertificacao()
        {
            bool concluido = false;
            bool voucheratingido = false;

            //validar conclusão pela requerida
            foreach (GridViewRow item in grdpesquisa.Rows)
            {
                DataKey data = grdpesquisa.DataKeys[item.RowIndex];
                string tipoprova = data["Tipo"].ToString();

                CheckBox chkSelecionado = (CheckBox)item.FindControl("chkSelecionado");
                DropDownList ddlStatusSelecionado = (DropDownList)item.FindControl("ddlStatus");
                TextBox DtProva = (TextBox)item.FindControl("txtDTProva");

                if (tipoprova == "Requerida" && ddlStatusSelecionado.SelectedValue == "1")
                {
                    concluido = true;
                    if (String.IsNullOrEmpty(txtdtcertificacao.Text))
                        txtdtcertificacao.Text = DtProva.Text;
                }
            }
            if (!concluido)
            {
                int qtdpendente = 0;
                bool existeobrigatoria = false;
                string dtProva = "";
                //
                DataTable dt = new DataTable();
                dt = (DataTable)Session["Grid"];
                //

                foreach (GridViewRow item in grdpesquisa.Rows)
                {
                    DataKey data = grdpesquisa.DataKeys[item.RowIndex];
                    //

                    int idpro = Convert.ToInt32(data["idprova"]);
                    string prova = "idprova = " + idpro.ToString() + " and (status='3' or status='2')";
                    DataRow[] find = dt.Select(prova);

                    //int numLinha = int.MinValue;
                    int qtdVoucher = 1;
                    int numrep = find.Count();
                    if (txtqtdvoucher.Text != "")
                    {
                        qtdVoucher = Convert.ToInt32(txtqtdvoucher.Text);
                    }

                    if (numrep == qtdVoucher)
                    {
                        voucheratingido = true;
                        break;
                    }
                    //
                    string tipoprova = data["Tipo"].ToString();

                    CheckBox chkSelecionado = (CheckBox)item.FindControl("chkSelecionado");
                    DropDownList ddlStatusSelecionado = (DropDownList)item.FindControl("ddlStatus");
                    TextBox DtProva = (TextBox)item.FindControl("txtDTProva");
                    dtProva = DtProva.Text;

                    if (tipoprova == "Obrigatória")
                        existeobrigatoria = true;

                    if (tipoprova == "Obrigatória" && ddlStatusSelecionado.SelectedValue == "")
                    {
                        qtdpendente = qtdpendente + 1;
                    }
                }
                if (existeobrigatoria && qtdpendente == 0)
                {
                    if (!voucheratingido)
                    {
                        concluido = true;
                        if ((String.IsNullOrEmpty(txtdtcertificacao.Text) || txtdtcertificacao.Text != dtProva) && (!string.IsNullOrEmpty(dtProva.ToString())))
                            txtdtcertificacao.Text = dtProva;
                    }
                }
                else
                    concluido = false;
            }


            if (ddlStatusCertificacao.SelectedValue != EOConst.CodStatusCertificacao.Inativo.ToString())
            {
                ddlStatusCertificacao.Enabled = true;
                if (concluido)
                {
                    ddlStatusCertificacao.SelectedValue = EOConst.CodStatusCertificacao.Concluida.ToString();
                }
                else
                {
                    if (voucheratingido)
                    {
                        ddlStatusCertificacao.SelectedValue = EOConst.CodStatusCertificacao.Reprovado.ToString();
                    }
                    else
                    {
                        ddlStatusCertificacao.SelectedValue = EOConst.CodStatusCertificacao.Em_Andamento.ToString();
                    }
                }
                ddlStatusCertificacao.Enabled = false;
            }
        }

        private void AtualizarDTSessao()
        {
            DataTable dt = null;
            if (Session["Grid"] != null)
                dt = ((DataTable)Session["Grid"]).Clone();
            Session.Remove("Grid");
            if (grdpesquisa.Rows.Count > 0)
            {
                int i = 0;
                foreach (GridViewRow item in grdpesquisa.Rows)
                {
                    DataKey data = grdpesquisa.DataKeys[item.RowIndex];
                    int idprova = Convert.ToInt32(data["idprova"]);

                    DataRow dr = dt.NewRow();
                    dr["selecionado"] = (item.FindControl("chkSelecionado") as CheckBox).Checked;
                    dr["idprova"] = idprova;
                    dr["Nome"] = data["Nome"].ToString();
                    dr["Alias"] = data["Alias"].ToString();
                    dr["dtprova"] = (item.FindControl("txtDTProva") as TextBox).Text;
                    dr["status"] = (item.FindControl("ddlStatus") as DropDownList).SelectedValue;
                    dr["tipo"] = data["Tipo"].ToString();

                    dr["idlinha"] = i;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            Session["Grid"] = dt;
        }

        private bool ValidarDadosAprovacao()
        {
            List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
            List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
            CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
            string msgvalidacao = "";
            bool valido = true;

            try
            {

                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                string colaborador = ddlColaborador.SelectedValue;
                if (colaborador == "")
                {
                    msgvalidacao = msgvalidacao + "Colaborador não informado; <br/>";
                    valido = false;
                }
                int status = Convert.ToInt32(ddlStatusCertificacao.SelectedValue);
                if (status != 1)
                {
                    msgvalidacao = msgvalidacao + "A certificação só pode ser enviada para aprovação quando estiver com o status de Concluída; <br/>";
                    valido = false;
                }
                string certificado = ddlidcertificado.SelectedValue;
                if (certificado == "")
                {
                    msgvalidacao = msgvalidacao + "Certificado não informado; <br/>";
                    valido = false;
                }
                string regulador = ddlidregulador.SelectedValue;
                if (regulador == "")
                {
                    msgvalidacao = msgvalidacao + "Regulador não informado; <br/>";
                    valido = false;
                }
                string dtcertificacao = txtdtcertificacao.Text;
                if (dtcertificacao == "" && ddlStatusCertificacao.SelectedValue == EOConst.CodStatusCertificacao.Concluida.ToString())
                {
                    msgvalidacao = msgvalidacao + "Data da certificação não informada;<br/>";
                    valido = false;
                }
                if (dtcertificacao != "")
                {
                    try
                    {
                        Convert.ToDateTime(dtcertificacao);
                    }
                    catch (Exception)
                    {
                        msgvalidacao = msgvalidacao + "Data da certificação informada inválida;<br/>";
                        valido = false;
                    }
                }
                string dtvalidade = txtvalidade.Text;
                if (dtvalidade == "" && !chkExpira.Checked && ddlStatusCertificacao.SelectedValue == EOConst.CodStatusCertificacao.Concluida.ToString())
                {
                    msgvalidacao = msgvalidacao + "Validade do certificado não informado;<br/>";
                    valido = false;
                }
                if (dtvalidade != "")
                {
                    try
                    {
                        Convert.ToDateTime(dtvalidade);
                    }
                    catch (Exception)
                    {
                        msgvalidacao = msgvalidacao + "Data de validade informada inválida;<br/>";
                        valido = false;
                    }
                }
                //INSERE DADOS DA PROVA

                foreach (GridViewRow row in grdpesquisa.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                    if (chkbox.Checked)
                    {
                        string msg = "Mensagem: Linha " + (row.RowIndex + 1) + ": </br>";
                        bool validacao = true;
                        CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                        CertificadoUsuarioProvaEntityImagem eoImagem = new CertificadoUsuarioProvaEntityImagem();

                        DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                        eoProva.idprova_certificado = Convert.ToInt32(data["idprova"]);
                        eoImagem.idprova = Convert.ToInt32(data["idprova"]);
                        TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;
                        if (txtDtProva.Text == "")
                        {
                            msg = msg + "Data da prova não informada; </br>";
                            validacao = false;
                            diverro.Visible = true;
                            lblmsgerro.Text = msgvalidacao;
                        }
                        else
                        {
                            eoProva.dtprova = Convert.ToDateTime(txtDtProva.Text);
                            eoImagem.dtprova = Convert.ToDateTime(txtDtProva.Text);
                        }
                        DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                        if (ddlStatus.SelectedValue == "")
                        {
                            msg = msg + "Status da prova não informado; </br>";
                            validacao = false;
                            diverro.Visible = true;
                            lblmsgerro.Text = msgvalidacao;
                        }
                        else
                        {
                            eoProva.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            eoImagem.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        }
                        eoProva.dtinclusao = DateTime.Now;
                        msg = msg + "<br />";
                        if (!validacao)
                        {
                            msgvalidacao = msgvalidacao + msg;
                            valido = false;
                        }
                        else
                        {
                            string nomeProva = data["Nome"].ToString();
                            string aliasProva = data["Alias"].ToString();
                            string TipoProva = data["Tipo"].ToString();
                            if (TipoProva == "Requerida")
                            {
                                eoImagem.requerida = EOConst.CodStatus.Ativo;
                            }
                            else
                            {
                                eoImagem.obrigatoria = EOConst.CodStatus.Ativo;
                            }
                            eoImagem.nome = nomeProva;
                            eoImagem.alias = aliasProva;

                            lstprova.Add(eoProva);
                            lstImagem.Add(eoImagem);

                        }
                    }
                }
                if (!valido)
                {
                    diverro.Visible = true;
                    lblmsgerro.Text = msgvalidacao;
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

            return valido;
        }

        private bool SalvarEnvioAprovacao()
        {

            List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
            List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
            CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
            string msgvalidacao = "";
            bool valido = true;

            try
            {

                int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                string colaborador = ddlColaborador.SelectedValue;
                if (colaborador == "")
                {
                    msgvalidacao = msgvalidacao + "Colaborador não informado; <br/>";
                    valido = false;
                }

                string certificado = ddlidcertificado.SelectedValue;
                if (certificado == "")
                {
                    msgvalidacao = msgvalidacao + "Certificado não informado; <br/>";
                    valido = false;
                }
                string regulador = ddlidregulador.SelectedValue;
                if (regulador == "")
                {
                    msgvalidacao = msgvalidacao + "Regulador não informado; <br/>";
                    valido = false;
                }
                string dtcertificacao = txtdtcertificacao.Text;
                if (dtcertificacao == "")
                {
                    msgvalidacao = msgvalidacao + "Data da certificação não informada;<br/>";
                    valido = false;
                }
                if (dtcertificacao != "")
                {
                    try
                    {
                        Convert.ToDateTime(dtcertificacao);
                    }
                    catch (Exception)
                    {
                        msgvalidacao = msgvalidacao + "Data da certificação informada inválida;<br/>";
                        valido = false;
                    }
                }
                string dtvalidade = txtvalidade.Text;
                if (dtvalidade == "" && !chkExpira.Checked && ddlStatusCertificacao.SelectedValue == EOConst.CodStatusCertificacao.Concluida.ToString())
                {
                    msgvalidacao = msgvalidacao + "Validade do certificado não informado;<br/>";
                    valido = false;
                }
                if (dtvalidade != "")
                {
                    try
                    {
                        Convert.ToDateTime(dtvalidade);
                    }
                    catch (Exception)
                    {
                        msgvalidacao = msgvalidacao + "Data de validade informada inválida;<br/>";
                        valido = false;
                    }
                }
                //INSERE DADOS DA PROVA

                foreach (GridViewRow row in grdpesquisa.Rows)
                {
                    CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                    if (chkbox.Checked)
                    {
                        string msg = "Mensagem: Linha " + (row.RowIndex + 1) + ": </br>";
                        bool validacao = true;
                        CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                        CertificadoUsuarioProvaEntityImagem eoImagem = new CertificadoUsuarioProvaEntityImagem();

                        DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                        eoProva.idprova_certificado = Convert.ToInt32(data["idprova"]);
                        eoImagem.idprova = Convert.ToInt32(data["idprova"]);
                        TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;
                        if (txtDtProva.Text == "")
                        {
                            msg = msg + "Data da prova não informada; </br>";
                            validacao = false;
                            diverro.Visible = true;
                            lblmsgerro.Text = msgvalidacao;
                        }
                        else
                        {
                            eoProva.dtprova = Convert.ToDateTime(txtDtProva.Text);
                            eoImagem.dtprova = Convert.ToDateTime(txtDtProva.Text);
                        }
                        DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                        if (ddlStatus.SelectedValue == "")
                        {
                            msg = msg + "Status da prova não informado; </br>";
                            validacao = false;
                            diverro.Visible = true;
                            lblmsgerro.Text = msgvalidacao;
                        }
                        else
                        {
                            eoProva.status = Convert.ToInt32(ddlStatus.SelectedValue);
                            eoImagem.status = Convert.ToInt32(ddlStatus.SelectedValue);
                        }
                        eoProva.dtinclusao = DateTime.Now;
                        msg = msg + "<br />";
                        if (!validacao)
                        {
                            msgvalidacao = msgvalidacao + msg;
                            valido = false;
                        }
                        else
                        {
                            string nomeProva = data["Nome"].ToString();
                            string aliasProva = data["Alias"].ToString();
                            string TipoProva = data["Tipo"].ToString();
                            if (TipoProva == "Requerida")
                            {
                                eoImagem.requerida = EOConst.CodStatus.Ativo;
                            }
                            else
                            {
                                eoImagem.obrigatoria = EOConst.CodStatus.Ativo;
                            }
                            eoImagem.nome = nomeProva;
                            eoImagem.alias = aliasProva;

                            lstprova.Add(eoProva);
                            lstImagem.Add(eoImagem);

                        }
                    }
                }
                if (!valido)
                {
                    diverro.Visible = true;
                    lblmsgerro.Text = msgvalidacao;
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

            return valido;
        }

        private void Salvar()
        {
            List<CertificadoUsuarioProvaEntity> lstprova = new List<CertificadoUsuarioProvaEntity>();
            List<CertificadoUsuarioProvaEntityImagem> lstImagem = new List<CertificadoUsuarioProvaEntityImagem>();
            CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
            int idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

            foreach (GridViewRow row in grdpesquisa.Rows)
            {
                CheckBox chkbox = (CheckBox)row.FindControl("chkSelecionado");
                if (chkbox.Checked)
                {
                    string msg = "Mensagem: Linha " + (row.RowIndex + 1) + ": </br>";
                    // bool validacao = true;
                    CertificadoUsuarioProvaEntity eoProva = new CertificadoUsuarioProvaEntity();
                    CertificadoUsuarioProvaEntityImagem eoImagem = new CertificadoUsuarioProvaEntityImagem();

                    DataKey data = grdpesquisa.DataKeys[row.RowIndex];
                    eoProva.idprova_certificado = Convert.ToInt32(data["idprova"]);
                    eoImagem.idprova = Convert.ToInt32(data["idprova"]);
                    TextBox txtDtProva = row.FindControl("txtDTProva") as TextBox;

                    eoProva.dtprova = Convert.ToDateTime(txtDtProva.Text);
                    eoImagem.dtprova = Convert.ToDateTime(txtDtProva.Text);

                    DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;
                    eoProva.status = Convert.ToInt32(ddlStatus.SelectedValue);
                    eoImagem.status = Convert.ToInt32(ddlStatus.SelectedValue);


                    eoProva.dtinclusao = DateTime.Now;

                    string nomeProva = data["Nome"].ToString();
                    string aliasProva = data["Alias"].ToString();
                    string TipoProva = data["Tipo"].ToString();
                    if (TipoProva == "Requerida")
                    {
                        eoImagem.requerida = EOConst.CodStatus.Ativo;
                    }
                    else
                    {
                        eoImagem.obrigatoria = EOConst.CodStatus.Ativo;
                    }
                    eoImagem.nome = nomeProva;
                    eoImagem.alias = aliasProva;

                    lstprova.Add(eoProva);
                    lstImagem.Add(eoImagem);

                }
            }

            CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();
            CertificacaousuarioEntity eocertuser = new CertificacaousuarioEntity();
            CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
            CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
            if (ddlColaborador.SelectedValue.Trim() != "")
                eocertuser.idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
            if (ddlidcertificado.SelectedValue.Trim() != "")
                eocertuser.idcertificado = Convert.ToInt32(ddlidcertificado.SelectedValue);
            if (ddlidregulador.SelectedValue.Trim() != "")
                eocertuser.idregulador = Convert.ToInt32(ddlidregulador.SelectedValue);
            // eocertuser.idusuario = idusuario;
            if (ddlStatusCertificacao.SelectedValue.Trim() != "")
                eocertuser.status = Convert.ToInt32(ddlStatusCertificacao.SelectedValue);
            if (txtvalidade.Text.Trim() != "")
                eocertuser.validade = Convert.ToDateTime(txtvalidade.Text);
            if (txtLicenca.Text.Trim() != "")
                eocertuser.licenca = txtLicenca.Text;
            if (txtURL.Text.Trim() != "")
                eocertuser.url = txtURL.Text;
            if (chkExpira.Checked)
                eocertuser.expira = 1;
            else
                eocertuser.expira = 0;
            if (txtdtcertificacao.Text.Trim() != "")
                eocertuser.dtcertificacao = Convert.ToDateTime(txtdtcertificacao.Text);
            if (chkAceite.Checked)
                eocertuser.aceite = 1;
            else
                eocertuser.aceite = 0;

            if (hdnId.Value != "")
            {
                eocertuser.idcertificacao = Convert.ToInt32(hdnId.Value);
                eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                blcertuser.Alterar(eocertuser);
                if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                {
                    eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                    eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                    eocertdigi.idcertificacao = Convert.ToInt32(hdnId.Value);

                    bool blretorno = blcertdigi.Consulta(eocertdigi.idcertificacao);
                    if (blretorno == false)
                    {
                        blcertdigi.Incluir(eocertdigi);
                        lblsucesso.Text = "Operação realizada com sucesso!";
                    }
                    else
                    {
                        blcertdigi.Alterar(eocertdigi);
                        lblsucesso.Text = "Operação realizada com sucesso!";
                    }
                }
                if (eocertuser.status == 1)
                {
                    CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                    eodel.idcertificacao = eocertuser.idcertificacao;
                    blProva.ExcluirImagem(eodel);
                    foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        blProva.Excluir(item);
                        item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    }
                    foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                    }
                }
                else
                {
                    List<CertificadoUsuarioProvaEntity> lstProvaExistente = new List<CertificadoUsuarioProvaEntity>();
                    CertificadoUsuarioProvaEntity eoProvaExistente = new CertificadoUsuarioProvaEntity();
                    eoProvaExistente.idcertificacao = eocertuser.idcertificacao;

                    lstProvaExistente = blProva.ConsultarList(eoProvaExistente);

                    foreach (CertificadoUsuarioProvaEntity item in lstProvaExistente)
                    {
                        if (!lstprova.Contains(item))
                        {
                            item.idcertificacao = eocertuser.idcertificacao;
                            blProva = new CertificadoUsuarioProvaBusinessLayer();
                            blProva.Excluir(item);
                        }
                    }

                    foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        blProva.Excluir(item);
                        item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    }
                }

            }
            else
            {
                eocertuser.Log = ((UsuarioEntity)Session["eoUs"]).Log;

                //if(Session["Origem"] != null)
                //{
                //    if((String)Session["Origem"] == "MinhasCertificacoes")
                //    {
                //        eocertuser.aprovacao = EOConst.CodStatusAprovacao.CertificacaoIniciada;
                //    }
                //}
                //else if(Session["Origem"] == null && Session["eoMinhaCertificacaousuario"] == null)
                //{
                //    eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                //}
                //else
                //{
                //    eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                //}
                eocertuser.aprovacao = EOConst.CodStatusAprovacao.NaoPrecisaAprovacao;
                eocertuser.idcertificacao = blcertuser.Incluir(eocertuser);
                if (Session["AlterouArquivoDigitalizado"] != null && Convert.ToBoolean(Session["AlterouArquivoDigitalizado"].ToString()))
                {
                    eocertdigi.arquivo = Session["NomeArquivoDigitalizado"].ToString();
                    eocertdigi.arquivodigitalizado = ((byte[])Session["ArquivoDigitalizado"]);
                    eocertdigi.idcertificacao = eocertuser.idcertificacao;
                    eocertdigi.idcertificadodigitalizado = blcertdigi.Incluir(eocertdigi);
                }
                if (eocertuser.status == 1)
                {
                    CertificadoUsuarioProvaEntityImagem eodel = new CertificadoUsuarioProvaEntityImagem();
                    eodel.idcertificacao = eocertuser.idcertificacao;
                    blProva.ExcluirImagem(eodel);
                    foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        blProva.Excluir(item);
                        item.idcertificacao_usuario_prova = blProva.Incluir(item);
                    }
                    foreach (CertificadoUsuarioProvaEntityImagem item in lstImagem)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        item.idcertificacao_usuario_prova_imagem = blcertdigi.IncluirImagem(item);
                    }
                }
                else
                {
                    foreach (CertificadoUsuarioProvaEntity item in lstprova)
                    {
                        item.idcertificacao = eocertuser.idcertificacao;
                        blProva = new CertificadoUsuarioProvaBusinessLayer();
                        item.idcertificacao_usuario_prova = blProva.Incluir(item);

                    }
                }
            }
            //    divsucesso.Visible = true;
            //    lblsucesso.Text = "Operação realizada com sucesso!";

        }


        #endregion

        protected void btnPreencherProva_Click(object sender, EventArgs e)
        {
            CertificadoUsuarioProvaBusinessLayer blProva = new CertificadoUsuarioProvaBusinessLayer();
            blProva.preencherNovaProvas(Convert.ToInt32(Session["recarregarSessao"].ToString()));

            Response.Redirect("CertificacaoColaboradorOperacao.aspx");

        }

    }
}