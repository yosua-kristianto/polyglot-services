namespace AuthService.Messaging.Producer
{
    public interface IAuthenticationSMTPMessageProducer
    {
        Task Send(AuthenticationOTPMessageProducerDTO dto);
    }
}
