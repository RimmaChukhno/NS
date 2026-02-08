using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RaptorMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Gravity")]
    public float gravity = -5f;

    [Header("Terrain Limits")]
    public float minX = -500f;
    public float maxX = 500f;
    public float minZ = -500f;
    public float maxZ = 500f;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        ApplyGravity();
        ClampPosition();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v);

        if (input.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            controller.Move(transform.forward * moveSpeed * Time.deltaTime);
        }
    }

    void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}



//using UnityEngine;


//[RequireComponent(typeof(CharacterController))]
//public class RaptorMovement : MonoBehaviour
//{
//    [Header("Movement")]
//    public float moveSpeed = 5f;
//    public float rotationSpeed = 10f;

//    [Header("Gravity")]
//    public float gravity = -9.81f;

//    CharacterController controller;
//    Vector3 velocity;

//    void Start()
//    {
//        controller = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        Move();
//        ApplyGravity();
//    }

//    void Move()
//    {
//        float h = Input.GetAxis("Horizontal"); // A / D
//        float v = Input.GetAxis("Vertical");   // W / S

//        Vector3 direction = new Vector3(h, 0, v);

//        if (direction.magnitude >= 0.1f)
//        {
//            // Поворот в сторону движения
//            Quaternion targetRotation = Quaternion.LookRotation(direction);
//            transform.rotation = Quaternion.Slerp(
//                transform.rotation,
//                targetRotation,
//                rotationSpeed * Time.deltaTime
//            );

//            // Движение вперёд
//            Vector3 move = transform.forward * moveSpeed;
//            controller.Move(move * Time.deltaTime);
//        }
//    }

//    void ApplyGravity()
//    {
//        if (controller.isGrounded && velocity.y < 0)
//            velocity.y = -2f;

//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);
//    }
//}
