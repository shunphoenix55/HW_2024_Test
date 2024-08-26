using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PulpitController : MonoBehaviour
{
    // Pulpits are platforms the player can walk on
    // Pulpits don't last long and disappear within seconds
    // Each Pulpit has a timer. When it runs out, the Pulpit is destroyed, and the player falls
    // Only two Pulpits can exist simultaneously. 
    // A new Pulpit appears when the timer of the previous one reaches a certain time (x seconds). 
    // Pulpits last for a random time between y and z seconds. 
    // They appear adjacent to the previous one, but not in the same position.

    public static PulpitController instance;

    public GameObject pulpitPrefab;

    public GameObject currentPulpit;
    public GameObject nextPulpit;

    public float pulpitDistance = 2.0f;
    public int pulpitCount = 1;

    public float spawnAnimationTime = 0.5f;

    private int previousRandomDirection;

    private void Awake()
    {
        // Singleton class
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public void SpawnPulpit(Vector3 originPos)
    {
        // Select a random direction for the next Pulpit, from North, South, East, West
        Vector3 direction = Vector3.zero;
        int randomDirection = -1;
        // Make sure the next Pulpit is not in the same position as the previous one
        while (randomDirection == -1 || randomDirection == previousRandomDirection)
        {
            randomDirection = Random.Range(0, 4);
        }
        switch (randomDirection)
        {
            case 0:
                direction = Vector3.forward;
                previousRandomDirection = 1;
                break;
            case 1:
                direction = Vector3.back;
                previousRandomDirection = 0;
                break;
            case 2:
                direction = Vector3.right;
                previousRandomDirection = 3;
                break;
            case 3:
                direction = Vector3.left;
                previousRandomDirection = 2;
                break;
        }

        

        // Calculate the position of the next Pulpit
        Vector3 nextPulpitPos = originPos + direction * pulpitDistance;

        nextPulpit = Instantiate(pulpitPrefab, nextPulpitPos, Quaternion.identity);

        pulpitCount++;

        // Set the scale to 0 then animate it to 1
        nextPulpit.transform.localScale = Vector3.zero;
        nextPulpit.transform.DOScale(Vector3.one, spawnAnimationTime);


    }

    // Coroutine to spawn Pulpit after a certain time
    public IEnumerator SpawnPulpitAfterTime(Vector3 position)
    {
        yield return new WaitForSeconds(GameDataManager.instance.pulpitData.pulpit_spawn_time);
        SpawnPulpit(position);
    }

}
