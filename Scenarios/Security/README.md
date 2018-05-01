## Security Sample

Este exemplo mostra como configurar a segurança em uma API utilizando o Tnf com o Tnf-Rac (servidor de authenticação do Tnf).

Na raiz do projeto existe uma pasta chamada "Rac" com dois arquivos .cmd:
	
	- _ExecuteMigrator.cmd: execute para gerar as migrações;
	- _StartRacInfrastructure.cmd: execute para subir o Tnf-Rac;
	

Tanto o Tnf-Rac quanto este exemplo estão configurados para rodar em SqLite por default. Em ambos
essa configuração pode ser alterada nos arquivos de configuração através do parâmetro "DefaultConnectionString".

OBS: Se você alterar a base de dados, certifique-se de que a configuração das connections string apontem para uma instância
existente.

Ao subir o Tnf-Rac executando o arquivo mencionado acima "_StartRacInfrastructure.cmd" você poderá executar o projeto
Security.Web.

O projeto Security.Web contém duas APIS:
	
	- Customer: estará fechada precisando de autenticação para ser acessada devido ao uso do atributo TnfAuthorize decorando o controller "CustomerController";
	- Product: estará aberta sem necessidade de autenticação.

Para realizar a authenticação pelo swagger rode a aplicação acessando a url http://localhost:5055/swagger e clique no botão
"Authorize". Na janela que irá abrir você deverá selecionar os escopos e clicar no botão Authorize sendo dessa forma redirecionado para o Tnf-Rac para que você
possa realizar o login utilizando o usuário "admin" e a senha "123qwe".

Para acessar a tela de gerenciamento do Tnf-Rac acesse http://localhost:5002, utilizando o mesmo usuário e senha mencionado acima.