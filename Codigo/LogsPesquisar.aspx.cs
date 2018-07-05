using Business;
using ClosedXML.Excel;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class LogsPesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.UsuariosLogsPesquisar, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    Response.Redirect("SemAcesso.aspx");
                }
                //this.carregarFuncionalidades();
                this.CarregarUsuario();
                txtDataInicio.Text = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
              //  ScriptManager.RegisterStartupScript(this, this.GetType(), "combo", "comboautocomplete();", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        public override bool EnableEventValidation
        {
            get { return false; }
            set { /*Do nothing*/ }
        }
        private void carregarFuncionalidades()
        {
            FuncionalidadeBusinessLayer bl = new FuncionalidadeBusinessLayer();
            ddlFuncionalidades.DataSource = bl.ConsultarList();

            ddlFuncionalidades.DataTextField = "nome";
            ddlFuncionalidades.DataValueField = "idfuncionalidade";
            ddlFuncionalidades.DataBind();
            ddlFuncionalidades.Items.Insert(0, "Todos");
            ddlFuncionalidades.Items[0].Value = "0";
        }

        private void CarregarUsuario()
        {
            UsuarioBusinessLayer bl = new UsuarioBusinessLayer();

            DataTable dtUsuario = bl.ConsultarUsuariosAtivos();

            ddlUsuario.Items.Clear();
            ListItem itemCliente = new ListItem();

            Telerik.Web.UI.RadComboBoxItem radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
            radComboBoxItem.Value = "0";
            radComboBoxItem.Text = "Todos";
            ddlUsuario.Items.Add(radComboBoxItem);

            if (dtUsuario != null)
            {
                if (dtUsuario.Rows != null)
                {
                    if (dtUsuario.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtUsuario.Rows)
                        {
                            radComboBoxItem = new Telerik.Web.UI.RadComboBoxItem();
                            radComboBoxItem.Value = dr["idusuario"].ToString();
                            radComboBoxItem.Text = dr["dscombo"].ToString();
                            ddlUsuario.Items.Add(radComboBoxItem);
                        }

                    }
                }
            }

            ddlUsuario.AllowCustomText = true;
            ddlUsuario.MarkFirstMatch = true;
            ddlUsuario.SelectedValue = "0";

            // ddlUsuario.DataSource = bl.ConsultarUsuariosAtivos();
            //ddlUsuario.DataTextField = "nome";
            //ddlUsuario.DataValueField = "idusuario";
            //ddlUsuario.DataBind();
            //ddlUsuario.Items.Insert(0, "Todos");
            //ddlUsuario.Items[0].Value = "0";
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            divErro.Visible = false;
            grdpesquisa.PageIndex = 0;
            CarregarGrid();
        }


        private void CarregarGrid()
        {
            try
            {
                LogRegistroBusinessLayer bl = new LogRegistroBusinessLayer();
                LogregistroEntity eo = new LogregistroEntity();


                string msgRetorno = ValidarFormLog();
                if (!string.IsNullOrEmpty(msgRetorno))
                    throw new Exception(msgRetorno);


                if (ddlUsuario.SelectedValue != "0")
                    eo.idusuario = Convert.ToInt32(ddlUsuario.SelectedValue);

                if (ddlAcao.SelectedValue != "0")
                    eo.tipolog = ddlAcao.SelectedItem.Text;

                if (ddlFuncionalidades.SelectedValue != "0")
                    eo.nomefuncionalidade = ddlFuncionalidades.SelectedItem.Text;

                eo.Log.dtInicio = Convert.ToDateTime(txtDataInicio.Text);
                eo.Log.dtFim = Convert.ToDateTime(txtDataFinal.Text);

                Session["Grid"] = bl.Consultar(eo.idusuario, eo.nomefuncionalidade, eo.tipolog, eo.Log.dtInicio, eo.Log.dtFim);

                DataTable lista = (DataTable)Session["Grid"];

                if (lista.Rows.Count == 0)
                {
                    divInfo.Visible = true;
                    lblmsInfo.Text = "Não existe registro para filtro informado!";
                }
                else
                {
                    divInfo.Visible = false;
                }

                grdpesquisa.DataSource = lista;
                grdpesquisa.DataBind();

            }
            catch (Exception ex)
            {
                divErro.Visible = true;
                lblMsgErro.Text = ex.Message;
            }

        }
        private string ValidarFormLog()
        {
            string msgRetorno = string.Empty;

            DateTime dtInicio = DateTime.MinValue;
            DateTime dtFim = DateTime.MinValue;

            if (string.IsNullOrEmpty(txtDataInicio.Text))
            {
                return msgRetorno = "o campo data inicial é obrigatório!";
            }

            if (string.IsNullOrEmpty(txtDataFinal.Text))
            {
                return msgRetorno = "o campo data final é obrigatório!";
            }

            if (!DateTime.TryParse(txtDataInicio.Text, out dtInicio))
            {
                return msgRetorno = "Data Inicial inválida";
            }

            if (!DateTime.TryParse(txtDataFinal.Text, out dtFim))
            {
                return msgRetorno = "Data Final inválida";
            }

            return msgRetorno;
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        private void ExportGridToExcel()
        {

            using (XLWorkbook wb = new XLWorkbook())
            {

                DataTable dt = (DataTable)Session["Grid"];
                

                wb.Worksheets.Add(dt, "Customers");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LogsPesquisar.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                Session["Grid"] = null;
            }
        }

        protected void grdpesquisa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex > -1)
            {
                grdpesquisa.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
        }

        protected void grdpesquisa_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {

        }
    }
}