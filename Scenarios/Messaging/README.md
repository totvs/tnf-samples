#### Messaging

##### Exemplo de um projeto utilizando a abstração de fila do TNF com Rabbitmq.

Este exemplo contém duas APIs (Messaging.Web1 e Messaging.Web2) que irão se comunicar através de mensageria, onde a API Messaging.Web1 irá publicar essa mensagem e a Messaging.Web2
irá assinar e cachear essa mensagem na memória.
Ambas possuem o swagger configurado para que você possa testar esse processo manualmente.
O projeto também possui um projeto console Messaging.Client exemplificando o envio de mensagens através de um projeto externo.

Para rodar os três projetos clique com o botão direito em cima da solução no visual studio e clique em propriedades.
Na janela de propriedades marque na coluna "Action" os projetos Messaging.Client, Messaging.Web1 e Messaging.Web2 para "Start".
Após alterar essa configuração quando você executar as aplicações através da tecla F5 os 3 projetos irão rodar em modo Debug.

##### Instalação

Para que este exemplo funcione você precisa ter o Rabbitmq instalado com o protocolo AMQP habilitado.
Para mais detalhes de como habilitar o protocolo AMQP no Rabbitmq e para aprender mais sobre conceitos relacionados
a mensageria acesse (http://tdn.totvs.com/display/TNF/Mensageria)

Para instalar via windows: https://www.rabbitmq.com/install-windows.html
Para instalar via docker: 
* Instale o docker
* Execute os comandos (o primeiro comando irá criar o container e os outros 3 irão habilitar o protocolo AMQP):
	docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.7.3-management-alpine
	docker exec -it rabbitmq bash
	cd plugins
	rabbitmq-plugins enable rabbitmq_amqp1_0
