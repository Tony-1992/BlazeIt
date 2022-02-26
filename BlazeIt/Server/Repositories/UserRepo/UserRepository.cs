using AutoMapper;
using BlazeIt.Server.Data;
using BlazeIt.Shared.DTOs.Request;
using BlazeIt.Shared.DTOs.Response;
using BlazeIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazeIt.Server.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest model)
        {
            // map DTO to entity
            var entityObj = _mapper.Map<User>(model);

            // Check if user already exists
            var exists = await DoesUserExist(model.Email);
            if (exists)
            {
                return new RegisterUserResponse
                {
                    Successful = false,
                    Error = "User already exists"
                };
            }


            // add to db
            await _db.TABLE_Users.AddAsync(entityObj);
            var successful = await Save();

            // Handle error
            if (!successful)
            {
                var badResponse = new RegisterUserResponse
                {
                    Successful = false,
                    Error = "Unable to register user"
                };

                return badResponse;
            }

            // update
            var response = new RegisterUserResponse
            {
                Successful = true
            };

            return response;
        }

        public async Task<ChangePasswordResponse> ChangePassword(string id, ChangePasswordRequest model)
        {
            // validate model
            if (model.Password != model.ConfirmNewPassword)
            {
                return new ChangePasswordResponse
                {
                    Successful = false,
                    Error = "Passwords do not match..."
                };
            }


            // Current entityObj if id & current password match
            var entity = await _db.TABLE_Users.FirstOrDefaultAsync(x => x.Id == id && x.Password == model.CurrentPassword);

            if (entity == null)
            {
                return new ChangePasswordResponse
                {
                    Successful = false,
                    Error = "Details do not match"
                };
            }

            // map DTO to entity
            // Source = what has the data
            // Destination = what you want to update
            entity = _mapper.Map(model, entity);

            _db.TABLE_Users.Update(entity);

            var successful = await Save();
            if (!successful)
            {
                return new ChangePasswordResponse
                {
                    Successful = false,
                    Error = "Unable to update details"
                };
            }


            var response = new ChangePasswordResponse
            {
                Successful = true
            };

            return response;
        }


        public async Task<DeleteAccountResponse> DeleteAccount(string id, DeleteAccountRequest model)
        {
            // validate model

            // Current entityObj
            var entity = await _db.TABLE_Users.FirstOrDefaultAsync(x => x.Id == id && x.Password == model.Password);

            if (entity == null)
            {
                return new DeleteAccountResponse
                {
                    Successful = false,
                    Error = "Incorrect password"
                };
            }

            _db.TABLE_Users.Remove(entity);

            var successful = await Save();
            if (!successful)
            {
                return new DeleteAccountResponse
                {
                    Successful = false,
                    Error = "Unable to update details"
                };
            }


            var response = new DeleteAccountResponse
            {
                Successful = true
            };

            return response;
        }

        public async Task<UpdatePasswordResponse> UpdateGeneralDetails(string id, UpdateGeneralDetailsRequest model)
        {
            // validate model

            // Current entityObj
            var entity = await _db.TABLE_Users.FirstOrDefaultAsync(x => x.Id == id);

            // map DTO to entity
            // Source = what has the data
            // Destination = what you want to update
            entity = _mapper.Map(model, entity);

            _db.TABLE_Users.Update(entity);

            var successful = await Save();
            if (!successful)
            {
                return new UpdatePasswordResponse
                {
                    Successful = false,
                    Error = "Unable to update details"
                };
            }


            var response = new UpdatePasswordResponse
            {
                Successful = true
            };

            return response;
        }

        public async Task<bool> DoesUserExist(string email)
        {
            var found = await _db.TABLE_Users.FirstOrDefaultAsync(x => x.Email == email);
            if (found == null)
            {
                return false;
            }

            return true;
        }

        public async Task<GetUserEmailResponse> GetUserEmail(string userId)
        {
            var user = await _db.TABLE_Users.FirstOrDefaultAsync(x => x.Id == userId);
            var response = new GetUserEmailResponse
            {
                Successful = true,
                Email = user.Email,
            };

            return response;
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
