using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using net_rpg.utils;

namespace net_rpg.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly Encryption _encryption;

        public UserService(IMapper mapper, DataContext context, Encryption encryption)
        {
            _mapper = mapper;
            _context = context;
            _encryption = encryption;
        }
   
        
        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(RequestUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);

            var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Username.ToLower() == newUser.Username.ToLower());
            if(dbUser is not null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists";
                return serviceResponse;
            }
                
            string hashedPass = _encryption.Encrypt(newUser.Password);

            user.HashedPassword = hashedPass;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
           var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            try {
                var dbUser = await _context.Users.SingleOrDefaultAsync(c => c.Id == id);
                if(dbUser is null)
                {
                    throw new Exception($"User with Id '{id}' not found");
                }
                 
                _context.Users.Remove(dbUser);
                await _context.SaveChangesAsync();

                var dbUsers = await _context.Users.ToListAsync();
                serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            } catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            try{
                var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                if(dbUser is null) {
                    throw new Exception($"User with Id '{id}' not found");
                }

                serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(RequestUserDto updatedUser, int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            
            try {
                var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
                if(dbUser is null)
                {
                    throw new Exception($"User with Id '{id}' not found");
                }

                if(dbUser.Username.ToLower() == updatedUser.Username.ToLower())
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User name already exists";
                    return serviceResponse;
                }

                _mapper.Map(updatedUser, dbUser);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            } catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}