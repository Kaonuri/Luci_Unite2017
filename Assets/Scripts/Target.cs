using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform hip;
    public float offset;
    public float lerpSpeed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, hip.transform.position, Time.deltaTime * lerpSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = hip.transform.position;
        }
    }
}
