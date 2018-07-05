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
    public partial class RelatoriosGráficos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!(ValidarAcesso.TemAcesso(EOConst.CodFuncionalidade.RelatriosDashboard, (List<PerfilfuncionalidadeEntity>)Session["eoFuncs"])))
                {
                    Response.Redirect("SemAcesso.aspx");
                }

                CarregarGridGrupo(new RelatorioGraficoEntity());
                CarregarGridDepartamento(new RelatorioGraficoEntity());
                CarregaGraficoGrupo(new RelatorioGraficoEntity());

                RelatorioGraficoEntity objGraficoDepartamento = new RelatorioGraficoEntity();
                objGraficoDepartamento.dtfiltrode = DateTime.Parse(DateTime.Now.AddDays(-180).ToString("dd/MM/yyyy"));
                objGraficoDepartamento.dtfiltroate = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"));
                CarregaGraficoDepartamento(objGraficoDepartamento);

                CarregarGrupo();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "data", "calendario();", true);
        }

        private void CarregaGraficoMensal()
        {
            string dia = "";
            string quantidade = "";
            string col = "";
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();
                RelatorioGraficoEntity eo = new RelatorioGraficoEntity();

                if (ddlGrupo.SelectedValue != "")
                    eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
                if (!String.IsNullOrEmpty(txtDataInicio.Text))
                    eo.dtfiltrode = Convert.ToDateTime(txtDataInicio.Text);
                if (!String.IsNullOrEmpty(txtDataFinal.Text))
                    eo.dtfiltroate = Convert.ToDateTime(txtDataFinal.Text);

                DataTable dt = bl.ConsultarCertificacoesPorDia(eo);
                DataView view = new DataView(dt);
                DataTable dtcertificacao = view.ToTable(true, "dtcertificacao");

                foreach (DataRow item in dtcertificacao.Rows)
                {
                    if (dia != "")
                        dia = dia + ",";
                    dia = dia + "'" + item["dtcertificacao"].ToString() + "'";

                    DataRow[] drs = dt.Select("dtcertificacao = '" + item["dtcertificacao"].ToString() + "'");

                    if (drs != null && drs.Length > 0)
                    {
                        if (quantidade != "")
                            quantidade = quantidade + ",";
                        quantidade = quantidade + drs[0]["qtdecertificacoes"].ToString();
                    }
                    else
                    {
                        if (quantidade != "")
                            quantidade = quantidade + ",";
                        quantidade = quantidade + "0";
                    }
                    drs = null;
                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

            string js = @"
               $('#pd-graficomensal').highcharts({
   credits: {
            enabled: false
        },
        
        chart: {
            backgroundColor: 'none',            
            type: 'spline'
        },

        exporting: { enabled: false },

       title: {
            text: 'QUANTIDADE DE CERTIFICADOS EMITIDOS NO PERÍODO SOLICITADO',
            style: {
                fontSize: '12px'
             }
    },

        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [" + dia + @"],
            title: {
                text: 'Dia'
            }
        },

        yAxis: {
            title: {
                text: 'Quantidade'
            }
        },

        plotOptions: {
            valueDecimals: 3,
            spline: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            },
                
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    // format: 'R${point.y:f}'
                }
            }
        },
         series: [{
            name: 'Certificados',
            data: [" + quantidade + @"],
            marker: {
                lineWidth: 2,
                lineColor: Highcharts.getOptions().colors[3],
                fillColor: 'white'
            }
        }]

    });

            ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "pd-graficomensal", js, true);

        }
        private void CarregaGraficoDepartamento(RelatorioGraficoEntity eo)
        {
            string departamento = "";
            string certificacoes = "";
            string col = "";
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();

                DataTable dt = bl.ConsultarPorDepartamento(eo);
                DataView view = new DataView(dt);
                DataTable dtdepartamento = view.ToTable(true, "departamento");

                try
                {
                    foreach (DataRow item in dtdepartamento.Rows)
                    {
                        if (departamento != "")
                            departamento = departamento + ",";
                        departamento = departamento + "'" + item["departamento"].ToString() + "'";

                        DataRow[] drs = dt.Select("departamento = '" + item["departamento"].ToString() + "'");

                        if (drs != null && drs.Length > 0)
                        {
                            if (certificacoes != "")
                                certificacoes = certificacoes + ",";
                            certificacoes = certificacoes + drs[0]["certificacoes"].ToString();
                        }
                        else
                        {
                            if (certificacoes != "")
                                certificacoes = certificacoes + ",";
                            certificacoes = certificacoes + "0";
                        }
                        drs = null;
                    }
                    string[] wordsDepartamentos = departamento.Split(',');
                    string[] WordsCertificaoes = certificacoes.Split(',');
                    string partesGrafico = "";
                    for (int i = 0; i < wordsDepartamentos.Length; i++)
                    {

                        if (i == 0)
                        {
                            partesGrafico += "{";
                            partesGrafico += "name: " + wordsDepartamentos[i] + ",";
                            partesGrafico += "\n";
                            partesGrafico += "y: " + WordsCertificaoes[i];
                            partesGrafico += "},";
                        }
                        else
                        {
                            partesGrafico += "{";
                            partesGrafico += "name: " + wordsDepartamentos[i] + ",";
                            partesGrafico += "\n";
                            partesGrafico += "y: " + WordsCertificaoes[i] + ",";
                            partesGrafico += "\n";
                            partesGrafico += "sliced: false,";
                            partesGrafico += "\n";
                            partesGrafico += "selected: false";
                            partesGrafico += "},";
                        }
                    }
                    if (partesGrafico != "")
                    {
                        if (partesGrafico.Substring(partesGrafico.Length - 1, 1) == ",")
                            partesGrafico = partesGrafico.Substring(0, partesGrafico.Length - 1);
                    }
                    col = col + @"{
                                    name: 'Qtde Certificados:',
                                    colorByPoint: true,
                                    data:[" + partesGrafico + @"]
                                } ";
                }
                catch
                {

                }

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

            string js = @"
               $('#pd-graficodepartamento').highcharts({
         credits: {
        enabled: false
        },

        chart: {
            backgroundColor: 'none',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            height: 350            
        },

        legend: {
            enabled:  true,
            itemStyle: {
                fontSize: '9px'
            },
            align: 'center',
            verticalAlign: 'bottom',
            layout: 'horizontal',
            x: 0,
            y: 0
        },
        exporting: { enabled: false },
        title: {
            text: '',
            style: {
                fontSize: '12px'
             }
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                    size: 200,
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                },
                showInLegend: true
            }
        },  
        
        series: [" + col + @"],

    });

            ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "pd-graficodepartamento", js, true);

        }
        private void CarregaGraficoGrupo(RelatorioGraficoEntity eo)
        {
            string grupo = "";
            string certificacoes = "";
            string col = "";
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();

                DataTable dt = bl.ConsultarPorGrupo(eo);
                DataView view = new DataView(dt);
                DataTable dtgrupo = view.ToTable(true, "grupo");

                foreach (DataRow item in dtgrupo.Rows)
                {
                    if (grupo != "")
                        grupo = grupo + ",";
                    grupo = grupo + "'" + item["grupo"].ToString() + "'";

                    DataRow[] drs = dt.Select("grupo = '" + item["grupo"].ToString() + "'");

                    if (drs != null && drs.Length > 0)
                    {
                        if (certificacoes != "")
                            certificacoes = certificacoes + ",";
                        certificacoes = certificacoes + drs[0]["certificacoes"].ToString();
                    }
                    else
                    {
                        if (certificacoes != "")
                            certificacoes = certificacoes + ",";
                        certificacoes = certificacoes + "0";
                    }
                    drs = null;
                }
                string[] wordsGrupos = grupo.Split(',');
                string[] WordsCertificaoes = certificacoes.Split(',');
                string partesGrafico = "";
                for (int i = 0; i < wordsGrupos.Length; i++)
                {

                    if (i == 0)
                    {
                        partesGrafico += "{";
                        partesGrafico += "name: " + wordsGrupos[i] + ",";
                        partesGrafico += "\n";
                        partesGrafico += "y: " + WordsCertificaoes[i];
                        partesGrafico += "},";
                    }
                    else
                    {
                        partesGrafico += "{";
                        partesGrafico += "name: " + wordsGrupos[i] + ",";
                        partesGrafico += "\n";
                        partesGrafico += "y: " + WordsCertificaoes[i] + ",";
                        partesGrafico += "\n";
                        partesGrafico += "sliced: false,";
                        partesGrafico += "\n";
                        partesGrafico += "selected: false";
                        partesGrafico += "},";
                    }
                }
                if (partesGrafico != "")
                {
                    if (partesGrafico.Substring(partesGrafico.Length - 1, 1) == ",")
                        partesGrafico = partesGrafico.Substring(0, partesGrafico.Length - 1);
                }
                col = col + @"{
                                    name: 'Qtde Certificados:',
                                    colorByPoint: true,
                                    data:[" + partesGrafico + @"]
                                } ";

            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }

            string js = @"
               $('#pd-graficogrupo').highcharts({
         credits: {
        enabled: false
        },

        chart: {
            backgroundColor: 'none',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            height: 350            
        },

        legend: {
            enabled:  true,
            itemStyle: {
                fontSize: '9px'
            },
            align: 'center',
            verticalAlign: 'bottom',
            layout: 'horizontal',
            x: 0,
            y: 0
        },
        exporting: { enabled: false },
        title: {
            text: '',
            style: {
                fontSize: '12px'
             }
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        plotOptions: {
            pie: {
                    size: 200,
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: false
                },
                showInLegend: true
            }
        },  
        
        series: [" + col + @"],

    });

            ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "pd-graficogrupo", js, true);

        }
        private void CarregarGridGrupo(RelatorioGraficoEntity eo)
        {
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();

                DataTable dt = bl.ConsultarPorGrupo(eo);

                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = "grupo";
                    dt = dt.DefaultView.ToTable();
                }

                grdpesquisaGrupo.DataSource = dt;
                grdpesquisaGrupo.DataBind();
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void CarregarGridDepartamento(RelatorioGraficoEntity eo)
        {
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();
                DataTable dt = bl.ConsultarPorDepartamento(eo);

                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.Sort = "departamento";
                    dt = dt.DefaultView.ToTable();
                }

                grdpesquisaDepartamento.DataSource = dt;
                grdpesquisaDepartamento.DataBind();
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }

        private void CarregarGrupo()
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

        protected void lnkPesquisar_Click(object sender, EventArgs e)
        {
            diverro.Visible = false;

            RelatorioGraficoEntity eo = new RelatorioGraficoEntity();

            if (ddlGrupo.SelectedValue != "")
                eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
            if (!String.IsNullOrEmpty(txtDataInicio.Text))
                eo.dtfiltrode = Convert.ToDateTime(txtDataInicio.Text);
            if (!String.IsNullOrEmpty(txtDataFinal.Text))
                eo.dtfiltroate = Convert.ToDateTime(txtDataFinal.Text);

            if (eo.dtfiltrode != DateTime.MinValue && eo.dtfiltroate != DateTime.MinValue && eo.dtfiltrode > eo.dtfiltroate)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: Período Certificação Inválido";
                return;
            }

            CarregarGridGrupo(eo);
            CarregarGridDepartamento(eo);
            CarregaGraficoGrupo(eo);
            CarregaGraficoDepartamento(eo);

            //grdpesquisaCertificacao.PageIndex = 0;
            //CarregarGridCertificacao();
            //CarregaGraficoMensal();
        }

        private void CarregarGridCertificacao()
        {
            try
            {
                RelatorioGraficoBusinessLayer bl = new RelatorioGraficoBusinessLayer();
                RelatorioGraficoEntity eo = new RelatorioGraficoEntity();
                if (ddlGrupo.SelectedValue != "")
                    eo.idgrupo = Convert.ToInt32(ddlGrupo.SelectedValue);
                if (!String.IsNullOrEmpty(txtDataInicio.Text))
                    eo.dtfiltrode = Convert.ToDateTime(txtDataInicio.Text);
                if (!String.IsNullOrEmpty(txtDataFinal.Text))
                    eo.dtfiltroate = Convert.ToDateTime(txtDataFinal.Text);

                //grdpesquisaCertificacao.DataSource = bl.ConsultarPorData(eo);
                //grdpesquisaCertificacao.DataBind();
            }
            catch (Exception ex)
            {
                diverro.Visible = true;
                lblmsgerro.Text = "Mensagem do Sistema: " + ex.Message;
            }
        }
        protected void grdpesquisa_PageIndexChanging(object source, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex > -1)
            {
                //grdpesquisaCertificacao.PageIndex = e.NewPageIndex;
                CarregarGridCertificacao();
            }
        }
    }
}