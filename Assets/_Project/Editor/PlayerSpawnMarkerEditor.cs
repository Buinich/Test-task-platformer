using _Project.Code.Player;
using UnityEditor;
using UnityEngine;

namespace _Project.Editor
{
  [CustomEditor(typeof(PlayerSpawnMarker))]
  public class PlayerSpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(PlayerSpawnMarker spawner, GizmoType gizmo)
    {
      Gizmos.color = Color.magenta;
      Gizmos.DrawSphere(spawner.transform.position, 0.3f);
    }
  }
}