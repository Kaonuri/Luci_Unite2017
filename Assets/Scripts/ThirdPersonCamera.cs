using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;
    public float zoomSpeed;
    public float nearlistDistance;
    public float farthestDistance;

    private void Start()
    {
        transform.LookAt(target);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ZoomIn();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            ZoomOut();
        }
    }

    private void LateUpdate()
    {
        transform.RotateAround(target.position, transform.TransformDirection(Vector3.up), rotateSpeed * Time.deltaTime);

        Vector3 newForward = target.position - transform.position;
        transform.forward = newForward;

        Vector3 tmpPos = transform.position;
        tmpPos.y = 2f;
        transform.position = tmpPos;
    }

    public void ZoomIn()
    {
        if (Vector3.Distance(transform.position, target.position) < nearlistDistance)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * zoomSpeed);
    }

    public void ZoomOut()
    {
        if (Vector3.Distance(transform.position, target.position) > farthestDistance)
            return;

        transform.Translate(Vector3.back * Time.deltaTime * zoomSpeed);
    }
}
