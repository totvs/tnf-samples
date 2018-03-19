#### Querying

Para que este exemplo funcione você precisa ter o LocalDb instalado em seu visual studio ou configurar uma instancia válida do SqlServer
nos config da aplicação no projeto
	
	Querying.Web 
		appsettings.Development.json e 
		appsettings.Production.json.

Exemplo de um projeto fazendo consultas através do repositório do TNF.

Neste projeto você irá encontrar exemplo de consultas:
* N pra N
* 1 pra N
* Atributo Fields do objeto RequestDto para carregar campos de uma entidade do banco de dados
* Atributo Expand do objeto RequestDto para carregar relacionamentos de uma entidade do banco de dados