using TMPro;
using UnityEngine;
using Input = UnityEngine.Input;
using Time = UnityEngine.Time;

public class TitleScene : Scene
{
    public FixedDurationMessage message;
    public float autoPlayDelay = 5f;    

    private Timer _timer = new Timer();

    public override void OnEnterScene()
    { 
        StartCoroutine(AirVRCameraFade.FadeAllCameras(this, true, 1f));

        if (GameManager.Instance.autoPlay)
        {
            _timer.Set(autoPlayDelay);
        }
    }

    protected override void ManualUpdate()
    {
        base.ManualUpdate();
        _timer.ManualUpdate(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.autoPlay = true;
            _timer.Set(autoPlayDelay);

            message.GetComponent<TextMeshPro>().text = "Autoplay is activated!";
            message.Show(2f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.autoPlay = false;
            message.GetComponent<TextMeshPro>().text = "Autoplay is dectivated!";
            message.Show(2f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeOutAndChangeScene("Main", 2f));
        }

        if (GameManager.Instance.autoPlay)
        {
            if (_timer.expired)
            {
                StartCoroutine(FadeOutAndChangeScene("Main", 2f));
                _timer.Reset();
            }
        }
    }

    public override void OnExitScene()
    {
    }
}
