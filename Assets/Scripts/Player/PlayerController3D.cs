using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController3D : MonoBehaviour
{
    // A simple player controller that allows for movement on the X and Z axis
    // This script is attached to the player object

    private float speed = 5.0f;
    private Rigidbody rb;
    private Animator animator;

    private Vector3 direction;

    void Start()
    {
        // Fetch speed from GameDataManager
        speed = GameDataManager.instance.playerData.speed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y).normalized * speed; // Normalize the vector to prevent faster diagonal movement
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Set Speed property of the AnimatorController
        animator.SetFloat("Speed", movement.magnitude);

        // Make character's rotation interpolate smoothly to the direction it's moving
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
    }

    // Using Unity's new input system
    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();

    }
 
}
