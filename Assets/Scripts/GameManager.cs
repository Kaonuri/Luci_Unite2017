using System.Collections;
using Ultimate;
using UnityEngine;
using Input = UnityEngine.Input;

public class GameManager : MonoBehaviour
{
    public bool autoPlay = false;
    public Vector3 InitHipPosition;

    public static GameManager Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && UGL.sceneManager.currentScene.sceneName != "Title")
        {
            UGL.sceneManager.ChangeScene("Title");
        }
    }
}
