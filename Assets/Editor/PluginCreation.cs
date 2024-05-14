using UnityEngine;
using UnityEditor;
using PlayerInteractive_Mediation;

[ExecuteInEditMode]
public class PluginCreation : EditorWindow
{
    [MenuItem("Player Interactive/ Create PlayerInteractive_AdmobManager")]
    public static void CreateAdsManager()
    {
        GameObject go = new GameObject("Player Interactive-Ads Manager");
        go.AddComponent<PlayerInteractive_Admob>();
        Selection.activeObject = go;
    }

}
