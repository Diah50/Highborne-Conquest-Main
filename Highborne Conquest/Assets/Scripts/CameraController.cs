using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float maxZoom = 5f;
    public float minZoom = 15f;
    public float moveSpeedAtMinZoom = 5f;
    public float moveSpeedAtMaxZoom = 10f;
    public AnimationCurve moveSpeedCurve;

    private Camera cam;
    private float targetZoom;
    private float moveSpeed;

    void Start()
    {
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

        // Move the camera with the mouse
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime*2;
            float mouseY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime*2;
            transform.position += new Vector3(-mouseX, -mouseY, 0);
        }
    }
}
