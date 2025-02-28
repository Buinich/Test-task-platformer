namespace _Project.Code.Events
{
  public class HealthChangedEvent
  {
    public float CurrentHealth { get; }
    public float MaxHealth { get; }

    public HealthChangedEvent(float currentHealth, float maxHealth)
    {
      CurrentHealth = currentHealth;
      MaxHealth = maxHealth;
    }
  }

  public class DamageTakenEvent
  {
    public float Amount { get; }

    public DamageTakenEvent(float amount)
    {
      Amount = amount;
    }
  }

  public class HealthRestoredEvent
  {
    public float Amount { get; }

    public HealthRestoredEvent(float amount)
    {
      Amount = amount;
    }
  }

  public class HealthResetEvent { }
}