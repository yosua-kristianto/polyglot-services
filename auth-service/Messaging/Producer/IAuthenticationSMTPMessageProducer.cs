using AuthService.Model.Object.Messaging.Producer;

namespace AuthService.Messaging.Producer
{
    public interface IAuthenticationSMTPMessageProducer
    {
        Task Send(AuthenticationOTPMessageProducerDTO dto);
    }
}
