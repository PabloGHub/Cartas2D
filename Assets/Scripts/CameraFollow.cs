using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public BoxCollider2D mapBounds;
    public float smoothSpeed = 0.5f;

    private float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float camOrthsize;
    private float cameraRatio;
    private Camera mainCam;
    private Vector3 smoothPos;

    void Start()
    {
        mainCam = GetComponent<Camera>();

        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;

        camOrthsize = mainCam.orthographicSize;
        cameraRatio = (xMax + xMin) / 3.0f;
    }

    void FixedUpdate()
    {
        camX = Mathf.Clamp(followTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        camY = Mathf.Clamp(followTransform.position.y, yMin + camOrthsize, yMax - camOrthsize);

        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), smoothSpeed);
        this.transform.position = smoothPos;
    }
}