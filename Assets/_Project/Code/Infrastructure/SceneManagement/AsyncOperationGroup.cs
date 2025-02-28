using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Code.Infrastructure.SceneManagement
{
  public readonly struct AsyncOperationGroup
  {
    public readonly List<AsyncOperation> Operations;

    public float Progress =>
      Operations.Count == 0 ? 0 : Operations.Average(op => op.progress);

    public bool IsDone =>
      Operations.All(op => op.isDone);

    public AsyncOperationGroup(int initialCapacity)
    {
      Operations = new List<AsyncOperation>(initialCapacity);
    }
  }
}