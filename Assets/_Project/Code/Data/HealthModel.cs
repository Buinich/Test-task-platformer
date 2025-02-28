using System;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  public class HealthModel
  {
    public event Action<float, float> OnHealthChanged;
    public event Action OnPlayerDied;

    private readonly float _maxHealth;
    private float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    public HealthModel(PlayerConfig config)
    {
      _maxHealth = config.MaxHealth;
      _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
      _currentHealth = Mathf.Max(0, _currentHealth - amount);
      OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
      CheckForDeath();
    }

    public void RestoreHealth(float amount)
    {
      _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
      OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void ResetHealth()
    {
      _currentHealth = _maxHealth;
      OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
    
    private void CheckForDeath()
    {
      if (_currentHealth <= 0)
        OnPlayerDied?.Invoke();
    }
  }
}