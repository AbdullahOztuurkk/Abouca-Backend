using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abouca.Application.Dto;
using Abouca.Application.Validations;
using Abouca.Domain.Information;
using Abouca.Domain.Interfaces;
using Abouca.Domain.User;
using AutoMapper;

namespace Abouca.Application.Services
{
    public class InformationService
    {
        private readonly IMapper mapper;
        private readonly IInformationRepository repository;
        private readonly ModifyInformationValidator modifyInformationValidator;
        private readonly DeleteInformationValidator deleteInformationValidator;
        public InformationService(IMapper mapper, IInformationRepository repository, ModifyInformationValidator modifyInformationValidator, DeleteInformationValidator deleteInformationValidator)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.modifyInformationValidator = modifyInformationValidator;
            this.deleteInformationValidator = deleteInformationValidator;
        }

        public async void Create(Information information)
        {
            var validatorResult = modifyInformationValidator.Validate(information);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }
            await repository.CreateAsync(information);
        }

        public async Task<Information> Delete(InformationDeleteDto information)
        {
            var validatorResult = deleteInformationValidator.Validate(information);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }

            var currentInformation = mapper.Map<Information>(information);
            await repository.RemoveAsync(currentInformation);
            return currentInformation;
        }

        public async Task<Information> GetOne(int Id)
        {
            var currentInformation = repository.FindOneAsync(m => m.Id == Id);
            if (currentInformation.Result != null)
            {
                return currentInformation.Result;
            }
            else
            {
                throw new Exception("Id cannot be empty.");
            }
        }

        public async Task<string> Update(Information information)
        {
            var validatorResult = modifyInformationValidator.Validate(information);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }

            await repository.UpdateAsync(information);
            return information.Id.ToString();
        }

        public async Task<IEnumerable<Information>> GetAll()
        {
            var UserList = repository.GetAllAsync();
            return UserList.Result;
        }
    }
}
