# 📜 Caso de uso Eula Modal Component
## 🛠️ Uso

### Importe o EulaModalComponent

Certifique-se de importar e registrar o componente em seu módulo ou diretamente em um componente standalone (Angular 14+). No exemplo abaixo, utilizamos o standalone diretamente no app.component.ts.
Para instalar a biblioteca, utilize o seguinte comando:

### Importe o módulo HttpClient

Esta biblioteca realiza chamadas HTTP; portanto, você deve ter o HttpClientModule importado em seu módulo raiz (geralmente `AppModule`).

Em `app.component.ts`:
``` typescript

import { Component } from '@angular/core';
import {
  EulaModalComponent,
  OptInOptions
} from 'totvs-smartlink-components/eula-modal';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    EulaModalComponent,
    // ... demais componentes standalone
  ],
  templateUrl: './app.component.html',
})
export class AppComponent {
  // Define as opções necessárias para o Eula
  eulaOptions: OptInOptions = {
    appCode: 'consignado',       // Identificação da sua aplicação
    keyword: 'consignado',       // Palavras-chave para localizar o termo
    totvsCode: 'TESTAPPCODE',    // Código TOTVS se aplicável
    // environment: EnvironmentType.Development, // Para teste/uso em dev
    // previewMode: true,          // Se quiser apenas visualizar (não registra aceite)
    // modalTitle: 'Termos de Uso', // Título customizado (opcional)
    // acceptButtonLabel: 'Concordo', // Rótulo customizado (opcional)
    // debugMode: true,            // Exibe logs de depuração no console
  };
}

```

Em `app.component.html`

```html
<eula-modal
  [options]="eulaOptions"
>
</eula-modal>
```

### Propriedades de Entrada - objeto `OptInOptions`

* **modalTitle**: O título do modal (opcional, valor padrão: 'Termos de aceite').
* **acceptButtonLabel**: O rótulo do botão de aceitar (opcional, valor padrão: 'Aceitar').
* **previewMode**: Se true, o modal será exibido no modo de visualização sem botões (opcional, valor padrão: false).
* **appCode**: Código de identificação do app para localização do termo. Caso não seja encontrado um termo específico para o App, será apresentado o termo de aceite padrão da TOTVS (obrigatório).
* **environment**: Enumeração de identificação do ambiente (opcional, valor padrão: EnvironmentType.Production).
* **clientInfo**: ver configuração do objeto abaixo (opcional, valor padrão: nulo)

## Propridades `ClientInfoDto`

Objeto opcional de configuração complementar do registro de opt-in.

* **isTenantAdmin**: informa se o usuário é admin do Tenant.
* **environmentType**: define o tipo de ambiente.
* **smartlinkVersion**: define o a versão do SmartLink.
* **erpVersion**: define a versão do ERP.
* **clientIpAddress**: define o endereço IP do cliente.
* **clientHostname**: define o hostname do cliente.
* **geoLocation**: define a geolocalização do cliente.

### Eventos de Saída

accepted: Emitido quando o usuário aceita os termos.
