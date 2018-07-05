$(function () {
    Highcharts.setOptions({
        chart: {
            style: {
                fontFamily: 'Estudotxtregular'
            }
        }
    });

    // GRÁFICOS MATRIZ DE CONHECIMENTO >>
    $('#certificadosgrupos').highcharts({
        credits: {
        enabled: false
        },

        chart: {
            backgroundColor: 'none',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            height: 270            
        },

        legend: {
            enabled:  true,
            itemStyle: {
                fontSize: "9px"
            },
            align: 'center',
            verticalAlign: 'bottom',
            layout: 'horizontal',
            x: 0,
            y: 0
        },

        title: {
            text: '',
            style: {
                fontSize: "12px"
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
        series: [{
            name: 'Qtd Certificado:',
            colorByPoint: true,
            data: [{
                name: 'Grupo 1',
                y: 55
            }, {
                name: 'Grupo 2',
                y: 35,
                sliced: false,
                selected: false
            }, {
                name: 'Grupo 3',
                y: 35,
                sliced: false,
                selected: false
            }, {
                name: 'Grupo 4',
                y: 5,
                sliced: false,
                selected: false
            }, {
                name: 'Grupo 5',
                y: 3,
                sliced: false,
                selected: false
            }]
        }]
    });

    $('#certificadosdpto').highcharts({
        credits: {
        enabled: false
        },

        chart: {
            backgroundColor: 'none',
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie',
            height: 270 

        },

        legend: {
            enabled:  true,
            itemStyle: {
                fontSize: "9px"
            },
            align: 'center',
            verticalAlign: 'bottom',
            layout: 'horizontal',
            x: 0,
            y: 0
        },

        title: {
            text: '',
            style: {
                fontSize: "12px"
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
        series: [{
            name: 'Qtd Certificado:',
            colorByPoint: true,
            data: [{
                name: 'Dpto 1',
                y: 12
            }, {
                name: 'Dpto 2',
                y: 5,
                sliced: false,
                selected: false
            }, {
                name: 'Dpto 3',
                y: 82,
                sliced: false,
                selected: false
            }, {
                name: 'Dpto 4',
                y: 36,
                sliced: false,
                selected: false
            }, {
                name: 'Dpto 5',
                y: 49,
                sliced: false,
                selected: false
            }]
        }]
    });

    $('#certificacaomensal').highcharts({
        credits: {
            enabled: false
        },
        
        chart: {
            backgroundColor: 'none',            
            type: 'spline'
        },

       title: {
            text: 'QUANTIDADE DE CERTIFICADOS EMITIDOS NO MÊS DE AGOSTO',
            style: {
                fontSize: "12px"
             }
        },

        subtitle: {
            text: ''
        },
        xAxis: {
            categories: ['1', '2', '3', '4', '5', '6', '7', '8'],
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
            data: [2, 3, 6, 3, 8, 15, 20, 7],
            marker: {
                lineWidth: 2,
                lineColor: Highcharts.getOptions().colors[3],
                fillColor: 'white'
            }
        }]
    });
    // FIM MATRIZ DE CONHECIMENTO >>    
});