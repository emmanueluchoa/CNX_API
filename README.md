<h2>Proposta do projeto</h2>
<p>Uma api que irá sugerir playlists de acordo com a cidade do usuário cadastrado.</p>

<h2>Tecnologias utilizadas</h2>
<ul>
  <li>.net Core 3</li>
  <li>Entity Framework</li>
  <li>Migrations</li>
  <li>Sql Server</li>
  <li>Autenticação via JWT</li>
  <li>Dependency injection</li>
  <li>Fluent Validation</li>
  <li>AutoMapper</li>
  <li>NLog</li>
</ul>

<h2>Serviços utilizados</h2>
<ul>
  <li><a href="https://developer.spotify.com/">Api Spotify</a></li>
  <li><a href="https://openweathermap.org/"> Api openweathermap</a></li>
</ul>

<h2>Arquitetura proposta</h2>
<p>Fora proposta uma arquitetura composta de 3 camadas, são elas Api, Domain e Repositório.</p>
<h3>Api</h3>
<p>Camada onde são disponibilizadas os métodos de ação da api. Tais métodos não devem possuir qualquer implementação lógica de negócio da aplicação servindo apenas de interface para chamadas a camada de domain.</p>
<h3>Domain</h3>
<p>Visando possibilitar uma fácil escalabilidade e manutenibilidade da aplicação a camada de domain fora desenvolvida contendo implementações de regras de negócios de toda a aplicação. Tais implementações são disponibilizadas por toda aplicação através de contratos (interfaces).</p>
<h3>Repositório</h3>
<p>Camada responsável por gerenciar as configurações e as implementações de persistência de dados. Por implementar os contratos estabelecidos na camada de domain é possível trocar a camada de persistência, que atualmente utiliza SQL Server, por qualquer outra tecnologia sem haver muito impacto a aplicação.</p>

<h2>Configurações</h2>
<h3>SQL Server e conexão com o banco de dados</h3>
<ul>
  <li>No <b>SQL SERVER</b> adicionar um novo login com o login name=CnxApiUser, marcar a opção SQL server authentication e inserir como senha CnxApiPass .</li>
<li>Ainda no cadastro do novo login no sql server, clicar na aba "Server roles" e marcar a opção "sysadmin".</li>
  <li>No projeto da api, localizar e abrir o arquivo appsettings.json. Localizar o nó CnxConnection e incluir alterar o valor "localhost\\sqlexpress" na string de conexão do Sql para a do seu servidor local.
</ul>
<h3>Postman</h3>
<p>Através deste arquivo de configurações do <a href="https://drive.google.com/file/d/1zQJgqEVK7f1aA-pVSA9jlK7Uww4akC9W/view?usp=sharing">Postman</a> é possível importar e testar as rotas disponíveis pela api.</p>

<h3>Swagger</h3>
<p>Sempre que a aplicação for inicializada, será exibida uma página do swagger contendo alguns detalhes sobre a api.</p>

<h3>Log</h3>
<p>A aplicação realiza logs de erros através de arquivos registrando-os na pasta C:\logs\ . Caso deseje alterar o local onde os arquivo de logs são salvos, localize no projeto API o arquivo nlog.config e altere a linha "<target name="logfile" xsi:type="File" fileName="<caminho-ou-pasta-desejada>" />" onde o campo <caminho-ou-pasta-desejada> é a pasta onde deseja salvar os logs</p>

