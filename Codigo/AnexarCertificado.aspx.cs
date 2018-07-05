using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Estudo.Web.GestaoConhecimento
{
    public partial class AnexarCertificado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void lnkvoltar_Click(object sender, EventArgs e)
        {
            string script = @"
if (window.opener)
{
    window.opener.returnValue = 'CANCELOU';
}
window.returnValue = 'CANCELOU';
window.parent.FecharModalAnexo('CANCELOU');
";
            ClientScript.RegisterStartupScript(this.GetType(), "Cancelar", script, true);
        }
        protected void lnksalvar_Click(object sender, EventArgs e)
        {
            try
            {

                diverro.Visible = false;
                if (UploadCertificacao.HasFile)
                {
                    if (UploadCertificacao.FileName.ToLower().EndsWith(".pdf"))
                    {
                        Session["NomeArquivoDigitalizado"] = UploadCertificacao.FileName;
                        Session["ArquivoDigitalizado"] = UploadCertificacao.FileBytes;
                        Session["AlterouArquivoDigitalizado"] = Convert.ToBoolean(true).ToString();
                        string script = @"
if (window.opener)
{
    window.opener.returnValue = 'ALTEROU';
}

 window.returnValue = 'ALTEROU';
 window.parent.FecharModalAnexo('ALTEROU');

 ";
                        ClientScript.RegisterStartupScript(this.GetType(), "ALTEROU", script, true);
                    }
                    else
                    {
                        diverro.Visible = true;
                        lblmsgerro.Text = "A extensão do anexo deve ser '.pdf'";
                    }

                }
                else
                {
                    diverro.Visible = true;
                    lblmsgerro.Text = "Selecione o arquivo do certificado para anexar";
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Erro ao incluir anexo: " + ex.Message;
            }
        }

    }
}
