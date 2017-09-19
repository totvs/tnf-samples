using System;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Application.Services;
using Tnf.App.Bus.Client;
using Tnf.App.Bus.Notifications;
using Tnf.App.Bus.Queue.Interfaces;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.Architecture.Application.Commands;
using Tnf.Architecture.Application.Events;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Services;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.ReadInterfaces;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Application.Services
{
    public class ProfessionalAppService : AppApplicationService, IProfessionalAppService,
        IPublish<SpecialtyCreateCommand>, // Informa que essa classe publicará uma mensagem (SpecialtyCreateCommand)
        ISubscribe<SpecialtyCreatedEvent> // Informa que essa classe assinará uma mensagem (SpecialtyCreatedEvent)
    {
        /// <summary>
        /// Propriedade de controle - utilizada para observar
        /// o processamento da fila no Sample
        /// </summary>
        public static int SpecialtyCreatedEventIdHelper { get; set; }

        private readonly IProfessionalService _service;
        private readonly IProfessionalReadRepository _readRepository;

        public ProfessionalAppService(IProfessionalService service, IProfessionalReadRepository readRepository)
        {
            _service = service;
            _readRepository = readRepository;
        }

        public ListDto<ProfessionalDto, ComposeKey<Guid, decimal>> GetAllProfessionals(GetAllProfessionalsDto request)
            => _readRepository.GetAllProfessionals(request);

        public ProfessionalDto GetProfessional(RequestDto<ComposeKey<Guid, decimal>> keys)
        {
            var professionalId = keys.GetId().SecundaryKey;
            var code = keys.GetId().PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            var entity = _service.GetProfessional(keys);
            var dto = entity.MapTo<ProfessionalDto>();

            dto.RemoveExpandable(keys);

            return dto;
        }

        public ProfessionalDto CreateProfessional(ProfessionalDto professional)
        {
            if (professional == null)
                RaiseNotification(nameof(professional));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            var professionalBuilder = new ProfessionalBuilder()
                .WithProfessionalId(professional.ProfessionalId)
                .WithCode(professional.Code)
                .WithName(professional.Name)
                .WithPhone(professional.Phone)
                .WithEmail(professional.Email)
                .WithAddress(professional.Address);

            /* -----------------------------
             * Lógica para sample com Fila
             * -----------------------------
             * #Sobre
             * Para fins didáticos, o sample foi alterado para permitir a demostração
             * do conceito dentro do mesmo contexto de WebApi em uma única aplicação.
             * O controle é feito através da observação da propriedade estática this.SpecialtyCreatedEventIdHelper
             * 
             * #Fluxo             
             * -> Swagger - Cria payload com dados do Professional e uma única Specialty com descrição e Id = 0
             *    ! Sample no final da classe
             * -> Swagger - Faz Post de um novo Professional para a API de Professional
             * -> ProfessionalAppService - (Id = 0) Publica uma mensagem de Command para criação de uma nova Specialty
             * -> SpecialtyAppService - Assina o command, logo, irá processar o command.
             * -> SpecialtyAppService - Finaliza criação da nova Specialty e publica evento de nova Specialty criada
             * -> ProfessionalAppService - Assina evento de nova Specialty
             * -> ProfessionalAppService - Atualiza propriedade estática com o Id da nova Specialty
             * -> ProfessionalAppService - Libera looping while e finaliza processo de criação de novo Professional
             */

            // Verifica se SpecialyId = 0
            if (professional.Specialties.First().Id == 0)
            {
                // Cria Command para solicitar criação de Specialty
                var specialtyCreateCommand = new SpecialtyCreateCommand
                {
                    Description = professional.Specialties.First().Description
                };

                // Invoca publicação do Command
                Handle(specialtyCreateCommand);

                // Controle - Agaurda processamento do Command
                while (SpecialtyCreatedEventIdHelper == 0)
                    // Quando Command é finalizado, evento gerado atualiza valor de SpecialtyCreatedEventIdHelper
                    Task.Delay(-1);

                // Adiciona o Id da nova Specialty no Builder de Professional
                professionalBuilder.WithSpecialties(
                    professional
                    .Specialties
                    .Select(s =>
                        new SpecialtyBuilder()
                            .WithId(SpecialtyCreatedEventIdHelper)
                            .WithDescription(s.Description)
                            .Build())
                            .ToList());
            }
            else
                // Se SpecialtyId != 0 processa sem Fila
                professionalBuilder.WithSpecialties(
                    professional
                    .Specialties
                    .Select(s =>
                        new SpecialtyBuilder()
                            .WithId(s.Id)
                            .WithDescription(s.Description)
                            .Build())
                            .ToList());

            var id = _service.CreateProfessional(professionalBuilder);

            professional.ProfessionalId = id.SecundaryKey;
            professional.Code = id.PrimaryKey;

            return professional;
        }

        public ProfessionalDto UpdateProfessional(ComposeKey<Guid, decimal> keys, ProfessionalDto professional)
        {
            var professionalId = keys.SecundaryKey;
            var code = keys.PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (professional == null)
                RaiseNotification(nameof(professional));

            if (Notification.HasNotification())
                return new ProfessionalDto();

            var professionalBuilder = new ProfessionalBuilder()
                .WithProfessionalId(keys.SecundaryKey)
                .WithCode(keys.PrimaryKey)
                .WithName(professional.Name)
                .WithPhone(professional.Phone)
                .WithEmail(professional.Email)
                .WithAddress(professional.Address)
                .WithSpecialties(professional.Specialties.Select(s => new SpecialtyBuilder().WithId(s.Id).WithDescription(s.Description).Build()).ToList());

            _service.UpdateProfessional(professionalBuilder);

            professional.ProfessionalId = keys.SecundaryKey;
            professional.Code = keys.PrimaryKey;
            return professional;
        }

        public void DeleteProfessional(ComposeKey<Guid, decimal> keys)
        {
            var professionalId = keys.SecundaryKey;
            var code = keys.PrimaryKey;

            if (professionalId <= 0)
                RaiseNotification(nameof(professionalId));

            if (code == Guid.Empty)
                RaiseNotification(nameof(code));

            if (!Notification.HasNotification())
                _service.DeleteProfessional(keys);
        }

        private void RaiseNotification(params object[] parameter)
        {
            Notification.Raise(NotificationEvent.DefaultBuilder
                                                .WithMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithDetailedMessage(AppConsts.LocalizationSourceName, Error.InvalidParameter)
                                                .WithMessageFormat(parameter)
                                                .Build());
        }

        /// <summary>
        /// Processa mensagem para Publicação
        /// </summary>
        /// <param name="message">Mensagem que será publicada</param>
        public void Handle(SpecialtyCreateCommand message) =>
            // Método de extensão para publicação da mensagem
            message.Publish(); 

        /// <summary>
        /// Processa uma mensagem assinada
        /// </summary>
        /// <param name="message">Mensagem recebida por assinatura</param>
        public void Handle(SpecialtyCreatedEvent message) =>
            // Recebe mensagem da assinatura do evento e atualiza propriedade de controle SpecialtyCreatedEventIdHelper
            SpecialtyCreatedEventIdHelper = message.SpecialtyId;
    }
}

/*
 * Payload Sample de Novo Professional
 {
  "name": "Steve Jobs",
  "address": {
    "street": "Rua Do Comercio",
    "number": "123",
    "complement": "APT 123",
    "zipCode": {
      "number": "12345678"
    }
  },
  "phone": "98653214",
  "email": "steve@apple.com",
  "specialties": [
    {
      "description": "Genius",
      "id": 0
    }
  ]
}
 */
