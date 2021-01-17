using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abouca.Application.Dto;
using Abouca.Application.Validations;
using Abouca.Domain.Interfaces;
using Abouca.Domain.User;
using AutoMapper;
using FluentValidation;

namespace Abouca.Application.Services
{
    public class UserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository repository;
        private readonly AddUserValidator addUserValidator;
        private readonly ModifyUserValidator modifyUserValidator;
        public UserService(IMapper mapper, IUserRepository repository, AddUserValidator addUserValidator, ModifyUserValidator modifyUserValidator)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.addUserValidator = addUserValidator;
            this.modifyUserValidator = modifyUserValidator;
        }

        public async void Create(UserRegisterDto user)
        {
            var validatorResult = addUserValidator.Validate(user);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }

            var currentUser = mapper.Map<User>(user);
            await repository.CreateAsync(currentUser);
        }

        public async Task<User> Delete(User aggregate)
        {
            var validatorResult = modifyUserValidator.Validate(aggregate);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }

            var user = mapper.Map<User>(aggregate);
            await repository.RemoveAsync(user);
            return user;
        }

        public async Task<User> GetOne(int Id)
        {
            var currentUser = repository.FindOneAsync(m => m.Id == Id);
            if (currentUser.Result !=null)
            {
                return currentUser.Result;
            }
            else
            {
                throw new Exception("Id cannot be empty.");
            }
        }

        public async Task<string> Update(User aggregate)
        {
            var validatorResult = modifyUserValidator.Validate(aggregate);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exception(validatorResult.Errors.ToString());
            }

            var user = mapper.Map<User>(aggregate);
            await repository.UpdateAsync(user);
            return user.Id.ToString();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var UserList = repository.GetAllAsync();
            return UserList.Result;
        }
    }
}
