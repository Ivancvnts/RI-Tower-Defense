using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Vector3 direction = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition = transform.position + transform.TransformDirection(direction);
        transform.position = newPosition;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 currentPosition = transform.position;

        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction = new Vector3(input.x, transform.position.y, input.y).normalized * movementSpeed * Time.deltaTime;
        }
        else
        {
            direction = Vector3.zero;
        }
    }
}
