﻿using Ultimate;
using Time = UnityEngine.Time;

public class MainScene : Scene
{
    private Timer _timer = new Timer();

    public float duration;

    public override void OnEnterScene()
    {
        StartCoroutine(AirVRCameraFade.FadeAllCameras(this, true, 1f));
        
        _timer.Set(duration);
    }

    protected override void ManualUpdate()
    {
        base.ManualUpdate();
        _timer.ManualUpdate(Time.deltaTime);

        if (_timer.expired)
        {
            _timer.Reset();
            StartCoroutine(FadeOutAndChangeScene("Title", 2f));
        }
    }

    public override void OnExitScene()
    {
    }
}