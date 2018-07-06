using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticOpeningCallsPainel.Data
{
    public class DataLog
    {
        public void inserirLogErro(int id, string Registro, string mensagemErro, string xmlEnvio, string xmlRetorno, string sqlQuery)
        {
            DataAcess dataAcces = new DataAcess();
            string sql = @"INSERT INTO TB_LOG_ERRO_PAINEL 
                            (ID_REGISTRO,REGISTRO,MENSAGEM_ERRO,XML_ENVIO,XML_RETORNO,SQL_QUERY) VALUES (@ID_REGISTRO,@REGISTRO,@MENSAGEM_ERRO,@XML_ENVIO,@XML_RETORNO,@SQL_QUERY)";

            if (!string.IsNullOrEmpty(Registro)) dataAcces.AddInParameter("@REGISTRO", Registro); else dataAcces.AddInParameter("@REGISTRO", DBNull.Value);
            if (!string.IsNullOrEmpty(mensagemErro)) dataAcces.AddInParameter("@MENSAGEM_ERRO", mensagemErro); else dataAcces.AddInParameter("@MENSAGEM_ERRO", DBNull.Value);
            if (!string.IsNullOrEmpty(xmlEnvio)) dataAcces.AddInParameter("@XML_ENVIO", xmlEnvio); else dataAcces.AddInParameter("@XML_ENVIO", DBNull.Value);
            if (!string.IsNullOrEmpty(xmlRetorno)) dataAcces.AddInParameter("@XML_RETORNO", xmlRetorno); else dataAcces.AddInParameter("@XML_RETORNO", DBNull.Value);
            if (!string.IsNullOrEmpty(sqlQuery)) dataAcces.AddInParameter("@SQL_QUERY", sqlQuery); else dataAcces.AddInParameter("@SQL_QUERY", DBNull.Value);
            if (id != int.MinValue) dataAcces.AddInParameter("@ID_REGISTRO", id); else dataAcces.AddInParameter("@ID_REGISTRO", DBNull.Value);

            dataAcces.Execute(sql);
        }
    }
}
