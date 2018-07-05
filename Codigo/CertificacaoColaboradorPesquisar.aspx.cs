using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class MinhaCertificacaoPesquisar : System.Web.UI.Page
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
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoIncluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                    lnkNovaCertificacao.Visible = false;

                CarregarComboColaborador();
                CarregarComboGrupoTodos();
                CarregarComboCertificadoTodos();
                //CarregarComboGrupoAtivos();
                //CarregarComboCertificado();
                CarregaAreaDept();
                CarregarComboRegulador();
                CarregarComboFornecedor();
                //preencherCliente();

                // CarregarGrid();
            }
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);
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

        //public void CarregarComboColaborador()
        //{
        //    ddlColaborador.Items.Clear();
        //    UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
        //    UsuarioEntity eo = new UsuarioEntity();
        //    eo.status = EOConst.CodStatusUsuario.Ativo;

        //    ddlColaborador.DataSource = bl.Consultar(eo);
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
            UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
            UsuarioEntity eo = new UsuarioEntity();
            eo.status = EOConst.CodStatusUsuario.Ativo;

            DataTable dtConsulta = bl.Consultar(eo);

            ddlColaborador.Items.Clear();
            ListItem itemCliente = new ListItem();

            Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
            radComboBoxItem.Value = "0";
            radComboBoxItem.Text = "";
            ddlColaborador.Items.Add(radComboBoxItem);

            if (dtConsulta != null)
            {
                if(dtConsulta.Rows != null)
                {
                    if(dtConsulta.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtConsulta.Rows)
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

            ddlColaborador.SelectedValue = "0";

        }

        //public void CarregarComboColaboradorAtivos()
        //{
        //    //ddlColaborador.Items.Clear();
        //    //UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
        //    //UsuarioEntity eo = new UsuarioEntity();
        //    //eo.status = EOConst.CodStatusUsuario.Ativo;

        //    //ddlColaborador.DataSource = bl.ConsultarUsuariosAtivos();
        //    //ddlColaborador.DataBind();
        //    //if (ddlColaborador.Items.Count > 0)
        //    //{
        //    //    ddlColaborador.Items.Insert(0, new ListItem("", ""));
        //    //}
        //    //else
        //    //{
        //    //    ddlColaborador.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        //    //}

        //    UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
        //    UsuarioEntity eo = new UsuarioEntity();
        //    eo.status = EOConst.CodStatusUsuario.Ativo;

        //    DataTable dtConsulta = bl.Consultar(eo);

        //    ddlColaborador.Items.Clear();
        //    ListItem itemCliente = new ListItem();

        //    Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
        //    radComboBoxItem.Value = "0";
        //    radComboBoxItem.Text = "";
        //    ddlColaborador.Items.Add(radComboBoxItem);

        //    if (dtConsulta != null)
        //    {
        //        if (dtConsulta.Rows != null)
        //        {
        //            if (dtConsulta.Rows.Count > 0)
        //            {

        //                foreach (DataRow dr in dtConsulta.Rows)
        //                {
        //                    radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
        //                    radComboBoxItem.Value = dr["idusuario"].ToString();
        //                    radComboBoxItem.Text = dr["dscombo"].ToString();
        //                    ddlColaborador.Items.Add(radComboBoxItem);
        //                }

        //            }
        //        }
        //    }

        //    ddlColaborador.AllowCustomText = true;
        //    ddlColaborador.MarkFirstMatch = true;

        //    ddlColaborador.SelectedValue = "0";

        //}
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

        private void CarregarComboGrupoTodos()
        {
            ddlgrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            //eoGrupo.status = EOConst.CodStatus.Ativo;
            ddlgrupo.DataSource = blGrupo.ConsultarTodos(eoGrupo);
            ddlgrupo.DataTextField = "nome";
            ddlgrupo.DataValueField = "idgrupo";
            ddlgrupo.DataBind();
            if (ddlgrupo.Items.Count > 0)
                ddlgrupo.Items.Insert(0, new ListItem("Todos", ""));
            else
                ddlgrupo.Items.Insert(0, new ListItem("Não existe registros cadastrados.", ""));
        }

        private void CarregarComboGrupoAtivos()
        {
            ddlgrupo.Items.Clear();
            GrupoBusinessLayer blGrupo = new GrupoBusinessLayer();
            GrupoEntity eoGrupo = new GrupoEntity();
            eoGrupo.status = EOConst.CodStatus.Ativo;
            ddlgrupo.DataSource = blGrupo.ConsultarGruposAtivos(eoGrupo);
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

        private void CarregarComboCertificadoTodos()
        {

            ddlcertificado.Items.Clear();
            CertificadoBusinessLayer blCertificado = new CertificadoBusinessLayer();
            CertificadoEntity eoCertificado = new CertificadoEntity();
            if (ddlgrupo.SelectedValue != "")
            {
                eoCertificado.idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
            }
            //eoCertificado.status = EOConst.CodStatus.Ativo;
            ddlcertificado.DataSource = blCertificado.ConsultarTodos(eoCertificado);
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
                lblmsgerro.Text = "";
                CertificacaousuarioBusinessLayer blcertuser = new CertificacaousuarioBusinessLayer();

                int idusuario = int.MinValue;
                int idgrupo = int.MinValue;
                int idfornecedor = int.MinValue;
                int idcertificado = int.MinValue;
                int idregulador = int.MinValue;
                int status = int.MinValue;
                int certificadoexpirado = int.MinValue;
                int statusaprovacao = int.MinValue;

                string departamento = "";
                DateTime dtinicertificacao = DateTime.MinValue;
                DateTime dtfimcertificacao = DateTime.MinValue;
                DateTime dtinivalidade = DateTime.MinValue;
                DateTime dtfimvalidade = DateTime.MinValue;
                bool valido = true;

                if (ddlColaborador.SelectedValue != "0")
                {
                    idusuario = Convert.ToInt32(ddlColaborador.SelectedValue);
                }
                if (ddlfornecedor.SelectedValue != "")
                {
                    idfornecedor = Convert.ToInt32(ddlfornecedor.SelectedValue);
                }
                if (ddlgrupo.SelectedValue != "")
                {
                    idgrupo = Convert.ToInt32(ddlgrupo.SelectedValue);
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
                    statusaprovacao = Convert.ToInt32(ddlStatusAprovacao.SelectedValue);
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
                    //DataTable lista = blcertuser.Consultar(idusuario, idgrupo, idfornecedor, idcertificado, idregulador, status, certificadoexpirado, departamento, dtinicertificacao, dtfimcertificacao, dtinivalidade, dtfimvalidade, statusaprovacao);
                    DataTable lista = blcertuser.ConsultarPesquisaCertificacoesColaborador(idusuario, idgrupo, idfornecedor, idcertificado, idregulador, status, certificadoexpirado, departamento, dtinicertificacao, dtfimcertificacao, dtinivalidade, dtfimvalidade, statusaprovacao);

                    DataTable dtConsulta = this.CriaDataTableConsulta();
                    DataRow linha;

                    int idUsuario = ((UsuarioEntity)Session["eoUs"]).idusuario;

                    if (lista != null)
                    {
                        if(lista.Rows != null)
                        {
                            if (lista.Rows.Count > 0)
                            {
                                foreach (DataRow linhaAux in lista.Rows)
                                {
                                    if ((int.Parse(linhaAux["idusuario"].ToString()) == idUsuario) && (linhaAux["aprovacao"].ToString() == "1" || (linhaAux["aprovacao"].ToString() == "2") || (linhaAux["aprovacao"] == null)))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        linha = dtConsulta.NewRow();

                                        linha["idcertificacao"] = linhaAux["idcertificacao"];
                                        linha["idusuario"] = linhaAux["idusuario"];
                                        linha["idcertificado"] = linhaAux["idcertificado"];
                                        linha["descricao"] = linhaAux["descricao"];
                                        linha["numlicenca"] = linhaAux["numlicenca"];
                                        linha["url"] = linhaAux["url"];
                                        linha["expira"] = linhaAux["expira"];
                                        linha["dtinclusao"] = linhaAux["dtinclusao"];
                                        linha["idregulador"] = linhaAux["idregulador"];
                                        linha["observacao"] = linhaAux["observacao"];
                                        linha["aprovacao"] = linhaAux["aprovacao"];
                                        linha["motivoreprovacao"] = linhaAux["motivoreprovacao"];
                                        linha["idreprovador"] = linhaAux["idreprovador"];
                                        linha["nomeprova"] = linhaAux["nomeprova"];
                                        linha["dtprova"] = linhaAux["dtprova"];
                                        linha["nomegrupo"] = linhaAux["nomegrupo"];
                                        linha["nomecertificado"] = linhaAux["nomecertificado"];
                                        linha["versao"] = linhaAux["versao"];
                                        linha["nomeregulador"] = linhaAux["nomeregulador"];
                                        linha["nomecolaborador"] = linhaAux["nomecolaborador"];
                                        linha["status"] = linhaAux["status"];
                                        linha["nomefornecedor"] = linhaAux["nomefornecedor"];
                                        linha["validade"] = linhaAux["validade"];
                                        linha["dtcertificacao"] = linhaAux["dtcertificacao"];
                                        linha["statusprova"] = !String.IsNullOrEmpty(linhaAux["statusprova"].ToString()) ? linhaAux["statusprova"].ToString():"";
                                        
                                     //   linha["aprovacao"] = linhaAux["aprovacao"];

                                        dtConsulta.Rows.Add(linha);
                                    }
                                }
                            }
                        }
                    }

                    if (dtConsulta.Rows.Count == 0)
                    {
                        divInfo.Visible = true;
                        lblmsInfo.Text = "Não existe registro para filtro informado!";
                    }
                    else
                    {
                        divInfo.Visible = false;
                    }

                   
                    grdpesquisa.DataSource = dtConsulta;
                    grdpesquisa.DataBind();
                    try
                    {
                        int countcoluna = grdpesquisa.Columns.Count;
                        if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoAlterarCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                            grdpesquisa.Columns[countcoluna - 3].Visible = false;
                        if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoExcluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                            grdpesquisa.Columns[countcoluna - 2].Visible = false;
                        if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoVisualizarCertificacao, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                            grdpesquisa.Columns[countcoluna - 4].Visible = false;
                        if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoDownload, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                            grdpesquisa.Columns[countcoluna - 5].Visible = false;

                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        public DataTable CriaDataTableConsulta()

        {

            DataTable mDataTable = new DataTable();

            DataColumn mDataColumn;
            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "idcertificacao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "idusuario";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "idcertificado";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "descricao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "numlicenca";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "url";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "expira";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "dtinclusao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "idregulador";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "observacao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.Int32");
            mDataColumn.ColumnName = "aprovacao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "motivoreprovacao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "idreprovador";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomeprova";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "dtprova";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomegrupo";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomecertificado";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "versao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomeregulador";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomecolaborador";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.Int32");
            mDataColumn.ColumnName = "status";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "nomefornecedor";
            mDataTable.Columns.Add(mDataColumn);


            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "validade";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "dtcertificacao";
            mDataTable.Columns.Add(mDataColumn);

            mDataColumn = new DataColumn();
            mDataColumn.DataType = Type.GetType("System.String");
            mDataColumn.ColumnName = "statusprova";
            mDataTable.Columns.Add(mDataColumn);

            return mDataTable;

        }


        protected void grdpesquisa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            diverro.Visible = false;
            try
            {
                if (e.CommandName == "cmdAlterar" || e.CommandName == "cmdExcluir" || e.CommandName == "cmdAbrir"
                    || e.CommandName == "cmdVisualizar")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdpesquisa.DataKeys[index];
                    if (e.CommandName == "cmdAlterar")
                    {
                        Session.Remove("Aprovacao");
                        Session["Origem"] = "CertificacaoColaborador";
                        Session["eoMinhaCertificacaousuario"] = data.Values["idcertificacao"].ToString();
                        Response.Redirect("CertificacaoColaboradorOperacao.aspx");
                    }
                    else if (e.CommandName == "cmdExcluir")
                    {
                        CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                        CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                        eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                        bl.Excluir(eo);
                        CarregarGrid();
                    }
                    else if (e.CommandName == "cmdAbrir")
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

                    else if (e.CommandName == "cmdVisualizar")
                    {
                        CertficadoDigitalizadoBusinessLayer bl = new CertficadoDigitalizadoBusinessLayer();
                        CertficadoDigitalizadoEntity eo = new CertficadoDigitalizadoEntity();
                        eo.idcertificacao = Convert.ToInt32(data.Values["idcertificacao"].ToString());
                        DataTable dt = new DataTable();
                        dt = bl.ConsultarArquivo(eo);
                        if (dt.Rows.Count > 0)
                        {
                            Session["ArquivoDigitalizado"] = (byte[])dt.Rows[0]["arquivodigitalizado"];
                            Session["NomeArquivoDigitalizado"] = dt.Rows[0]["arquivo"].ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "visualizar", "window.open('VisualizarCertificado.aspx');", true);
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.CertificacaoExcluirCertificacoesColaborador, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    LinkButton btnexc = e.Row.Cells[grdpesquisa.Columns.Count - 1].Controls[0] as LinkButton;
                    btnexc.OnClientClick = "if (confirm('Confirma a exclusão desse registro?') == false) return false;";
                    btnexc.ToolTip = "Inativar Registro.";
                }

                try
                {
                    LinkButton btnalt = e.Row.Cells[grdpesquisa.Columns.Count - 2].Controls[0] as LinkButton;
                    btnalt.ToolTip = "Alterar Registro";
                }
                catch { }

                try
                {
                    LinkButton btnalt = e.Row.Cells[grdpesquisa.Columns.Count - 3].Controls[0] as LinkButton;
                    btnalt.ToolTip = "Visualizar Certificado";
                }
                catch { }

                try
                {
                    LinkButton btnalt = e.Row.Cells[grdpesquisa.Columns.Count - 4].Controls[0] as LinkButton;
                    btnalt.ToolTip = "Download Certificado";
                }
                catch { }
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

        protected void lnkNovaCertificacao_Click(object sender, EventArgs e)
        {
            Session["eoMinhaCertificacaousuario"] = null;
            Session["dtcombocolaborador"] = null;
            Session["Origem"] = null;
            Session["recarregarSessao"] = null;
            Response.Redirect("CertificacaoColaboradorOperacao.aspx");
        }

    }
}