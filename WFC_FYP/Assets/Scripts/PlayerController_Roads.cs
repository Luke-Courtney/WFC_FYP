using UnityEngine;

public class PlayerController_Roads : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;       // Forward/backward speed
    [SerializeField] private float turnSpeed = 100.0f; // Turning speed

    private float moveInput;
    private float turnInput;

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = (moveInput != 0) ? Input.GetAxis("Horizontal") : 0;

        transform.Translate(Vector3.forward * moveInput * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}
