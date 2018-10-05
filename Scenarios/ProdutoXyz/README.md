## ProdutoXyz.Web

O RAC é um produto TOTVS desenvolvido com o Framework TNF para prover autenticação e autorização das aplicações desenvolvidas pela TOTVS.
Ele segue os padrões estipulados pelos protocolos OAuth2 e openid e foi construído utilizando um framework de open source chamado
Identity Server 4 (http://identityserver.io/)

Este é um exemplo de aplicação utilizando O RAC como provedor de identidade fazendo a autenticação e autorização de um produto batizado de
"Produto XYZ" para um Tenant definido como "Grupo ABC".

Para executar esse exemplo (F5) não será preciso configurar nada.
Ao executar a aplicação (F5) a mesma será compilada (devido ao angular estar incorporado ao projeto junto ao THF)
Acesse http://abc.localhost:5055 e você será redirecionado para o RAC para realizar a autenticação.
Realize a autenticação através do usuário "admin" e senha "totvs@123"

IMPORTANTE: 
Para resolução do Tenant tanto o RAC como nesta aplicação usando o DNS, onde o tenant resolvido será através do padrão http://{tenant}.localhost:5055.
Para que seja possível realizar o teste da aplicação local, você deverá colocar o DNS "abc.localhost" no seu arquivo de HOSTS do windows.
Caso você não deseja registrar essa informação no arquivo HOSTS do windows, pode ser usado o navegador Google Chrome que irá aceitar que você digite a url http://abc.localhost:5055
sem nenhuma configuração adicional.

Para visualizar a API acesse http://localhost:5055/swagger/index.html

### No RAC temos dois níveis de acesso que terão as respectivas permissões de gerenciamento:
	Acesso para Hosts (administração do RAC, nível mais alto de permissionamento):
		* Cadastro de Tenants: um Tenant poderá ser criado para isolar as aplicações e seus domínios (Isolamento de dados).
			- Para todo Tenant criado será criado também um usuário "admin" de senha "totvs@123";
			- Este exemplo possui um CRUD de Produtos onde toda operação envolve o Tenant que está logado.

		* Cadastro de Perfis: perfis poderão ser cadastrados para agrupar permissões de uma aplicação.
		* Cadastro de Usuários: um usuário terá acesso ao RAC.
		* Cadastro de Clientes OAuth: quando uma aplicação precisar ser autenticada e autorizada ela fará esse processo através de um cliente OAuth.
		  Para isso precisará ser informado algumas informações sobre a aplicação que será protegida no cadastro do cliente:

			- Fluxo de autenticação: http://docs.identityserver.io/en/release/topics/grant_types.html.
			
			- Produto: Um produto pode ser informado no campo ou então selecionado caso ele já exista. 
			  A referência do produto no RAC serve para que possam ser feitos os relacionamentos entre Tenant e produto 
			  (Um Tenant pode estar associado a N produtos. Acesse o cadastro de Tenants para visualizar o relacionamento).
			  A referência do produto também será utilizada para fazer a associação das permissões de uma aplicação.

			- Id do Cliente OAuth: Identificador que será usado para o cliente OAuth. Ele sempre será uma referência na hora de proteger a sua aplicação.

			- Chaves de Acesso: um cliente pode ter N chaves de acesso. Essas chaves serão usadas junto ao Id do Cliente OAuth para indicar como a aplicação será protegida 
			  (Verifique os arquivos de configuração racsettings.Development.json e racsettings.Production.json para maiores detalhes).

	Acesso para Tenants (administração de Tenants)
		- Cadastro de Organizações: um Tenant pode possuir uma estrutura organizacional contendo N níveis hierárquicos.
		- Cadastro de Perfis: perfis poderão ser cadastrados para agrupar permissões de uma aplicação de um Tenant.
		- Cadastro de Usuários: um usuário terá acesso ao RAC para gerenciamento daquele Tenant.

### Descoberta do Tenant pelo RAC:
	Existem 3 formas de resolver o Tenant de um usuário no RAC:

		* DNS: através de um wildcard (*, ?) utilizado no dns http://*.rac.totvs.com.br/totvs.rac, será realizada a descoberta do Tenant. 
		Exemplos de url válidas:
			- Administração do RAC: http://admin.rac.totvs.com.br/totvs.rac
			- Login com o Tenant ABC: http://abc.rac.totvs.com.br/totvs.rac

		* Ao realizar o login no RAC poderá ser enviado um parâmetro na query string chamado "acr_values" provindo do padrão openid que informa o tenant a ser autenticado.
		Isso pode ser feito diretamente no arquivo "auth.service.ts" dentro da pasta ClientApp/src/app/auth desta aplicação
		Exemplo (comentado na linha 88 do arquivo "auth.service.ts"):
			acr_values=tenant:abc

		* Ao realizar o login no RAC pode ser enviado um parâmetro no header da requisição com a chave "Tnf.TenantId" onde perá ser informado o id do tenant a ser logado.

### Como o RAC faz a descoberta das funcionalidades de uma aplicação?
	
	* Quando uma aplicação utilizando o TNF com o pacote de segurança "Tnf.AspNetCore.Security" configurado é executada, são procuradas todas as referências de attributos
	  do tipo "TnfAuthorizeAttribute" e então são recuperadas as permissões de toda a aplicação para enviar ao RAC.
	  Após isso é feita uma comunicação com o RAC para verificar se ele possui essas permissões. Caso ele não possua é feito o envio.

	* Neste exemplo serão encontradas as permissões "ProdutoXyz.ApplicationName" e "ProdutoXyz.Products" que estão sendo utilizadas nos controllers desta aplicação.

	* Para gerenciar se o usuário possui acesso a esse recurso você pode acessar o gerenciamento do RAC do Tenant ABC disponível em http://abc.rac.totvs.com.br/totvs.rac
	  utilizando o usuário "admin" e senha "totvs@123" e verificar no Menu "Cadastro de Usuários" os perfis de acesso associados a ele.
	  Lembrando: um usuário pode ter vários perfis de acesso e um perfil de acesso pode ceder N permissões a ele.

### Configuração

Acesse a administração do RAC utilizando o endereço http://admin.rac.totvs.com.br/totvs.rac com usuário "admin" e senha "123qwe".
Para acessar um Tenant (exemplo utilizado nesse cenário ABC) acesse http://abc.rac.totvs.com.br/totvs.rac utilizando o usuário "admin" e senha "totvs@123".

As configurações do RAC estão nos arquivos utilizados conforme o ambiente utilizado:
		racsettings.Development.json e 
		racsettings.Production.json.

Nestes arquivos temos as seguintes configurações:
  
	* AuthorityEndpoint: Endereço da url do RAC onde o produto será autenticado e autorizado;
	* ClientId: Identificador de cliente OAuth cadastrado no RAC;
	* ClientSecret: Chave de acesso do cliente OAuth cadastrado no RAC;

Dentro do front-end:
	ClientApp
		src
			environments
				environment.prod.ts
				environment.ts