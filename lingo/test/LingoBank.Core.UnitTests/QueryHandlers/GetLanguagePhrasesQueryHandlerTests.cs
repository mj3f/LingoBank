using System;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using EntityFrameworkCore.Testing.NSubstitute;
using FluentAssertions;
using LingoBank.Core.Dtos;
using LingoBank.Core.Queries;
using LingoBank.Core.QueryHandlers;
using LingoBank.Database.Contexts;
using LingoBank.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LingoBank.Core.UnitTests.QueryHandlers;

public class GetLanguagePhrasesQueryHandlerTests
{
    private GetLanguagePhrasesQueryHandler _sut;
    private readonly LingoContext _context;
    
    public GetLanguagePhrasesQueryHandlerTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<LingoContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = Create.MockedDbContextFor<LingoContext>(dbContextOptions);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNull_WhenNoPhrasesExist()
    {
        _sut = new GetLanguagePhrasesQueryHandler(_context);
        Paged<PhraseDto> phrases = await _sut.ExecuteAsync(new GetLanguagePhrasesQuery
        {
            LanguageId = Guid.NewGuid().ToString(),
            Page = 1
        });

        phrases.Should().BeNull();

    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPagedPhrases_WhenPhrasesExist()
    {
        var languageId = Guid.NewGuid().ToString();
        _sut = new GetLanguagePhrasesQueryHandler(_context);
        
        var fixture = new Fixture();
        // FIXME: Bug in the EntityFrameworkCore.Testing.NSubstitue library that causes this to throw an exception.
        // https://github.com/rgvlee/EntityFrameworkCore.Testing/issues/112
        _context.Set<PhraseEntity>().AddRange(fixture.CreateMany<PhraseEntity>(11).ToList());
        await _context.SaveChangesAsync();
        
        Paged<PhraseDto> phrases = await _sut.ExecuteAsync(new GetLanguagePhrasesQuery
        {
            LanguageId = languageId,
            Page = 1
        });

        phrases.Should().NotBeNull();
    }
}