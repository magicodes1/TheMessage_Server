using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TheMessage.Common;
using TheMessage.Comunications;
using TheMessage.Data;
using TheMessage.Interfaces.Servives;
using TheMessage.Models;
using TheMessage.Resources;

namespace TheMessage.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly JwtConfigs jwtOption;

        private readonly ChatDbContext _context;
        private readonly IMapper _mapper;


        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IOptions<JwtConfigs> jwtOption, ChatDbContext context, IMapper mapper)
        {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.jwtOption = jwtOption.Value;
            _context = context;
            _mapper = mapper;
        }

        public async Task<SignInResponse> signIn(SignInResource signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, false, false);

            if (!result.Succeeded)
            {
                return new SignInResponse { Status = false, Message = "Either user name or password is wrong.", Token = "" };
            }

            var user = await userManager.FindByNameAsync(signInModel.UserName);

            var roles = await userManager.GetRolesAsync(user);


            var token = TokenGeneration(signInModel.UserName, roles as List<string>);


            user.isOnline = true;

            _context.ApplicationUsers.Update(user);

            await _context.SaveChangesAsync();

            var userResource = new UserResource { Id = user.Id, UserName = user.UserName, isOnline = user.isOnline };

            return new SignInResponse { Status = true, Message = "", Token = token, User = userResource };
        }



        private string TokenGeneration(string userName, List<string> roles)
        {
            var secretKey = Hash.Hashkey(jwtOption.Secret);

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userName),
                new Claim(ClaimTypes.Name,userName)
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public async Task<SignUpResponse> signUp(SignUpResource signUpModel)
        {
            var user = new ApplicationUser { UserName = signUpModel.UserName, isOnline = false };

            var result = await userManager.CreateAsync(user, signUpModel.Password);


            if (!result.Succeeded)
            {
                return new SignUpResponse { Status = false, Message = $"Register user {signUpModel.UserName} has been fail." };
            }

            if (!await roleManager.RoleExistsAsync(signUpModel.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(signUpModel.Role));
            }

            var existUser = await userManager.FindByNameAsync(signUpModel.UserName);

            if (existUser == null)
            {
                return new SignUpResponse { Status = false, Message = $"User {signUpModel.UserName} has not found." };
            }

            await userManager.AddToRoleAsync(existUser, signUpModel.Role);

            return new SignUpResponse { Status = true, Message = "" };
        }

        public async Task<LogoutResponse> logout(string userId)
        {
            var user = await _context.ApplicationUsers.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new LogoutResponse { Status = false, Message = "User is not found" };
            }

            user.isOnline = false;

            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();

            return new LogoutResponse { Status = true, Message = "" };
        }

        public async Task<OnlineUserResponse> getOnlineUsers()
        {
            var onlineUsers = await _context.ApplicationUsers.Where(u => u.isOnline == true).Select(u=>_mapper.Map<UserResource>(u)).ToListAsync();

            return new OnlineUserResponse { Status=true,Message="",Users=onlineUsers};
        }
    }
}
