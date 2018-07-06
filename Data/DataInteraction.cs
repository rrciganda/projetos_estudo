using AutomaticOpeningCallsPainel.Business;
using AutomaticOpeningCallsPainel.Model;
using AutomaticOpeningCallsPainel.Util;
using Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticOpeningCallsPainel.Data
{
    public class DataInteraction : LogBusiness
    {

        public void inserirInteraction(RegistroModel call)
        {

            DataAcess dataAcces = new DataAcess();
            String sql = @"INSERT INTO TB_TRBTKT_FROM_PAINEL
                           ( CONDICAO_SERVICO
                            ,CONDICAO_CATEGORIA
                            ,CONDICAO_ITEM
                            ,CONDICAO_SUB_ITEM
                            ,CONDICAO_DESCRICAO
                            ,CONDICAO_TITULO
                            ,CONDICAO_NOME_CLIENTE
                            ,RESULTADO_SINTOMA
                            ,RESULTADO_URGENCIA
                            ,RESULTADO_IMPACTO
                            ,TIMEINSERT
                            ,STATUS_REGISTRO
                            ,CONTATO_CLIENTE
                            ,DS_CALL
                            ,COMPANY_NAME
                            ,CNPJ
                            ,CALL_OPENTIME
                            ,PAINEL_ID
                            ,ID_CONTATO
                            ,CONDICAO_TIPO_REQUISICAO
                            ,RESULTADO_GRUPO
                            ,REQUISITANTE_NOME
                            ,REQUISITANTE_ID
                            ,CONTATO_TELEFONE)
                           VALUES
                           ( @CONDICAO_SERVICO
                            ,@CONDICAO_CATEGORIA
                            ,@CONDICAO_ITEM
                            ,@CONDICAO_SUB_ITEM
                            ,@CONDICAO_DESCRICAO
                            ,@CONDICAO_TITULO
                            ,@CONDICAO_NOME_CLIENTE
                            ,@RESULTADO_SINTOMA
                            ,@RESULTADO_URGENCIA
                            ,@RESULTADO_IMPACTO
                            ,@TIMEINSERT
                            ,@STATUS_REGISTRO
                            ,@CONTATO_CLIENTE
                            ,@DS_CALL
                            ,@COMPANY_NAME
                            ,@CNPJ
                            ,@CALL_OPENTIME
                            ,@PAINEL_ID
                            ,@ID_CONTATO
                            ,@CONDICAO_TIPO_REQUISICAO
                            ,@RESULTADO_GRUPO
                            ,@REQUISITANTE_NOME
                            ,@REQUISITANTE_ID
                            ,@CONTATO_TELEFONE)";
            try
            {

                if (!string.IsNullOrEmpty(call.CONDICAO_SERVICO)) dataAcces.AddInParameter("@CONDICAO_SERVICO", call.CONDICAO_SERVICO); else dataAcces.AddInParameter("@CONDICAO_SERVICO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_CATEGORIA)) dataAcces.AddInParameter("@CONDICAO_CATEGORIA", call.CONDICAO_CATEGORIA); else dataAcces.AddInParameter("@CONDICAO_CATEGORIA", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_ITEM)) dataAcces.AddInParameter("@CONDICAO_ITEM", call.CONDICAO_ITEM); else dataAcces.AddInParameter("@CONDICAO_ITEM", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_SUB_ITEM)) dataAcces.AddInParameter("@CONDICAO_SUB_ITEM", call.CONDICAO_SUB_ITEM); else dataAcces.AddInParameter("@CONDICAO_SUB_ITEM", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_DESCRICAO)) dataAcces.AddInParameter("@CONDICAO_DESCRICAO", call.CONDICAO_DESCRICAO); else dataAcces.AddInParameter("@CONDICAO_DESCRICAO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_TITULO)) dataAcces.AddInParameter("@CONDICAO_TITULO", call.CONDICAO_TITULO); else dataAcces.AddInParameter("@CONDICAO_TITULO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_NOME_CLIENTE)) dataAcces.AddInParameter("@CONDICAO_NOME_CLIENTE", call.CONDICAO_NOME_CLIENTE); else dataAcces.AddInParameter("@CONDICAO_NOME_CLIENTE", DBNull.Value);
                if (!string.IsNullOrEmpty(call.RESULTADO_SINTOMA)) dataAcces.AddInParameter("@RESULTADO_SINTOMA", call.RESULTADO_SINTOMA); else dataAcces.AddInParameter("@RESULTADO_SINTOMA", DBNull.Value);
                if (!string.IsNullOrEmpty(call.RESULTADO_URGENCIA)) dataAcces.AddInParameter("@RESULTADO_URGENCIA", call.RESULTADO_URGENCIA); else dataAcces.AddInParameter("@RESULTADO_URGENCIA", DBNull.Value);
                if (!string.IsNullOrEmpty(call.RESULTADO_IMPACTO)) dataAcces.AddInParameter("@RESULTADO_IMPACTO", call.RESULTADO_IMPACTO); else dataAcces.AddInParameter("@RESULTADO_IMPACTO", DBNull.Value);
                if (call.CALL_OPENTIME != DateTime.MinValue) dataAcces.AddInParameter("@CALL_OPENTIME", call.CALL_OPENTIME); else dataAcces.AddInParameter("@CALL_OPENTIME", DBNull.Value);
                if (!string.IsNullOrEmpty(call.ID_CONTATO)) dataAcces.AddInParameter("@ID_CONTATO", call.ID_CONTATO); else dataAcces.AddInParameter("@ID_CONTATO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.DS_CALL)) dataAcces.AddInParameter("@DS_CALL", call.DS_CALL); else dataAcces.AddInParameter("@DS_CALL", DBNull.Value);
                if (!string.IsNullOrEmpty(call.COMPANY_NAME)) dataAcces.AddInParameter("@COMPANY_NAME", call.COMPANY_NAME); else dataAcces.AddInParameter("@COMPANY_NAME", DBNull.Value);
                if (!string.IsNullOrEmpty(call.PAINEL_ID)) dataAcces.AddInParameter("@PAINEL_ID", call.PAINEL_ID); else dataAcces.AddInParameter("@PAINEL_ID", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONTATO_CLIENTE)) dataAcces.AddInParameter("@CONTATO_CLIENTE", call.CONTATO_CLIENTE); else dataAcces.AddInParameter("@CONTATO_CLIENTE", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CNPJ)) dataAcces.AddInParameter("@CNPJ", call.CNPJ); else dataAcces.AddInParameter("@CNPJ", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONDICAO_TIPO_REQUISICAO)) dataAcces.AddInParameter("@CONDICAO_TIPO_REQUISICAO", call.CONDICAO_TIPO_REQUISICAO); else dataAcces.AddInParameter("@CONDICAO_TIPO_REQUISICAO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.RESULTADO_GRUPO)) dataAcces.AddInParameter("@RESULTADO_GRUPO", call.RESULTADO_GRUPO); else dataAcces.AddInParameter("@RESULTADO_GRUPO", DBNull.Value);
                if (!string.IsNullOrEmpty(call.REQUISITANTE_NOME)) dataAcces.AddInParameter("@REQUISITANTE_NOME", call.REQUISITANTE_NOME); else dataAcces.AddInParameter("@REQUISITANTE_NOME", DBNull.Value);
                if (!string.IsNullOrEmpty(call.REQUISITANTE_ID)) dataAcces.AddInParameter("@REQUISITANTE_ID", call.REQUISITANTE_ID); else dataAcces.AddInParameter("@REQUISITANTE_ID", DBNull.Value);
                if (!string.IsNullOrEmpty(call.CONTATO_TELEFONE)) dataAcces.AddInParameter("@CONTATO_TELEFONE", call.CONTATO_TELEFONE); else dataAcces.AddInParameter("@CONTATO_TELEFONE", DBNull.Value);
                dataAcces.AddInParameter("@STATUS_REGISTRO", UtilConst.StatusRegistro.CriadoInteraction);
                dataAcces.AddInParameter("@TIMEINSERT", DateTime.Now);

                dataAcces.Execute(sql);
            }
            catch (Exception ex)
            {
                logErro(call.ID_TRBTKT_FROM_PAINEL, call.DS_CALL, "Banco de dados, Messagem: " + ex.Message, "", "", sql);

            }
        }


        public bool verificarInteraction(RegistroModel call)
        {
            DataAcess dataAcces = new DataAcess();
            string sql = "SELECT * FROM TB_TRBTKT_FROM_PAINEL WHERE DS_CALL = @DS_CALL";

            dataAcces.AddInParameter("@DS_CALL", call.DS_CALL);

            using (SqlDataReader reader = dataAcces.GetDataReader(sql))
            {
                while (reader.Read())
                {
                    return false;
                }
                reader.Close();
                try
                {
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    logErro(call.ID_TRBTKT_FROM_PAINEL, call.DS_CALL, "Banco de dados, Messagem: " + ex.Message, "", "", sql);
                }

                return true;
            }
        }

        public List<RegistroModel> BuscarInteraction()
        {
            List<RegistroModel> calls = new List<RegistroModel>();
            RegistroModel model = new RegistroModel();
            DataAcess dataAcces = new DataAcess();
            string sql = "SELECT * FROM TB_TRBTKT_FROM_PAINEL WHERE STATUS_REGISTRO = @STATUS_REGISTRO";

            dataAcces.AddInParameter("@STATUS_REGISTRO", UtilConst.StatusRegistro.CriadoInteraction);

            using (SqlDataReader reader = dataAcces.GetDataReader(sql))
            {
                while (reader.Read())
                {
                    model = new RegistroModel();
                    model.ID_TRBTKT_FROM_PAINEL = Convert.ToInt32(reader["ID_TRBTKT_FROM_PAINEL"]);
                    model.TIMEINSERT = Convert.ToDateTime(reader["TIMEINSERT"].ToString());
                    model.DS_CALL = reader["DS_CALL"].ToString();
                    model.STATUS_REGISTRO = reader["STATUS_REGISTRO"].ToString();
                    model.CONDICAO_CATEGORIA = reader["CONDICAO_CATEGORIA"].ToString();
                    model.CONDICAO_SERVICO = reader["CONDICAO_SERVICO"].ToString();
                    model.CONDICAO_ITEM = reader["CONDICAO_ITEM"].ToString();
                    model.CONDICAO_SUB_ITEM = reader["CONDICAO_SUB_ITEM"].ToString();
                    model.CONDICAO_TITULO = reader["CONDICAO_TITULO"].ToString();
                    model.CONDICAO_NOME_CLIENTE = reader["CONDICAO_NOME_CLIENTE"].ToString();
                    model.CONDICAO_DESCRICAO = reader["CONDICAO_DESCRICAO"].ToString();
                    model.CONDICAO_TIPO_REQUISICAO = reader["CONDICAO_TIPO_REQUISICAO"].ToString();
                    model.RESULTADO_IMPACTO = reader["RESULTADO_IMPACTO"].ToString();
                    model.RESULTADO_URGENCIA = reader["RESULTADO_URGENCIA"].ToString();
                    model.CALL_OPENTIME = Convert.ToDateTime(reader["CALL_OPENTIME"].ToString());
                    model.COMPANY_NAME = reader["COMPANY_NAME"].ToString();
                    model.ID_CONTATO = reader["ID_CONTATO"].ToString();
                    model.CONTATO_CLIENTE = reader["CONTATO_CLIENTE"].ToString();
                    model.CNPJ = reader["CNPJ"].ToString();
                    model.PAINEL_ID = reader["PAINEL_ID"].ToString();
                    model.RESULTADO_GRUPO = reader["RESULTADO_GRUPO"].ToString();
                    model.REQUISITANTE_NOME = reader["REQUISITANTE_NOME"].ToString();
                    model.REQUISITANTE_ID = reader["REQUISITANTE_ID"].ToString();
                    model.CONTATO_TELEFONE = reader["CONTATO_TELEFONE"].ToString();
                    calls.Add(model);
                }
                reader.Close();
                try
                {
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    logErro(int.MinValue, "", "Banco de dados, Messagem: " + ex.Message, "", "", sql);
                }

                return calls;
            }
        }
    }
}
