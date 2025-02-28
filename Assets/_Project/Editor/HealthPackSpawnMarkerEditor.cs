using _Project.Code.Level.Loot;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
  [CustomEditor(typeof(HealthPackSpawnMarker))]
  public class HealthPackSpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(HealthPackSpawnMarker spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawSphere(spawner.transform.position, 0.3f);
    }
  }
}