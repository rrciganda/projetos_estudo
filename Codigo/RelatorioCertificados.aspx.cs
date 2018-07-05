using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.Drawing;
using System.IO;
using System.Text;
using System.Data;
using ClosedXML.Excel;


namespace Estudo.Web.GestaoConhecimento
{
    public partial class RelatorioCertificados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoPesquisarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAlterarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])) &&
                    !(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoExcluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"]))
                    )
                {
                    Response.Redirect("SemAcesso.aspx");
                }


                CarregarComboRegulador();
                CarregarComboGrupo();
                CarregarComboCertificado();
                CarregarComboColaborador();
                CarregaAreaDept();
                CarregarComboFornecedor();
                ddlStatusAprovacao.SelectedValue = "0";
                // CarregarGrid();
            }
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);

        }
        public void CarregarComboFornecedor()
        {
            ddlfornecedor.Items.Clear();
            FornecedorBusinessLayer blFornecedor = new FornecedorBusinessLayer();
            FornecedorEntity eoFornecedor = new FornecedorEntity();
            //eoGrupo.status = EOConst.CodStatus.Ativo;
            ddlfornecedor.DataSource = blFornecedor.ConsultarAtivo(eoFornecedor);
            ddlfornecedor.DataTextField = "nome";
            ddlfornecedor.DataValueField = "idfornecedor";
            ddlfornecedor.DataBind();
            if (ddlfornecedor.Items.Count > 0)
                ddlfornecedor.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlfornecedor.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        private void CarregaAreaDept()
        {
            ddldepartamento.Items.Clear();
            UsuarioBusinessLayer blUsuario = new UsuarioBusinessLayer();
            UsuarioEntity eoUsuario = new UsuarioEntity();
            ddldepartamento.DataSource = blUsuario.ConsultarDepartamento(eoUsuario);
            ddldepartamento.DataTextField = "departamento";
            ddldepartamento.DataValueField = "departamento";
            ddldepartamento.DataBind();
            if (ddldepartamento.Items.Count > 0)
                ddldepartamento.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddldepartamento.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        public void CarregarComboColaborador()
        {
            ddlColaborador.Items.Clear();
            UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
            UsuarioEntity eo = new UsuarioEntity();
            eo.status = EOConst.CodStatusUsuario.Ativo;

            DataTable dtcolaborador = bl.Consultar(eo);

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
        private void CarregarComboRegulador()
        {
            ddlregulador.Items.Clear();
            ReguladorBusinessLayer blRegulador = new ReguladorBusinessLayer();
            ReguladorEntity eoRegulador = new ReguladorEntity();
            ddlregulador.DataSource = blRegulador.Consultar(eoRegulador);
            ddlregulador.DataTextField = "nome";
            ddlregulador.DataValueField = "idregulador";
            ddlregulador.DataBind();
            if (ddlregulador.Items.Count > 0)
                ddlregulador.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlregulador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));

        }
        private void CarregarComboGrupo()
        {
            ddlgrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            eoGrupo.status = EOConst.CodStatus.Ativo;
            ddlgrupo.DataSource = blGrupo.Consultar(eoGrupo);
            ddlgrupo.DataTextField = "nome";
            ddlgrupo.DataValueField = "idgrupo";
            ddlgrupo.DataBind();
            if (ddlgrupo.Items.Count > 0)
                ddlgrupo.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlgrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }
        private void CarregarComboCertificado()
        {

            ddlcertificado.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            if (ddlgrupo.SelectedValue != "")
            {
                eoCertificado.idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
            }
            eoCertificado.status = EOConst.CodStatus.Ativo;
            ddlcertificado.DataSource = blCertificado.Consultar(eoCertificado);
            ddlcertificado.DataTextField = "nome";
            ddlcertificado.DataValueField = "idcertificado";
            ddlcertificado.DataBind();
            if (ddlcertificado.Items.Count > 0)
                ddlcertificado.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlcertificado.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        protected void ddlgrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarComboCertificado();
        }
        protected void lnkPesquisar_Click(object sender, EventArgs e)
        {
            diverro.Visible = false;
            grdpesquisa.PageIndex = 0;
            CarregarGrid();
        }
        private void CarregarGrid()
        {
            try
            {
                Session["Grid"] = null;
                lblmsgerro.Text = "";
                CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();

                int idusuario = int.MinValue;
                int idgrupo = int.MinValue;
                int idfornecedor = int.MinValue;
                int idcertificado = int.MinValue;
                int idregulador = int.MinValue;
                int status = int.MinValue;
                int certificadoexpirado = int.MinValue;
                int aprovacao = int.MinValue;
                string departamento = "";
                DateTime dtinicertificacao = DateTime.MinValue;
                DateTime dtfimcertificacao = DateTime.MinValue;
                DateTime dtinivalidade = DateTime.MinValue;
                DateTime dtfimvalidade = DateTime.MinValue;
                bool valido = true;

                if (ddlColaborador.SelectedValue != "")
                {
                    idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
                }
                if (ddlgrupo.SelectedValue != "")
                {
                    idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
                }
                if (ddlfornecedor.SelectedValue != "")
                {
                    idfornecedor = Convert.ToInt32(ddlfornecedor.SelectedValue);
                }
                if (ddlcertificado.SelectedValue != "")
                {
                    idcertificado = Convert.ToInt32(ddlcertificado.SelectedValue);
                }
                if (ddlregulador.SelectedValue.ToString().Trim() != "")
                {
                    idregulador = Convert.ToInt32(ddlregulador.SelectedValue.ToString());
                }
                if (ddlStatus.SelectedValue != "")
                {
                    status = Convert.ToInt32(ddlStatus.SelectedValue.ToString());
                }
                if (ddlcertificadoexpirado.SelectedValue != "")
                {
                    certificadoexpirado = Convert.ToInt32(ddlcertificadoexpirado.SelectedValue);
                }
                if (ddldepartamento.SelectedValue != "")
                {
                    departamento = ddldepartamento.SelectedValue;
                }
                if (ddlStatusAprovacao.SelectedValue != "")
                {
                    aprovacao = Convert.ToInt32(ddlStatusAprovacao.SelectedValue);
                }

                if (txtdtinicertificacao.Text != "")
                {
                    try
                    {
                        dtinicertificacao = Convert.ToDateTime(txtdtinicertificacao.Text);
                    }
                    catch (Exception)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data inicial da certificação inválida.";
                    }
                }

                if (txtdtfimcertificacao.Text != "")
                {
                    try
                    {
                        dtfimcertificacao = Convert.ToDateTime(txtdtfimcertificacao.Text);
                    }
                    catch (Exception)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data final da certificação inválida.";
                    }
                }
                if (dtinicertificacao > DateTime.MinValue && dtfimcertificacao > DateTime.MinValue)
                {
                    if (dtinicertificacao > dtfimcertificacao)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data inicial da certificação não pode ser maior que a data final.";
                    }
                }


                if (txtdtinivalidade.Text != "")
                {
                    try
                    {
                        dtinivalidade = Convert.ToDateTime(txtdtinivalidade.Text);
                    }
                    catch (Exception)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data inicial da validade inválida.";
                    }
                }

                if (txtdtfimvalidade.Text != "")
                {
                    try
                    {
                        dtfimvalidade = Convert.ToDateTime(txtdtfimvalidade.Text);
                    }
                    catch (Exception)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data final da validade inválida.";
                    }
                }
                if (dtinivalidade > DateTime.MinValue && dtfimvalidade > DateTime.MinValue)
                {
                    if (dtinivalidade > dtfimvalidade)
                    {
                        valido = false;
                        lblmsgerro.Text = lblmsgerro.Text + "Data inicial da validade não pode ser maior que a data final.";
                    }
                }

                if (valido)
                {
                    //DataTable lista = blcertuser.Consultar(idgrupo, idcertificado, idregulador, idusuario, status);
                    DataTable lista = blcertuser.Consultar(idusuario, idgrupo, idfornecedor, idcertificado, idregulador, status, certificadoexpirado, departamento, dtinicertificacao, dtfimcertificacao, dtinivalidade, dtfimvalidade, aprovacao);

                    if (lista.Rows.Count == 0)
                    {
                        divInfo.Visible = true;
                        lblmsInfo.Text = "Não existe registro para filtro informado!";
                    }
                    else
                    {
                        divInfo.Visible = false;
                    }
                    Session["Grid"] = lista;
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
                    //if (e.CommandName == "cmdAlterar")
                    //{
                    //    Session["eoMinhaCertificacaousuario"] = data.Values["idcertificacao"].ToString();
                    //    Response.Redirect("CertificacaoColaboradorOperacao.aspx");
                    //}
                    //else if (e.CommandName == "cmdExcluir")
                    //{
                    //    CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                    //    CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                    //    eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                    //    bl.Excluir(eo);
                    //    CarregarGrid();
                    //}
                    //else 
                    if (e.CommandName == "cmdAbrir")
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
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton btn = e.Row.Cells[grdpesquisa.Columns.Count - 2].Controls[0] as LinkButton;
            //    btn.OnClientClick = "if (confirm('Confirma a exclusão desse registro?') == false) return false;";
            //}
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
            Session["eoMinhaCertificacaousuario"] = null;
            Response.Redirect("CertificacaoColaboradorOperacao.aspx");
        }


        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            CriaExcel();
        }

        public void CriaExcel()
        {
            try
            {
                GridviewToExcel();
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

        }

        public void GridviewToExcel()
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = (DataTable)Session["Grid"];
                if (dt != null && dt.Rows.Count > 0)
                {
                    try { dt.Columns.Remove("idcertificacao"); } catch { }
                    try { dt.Columns.Remove("idusuario"); } catch { }
                    try { dt.Columns.Remove("idcertificado"); } catch { }
                    try { dt.Columns.Remove("idregulador"); } catch { }
                    try { dt.Columns.Remove("descricao"); } catch { }
                    try { dt.Columns.Remove("numlicenca"); } catch { }
                    try { dt.Columns.Remove("url"); } catch { }
                    try { dt.Columns.Remove("expira"); } catch { }
                    try { dt.Columns.Remove("dtinclusao"); } catch { }
                    try { dt.Columns.Remove("observacao"); } catch { }

                    try { dt.Columns["nomegrupo"].ColumnName = "Grupo"; } catch { }
                    try { dt.Columns["nomefornecedor"].ColumnName = "Nome Fornecedor"; } catch { }
                    try { dt.Columns["versao"].ColumnName = "Versão"; } catch { }
                    try { dt.Columns["nomecertificado"].ColumnName = "Certificado"; } catch { }
                    try { dt.Columns["idreprovador"].ColumnName = "ID Reprovador"; } catch { }          
                    try { dt.Columns["motivoreprovacao"].ColumnName = "Motivo Reprovação"; } catch { }
                    try { dt.Columns["nomeregulador"].ColumnName = "Regulador"; } catch { }
                    try { dt.Columns["nomecolaborador"].ColumnName = "Colaborador"; } catch { }
                    try { dt.Columns["validade"].ColumnName = "Data Validade"; } catch { }
                    try { dt.Columns["dtcertificacao"].ColumnName = "Data Certificação"; } catch { }
                    try { dt.Columns["status"].ColumnName = "Status"; } catch { }
                    try { dt.Columns["nomeprova"].ColumnName = "Nome Prova"; } catch { }
                    try { dt.Columns["dtprova"].ColumnName = "Data Prova"; } catch { }

                    dt.Columns.Add("dsstatus");
                    dt.Columns.Add("dsstatusProva");
                    dt.Columns.Add("aprovacaoAux");

                    foreach (DataRow item in dt.Rows)
                    {
                        item["dsstatus"] = Entity.EOUtil.RetornarStatusPadraoCertificacao((Int32)item["Status"]);
                        item["dsstatusProva"] = Entity.EOUtil.RetornoStatusProva(item["statusprova"].ToString());
                        if (item["aprovacao"] != null && !String.IsNullOrEmpty(item["aprovacao"].ToString()))
                        {
                            item["aprovacaoAux"] = Entity.EOUtil.RetornarStatusAprovacaoCertificacao(Convert.ToInt32(item["aprovacao"].ToString()));
                        }

                    }

                    try { dt.Columns.Remove("status"); } catch { }
                    try { dt.Columns.Remove("statusprova"); } catch { }
                    try { dt.Columns.Remove("versao"); } catch { }
                    try { dt.Columns.Remove("nomeprova"); } catch { }
                    try { dt.Columns.Remove("dtprova"); } catch { }
                    try { dt.Columns.Remove("nomefornecedor"); } catch { }
                    try { dt.Columns.Remove("aprovacao"); } catch { }
                    try { dt.Columns.Remove("motivoreprovacao"); } catch { }
                    try { dt.Columns.Remove("idreprovador"); } catch { }


                    try { dt.Columns["dsstatus"].ColumnName = "Status"; } catch { }

                    try { dt.Columns["dsstatusProva"].ColumnName = "Status Prova"; } catch { }
                    try { dt.Columns["aprovacaoAux"].ColumnName = "Aprovação"; } catch { }

                    wb.Worksheets.Add(dt, "Certificados");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=RelatorioCertificado.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                    Session["Grid"] = null;
                }
                else
                {
                    diverro.Visible = true;
                    lblmsgerro.Text = "Não possui registros para exportar";
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


    }
}