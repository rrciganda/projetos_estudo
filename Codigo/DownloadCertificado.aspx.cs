using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class DownloadCertificado : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DownloadPDF();
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //  DownloadPDF();
        }
        private void DownloadPDF()
        {
            //CertficadoDigitalizadoBusinessLayer blcertdigi = new CertficadoDigitalizadoBusinessLayer();
            //CertficadoDigitalizadoEntity eocertdigi = new CertficadoDigitalizadoEntity();
            //eocertdigi.idcertificacao = Convert.ToInt32(idcertificacao);
            //DataTable dt = new DataTable();
            //dt = blcertdigi.ConsultarArquivo(eocertdigi);

            byte[] dbbyte;
            //dbbyte = (byte[])dt.Rows[0]["arquivodigitalizado"];
            //string nomeArquivo = dt.Rows[0]["arquivo"].ToString();
            if (Session["NomeArquivoDigitalizado"] == null)
            {
                return;
            }
            dbbyte = (byte[])Session["ArquivoDigitalizado"];
            string nomeArquivo = Session["NomeArquivoDigitalizado"].ToString();
            Response.Clear();
            MemoryStream ms = new MemoryStream(dbbyte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + nomeArquivo);
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);


            try
            {
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception)
            {

            }
        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            DownloadPDF();
            string script = @"
alert('1');
if (window.opener)
{
    window.opener.returnValue = 'DOWNLOAD';
}
window.returnValue = 'DOWNLOAD';
window.parent.FecharModalAnexo('DOWNLOAD');

";
            ClientScript.RegisterStartupScript(this.GetType(), "DOWNLOAD", script, true);

            // DownloadPDF();
            //ltljs.Text = "<script>" + script + "</script>";
        }
    }
}