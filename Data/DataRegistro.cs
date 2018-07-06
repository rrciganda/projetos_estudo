using AutomaticOpeningCallsPainel.Business;
using AutomaticOpeningCallsPainel.Model;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticOpeningCallsPainel.Data
{
    public class DataRegistro : LogBusiness
    {

        public void atualizarRegistro(RegistroModel registro, String descricaoStatus, int status)
        {

            DataAcess dataAcces = new DataAcess();
            String sql = @"UPDATE TB_TRBTKT_FROM_PAINEL 
                            SET ID_CATEGORY_MANUAL = @ID_CATEGORY_MANUAL,
                            RESULTADO_SERVICO = @RESULTADO_SERVICO,
                            RESULTADO_CATEGORIA = @RESULTADO_CATEGORIA,
                            RESULTADO_SUBCATEGORIA = @RESULTADO_SUBCATEGORIA,
                            RESULTADO_TIPOPRODUTO = @RESULTADO_TIPOPRODUTO,
                            RESULTADO_SINTOMA = @RESULTADO_SINTOMA,
                            RESULTADO_GRUPO = @RESULTADO_GRUPO,
                            LOG_RESULTADO = @LOG_RESULTADO,
                            RESULTADO_IMPACTO = @RESULTADO_IMPACTO,
                            RESULTADO_URGENCIA = @RESULTADO_URGENCIA,
                            STATUS_REGISTRO = @STATUS_REGISTRO
                          WHERE ID_TRBTKT_FROM_PAINEL = @ID_TRBTKT_FROM_PAINEL";
            try
            {
                if (registro.ID_CATEGORY_MANUAL != int.MinValue) dataAcces.AddInParameter("@ID_CATEGORY_MANUAL", registro.ID_CATEGORY_MANUAL); else dataAcces.AddInParameter("@ID_CATEGORY_MANUAL", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_SERVICO)) dataAcces.AddInParameter("@RESULTADO_SERVICO", registro.RESULTADO_SERVICO); else dataAcces.AddInParameter("@RESULTADO_SERVICO", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_CATEGORIA)) dataAcces.AddInParameter("@RESULTADO_CATEGORIA", registro.RESULTADO_CATEGORIA); else dataAcces.AddInParameter("@RESULTADO_CATEGORIA", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_SUBCATEGORIA)) dataAcces.AddInParameter("@RESULTADO_SUBCATEGORIA", registro.RESULTADO_SUBCATEGORIA); else dataAcces.AddInParameter("@RESULTADO_SUBCATEGORIA", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_TIPOPRODUTO)) dataAcces.AddInParameter("@RESULTADO_TIPOPRODUTO", registro.RESULTADO_TIPOPRODUTO); else dataAcces.AddInParameter("@RESULTADO_TIPOPRODUTO", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_SINTOMA)) dataAcces.AddInParameter("@RESULTADO_SINTOMA", registro.RESULTADO_SINTOMA); else dataAcces.AddInParameter("@RESULTADO_SINTOMA", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_GRUPO)) dataAcces.AddInParameter("@RESULTADO_GRUPO", registro.RESULTADO_GRUPO); else dataAcces.AddInParameter("@RESULTADO_GRUPO", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_IMPACTO)) dataAcces.AddInParameter("@RESULTADO_IMPACTO", registro.RESULTADO_IMPACTO); else dataAcces.AddInParameter("@RESULTADO_IMPACTO", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.RESULTADO_URGENCIA)) dataAcces.AddInParameter("@RESULTADO_URGENCIA", registro.RESULTADO_URGENCIA); else dataAcces.AddInParameter("@RESULTADO_URGENCIA", DBNull.Value);
                if (!string.IsNullOrEmpty(descricaoStatus)) dataAcces.AddInParameter("@LOG_RESULTADO", descricaoStatus); else dataAcces.AddInParameter("@LOG_RESULTADO", DBNull.Value);

                dataAcces.AddInParameter("@STATUS_REGISTRO", status);
                dataAcces.AddInParameter("@ID_TRBTKT_FROM_PAINEL", registro.ID_TRBTKT_FROM_PAINEL);

                dataAcces.Execute(sql);

            }
            catch (Exception ex)
            {
                logErro(registro.ID_TRBTKT_FROM_PAINEL, registro.DS_CALL, "Banco de dados, Messagem: " + ex.Message, "", "", sql);
            }
        }

        public void atualizarInfoRegistro(RegistroModel registro, String descricaoStatus, int status)
        {
            DataAcess dataAcces = new DataAcess();
            String sql = @"UPDATE TB_TRBTKT_FROM_PAINEL 
                            SET DS_REGISTRO = @DS_REGISTRO,
                            TIME_INSERT_REGISTRO = @TIME_INSERT_REGISTRO,
                            LOG_RESULTADO = @LOG_RESULTADO,
                            STATUS_REGISTRO = @STATUS_REGISTRO
                          WHERE ID_TRBTKT_FROM_PAINEL = @ID_TRBTKT_FROM_PAINEL";
            try
            {
                if (registro.TIME_INSERT_REGISTRO != DateTime.MinValue) dataAcces.AddInParameter("@TIME_INSERT_REGISTRO", registro.TIME_INSERT_REGISTRO); else dataAcces.AddInParameter("@TIME_REGISTRO", DBNull.Value);
                if (!string.IsNullOrEmpty(registro.DS_REGISTRO)) dataAcces.AddInParameter("@DS_REGISTRO", registro.DS_REGISTRO); else dataAcces.AddInParameter("@DS_REGISTRO", DBNull.Value);
                dataAcces.AddInParameter("@ID_TRBTKT_FROM_PAINEL", registro.ID_TRBTKT_FROM_PAINEL);
                dataAcces.AddInParameter("@STATUS_REGISTRO", status);
                dataAcces.AddInParameter("@LOG_RESULTADO", descricaoStatus);


                dataAcces.Execute(sql);

            }
            catch (Exception ex)
            {
                logErro(registro.ID_TRBTKT_FROM_PAINEL, registro.DS_CALL, "Banco de dados, Messagem: " + ex.Message, "", "", sql);

            }
        }

        public void atualizarStatusRegistro(int id, String descricaoStatus, int status)
        {

            DataAcess dataAcces = new DataAcess();
            String sql = @"UPDATE TB_TRBTKT_FROM_PAINEL 
                            SET STATUS_REGISTRO = @STATUS_REGISTRO,
                                LOG_RESULTADO = @LOG_RESULTADO
                          WHERE ID_TRBTKT_FROM_PAINEL = @ID_TRBTKT_FROM_PAINEL";
            try
            {
                dataAcces.AddInParameter("@ID_TRBTKT_FROM_PAINEL", id);
                dataAcces.AddInParameter("@STATUS_REGISTRO", status);
                dataAcces.AddInParameter("@LOG_RESULTADO", descricaoStatus);

                dataAcces.Execute(sql);

            }
            catch (Exception ex)
            {
                logErro(id, "", "Banco de dados, Messagem: " + ex.Message, "", "", sql);

            }
        }

        public DataTable buscarCategoriaManual(RegistroModel eo)
        {
            DataTable categorizacaoManual = new DataTable();
            DataAcess dataAcces = new DataAcess();
            String sql = "";

            try
            {

                if (!string.IsNullOrEmpty(eo.CONDICAO_NOME_CLIENTE)) dataAcces.AddInParameter("@DS_CONDICAO_COMPANY", eo.CONDICAO_NOME_CLIENTE); else dataAcces.AddInParameter("@DS_CONDICAO_COMPANY", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_SERVICO)) dataAcces.AddInParameter("@DS_CONDICAO_SERVICO", eo.CONDICAO_SERVICO); else dataAcces.AddInParameter("@DS_CONDICAO_SERVICO", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_ITEM)) dataAcces.AddInParameter("@DS_CONDICAO_ITEM", eo.CONDICAO_ITEM); else dataAcces.AddInParameter("@DS_CONDICAO_ITEM", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_SUB_ITEM)) dataAcces.AddInParameter("@DS_CONDICAO_SUB_ITEM", eo.CONDICAO_SUB_ITEM); else dataAcces.AddInParameter("@DS_CONDICAO_SUB_ITEM", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_TITULO)) dataAcces.AddInParameter("@DS_CONDICAO_TITULO", eo.CONDICAO_TITULO); else dataAcces.AddInParameter("@DS_CONDICAO_TITULO", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_DESCRICAO)) dataAcces.AddInParameter("@DS_CONDICAO_DESCRICAO", eo.CONDICAO_DESCRICAO); else dataAcces.AddInParameter("@DS_CONDICAO_DESCRICAO", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_TIPO_REQUISICAO)) dataAcces.AddInParameter("@DS_CONDICAO_TIPO_REQUISICAO", eo.CONDICAO_TIPO_REQUISICAO); else dataAcces.AddInParameter("@DS_CONDICAO_TIPO_REQUISICAO", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_IMPACTO_FINANCEIRO)) dataAcces.AddInParameter("@DS_CONDICAO_IMPACTO_FINANCEIRO", eo.CONDICAO_IMPACTO_FINANCEIRO); else dataAcces.AddInParameter("@DS_CONDICAO_IMPACTO_FINANCEIRO", DBNull.Value);
                if (!string.IsNullOrEmpty(eo.CONDICAO_IMPACTO_NEGOCIO)) dataAcces.AddInParameter("@DS_CONDICAO_IMPACTO_NEGOCIO", eo.CONDICAO_IMPACTO_NEGOCIO); else dataAcces.AddInParameter("@DS_CONDICAO_IMPACTO_NEGOCIO", DBNull.Value);

                sql = @"Select CM.* from TB_CATEGORIZACAO_MANUAL_PAINEL CM WHERE CM.IN_STATUS = 1 AND
             (
                --1       
                  ((CM.DS_TIPO_COMPARACAO_TITULO = 'Igual' and CM.DS_CONDICAO_TITULO = @DS_CONDICAO_TITULO) or
                   (CM.DS_TIPO_COMPARACAO_TITULO = 'Contém' and CM.DS_CONDICAO_TITULO like '%' + replace(@DS_CONDICAO_TITULO,'[','[[]') +'%'))
                   and CM.DS_CONDICAO_COMPANY = @DS_CONDICAO_COMPANY 
                   and CM.DS_CONDICAO_SERVICO = @DS_CONDICAO_SERVICO
                   and CM.DS_CONDICAO_ITEM = @DS_CONDICAO_ITEM
                   and CM.DS_CONDICAO_SUB_ITEM = @DS_CONDICAO_SUB_ITEM
                   and CM.DS_CONDICAO_TIPO_REQUISICAO = @DS_CONDICAO_TIPO_REQUISICAO
                   and CM.DS_CONDICAO_IMPACTO_FINANCEIRO = @DS_CONDICAO_IMPACTO_FINANCEIRO
                   and CM.DS_CONDICAO_IMPACTO_NEGOCIO = @DS_CONDICAO_IMPACTO_NEGOCIO
                   and ((CM.DS_TIPO_COMPARACAO_DESCRICAO = 'Igual' and CM.DS_CONDICAO_DESCRICAO = @DS_CONDICAO_DESCRICAO) or
                   (CM.DS_TIPO_COMPARACAO_DESCRICAO = 'Contém' and DS_CONDICAO_DESCRICAO like '%' + replace(@DS_CONDICAO_DESCRICAO,'[','[[]') +'%'))
             )";

                return dataAcces.GetDataTable(sql);

            }
            catch (Exception ex)
            {
                logErro(eo.ID_TRBTKT_FROM_PAINEL, eo.DS_CALL, "Banco de dados, Messagem: " + ex.Message, "", "", sql);
                return null;
            }

        }
    }
}
