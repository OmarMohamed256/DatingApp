using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.DTOS;
using DatingApp.API.Entities;
using DatingApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        public IUserRepository _userRepository { get; }
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username){
            return await _userRepository.GetMemberAsync(username);
        }
    }
}