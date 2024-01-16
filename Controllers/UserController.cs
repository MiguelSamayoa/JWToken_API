using API_BasicStore.DTOs;
using API_BasicStore.Models;
using API_BasicStore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API_BasicStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IUserServices userServices;
        private readonly ISessionTokenServices tokenServices;
        private ILogger logger;

        public UserController(IConfiguration configuration, IMapper mapper, IUserServices userServices, ISessionTokenServices tokenServices, ILogger<UserController> logger)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.userServices = userServices;
            this.tokenServices = tokenServices;
            this.logger = logger;
        }

        [HttpPost("/CrearUsuario")]
        public async Task<ActionResult<TokenDTO>> CreateUser(Credenciales credenciales)
        {
            User user =  await userServices.CreateCliente(credenciales);

            if (user.Id == 0) return BadRequest("No se pudo ingresar el usuario");

            SessionToken token = await tokenServices.Savetoken(BuildToken(user, credenciales));

            if (token == null) return BadRequest();

            TokenDTO tokenDTO = new TokenDTO()
            {
                Token = token.Token,
                Creacion = token.Creacion,
                Usuario = new UserDTO()
                {
                    UserTag = token.Usuario.UserTag,
                    Id = token.Usuario.Id,
                }
            };

            logger.LogInformation($"\n\nReturn: \n[ " +
                $"\n    Token: {tokenDTO.Token}" +
                $"\n    User: {tokenDTO.Usuario}" +
                $"\n    Fecha de creacion: {tokenDTO.Creacion}" +
                $"\n]");
            return Ok(tokenDTO);
        }


        [HttpPost("/Login")]
        public async Task<ActionResult<TokenDTO>> Login(Credenciales credenciales)
        {

            User user = await userServices.Login(credenciales);

            if (user == null) return NotFound();

            SessionToken token = await tokenServices.Savetoken(BuildToken(user, credenciales));

            if (token == null) return BadRequest();

            TokenDTO tokenDTO = new TokenDTO()
            {
                Token = token.Token,
                Creacion = token.Creacion,
                Usuario = new UserDTO()
                {
                    UserTag = token.Usuario.UserTag,
                    Id = token.Usuario.Id,
                }
            };


            logger.LogInformation($"\n\nReturn: \n[ " +
                $"\n    Token: {tokenDTO.Token}"+
                $"\n    User: {tokenDTO.Usuario}" +
                $"\n    Fecha de creacion: {tokenDTO.Creacion}" +
                $"\n]");

            return Ok(tokenDTO);
        }

        private SessionToken BuildToken(User user, Credenciales credenciales)
        {
            // --- Se crea el SessionToken, que contiene el JWT, fecha de expiracion, fecha de creacion y el empleado a quien pertenece 
            SessionToken sessionToken = new();

            sessionToken.Creacion = DateTime.Now;

            // --- --- Se generan los Claims que se almacenaran en el JWT
            List<Claim> claims = new()
            {
                new Claim("IdUser", user.Id.ToString() ),
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["LlaveJwt"]));

            var cred = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);


            var SecurityToken = new JwtSecurityToken(issuer: null,
                                                    audience: null,
                                                    claims: claims,
                                                    expires: DateTime.Now.AddHours(1),
                                                    signingCredentials: cred);


            sessionToken.Token = new JwtSecurityTokenHandler().WriteToken(SecurityToken);
            sessionToken.Usuario = user;

            return sessionToken;
        }

    }
}
