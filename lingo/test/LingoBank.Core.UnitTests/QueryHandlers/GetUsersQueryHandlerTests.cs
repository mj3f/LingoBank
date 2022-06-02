using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkCore.Testing.NSubstitute;
using FluentAssertions;
using LingoBank.Core.Queries;
using LingoBank.Core.QueryHandlers;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LingoBank.Core.UnitTests.QueryHandlers;

public class GetUsersQueryHandlerTests
{
    private GetUsersQueryHandler _sut;

    private readonly LingoContext _context;
    private List<ApplicationUser> _data;

    public GetUsersQueryHandlerTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<LingoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = Create.MockedDbContextFor<LingoContext>(dbContextOptions);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        _sut = new GetUsersQueryHandler(_context);
        var users = await _sut.ExecuteAsync(new GetUsersQuery());

        // Assert.NotNull(users);
        users.Should().HaveCount(0);

    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnListOfUsers_WhenUsersExist()
    {
        _context.Set<ApplicationUser>().Add(new ApplicationUser
        {
            UserName = "TEST",
            Role = "User"
        });
        await _context.SaveChangesAsync();
        
        _sut = new GetUsersQueryHandler(_context);
        var users = await _sut.ExecuteAsync(new GetUsersQuery());

        users.Should().HaveCountGreaterThan(0);
    }
}