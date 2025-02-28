using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    private readonly DiContainer _container;

    public AssetProvider(DiContainer container)
    {
      _container = container;
    }
    
    public GameObject Instantiate(string path, Vector3 at, Transform parent)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity, parent);
    }
    
    public GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
    
    public GameObject InstantiateWithInject(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
    }

    public GameObject InstantiateWithInject(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return _container.InstantiatePrefab(prefab);
    }
  }
}