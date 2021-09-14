#### New Messaging

##### Exemplo de um projeto utilizando a abstração de fila do TNF com Rabbitmq.

Este exemplo contém duas APIs (NewMessaging.ApplicationA e NewMessaging.ApplicationB) que irão se comunicar através de mensageria.

Ambas possuem o swagger configurado para que você possa testar esse processo manualmente.

A aplicação A envia uma mensagem incompleta para a aplicação B onde a mensagem se torna completa.
Após a aplicação B envia de volta a mensagem onde é possível visualizar a mensagem de forma finalizada.
Ambas as aplicações salvam os dados em memória.

##### Solução padrão

Para rodar os 2 projetos clique com o botão direito em cima da solução no visual studio e clique em propriedades.
Na janela de propriedades marque na coluna "Action" os projetos NewMessaging.ApplicationA, NewMessaging.ApplicationB para "Start".
Após alterar essa configuração quando você executar as aplicações através da tecla F5 os 2 projetos irão rodar em modo Debug.

Para que este exemplo funcione você precisa ter o Rabbitmq instalado com o protocolo AMQP habilitado.

##### Solução com Docker

Para rodar os 2 projetos você deve definir o projeto Docker-Compose como default através da tecla F5 os 2 projetos irão rodar em modo Debug.

Para que este exemplo funcione você precisa ter o Docker Desktop instalado.