using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace net_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
   
        
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Name.ToLower() == newCharacter.Name.ToLower());
            if(dbCharacter is not null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character already exists";
                return serviceResponse;
            }
       
            
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync();

            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
           var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            
            try {
                var dbCharacter = await _context.Characters.SingleOrDefaultAsync(c => c.Id == id);
                if(dbCharacter is null)
                {
                    throw new Exception($"Character with Id '{id}' not found");
                }
                 
                _context.Characters.Remove(dbCharacter);
                await _context.SaveChangesAsync();

                var dbCharacters = await _context.Characters.ToListAsync();
                serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            } catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try{
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if(dbCharacter is null) {
                    throw new Exception($"Character with Id '{id}' not found");
                }

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            } catch(Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            
            try {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if(dbCharacter is null)
                {
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found");
                }

                if(dbCharacter.Name.ToLower() == updatedCharacter.Name.ToLower())
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character name already exists";
                    return serviceResponse;
                }

                _mapper.Map(updatedCharacter, dbCharacter);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            } catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}