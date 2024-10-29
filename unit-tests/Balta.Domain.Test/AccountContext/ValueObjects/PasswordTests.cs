using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Extensions;
using Bogus;
using FluentAssertions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    private readonly Faker _faker = new Faker();
    
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
        Action password = () => Password.ShouldCreate(null);
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        Action password = () => Password.ShouldCreate("");
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        Action password = () => Password.ShouldCreate(" ");
        
        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        var shortPassword = _faker.Internet.Password(_faker.Random.Int(1, 7));
        Action password = () => Password.ShouldCreate(shortPassword);

        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        var largePassword = _faker.Internet.Password(_faker.Random.Int(33, 64));
        Action password = () => Password.ShouldCreate(largePassword);

        password.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void ShouldHashPassword()
    {
        var validPassword = _faker.Internet.Password(_faker.Random.Int(8, 32));
        
        var password = Password.ShouldCreate(validPassword);
        
        password.Hash.Should().NotBeNull();
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        
    }
    
    [Fact]
    public void ShouldGenerateStrongPassword() => Assert.Fail();
    
    [Fact]
    public void ShouldImplicitConvertToString() => Assert.Fail();
    
    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired() => Assert.Fail();
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange() => Assert.Fail();
}