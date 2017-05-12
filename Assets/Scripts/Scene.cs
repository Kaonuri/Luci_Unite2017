using System;
using System.Collections;
using Ultimate;

public abstract class Scene : SceneHandlerBase
{
    public IEnumerator FadeOutAndChangeScene(string sceneName, float duration)
    {
        yield return StartCoroutine(AirVRCameraFade.FadeAllCameras(this, false, duration));
        UGL.sceneManager.ChangeScene(sceneName);
    }
}
