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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.HomeVisualizarIndicadoresGeral, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
            {
                HomeBusinessLayer hb = new HomeBusinessLayer();
                lbCertificacaoregistrada.Text = hb.CertificacoesRegistradas(0).ToString();
                lbCertificacoesConcluidas.Text = hb.CertificacoesConcluidas(0).ToString();
                lbCertificacoesEmAndamento.Text = hb.CertificacoesEmAndamento(0).ToString();
                lblCertificacoesExpiradas.Text = hb.CertificacoesExpiradas(0).ToString();
                lbColCadastrosECertificados.Text = hb.ColCadastroECertificados().ToString();
                lbUltCertificacaoCadastrada.Text = hb.UltimoCertificadoCadastrado(0).ToString();

                if (lbUltCertificacaoCadastrada.Text == string.Empty)
                {
                    lbUltCertificacaoCadastrada.Text = "N/A";
                    lbUltCertificacaoCadastrada.Font.Bold = true;
                }

                lblSemCertificado.Text = hb.SemCertificado().ToString();

                //Nova Funcionalidade Fabiano
                lblCertificacoesPendentesEnvio.Text = this.RetornaCertificacoesPendentesEnvioAprovacao(0).ToString();
                lblCertificacoesPendentesAprovacao.Text = this.RetornaCertificacoesPendentesAprovacao(0).ToString();
                lblCertificacoesReprovadas.Text  = this.RetornaCertificacoesReprovadas(0).ToString();

                topcertificacoes.DataSource = hb.TopCertificacoes();
                topcertificacoes.DataBind();

                topporgrupo.DataSource = hb.TopPorGrupo();
                topporgrupo.DataBind();
            }
            else
            {
                if ((ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.HomeVisualizarIndicadoresUsuario, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    HomeBusinessLayer hb = new HomeBusinessLayer();
                    lbCertificacaoregistrada.Text = hb.CertificacoesRegistradas(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();
                    lbCertificacoesConcluidas.Text = hb.CertificacoesConcluidas(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();
                    lbCertificacoesEmAndamento.Text = hb.CertificacoesEmAndamento(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();
                    lblCertificacoesExpiradas.Text = hb.CertificacoesExpiradas(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();

                    divSemCertificado.Visible = false;
                    divColCadastrosECertificados.Visible = false;
                    lbUltCertificacaoCadastrada.Text = hb.UltimoCertificadoCadastrado(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();

                    if (lbUltCertificacaoCadastrada.Text == string.Empty)
                    {
                        lbUltCertificacaoCadastrada.Text = "N/A";
                        lbUltCertificacaoCadastrada.Font.Bold = true;
                    }

                    //Nova Funcionalidade Fabiano
                    lblCertificacoesPendentesEnvio.Text = this.RetornaCertificacoesPendentesEnvioAprovacao(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();
                    lblCertificacoesPendentesAprovacao.Text = this.RetornaCertificacoesPendentesAprovacao(((UsuarioEntity)Session["eoUs"]).idusuario).ToString();
                    lblCertificacoesReprovadas.Text = this.RetornaCertificacoesReprovadas(0).ToString();

                    divTopCertGrupo.Visible = false;
                    divTopCertificacoes.Visible = false;
                }
                else
                {

                    divindicadores.Visible = false;
                    divTopCertGrupo.Visible = false;
                    divTopCertificacoes.Visible = false;
                }
            }
        }

        private int RetornaCertificacoesPendentesEnvioAprovacao(int idusuario)
        {
            HomeBusinessLayer hb = new HomeBusinessLayer();
            DataTable dtRetorno = hb.CertificacoesPendentesEnvioAprovacao(idusuario);

            if(dtRetorno != null)
            {
                if(dtRetorno.Rows != null)
                {
                    if(dtRetorno.Rows.Count > 0)
                    {
                        return dtRetorno.Rows.Count;
                    }
                }
            }
            return 0;
        }

        private int RetornaCertificacoesPendentesAprovacao(int idusuario)
        {
            HomeBusinessLayer hb = new HomeBusinessLayer();
            DataTable dtRetorno = hb.CertificacoesPendentesAprovacao(idusuario);

            if (dtRetorno != null)
            {
                if (dtRetorno.Rows != null)
                {
                    if (dtRetorno.Rows.Count > 0)
                    {
                        return dtRetorno.Rows.Count;
                    }
                }
            }
            return 0;
        }

        private int RetornaCertificacoesReprovadas(int idusuario)
        {
            HomeBusinessLayer hb = new HomeBusinessLayer();
            DataTable dtRetorno = hb.CertificacoesReprovadas(idusuario);
            
            if (dtRetorno != null)
            {
                if (dtRetorno.Rows != null)
                {
                    if (dtRetorno.Rows.Count > 0)
                    {
                        return dtRetorno.Rows.Count;
                    }
                }
            }

            return 0;
        }

    }
}