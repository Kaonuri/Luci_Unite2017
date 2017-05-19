using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;

    private void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * Time.deltaTime * rotateSpeed);
    }
}
