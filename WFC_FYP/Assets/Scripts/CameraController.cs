using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool active = true;

    private GameObject player;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float zoomSpeed = 2.0f;
    [SerializeField] private float minY = 5.0f; 
    [SerializeField] private float maxY = 150.0f;

    private Vector3 targetPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        if (active && player != null)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            offset.y -= scrollInput * zoomSpeed;
            offset.y = Mathf.Clamp(offset.y, minY, maxY); // Keep zoom within limits

            // Sets camera position to that of player position + offset
            targetPos = player.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        }
    }
}
