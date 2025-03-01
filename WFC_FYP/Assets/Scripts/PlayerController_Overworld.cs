using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Overworld : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
         //Turn to face the mouse
        FaceMouse();

        //Movement input
        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        transform.Translate(movementInput * speed * Time.deltaTime);
    }

    //Turn to face the mouse
    void FaceMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;//Make sure its 2D

        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
       
}
