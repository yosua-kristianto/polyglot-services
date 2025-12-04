namespace AuthService.Api.Handler;


interface IAuthControllerHandller
{
    /// <summary>
    /// Register a new user with the given email. 
    /// The status of registered user is set to unverified, and a OTP is sent to the passed email.
    /// 
    /// 1. Check if the email is already registered.
    /// 2. If not, create a new user with unverified status.
    /// 3. Send an OTP email to the provided address.
    /// 4. OTP valid for 2 minutes.
    /// </summary>
    /// <param name="email"></param>
    public void Register(string email);

    /// <summary>
    /// Verify the email of a user using the provided OTP.
    /// 
    /// 1. Check if the email exists and is unverified.
    /// 2. Check if the provided OTP matches the one sent to the email.
    /// 3. If valid, update the user's status to verified.
    /// 4. Call out Login function to return Access Token.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="otp"></param>
    public void VerifyEmailRegistration(string email, string otp);

    /// <summary>
    /// Invoke OTP for the given email for login purposes.
    /// 
    /// 1. Check if the email exists and is verified.
    /// 2. Generate a new OTP and send it to the email.
    /// 3. The OTP is valid for a limited time period. (2 minutes)
    /// </summary>
    /// <param name="email"></param>
    public void InvokeOTP(string email);

    /// <summary>
    /// Login a user using the provided email and OTP.
    /// 
    /// 1. Check if the email exists and is verified.
    /// 2. Check if the provided OTP matches the one sent to the email.
    /// 3. If valid delete the OTP, generate and return an access and refresh token for the user.
    /// 4. The access token is valid for a limited time period. (5 minutes)
    /// </summary>
    /// <param name="email"></param>
    /// <param name="otp"></param>
    public void Login(string email, string otp, string deviceId);

    /// <summary>
    /// Refresh the OTP for the given email.
    /// 
    /// 1. Check if the OTP request exists.
    /// 2. Generate a new OTP and send it to the email.
    /// </summary>
    /// <param name="email"></param>
    public void RefreshOTP(string email);
}