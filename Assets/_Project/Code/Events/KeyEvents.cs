namespace _Project.Code.Events
{
  public class KeyCollectedEvent { }
  
  public class AllKeysCollectedEvent { }

  public class KeysCountUpdatedEvent
  {
    public int CurrentKeys { get; }
    public int TotalKeys { get; }

    public KeysCountUpdatedEvent(int currentKeys, int totalKeys)
    {
      CurrentKeys = currentKeys;
      TotalKeys = totalKeys;
    }
  }
}