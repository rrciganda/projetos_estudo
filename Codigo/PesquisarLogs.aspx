<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PesquisarLogs.aspx.cs" Inherits="Estudo.Web.GestaoConhecimento.PesquisarLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-sm-12">
        <h1 class="text-bold">LOGS</h1>
        <span class="text-uppercase">Aqui você consegue visualizar e emitir relatório de logs</span>
        <hr>
    </div>

    <div class="col-sm-12 form-group">
        <label class="control-label text-uppercase">Pesquisar Logs</label>
    </div>

    <div class="col-sm-4 form-group">
        <label for="sel2">Funcionalidades</label>
        <select class="form-control" id="sel2">
            <option>Todos</option>
        </select>
    </div>

    <div class="col-sm-4 form-group">
        <label for="sel2">Tipo de Usuário</label>
        <select class="form-control" id="sel2">
            <option>Admin</option>
            <option>Aprovador</option>
            <option>Colaborador</option>
        </select>
    </div>

    <!--                 <div class="col-sm-4 form-group">
                    <label for="sel2">Colaborador</label>
                    <select class="form-control" id="sel2">
                        <option>Anderson Alencar</option>
                        <option>Rodrigo Rodrigues</option>
                        <option>Ricardo Ciganda</option>
                    </select>
                </div>  -->

    <div class="row">
        <div class="col-sm-12">
            <div class="col-sm-4 form-group">
                <label for="sel2">Ação</label>
                <select class="form-control" id="sel2">
                    <option>Alterou</option>
                    <option>Excluiu</option>
                    <option>Incluiu</option>
                </select>
            </div>

            <div class="col-sm-2 form-group">
                <label class="control-label" for="filterBase">Data Inicial</label>
                <input class="form-control" type="text" name="datainicial" size="40" value="01/01/2016">
            </div>

            <div class="col-sm-2 form-group">
                <label class="control-label" for="filterBase">Data de Final</label>
                <input class="form-control" type="text" name="datavalidade" size="40" value="01/01/2017">
            </div>

            <div class="col-sm-1 form-group">
                <a class="btn btn-primary btn-align-bottom" href="" role="button"><i class="icon-search"></i></a>
            </div>
        </div>
    </div>

    <hr class="col-sm-12">

    <div class="col-sm-12 form-group">
        <label class="control-label" for="filterBase">Resultado da Pesquisa</label>
        <div class="table table-responsive">
            <table class="table table-striped">
                <tbody>
                    <tr>
                        <th class="text-uppercase">Funcionalidade</th>
                        <th class="text-uppercase">Colaborador</th>
                        <th class="text-uppercase">Ação</th>
                        <th class="text-uppercase">Data</th>
                        <th class="text-uppercase">Código</th>
                        <th class="text-uppercase">Registro XML</th>
                    </tr>
                    <tr>
                        <td>Perfil</td>
                        <td>Administrador do Sistema</td>
                        <td>Incluiu</td>
                        <td>17/08/2016 10:55:44</td>
                        <td>8</td>
                        <td>&lt;?xml version="1.0"?&gt;
                                        &lt;EOPerfil xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
                                        &lt;idperfil&gt;8&lt;/idperfil&gt;
                                        &lt;nome&gt;Perfil de Apresentação&lt;/nome&gt;
                                        &lt;statusperfil&gt;1&lt;/statusperfil&gt;
                                        &lt;descStatusPerfil /&gt;
                                        &lt;dtalteracao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtalteracao&gt;
                                        &lt;dtcriacao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtcriacao&gt;
                                        &lt;Log&gt;
                                        &lt;IdUsuario&gt;1&lt;/IdUsuario&gt;
                                        &lt;IdPerfil&gt;0&lt;/IdPerfil&gt;
                                        &lt;dtInicio&gt;0001-01-01T00:00:00&lt;/dtInicio&gt;
                                        &lt;dtFim&gt;0001-01-01T00:00:00&lt;/dtFim&gt;
                                        &lt;/Log&gt;
                                        &lt;/EOPerfil&gt;
                        </td>
                    </tr>
                    <tr>
                        <td>Perfil</td>
                        <td>Administrador do Sistema</td>
                        <td>Incluiu</td>
                        <td>17/08/2016 10:55:44</td>
                        <td>8</td>
                        <td>&lt;?xml version="1.0"?&gt;
                                        &lt;EOPerfil xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
                                        &lt;idperfil&gt;8&lt;/idperfil&gt;
                                        &lt;nome&gt;Perfil de Apresentação&lt;/nome&gt;
                                        &lt;statusperfil&gt;1&lt;/statusperfil&gt;
                                        &lt;descStatusPerfil /&gt;
                                        &lt;dtalteracao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtalteracao&gt;
                                        &lt;dtcriacao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtcriacao&gt;
                                        &lt;Log&gt;
                                        &lt;IdUsuario&gt;1&lt;/IdUsuario&gt;
                                        &lt;IdPerfil&gt;0&lt;/IdPerfil&gt;
                                        &lt;dtInicio&gt;0001-01-01T00:00:00&lt;/dtInicio&gt;
                                        &lt;dtFim&gt;0001-01-01T00:00:00&lt;/dtFim&gt;
                                        &lt;/Log&gt;
                                        &lt;/EOPerfil&gt;
                        </td>
                    </tr>
                    <tr>
                        <td>Perfil</td>
                        <td>Administrador do Sistema</td>
                        <td>Incluiu</td>
                        <td>17/08/2016 10:55:44</td>
                        <td>8</td>
                        <td>&lt;?xml version="1.0"?&gt;
                                        &lt;EOPerfil xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
                                        &lt;idperfil&gt;8&lt;/idperfil&gt;
                                        &lt;nome&gt;Perfil de Apresentação&lt;/nome&gt;
                                        &lt;statusperfil&gt;1&lt;/statusperfil&gt;
                                        &lt;descStatusPerfil /&gt;
                                        &lt;dtalteracao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtalteracao&gt;
                                        &lt;dtcriacao&gt;2016-08-17T10:55:42.3498505-03:00&lt;/dtcriacao&gt;
                                        &lt;Log&gt;
                                        &lt;IdUsuario&gt;1&lt;/IdUsuario&gt;
                                        &lt;IdPerfil&gt;0&lt;/IdPerfil&gt;
                                        &lt;dtInicio&gt;0001-01-01T00:00:00&lt;/dtInicio&gt;
                                        &lt;dtFim&gt;0001-01-01T00:00:00&lt;/dtFim&gt;
                                        &lt;/Log&gt;
                                        &lt;/EOPerfil&gt;
                        </td>
                    </tr>
                </tbody>
            </table>
            <button type="button" class="btn btn-primary btn-sm pull-right"><i class="icon-doc-text noleft"></i>Exportar</button>
        </div>
    </div>

    <script src="js/jquery.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/highcharts-3d.js"></script>

</asp:Content>
