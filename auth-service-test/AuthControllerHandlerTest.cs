using AuthService.Api.Handler;
using AuthService.Common.Exceptions;
using AuthService.Messaging.Producer;
using AuthService.Model.Entity;
using AuthService.Model.Memcache;
using AuthService.Model.Object.Messaging.Producer;
using AuthService.model.Object.Response;
using AuthService.Repository.Memcache.Otp;
using AuthService.Repository.Memcache.Token;
using AuthService.Repository.UMA;
using Moq;
using Microsoft.Build.Utilities;

namespace AuthService.Tests;

/// <summary>
/// This Test Class handles Authentication Controller Handler from AuthService.
/// <see cref="IAuthControllerHandler"/>
/// </summary>
[TestClass]
public class AuthControllerHandlerTest
{
    private readonly Mock<IUMARepository> _mockUmaRepository;
    private Mock<IOtpRepository> _mockOtpRepository;
    private Mock<ITokenCacheRepository> _mockTokenCacheRepository;
    private Mock<IAuthenticationSMTPMessageProducer> _mockAuthOtpProducer;

    // Define all "Globally scoped-mock" here
    private readonly Guid MockUserId = Guid.NewGuid();
    private readonly string MockUserEmail = "testuser@example.com";
    private readonly string MockUserName = "Test User";
    private readonly string MockOtpValue = "12345678";
    private readonly string MockDeviceId = "device-001";

    public AuthControllerHandlerTest()
    {
        this._mockUmaRepository = new Mock<IUMARepository>();
        this._mockOtpRepository = new Mock<IOtpRepository>();
        this._mockTokenCacheRepository = new Mock<ITokenCacheRepository>();
        this._mockAuthOtpProducer = new Mock<IAuthenticationSMTPMessageProducer>();
    }

    #region Helper Methods

    /// <summary>
    /// Creates a mock User entity with specified status.
    /// </summary>
    private User CreateMockUser(short status = User.STATUS_ACTIVE)
    {
        return new User
        {
            Id = MockUserId,
            Email = MockUserEmail,
            Name = MockUserName,
            Status = status
        };
    }

    /// <summary>
    /// Creates a mock OtpCache entity.
    /// </summary>
    private OtpCache CreateMockOtpCache(string otpValue = null)
    {
        return new OtpCache
        {
            UserId = MockUserId,
            Otp = otpValue ?? MockOtpValue,
            ExpiredAt = DateTimeOffset.UtcNow.AddMinutes(2).ToUnixTimeMilliseconds()
        };
    }

    /// <summary>
    /// Creates an instance of AuthControllerHandler with mocked dependencies.
    /// </summary>
    private AuthControllerHandler CreateHandler()
    {
        return new AuthControllerHandler(
            this._mockUmaRepository.Object,
            this._mockOtpRepository.Object,
            this._mockTokenCacheRepository.Object,
            this._mockAuthOtpProducer.Object
        );
    }

    /// <summary>
    /// Sets up mock for finding user by email.
    /// </summary>
    private void SetupFindUserByEmail(User user, string email = null)
    {
        this._mockUmaRepository
            .Setup(r => r.FindUserByEmail(email ?? MockUserEmail))
            .Returns(user);
    }

    /// <summary>
    /// Sets up mock for getting OTP by user ID.
    /// </summary>
    private void SetupGetOtpByUserId(OtpCache otpCache)
    {
        this._mockOtpRepository
            .Setup(r => r.GetOtpByUserId(MockUserId))
            .ReturnsAsync(otpCache);
    }

    /// <summary>
    /// Sets up mock for creating token.
    /// </summary>
    private void SetupCreateToken()
    {
        this._mockTokenCacheRepository
            .Setup(r => r.CreateToken(It.IsAny<TokenCache>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);
    }

    #endregion

    #region InvokeOTP Tests

    /// <summary>
    /// This test function ensures TestPositive for InvokeOTP function.
    ///
    /// Passing: When the provided email is found and user is active, OTP is created and sent successfully.
    /// Non-Pass: Any exception occurred.
    ///
    /// <see cref="AuthControllerHandler.InvokeOTP"/>
    /// </summary>
    [TestMethod]
    public void TestPositive_InvokeOTP_Success()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_ACTIVE);
        SetupFindUserByEmail(user);

        this._mockOtpRepository
            .Setup(r => r.CreateOtp(It.IsAny<OtpCache>()));

        this._mockAuthOtpProducer
            .Setup(p => p.Send(It.IsAny<AuthenticationOTPMessageProducerDTO>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        var handler = CreateHandler();

        // Act & Assert
        handler.InvokeOTP(MockUserEmail);

        // Verify OTP was created
        this._mockOtpRepository.Verify(r => r.CreateOtp(It.Is<OtpCache>(o => o.UserId == MockUserId)), Times.Once);


        // Verify message was sent
        this._mockAuthOtpProducer.Verify(p => p.Send(It.Is<AuthenticationOTPMessageProducerDTO>(
            dto => dto.UserId == MockUserId && dto.Email == MockUserEmail)), Times.Once);
    }

    /// <summary>
    /// This test function ensures TestNegative for InvokeOTP function when user is not found.
    ///
    /// Passing: The UserNotFoundException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.InvokeOTP"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_InvokeOTP_UserNotFound()
    {
        // Arrange
        SetupFindUserByEmail(null);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotFoundException>(() =>
        {
            handler.InvokeOTP(MockUserEmail);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for InvokeOTP function when user is inactive.
    ///
    /// Passing: The UserNotActivatedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.InvokeOTP"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_InvokeOTP_UserInactive()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_INACTIVE);
        SetupFindUserByEmail(user);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotActivatedException>(() =>
        {
            handler.InvokeOTP(MockUserEmail);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for InvokeOTP function when user is suspended.
    ///
    /// Passing: The UserSuspendedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.InvokeOTP"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_InvokeOTP_UserSuspended()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_SUSPENDED);
        SetupFindUserByEmail(user);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserSuspendedException>(() =>
        {
            handler.InvokeOTP(MockUserEmail);
        });
    }

    #endregion

    #region Login Tests

    /// <summary>
    /// This test function ensures TestPositive for Login function.
    ///
    /// Passing: When valid email and OTP are provided, login succeeds and returns tokens.
    /// Non-Pass: Any exception occurred.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestPositive_Login_Success()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_ACTIVE);
        var otpCache = CreateMockOtpCache();
        
        SetupFindUserByEmail(user);
        SetupGetOtpByUserId(otpCache);
        SetupCreateToken();

        var handler = CreateHandler();

        // Act
        var result = handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrEmpty(result.AccessToken));
        Assert.IsFalse(string.IsNullOrEmpty(result.RefreshToken));
        
        // Verify token was created
        _mockTokenCacheRepository.Verify(r => r.CreateToken(It.Is<TokenCache>(t => t.UserId == MockUserId)), Times.Once);
    }

    /// <summary>
    /// This test function ensures TestNegative for Login function when user is not found.
    ///
    /// Passing: The UserNotFoundException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Login_UserNotFound()
    {
        // Arrange
        SetupFindUserByEmail(null);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotFoundException>(() =>
        {
            handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for Login function when user is inactive.
    ///
    /// Passing: The UserNotActivatedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Login_UserInactive()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_INACTIVE);
        SetupFindUserByEmail(user);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotActivatedException>(() =>
        {
            handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for Login function when user is suspended.
    ///
    /// Passing: The UserSuspendedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Login_UserSuspended()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_SUSPENDED);
        SetupFindUserByEmail(user);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserSuspendedException>(() =>
        {
            handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for Login function when OTP data is not found.
    ///
    /// Passing: The UnauthorizedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Login_OtpNotFound()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_ACTIVE);
        SetupFindUserByEmail(user);
        SetupGetOtpByUserId(null);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UnauthorizedException>(() =>
        {
            handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for Login function when OTP is invalid.
    ///
    /// Passing: The UnauthorizedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Login"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Login_OtpInvalid()
    {
        // Arrange
        var user = CreateMockUser(User.STATUS_ACTIVE);
        var otpCache = CreateMockOtpCache("99999999"); // Different OTP value
        
        SetupFindUserByEmail(user);
        SetupGetOtpByUserId(otpCache);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UnauthorizedException>(() =>
        {
            handler.Login(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    #endregion

    #region Register Tests

    /// <summary>
    /// This test function ensures TestPositive for Register function.
    ///
    /// Passing: When new user is registered successfully, OTP is created and sent.
    /// Non-Pass: Any exception occurred.
    ///
    /// <see cref="AuthControllerHandler.Register"/>
    /// </summary>
    [TestMethod]
    public void TestPositive_Register_Success()
    {
        // Arrange
        var newEmail = "newuser@example.com";
        var newName = "New User";
        var newUserId = Guid.NewGuid();

        _mockUmaRepository
            .Setup(r => r.FindUserByEmail(newEmail))
            .Returns((User)null);

        _mockUmaRepository
            .Setup(r => r.CreateUser(It.IsAny<User>()))
            .Returns((User u) =>
            {
                u.Id = newUserId;
                return u;
            });

        _mockOtpRepository
            .Setup(r => r.CreateOtp(It.IsAny<OtpCache>()));

        _mockAuthOtpProducer
            .Setup(p => p.Send(It.IsAny<AuthenticationOTPMessageProducerDTO>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        var handler = CreateHandler();

        // Act
        handler.Register(newEmail, newName);

        // Assert
        _mockUmaRepository.Verify(r => r.CreateUser(It.Is<User>(u => 
            u.Email == newEmail && 
            u.Name == newName && 
            u.Status == User.STATUS_INACTIVE)), Times.Once);
        
        _mockOtpRepository.Verify(r => r.CreateOtp(It.Is<OtpCache>(o => o.UserId == newUserId)), Times.Once);
        
        _mockAuthOtpProducer.Verify(p => p.Send(It.Is<AuthenticationOTPMessageProducerDTO>(
            dto => dto.Email == newEmail && dto.RecipientName == newName)), Times.Once);
    }

    /// <summary>
    /// This test function ensures TestNegative for Register function when user already exists.
    ///
    /// Passing: The DuplicatedUserException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.Register"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_Register_DuplicateUser()
    {
        // Arrange
        var existingUser = CreateMockUser();
        SetupFindUserByEmail(existingUser);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<DuplicatedUserException>(() =>
        {
            handler.Register(MockUserEmail, MockUserName);
        });
    }

    #endregion

    #region VerifyEmailRegistration Tests

    /// <summary>
    /// This test function ensures TestPositive for VerifyEmailRegistration function.
    ///
    /// Passing: When email verification succeeds, user is activated and tokens are returned.
    /// Non-Pass: Any exception occurred.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestPositive_VerifyEmailRegistration_Success()
    {
        // Arrange
        var inactiveUser = CreateMockUser(User.STATUS_INACTIVE);
        var activatedUser = CreateMockUser(User.STATUS_ACTIVE);
        var otpCache = CreateMockOtpCache();

        // First call returns inactive user (for VerifyEmailRegistration)
        // Second call returns active user (for Login called internally)
        var callCount = 0;
        _mockUmaRepository
            .Setup(r => r.FindUserByEmail(MockUserEmail))
            .Returns(() =>
            {
                callCount++;
                return callCount == 1 ? inactiveUser : activatedUser;
            });

        SetupGetOtpByUserId(otpCache);
        
        _mockUmaRepository
            .Setup(r => r.updateUserStatus(MockUserId, User.STATUS_ACTIVE));

        SetupCreateToken();

        var handler = CreateHandler();

        // Act
        var result = handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrEmpty(result.AccessToken));
        Assert.IsFalse(string.IsNullOrEmpty(result.RefreshToken));
        
        _mockUmaRepository.Verify(r => r.updateUserStatus(MockUserId, User.STATUS_ACTIVE), Times.Once);
    }

    /// <summary>
    /// This test function ensures TestNegative for VerifyEmailRegistration function when user is not found.
    ///
    /// Passing: The UserNotFoundException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_VerifyEmailRegistration_UserNotFound()
    {
        // Arrange
        SetupFindUserByEmail(null);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotFoundException>(() =>
        {
            handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for VerifyEmailRegistration function when user is already active.
    ///
    /// Passing: The UserNotFoundException is thrown (user not in pending state).
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_VerifyEmailRegistration_UserAlreadyActive()
    {
        // Arrange
        var activeUser = CreateMockUser(User.STATUS_ACTIVE);
        SetupFindUserByEmail(activeUser);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotFoundException>(() =>
        {
            handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for VerifyEmailRegistration function when user is suspended.
    ///
    /// Passing: The UserNotFoundException is thrown (user not in pending state).
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_VerifyEmailRegistration_UserSuspended()
    {
        // Arrange
        var suspendedUser = CreateMockUser(User.STATUS_SUSPENDED);
        SetupFindUserByEmail(suspendedUser);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UserNotFoundException>(() =>
        {
            handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for VerifyEmailRegistration function when OTP data is not found.
    ///
    /// Passing: The UnauthorizedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_VerifyEmailRegistration_OtpNotFound()
    {
        // Arrange
        var inactiveUser = CreateMockUser(User.STATUS_INACTIVE);
        SetupFindUserByEmail(inactiveUser);
        SetupGetOtpByUserId(null);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UnauthorizedException>(() =>
        {
            handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    /// <summary>
    /// This test function ensures TestNegative for VerifyEmailRegistration function when OTP is invalid.
    ///
    /// Passing: The UnauthorizedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.VerifyEmailRegistration"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_VerifyEmailRegistration_OtpInvalid()
    {
        // Arrange
        var inactiveUser = CreateMockUser(User.STATUS_INACTIVE);
        var otpCache = CreateMockOtpCache("99999999"); // Different OTP value
        
        SetupFindUserByEmail(inactiveUser);
        SetupGetOtpByUserId(otpCache);
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<UnauthorizedException>(() =>
        {
            handler.VerifyEmailRegistration(MockUserEmail, MockOtpValue, MockDeviceId);
        });
    }

    #endregion

    #region RefreshOTP Tests

    /// <summary>
    /// This test function ensures TestNegative for RefreshOTP function as it is not implemented.
    ///
    /// Passing: The NotImplementedException is thrown.
    /// Non-Pass: No error or another exception was thrown.
    ///
    /// <see cref="AuthControllerHandler.RefreshOTP"/>
    /// </summary>
    [TestMethod]
    public void TestNegative_RefreshOTP_NotImplemented()
    {
        // Arrange
        var handler = CreateHandler();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() =>
        {
            handler.RefreshOTP(MockUserEmail);
        });
    }

    #endregion
}