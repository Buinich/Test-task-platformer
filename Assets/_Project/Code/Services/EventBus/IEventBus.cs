using System;
using System.Threading.Tasks;

namespace CodeBase.Services.EventBus
{
  public interface IEventBusService
  {
    void Subscribe<T>(Action<T> handler);
    void Unsubscribe<T>(Action<T> handler);
    void Publish<T>(T eventData);
    void Clear();
  }
}