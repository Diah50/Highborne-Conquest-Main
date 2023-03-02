using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float maxZoom = 5f;
    public float minZoom = 15f;
    public float moveSpeedAtMinZoom = 5f;
    public float moveSpeedAtMaxZoom = 10f;
    public AnimationCurve moveSpeedCurve;

    public float movementSpeed = 10.0f;
    public float edgeSize = 10.0f;

    private float screenWidth;
    private float screenHeight;

    private Camera cam;
    private float targetZoom;
    private float moveSpeed;

    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
        moveSpeed = moveSpeedAtMinZoom;
    }

    void Update()
    {
        // Move the camera with WASD keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Zoom the camera with the mouse scroll wheel
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollWheel * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * 5f);

        // Update move speed based on current camera zoom level
        float normalizedZoom = Mathf.InverseLerp(maxZoom, minZoom, targetZoom);
        float curveEval = moveSpeedCurve.Evaluate(normalizedZoom);
        moveSpeed = Mathf.Lerp(moveSpeedAtMaxZoom, moveSpeedAtMinZoom, curveEval);

        Vector3 position = transform.position;
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x > screenWidth - edgeSize)
        {
            position.x += movementSpeed * Time.deltaTime;
        }
        else if (mousePosition.x < edgeSize)
        {
            position.x -= movementSpeed * Time.deltaTime;
        }

        if (mousePosition.y > screenHeight - edgeSize)
        {
            position.y += movementSpeed * Time.deltaTime;
        }
        else if (mousePosition.y < edgeSize)
        {
            position.y -= movementSpeed * Time.deltaTime;
        }

        transform.position = position;
    }
}
