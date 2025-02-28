using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.EventBus
{
  public class EventBusService : IEventBusService
  {
    private readonly Dictionary<Type, Delegate> _eventHandlers = new();

    public void Subscribe<T>(Action<T> handler)
    {
      Type eventType = typeof(T);
      if (!_eventHandlers.TryAdd(eventType, handler))
      {
        _eventHandlers[eventType] = Delegate.Combine(_eventHandlers[eventType], handler);
      }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
      Type eventType = typeof(T);
      if (_eventHandlers.ContainsKey(eventType))
      {
        _eventHandlers[eventType] = Delegate.Remove(_eventHandlers[eventType], handler);
      }
    }

    public void Publish<T>(T eventData)
    {
      Type eventType = typeof(T);
      if (_eventHandlers.TryGetValue(eventType, out Delegate handlers))
      {
        (handlers as Action<T>)?.Invoke(eventData);
      }
    }

    public void Clear()
    {
      _eventHandlers.Clear();
    }
  }
}