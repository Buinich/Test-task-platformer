using _Project.Code.Level.Loot;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
  [CustomEditor(typeof(KeySpawnMarker))]
  public class KeySpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(KeySpawnMarker spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawSphere(spawner.transform.position, 0.3f);
    }
  }
}