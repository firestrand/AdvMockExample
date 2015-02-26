using System.Threading.Tasks;
using AdvanceMockExample.Models;

namespace AdvanceMockExample.Business.Messaging
{
    public interface IMessageService
    {
        Task SendAsync(Message message);
    }
}
