using System;
using System.Collections.Generic;
using LingoBank.Core.Dtos;

namespace LingoBank.Core.UnitTests
{
    public sealed class UserWithPasswordTestDataGenerator
    {
        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                new UserWithPasswordDto 
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailAddress = string.Empty,
                    Password = "ditidhtgihtgihti",
                    Role = "User",
                    UserName = "Test1"
                },
                new UserWithPasswordDto
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailAddress = "user@example.com",
                    Password = String.Empty,
                    Role = "User",
                    UserName = "Test2"
                },
                new UserWithPasswordDto
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailAddress = "user@example.com",
                    Password = "EZPZ1235!",
                    Role = "User",
                    UserName = String.Empty
                }
            };
        }
    }
}