using UnityEngine;

public class ReflectionCameraControl : MonoBehaviour
{
    private void OnPreRender()
    {
        GL.SetRevertBackfacing(revertBackFaces: true);
    }

    private void OnPostRender()
    {
        //GetComponent<Camera>().targetTexture = MirrorReflection.m_ReflectionTexture;
        GL.SetRevertBackfacing(revertBackFaces: false);
    }

    private void OnDestroy()
    {
        GL.SetRevertBackfacing(revertBackFaces: false);
    }
}