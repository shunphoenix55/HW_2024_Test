using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Makes the camera follow the player
public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
