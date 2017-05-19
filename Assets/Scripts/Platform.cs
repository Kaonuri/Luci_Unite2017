using Ultimate;
using UnityEngine;
using Time = UnityEngine.Time;

public class Platform : MonoBehaviour
{
    public float upYPos;
    public float upDuration;
    public float downYPos;
    public float downDuration;
    public float downMinDelay;
    public float downMaxDelay;

    public float downDelay { set; get; }

    public bool isDown = false;

    public float elapsedTime = 0f;

    public int index = -1;

    private Floor floor;

    private void Awake()
    {
        floor = FindObjectOfType<Floor>();
    }

    private void Update()
    {
        if (!isDown)
        {
            Vector3 destination = transform.localPosition;
            destination.y = upYPos;

            transform.localPosition = Vector3.Lerp(transform.localPosition, destination, Time.deltaTime / upDuration);

            if (transform.localPosition.y >= upYPos * 1.01f)
            {
                isDown = true;
            }
        }

        else
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= downDelay)
            {
                Vector3 destination = transform.localPosition;
                destination.y = downYPos;

                transform.localPosition = Vector3.Lerp(transform.localPosition, destination,
                    Time.deltaTime / downDuration);

                if (transform.localPosition.y <= downYPos * 0.9f)
                {
                    floor.RemovePlatform(index);

                    isDown = false;
                    elapsedTime = 0f;
                    index = -1;

                    UGL.contentsManager.Destroy(gameObject, "Platforms");
                }
            }
        }
    }
}
