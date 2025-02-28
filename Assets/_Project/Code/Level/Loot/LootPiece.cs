using System;
using UnityEngine;

namespace _Project.Code.Level.Loot
{
  public class LootPiece : MonoBehaviour
  {
    public event Action OnCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        OnCollected?.Invoke();
        Destroy(gameObject);
      }
    }
  }
}