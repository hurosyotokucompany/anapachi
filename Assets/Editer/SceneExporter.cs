#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class SceneExporter : EditorWindow
{
    [MenuItem("Tools/Export Scene Info")]
    public static void ShowWindow()
    {
        GetWindow<SceneExporter>("Scene Exporter");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Export Scene Info"))
        {
            ExportSceneInfo();
        }
    }

    private void ExportSceneInfo()
    {
        StringBuilder sb = new StringBuilder();
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            sb.AppendLine("GameObject: " + obj.name);
            foreach (Component component in obj.GetComponents<Component>())
            {
                sb.AppendLine("  Component: " + component.GetType());
            }
        }

        string filePath = EditorUtility.SaveFilePanel("Save Scene Info", "", "SceneInfo", "txt");
        if (!string.IsNullOrEmpty(filePath))
        {
            File.WriteAllText(filePath, sb.ToString());
            EditorUtility.DisplayDialog("Export Complete", "Scene information exported successfully.", "OK");
        }
    }
}
# endif