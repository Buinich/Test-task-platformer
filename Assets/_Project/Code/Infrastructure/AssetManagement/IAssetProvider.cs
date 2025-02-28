using CodeBase.Services;
using UnityEngine;

namespace _Project.Code.Infrastructure.AssetManagement
{
  public interface IAssetProvider
  {
    GameObject Instantiate(string path, Vector3 at, Transform parent);
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
    GameObject InstantiateWithInject(string path, Vector3 at);
    GameObject InstantiateWithInject(string path);
  }
}