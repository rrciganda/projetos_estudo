using Business;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class CertificacaoUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["dtcertificado"] = null;
                Session["dtusuario"] = null;
                Session["dtcolaboradorddl"] = null;
                AdicionarLinhaCertificado();
                AdicionarLinhaUsuario();

                CarregarComboCertificacao();
                CarregarComboColaborador();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);
        }

        public void CarregarComboColaborador()
        {
            ddlColaborador.DataSource = GETDTColaborador();
            ddlColaborador.DataBind();
            if (ddlColaborador.Items.Count > 0)
            {
                ddlColaborador.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                ddlColaborador.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }

        public DataTable GETDTColaborador()
        {
            if (Session["dtcolaboradorddl"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idusuario");
                dt.Columns.Add("dscombo");
                UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                UsuarioEntity eo = new UsuarioEntity();
                eo.status = EOConst.CodStatusUsuario.Ativo;
                dt = bl.Consultar(eo);
                Session["dtcolaboradorddl"] = dt;
            }
            return (DataTable)Session["dtcolaboradorddl"];
        }
        public void CarregarComboCertificacao()
        {
            ddlcertificacao.DataSource = GETDTCertificacao();
            ddlcertificacao.DataBind();
            if (ddlcertificacao.Items.Count > 0)
            {
                ddlcertificacao.Items.Insert(0, new ListItem("", ""));
            }
            else
            {
                ddlcertificacao.Items.Insert(0, new ListItem("", "Não existe registro cadastrado."));
            }
        }
        public DataTable GETDTCertificacao()
        {
            if (Session["dtcertificacao"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idcertificacao");
                dt.Columns.Add("nome");
                CertificadoBusinessLayer bl = new CertificadoBusinessLayer();
                CertificadoEntity eo = new CertificadoEntity();
                eo.status = EOConst.CodStatus.Ativo;
                dt = bl.Consultar(eo);
                Session["dtcertificacao"] = dt;
            }
            return (DataTable)Session["dtcertificacao"];
        }

        public DataTable GETDTRegulador()
        {
            if (Session["dtregulador"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("idregulador");
                dt.Columns.Add("nome");
                ReguladorBusinessLayer bl = new ReguladorBusinessLayer();
                ReguladorEntity eo = new ReguladorEntity();
                eo.status = EOConst.CodStatus.Ativo;
                dt = bl.Consultar(eo);
                Session["dtregulador"] = dt;
            }
            return (DataTable)Session["dtregulador"];
        }

        public void CriarDTCertificado()
        {
            if (Session["dtcertificado"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("idcertificacao");
                dt.Columns.Add("idregulador");
                dt.Columns.Add("dtcertificacao");
                dt.Columns.Add("dtvalidade");
                Session["dtcertificado"] = dt;
            }
        }

        public void AdicionarLinhaCertificado()
        {
            if (Session["dtcertificado"] == null)
            {
                CriarDTCertificado();
            }
            DataTable dt = (DataTable)Session["dtcertificado"];
            DataRow dr = dt.NewRow();
            dr["id"] = dt.Rows.Count + 1;
            dr["idcertificacao"] = "";
            dr["idregulador"] = "";
            dr["dtcertificacao"] = "";
            dr["dtvalidade"] = "";
            dt.Rows.Add(dr);

            Session["dtcertificado"] = dt;
            grdcertificacao.DataSource = (DataTable)Session["dtcertificado"];
            grdcertificacao.DataBind();
        }

        public void AdicionarLinhaCertificado(string idcertificacao, string idregulador, string dtcertificacao, string dtvalidade)
        {
            if (Session["dtcertificado"] == null)
            {
                CriarDTCertificado();
            }
            DataTable dt = (DataTable)Session["dtcertificado"];
            DataRow dr = dt.NewRow();
            dr["id"] = dt.Rows.Count + 1;
            dr["idcertificacao"] = idcertificacao;
            dr["idregulador"] = idregulador;
            dr["dtcertificacao"] = dtcertificacao;
            dr["dtvalidade"] = dtvalidade;
            dt.Rows.Add(dr);

            Session["dtcertificado"] = dt;
            grdcertificacao.DataSource = (DataTable)Session["dtcertificado"];
            grdcertificacao.DataBind();
        }

        public void AdicionarLinhaUsuario()
        {
            if (Session["dtusuario"] == null)
            {
                CriarDTUsuario();
            }
            DataTable dt = (DataTable)Session["dtusuario"];
            DataRow dr = dt.NewRow();
            dr["id"] = dt.Rows.Count + 1;
            dr["idusuario"] = "";
            dr["idregulador"] = "";
            dr["dtcertificacao"] = "";
            dr["dtvalidade"] = "";
            //dr["msgvalidacaousuario"] = "";
            //dr["cormsg"] = "DarkRed";

            dt.Rows.Add(dr);

            Session["dtusuario"] = dt;
            grdusuario.DataSource = (DataTable)Session["dtusuario"];
            grdusuario.DataBind();

        }

        public void AdicionarLinhaUsuario(string idusuario, string idregulador, string dtcertificacao, string dtvalidade)//, string msgvalidacaousuario, string cormsg)
        {
            if (Session["dtusuario"] == null)
            {
                CriarDTUsuario();
            }
            DataTable dt = (DataTable)Session["dtusuario"];
            DataRow dr = dt.NewRow();
            dr["id"] = dt.Rows.Count + 1;
            dr["idusuario"] = idusuario;
            dr["idregulador"] = idregulador;
            dr["dtcertificacao"] = dtcertificacao;
            dr["dtvalidade"] = dtvalidade;
            //dr["msgvalidacaousuario"] = msgvalidacaousuario;
            //dr["cormsg"] = cormsg;

            dt.Rows.Add(dr);

            Session["dtusuario"] = dt;
            grdusuario.DataSource = (DataTable)Session["dtusuario"];
            grdusuario.DataBind();

        }

        public void CriarDTUsuario()
        {
            if (Session["dtusuario"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("idusuario");
                dt.Columns.Add("idregulador");
                dt.Columns.Add("dtcertificacao");
                dt.Columns.Add("dtvalidade");
                //dt.Columns.Add("msgvalidacaousuario");
                //dt.Columns.Add("cormsg");
                Session["dtusuario"] = dt;
            }
        }

        protected void RadioButtonUsuario_CheckedChanged(object sender, EventArgs e)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            divsucesso.Visible = false;
            lblsucesso.Text = "";
            DivTelaUsuario.Visible = true;
            DivTelaCertificacao.Visible = false;
        }

        protected void RadioButtonCertificacao_CheckedChanged(object sender, EventArgs e)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            divsucesso.Visible = false;
            lblsucesso.Text = "";
            DivTelaUsuario.Visible = false;
            DivTelaCertificacao.Visible = true;
        }

        protected void lnkAdicionarCertificacao_Click(object sender, EventArgs e)
        {
            AtualizarDtCertificacao();
            AdicionarLinhaCertificado();
        }

        protected void grdcertificacao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl = ((DropDownList)e.Row.FindControl("ddlgridcertificacao"));
                ddl.DataSource = GETDTCertificacao();
                ddl.DataBind();
                if (ddl.Items.Count > 0)
                {
                    ddl.Items.Insert(0, new ListItem("", ""));
                }

                try
                {
                    ddl.SelectedValue = ((HiddenField)e.Row.FindControl("hdngrididcertificacao")).Value;
                }
                catch
                {
                }

                DropDownList ddlregulador = ((DropDownList)e.Row.FindControl("ddlgridregulador"));
                ddlregulador.DataSource = GETDTRegulador();
                ddlregulador.DataBind();
                if (ddlregulador.Items.Count > 0)
                {
                    ddlregulador.Items.Insert(0, new ListItem("", ""));
                }

                try
                {
                    ddlregulador.SelectedValue = ((HiddenField)e.Row.FindControl("hdngrididregulador")).Value;
                }
                catch
                {
                }
            }

        }
        private void AtualizarDtCertificacao()
        {
            Session["dtcertificado"] = null;

            if (grdcertificacao.Rows.Count > 0)
            {
                foreach (GridViewRow item in grdcertificacao.Rows)
                {
                    string idcertificacao = "";
                    string idregulador = "";
                    string dtcertificacao = "";
                    string dtvalidade = "";
                    if (item.FindControl("ddlgridcertificacao") != null)
                        idcertificacao = ((DropDownList)item.FindControl("ddlgridcertificacao")).SelectedValue;
                    if (item.FindControl("ddlgridregulador") != null)
                        idregulador = ((DropDownList)item.FindControl("ddlgridregulador")).SelectedValue;
                    if (item.FindControl("txtgriddtcertificacao") != null)
                        dtcertificacao = ((TextBox)item.FindControl("txtgriddtcertificacao")).Text;
                    if (item.FindControl("txtgriddtvalidade") != null)
                        dtvalidade = ((TextBox)item.FindControl("txtgriddtvalidade")).Text;

                    AdicionarLinhaCertificado(idcertificacao, idregulador, dtcertificacao, dtvalidade);

                }
            }
        }

        private void AtualizarDtUsuario()
        {
            Session["dtusuario"] = null;

            if (grdusuario.Rows.Count > 0)
            {
                foreach (GridViewRow item in grdusuario.Rows)
                {
                    string idusuario = "";
                    string idregulador = "";
                    string dtcertificacao = "";
                    string dtvalidade = "";
                    string msgvalidacaousuario = "";
                    string cormsg = "";

                    if (item.FindControl("ddlgridcolaborador") != null)
                        idusuario = ((DropDownList)item.FindControl("ddlgridcolaborador")).SelectedValue;
                    if (item.FindControl("ddlgridregulador") != null)
                        idregulador = ((DropDownList)item.FindControl("ddlgridregulador")).SelectedValue;
                    if (item.FindControl("txtgriddtcertificacao") != null)
                        dtcertificacao = ((TextBox)item.FindControl("txtgriddtcertificacao")).Text;
                    if (item.FindControl("txtgriddtvalidade") != null)
                        dtvalidade = ((TextBox)item.FindControl("txtgriddtvalidade")).Text;
                    //if (item.FindControl("lblgridvalidacaousuario") != null)
                    //{
                    //    msgvalidacaousuario = ((Label)item.FindControl("lblgridvalidacaousuario")).Text;
                    //    cormsg = ((Label)item.FindControl("lblgridvalidacaousuario")).ForeColor.Name.ToString();
                    //}

                    //AdicionarLinhaUsuario(idusuario, idregulador, dtcertificacao, dtvalidade, msgvalidacaousuario, cormsg);
                    AdicionarLinhaUsuario(idusuario, idregulador, dtcertificacao, dtvalidade);

                }
            }
        }

        private void RemoverLinhaDtUsuario(string id)
        {
            DataTable dt = (DataTable)Session["dtusuario"];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["id"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }

            int novoid = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["id"] = novoid;
                novoid++;
            }
            Session["dtusuario"] = dt;
            grdusuario.DataSource = dt;
            grdusuario.DataBind();
        }

        private void RemoverLinhaDtCertificacao(string id)
        {
            DataTable dt = (DataTable)Session["dtcertificado"];

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["id"].ToString() == id)
                {
                    dt.Rows.Remove(dr);
                    break;
                }
            }

            int novoid = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["id"] = novoid;
                novoid++;
            }
            Session["dtcertificado"] = dt;
            grdcertificacao.DataSource = dt;
            grdcertificacao.DataBind();
        }

        protected void grdcertificacao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    AtualizarDtCertificacao();
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdcertificacao.DataKeys[index];
                    RemoverLinhaDtCertificacao(data.Values["id"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }

        }

        protected void grdusuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlcolaborador = ((DropDownList)e.Row.FindControl("ddlgridcolaborador"));
                ddlcolaborador.DataSource = GETDTColaborador();
                ddlcolaborador.DataBind();
                if (ddlcolaborador.Items.Count > 0)
                {
                    ddlcolaborador.Items.Insert(0, new ListItem("", ""));
                }

                try
                {
                    ddlcolaborador.SelectedValue = ((HiddenField)e.Row.FindControl("hdngrididusuario")).Value;
                }
                catch
                {
                }


                DropDownList ddlregulador = ((DropDownList)e.Row.FindControl("ddlgridregulador"));
                ddlregulador.DataSource = GETDTRegulador();
                ddlregulador.DataBind();
                if (ddlregulador.Items.Count > 0)
                {
                    ddlregulador.Items.Insert(0, new ListItem("", ""));
                }

                try
                {
                    ddlregulador.SelectedValue = ((HiddenField)e.Row.FindControl("hdngrididregulador")).Value;
                }
                catch
                {
                }
                try
                {
                    HiddenField hdncormsg = ((HiddenField)e.Row.FindControl("hdncormsg"));
                    if (hdncormsg.Value != "")
                    {
                        ((Label)e.Row.FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.FromName(hdncormsg.Value);
                    }
                }
                catch (Exception)
                {

                }
            }

        }

        protected void grdusuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcluir")
            {
                try
                {
                    AtualizarDtUsuario();
                    int index = Convert.ToInt32(e.CommandArgument);
                    DataKey data = grdusuario.DataKeys[index];
                    RemoverLinhaDtUsuario(data.Values["id"].ToString());
                }
                catch (Exception ex)
                {
                    //   summary.Text = "Mensagem do Sistema: " + ex.Message;
                }
            }
            if (e.CommandName == "cmdValidarUsuario")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).Text = "";
                    if (((TextBox)grdusuario.Rows[index].FindControl("txtgrididusuario")).Text == "")
                    {
                        ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).Text = "Informe o login.";
                        ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else
                    {
                        UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                        UsuarioEntity eo = new UsuarioEntity();
                        //eo.matricula = ((TextBox)grdusuario.Rows[index].FindControl("txtgrididusuario")).Text;
                        eo.login = ((TextBox)grdusuario.Rows[index].FindControl("txtgrididusuario")).Text;
                        eo.status = EOConst.CodStatus.Ativo;
                        DataTable dt = bl.ConsultarPorLoginOuMatricula(eo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (dt.Rows.Count == 1)
                            {
                                ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).Text = "Usuário Válido.";
                                ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.DarkGreen;
                            }
                        }
                        else
                        {
                            ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).Text = "Login inexistente.";
                            ((Label)grdusuario.Rows[index].FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.DarkRed;
                        }
                    }


                }
                catch
                {
                }
            }
        }

        protected void lnkValidarUsuario_Click(object sender, EventArgs e)
        {
            //lblvalidacaousuario.Text = "";
            //if (txtloginmatricula.Text == "")
            //{
            //    lblvalidacaousuario.Text = "Informe o login e/ou matricula para ser validado.";
            //    lblvalidacaousuario.ForeColor = System.Drawing.Color.DarkRed;
            //}
            //else
            //{
            //    UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
            //    UsuarioEntity eo = new UsuarioEntity();
            //    eo.matricula = txtloginmatricula.Text;
            //    eo.login = txtloginmatricula.Text;
            //    eo.status = EOConst.CodStatus.Ativo;
            //    DataTable dt = bl.ConsultarPorLoginOuMatricula(eo);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows.Count == 1)
            //        {
            //            lblvalidacaousuario.Text = "Usuário Válido.";
            //            lblvalidacaousuario.ForeColor = System.Drawing.Color.DarkGreen;
            //        }
            //    }
            //    else
            //    {
            //        lblvalidacaousuario.Text = "Login e/ou matricula inexistente.";
            //        lblvalidacaousuario.ForeColor = System.Drawing.Color.DarkRed;
            //    }
            //}
        }

        protected void lnkAdicionarUsuario_Click(object sender, EventArgs e)
        {
            AtualizarDtUsuario();
            AdicionarLinhaUsuario();
        }

        protected void lnkSalvar_Click(object sender, EventArgs e)
        {
            diverro.Visible = false;
            lblmsgerro.Text = "";
            divsucesso.Visible = false;
            lblsucesso.Text = "";

            string msgvalidacao = "";
            bool valido = true;
            List<CertificacaousuarioEntity> lst = new List<CertificacaousuarioEntity>();
            AtualizarDtCertificacao();
            AtualizarDtUsuario();
            if (RadioButtonCertificacao.Checked)
            {
                if (ddlcertificacao.SelectedValue == "")
                {
                    msgvalidacao = msgvalidacao + "Certificação não informada; <br />";
                    valido = false;
                }
                foreach (GridViewRow item in grdusuario.Rows)
                {
                    bool linhavalida = true;
                    string linha = item.Cells[0].Text;
                    string msglinha = "Erro Linha " + linha + ": ";
                    //int idusuario = int.MinValue;

                    string colaborador = ((DropDownList)item.FindControl("ddlgridcolaborador")).SelectedValue;
                    if (colaborador == "")
                    {
                        msglinha = msglinha + "Colaborador não informado; ";
                        linhavalida = false;
                    }
                    //if (idusuariogrid == "")
                    //{
                    //    msglinha = msglinha + "Usuário não informado; ";
                    //    linhavalida = false;
                    //}
                    //else
                    //{
                    //    UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                    //    UsuarioEntity eo = new UsuarioEntity();
                    //    eo.matricula = ((TextBox)item.FindControl("txtgrididusuario")).Text;
                    //    eo.login = ((TextBox)item.FindControl("txtgrididusuario")).Text;
                    //    eo.status = EOConst.CodStatus.Ativo;
                    //    DataTable dt = bl.ConsultarPorLoginOuMatricula(eo);
                    //    if (dt != null && dt.Rows.Count > 0)
                    //    {
                    //        if (dt.Rows.Count == 1)
                    //        {
                    //            ((Label)item.FindControl("lblgridvalidacaousuario")).Text = "Usuário Válido.";
                    //            ((Label)item.FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.DarkGreen;
                    //            idusuario = Convert.ToInt32(dt.Rows[0]["idusuario"].ToString());
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ((Label)item.FindControl("lblgridvalidacaousuario")).Text = "Login e/ou matricula inexistente.";
                    //        ((Label)item.FindControl("lblgridvalidacaousuario")).ForeColor = System.Drawing.Color.DarkRed;
                    //        msglinha = msglinha + "Login e/ou matricula inexistente; ";
                    //        linhavalida = false;
                    //    }
                    //}

                    string regulador = ((DropDownList)item.FindControl("ddlgridregulador")).SelectedValue;
                    if (regulador == "")
                    {
                        msglinha = msglinha + "Regulador não informado; ";
                        linhavalida = false;
                    }

                    string dtcertificacao = ((TextBox)item.FindControl("txtgriddtcertificacao")).Text;
                    if (dtcertificacao == "")
                    {
                        msglinha = msglinha + "Data da certificação não informada; ";
                        linhavalida = false;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToDateTime(dtcertificacao);
                        }
                        catch (Exception)
                        {
                            msglinha = msglinha + "Data da certificação informada não é valida; ";
                            linhavalida = false;
                        }
                    }

                    string dtvalidade = ((TextBox)item.FindControl("txtgriddtvalidade")).Text;
                    //if (dtvalidade == "")
                    //{
                    //    msglinha = msglinha + "Data da validade não informada; ";
                    //    linhavalida = false;
                    //}
                    //else
                    //{
                    //    try
                    //    {
                    //        Convert.ToDateTime(dtvalidade);
                    //    }
                    //    catch (Exception)
                    //    {
                    //        msglinha = msglinha + "Data da validade informada não é valida; ";
                    //        linhavalida = false;
                    //    }
                    //}

                    if (!linhavalida)
                    {
                        msgvalidacao = msgvalidacao + msglinha + "<br />";
                        valido = false;
                    }
                    else
                    {
                        if (valido)
                        {
                            CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                            eo.idcertificado = Convert.ToInt32(ddlcertificacao.SelectedValue);
                            eo.idusuario = Convert.ToInt32(colaborador);
                            eo.idregulador = Convert.ToInt32(regulador);
                            eo.dtcertificacao = Convert.ToDateTime(dtcertificacao);
                            eo.validade = Convert.ToDateTime(dtvalidade);
                            //eo.status = EOConst.CodStatusCertificacao.Pendente;
                            eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                            lst.Add(eo);
                        }
                    }
                }
            }

            if (RadioButtonUsuario.Checked)
            {
                int idusuario = int.MinValue;

                if (ddlColaborador.SelectedValue == "")
                {
                    msgvalidacao = msgvalidacao + "Usuário não informado; <br />";
                    valido = false;

                }
                else
                {
                    //UsuarioBusinessLayer bl = new UsuarioBusinessLayer();
                    //UsuarioEntity eo = new UsuarioEntity();
                    //eo.matricula = txtloginmatricula.Text;
                    //eo.login = txtloginmatricula.Text;
                    //eo.status = EOConst.CodStatus.Ativo;
                    //DataTable dt = bl.ConsultarPorLoginOuMatricula(eo);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    if (dt.Rows.Count == 1)
                    //    {
                    //        lblvalidacaousuario.Text = "Usuário Válido.";
                    //        lblvalidacaousuario.ForeColor = System.Drawing.Color.DarkGreen;
                    //        idusuario = Convert.ToInt32(dt.Rows[0]["idusuario"].ToString());
                    //    }
                    //}
                    //else
                    //{
                    //    lblvalidacaousuario.Text = "Login e/ou matricula inexistente.";
                    //    lblvalidacaousuario.ForeColor = System.Drawing.Color.DarkRed;
                    //    valido = false;
                    //    msgvalidacao = msgvalidacao + "Login e/ ou matricula inexistente; < br />";

                    //}
                }
                foreach (GridViewRow item in grdcertificacao.Rows)
                {
                    bool linhavalida = true;
                    string linha = item.Cells[0].Text;
                    string msglinha = "Erro Linha " + linha + ": ";
                    string idcertgrid = ((DropDownList)item.FindControl("ddlgridcertificacao")).SelectedValue;

                    if (idcertgrid == "")
                    {
                        msglinha = msglinha + "Certificação não informada; ";
                        linhavalida = false;
                    }

                    string regulador = ((DropDownList)item.FindControl("ddlgridregulador")).SelectedValue;
                    if (regulador == "")
                    {
                        msglinha = msglinha + "Regulador não informado; ";
                        linhavalida = false;
                    }

                    string dtcertificacao = ((TextBox)item.FindControl("txtgriddtcertificacao")).Text;
                    if (dtcertificacao == "")
                    {
                        msglinha = msglinha + "Data da certificação não informada; ";
                        linhavalida = false;
                    }
                    else
                    {
                        try
                        {
                            Convert.ToDateTime(dtcertificacao);
                        }
                        catch (Exception)
                        {
                            msglinha = msglinha + "Data da certificação informada não é valida; ";
                            linhavalida = false;
                        }
                    }

                    string dtvalidade = ((TextBox)item.FindControl("txtgriddtvalidade")).Text;
                    //if (dtvalidade == "")
                    //{
                    //    msglinha = msglinha + "Data da validade não informada; ";
                    //    linhavalida = false;
                    //}
                    //else
                    //{
                    //    try
                    //    {
                    //        Convert.ToDateTime(dtvalidade);
                    //    }
                    //    catch (Exception)
                    //    {
                    //        msglinha = msglinha + "Data da validade informada não é valida; ";
                    //        linhavalida = false;
                    //    }
                    //}

                    if (!linhavalida)
                    {
                        msgvalidacao = msgvalidacao + msglinha + "<br />";
                        valido = false;
                    }
                    else
                    {
                        if (valido)
                        {
                            CertificacaousuarioEntity eo = new CertificacaousuarioEntity();
                            eo.idcertificado = Convert.ToInt32(idcertgrid);
                            eo.idusuario = idusuario;
                            eo.idregulador = Convert.ToInt32(regulador);
                            eo.dtcertificacao = Convert.ToDateTime(dtcertificacao);
                            eo.validade = Convert.ToDateTime(dtvalidade);
                            //eo.status = EOConst.CodStatusCertificacao.Pendente;
                            eo.Log = ((UsuarioEntity)Session["eoUs"]).Log;
                            lst.Add(eo);
                        }
                    }
                }
            }
            if (!valido)
            {
                diverro.Visible = true;
                lblmsgerro.Text = msgvalidacao;
            }
            else
            {
                foreach (CertificacaousuarioEntity item in lst)
                {
                    CertificacaousuarioBusinessLayer bl = new CertificacaousuarioBusinessLayer();
                    bl.Incluir(item);
                }
                Session["dtcertificado"] = null;
                Session["dtusuario"] = null;
                AdicionarLinhaCertificado();
                AdicionarLinhaUsuario();

                divsucesso.Visible = true;
                lblsucesso.Text = "Certificações incluidas com sucesso!";
            }

        }
    }
}
