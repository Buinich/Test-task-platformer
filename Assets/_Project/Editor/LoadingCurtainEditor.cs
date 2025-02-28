using _Project.Code.Infrastructure.SceneManagement;
using UnityEditor;

namespace _Project.Editor
{
  [CustomEditor(typeof(LoadingCurtain))]
  public class LoadingCurtainEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LoadingCurtain script = (LoadingCurtain)target;

      if (!script.UseLoadingBar)
        return;

      serializedObject.Update();
      EditorGUILayout.PropertyField(serializedObject.FindProperty("loadingBar"));
      serializedObject.ApplyModifiedProperties();
    }
  }
}