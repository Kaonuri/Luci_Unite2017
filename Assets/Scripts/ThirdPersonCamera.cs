using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed;
    public float zoomSpeed;

    public float height;
    [SerializeField] private float distance;

    public float nearlistDistance;
    public float farthestDistance;

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
//        Vector3 tmpPos = (transform.position - target.position).normalized * distance;
//        tmpPos.y = 2f;
//        transform.position = tmpPos;
//
//        transform.forward = target.position - transform.position;

        Vector3 lookAtPos = target.position;
        lookAtPos.y = height;

        transform.LookAt(lookAtPos);

        distance = Vector3.Distance(transform.position, target.position);

        if (distance < nearlistDistance)
        {
            distance = nearlistDistance;
        }

        if (distance > farthestDistance)
        {
            distance = farthestDistance;
        }

        transform.Translate(Vector3.right * Time.deltaTime * rotateSpeed);

        
        //        transform.RotateAround(target.position, transform.TransformDirection(Vector3.up), rotateSpeed * Time.deltaTime);
        //        transform.forward = target.position - transform.position;
    }

    public void ZoomIn()
    {
        if (distance < nearlistDistance)
        {
            distance = nearlistDistance;
            return;
        }            

        distance -= zoomSpeed * Time.deltaTime;
    }

    public void ZoomOut()
    {
        if (distance < farthestDistance)
        {
            distance = farthestDistance;
            return;
        }            

        distance += zoomSpeed * Time.deltaTime;
    }
}
