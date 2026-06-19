

using Layla.Domain.Events;

namespace Layla.Domain.Events
{
    public class UserRegisteredEvent : IEvent
    {        
        public Guid UserGuid { get; }
        public string Token { get; }
        public UserRegisteredEvent(Guid userGuid, string token)
        {
            UserGuid = userGuid;
            Token = token;
        }
    }
}
