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
    public partial class DetalheHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["Acao"] == null || Request["Acao"].ToString() == "")
                {
                    lblmsgerro.Text = "Indicador não informado";
                    diverro.Visible = true;
                }
                else
                {
                    grdpesquisa.PageIndex = 0;
                    CarregarGrid();
                }

            }
        }

        public void CarregarGrid()
        {
            int idusuario = 0;
            if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.HomeVisualizarIndicadoresGeral, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
            {
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.HomeVisualizarIndicadoresUsuario, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    idusuario = ((UsuarioEntity)Session["eoUs"]).idusuario;
                }

             }

            HomeBusinessLayer hb = new HomeBusinessLayer();
            DataTable dt = null;
            if (Request["Acao"].ToString() == "CertificacaoRegistrada")
            {
                dt = hb.CertificacoesRegistradasDetalhe(idusuario);
                lbltitulo.Text = "Certificações Registradas";
            }

            if (Request["Acao"].ToString() == "CertificacaoConcluida")
            {
                dt = hb.CertificacoesConcluidasDetalhe(idusuario);
                lbltitulo.Text = "Certificações Concluídas";
            }

            if (Request["Acao"].ToString() == "CertificacaoEmAndamento")
            {
                dt = hb.CertificacoesEmAndamentoDetalhe(idusuario);
                lbltitulo.Text = "Certificações em Andamento";
            }

            if (Request["Acao"].ToString() == "CertificacaoExpirada")
            {
                dt = hb.CertificacoesExpiradasDetalhe();
                lbltitulo.Text = "Certificações Expiradas";
            }

            if (Request["Acao"].ToString() == "ColaboradorCertificado")
            {
                dt = hb.ColCadastroECertificadosDetalhe();
                lbltitulo.Text = "Profissionais Cadastrados e Certificados";
            }

            if (Request["Acao"].ToString() == "DataUltimaCertificacao")
            {
                dt = hb.UltimoCertificadoCadastradoDetalhe(idusuario);
                lbltitulo.Text = "Data Ultima Certificação";
            }


            if (Request["Acao"].ToString() == "SemCertificado")
            {
                dt = hb.SemCertificadoDetalhe();
                lbltitulo.Text = "Certificações cadastradas sem usuários certificados";
            }

            if (Request["Acao"].ToString() == "CertificacoesPendentesEnvio")
            {
                dt = hb.CertificacoesPendentesEnvioAprovacao(idusuario);
                lbltitulo.Text = "Certificações Pendentes de Envio para Aprovação";
            }

            if (Request["Acao"].ToString() == "CertificacoesPendentesAprovacao")
            {
                dt = hb.CertificacoesPendentesAprovacao(idusuario);
                lbltitulo.Text = "Certificações Pendentes de Aprovação";
            }

            if (Request["Acao"].ToString() == "CertificacoesReprovadas")
            {
                dt = hb.CertificacoesReprovadas(idusuario);
                lbltitulo.Text = "Certificações Reprovadas";
            }
            
            if (dt != null && dt.Rows.Count > 0)
            {
                grdpesquisa.DataSource = dt;
                grdpesquisa.DataBind();
            }
            else
            {
                lblmsInfo.Text = "Registros não encontrados para a pesquisa";
                divInfo.Visible = true;
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
    }
}