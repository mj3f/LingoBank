using System;
using System.Linq;
using System.Threading.Tasks;
using LingoBank.API.Authentication;
using LingoBank.Core;
using LingoBank.Core.Commands;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LingoBank.API.UnitTests.Controllers
{
    public sealed class AuthControllerTests
    {
        // private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock = new();
        // private readonly Mock<IRuntime> _runtimeMock = new();

        private readonly LingoContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthControllerTests(LingoContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [Fact(Skip = "Broken")]
        public async Task Login_LoginWithDefaultAdminUser_ShouldReturnAJwtToken()
        {
            string email = "admin@example.com";
            _context.Users.Add(new ApplicationUser
            {
                Email = email,
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                Role = "Administrator"
            });

            await _context.SaveChangesAsync();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            string token = await _jwtTokenGenerator.BuildToken(user.Email);
            Assert.NotNull(token);
            // }
        }
    }
}