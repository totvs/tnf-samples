### Super Market

Este exemplo contém um cenário simples de criação de um pedido utilizando o TNF com Redis e Rabbitmq e SqlServer.

Toda vez que um pedido é criado/alterado será gerado um evento (mensageria) que será consumido por outro serviço responsável pelo cálculo
do imposto.
Após este cálculo ser efetuado será gerado outro evento (mensageria) para atualização do pedido, onde serão atualizadas informações, sobre o imposto gerado e o valor total recalculado
na operação.

O exemplo foi separado em 3 contextos: Cruds, Sales e FiscalService.
Ambos utilizam bancos de dados separados para fins de exemplo.
Todos utilizam SqlServer e a troca de mensagens entre o serviço de Sales e FiscalService se da através do Rabbitmq.

#### Instalação

##### Configs e banco de dados

Para que este exemplo funcione você precisa ter o LocalDb instalado em seu visual studio ou configurar uma instancia válida do SqlServer
nos config da aplicação nos projetos
	
	SuperMarket.Backoffice.Crud.Web
		appsettings.Development.json e 
		appsettings.Production.json

	SuperMarket.Backoffice.Sales.Web
		appsettings.Development.json e 
		appsettings.Production.json

	SuperMarket.FiscalService.Web
		appsettings.Development.json e 
		appsettings.Production.json

##### Mensageria

Para que a mensageria funcione o Rabbitmq terá de ser instalado e configurado para o protocolo AMQP estar habilitado.
Para mais detalhes de como habilitar o protocolo AMQP no Rabbitmq e para aprender mais sobre conceitos relacionados
a mensageria acesse (http://tdn.totvs.com/display/TNF/Mensageria)

Para instalar via windows: (https://www.rabbitmq.com/install-windows.html)

Para instalar via docker: 
* Instale o docker
* Execute os comandos (o primeiro comando irá criar o container e os outros 3 irão habilitar o protocolo AMQP):
	* docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.7.3-management-alpine
	* docker exec -it rabbitmq bash
	* cd plugins
	* rabbitmq-plugins enable rabbitmq_amqp1_0

##### Redis Cache

Para instalar via windows: (https://github.com/rgl/redis/downloads)

Para instalar via docker:
* Instale o docker
* Execute o comando:
	* docker run --name redis -d -p 6379:6379 -i -t redis:3.2.11-alpine