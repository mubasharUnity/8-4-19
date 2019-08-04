using UnityEngine;

public class UserInteractionScript : MonoBehaviour
{
    public SliceScript sphere;

    private Vector3[] points;//world position points
    private int pointMaxCount = 2, currentCountPoint = 0;
    private Camera camera;
    private float distanceFromCamera = 0;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        points = new Vector3[pointMaxCount];
        distanceFromCamera = Vector3.Distance(sphere.transform.position, camera.transform.position);//to offeset screen z-position
    }

    private float mouseDownTime;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time + 0.2f;//if mouse is up in 0.2 second, consider it a click
        }

        if (Input.GetMouseButtonUp(0) && Time.time < mouseDownTime)//if mouse is up in 0.2 second, consider it a click
        {
            var screenPosition = Input.mousePosition;
            screenPosition.z = distanceFromCamera;
            points[currentCountPoint] = camera.ScreenToWorldPoint(screenPosition);
            ++currentCountPoint;
            if (currentCountPoint == pointMaxCount)
            {
                currentCountPoint = 0;
                sphere.SliceWithPoints(points[0], points[1]);//when ever there are two new point call this method.
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(points.Length > 0)
        Gizmos.DrawLine(points[0], points[1]);
    }

}
