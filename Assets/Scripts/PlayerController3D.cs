using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController3D : MonoBehaviour
{
    // A simple player controller that allows for movement on the X and Z axis
    // This script is attached to the player object

    private float speed = 5.0f;
    private Rigidbody rb;

    private Vector3 direction;

    void Start()
    {
        // Fetch speed from GameDataManager
        speed = GameDataManager.instance.playerData.speed;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y).normalized * speed; // Normalize the vector to prevent faster diagonal movement
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    // Using Unity's new input system
    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();

    }
 
}
