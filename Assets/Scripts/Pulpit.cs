using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pulpit : MonoBehaviour
{
   private float timeToLive;
    private float minTimeToLive;
    private float maxTimeToLive;
    
    private float timeToNextPulpit;

    private bool entered = false;

    public float currentTimetoLive;
    public bool pulpitSpawnFlag = false;

    public TMP_Text timerText;



    private void Start()
    {
        // Get the min and max time a pulpit can exist from the GameDataManager
        minTimeToLive = GameDataManager.instance.pulpitData.min_pulpit_destroy_time;
        maxTimeToLive = GameDataManager.instance.pulpitData.min_pulpit_destroy_time;
        timeToLive = Random.Range(minTimeToLive, maxTimeToLive);

        timeToNextPulpit = GameDataManager.instance.pulpitData.pulpit_spawn_time;

        currentTimetoLive = timeToLive;

        StartCoroutine(DestroyPulpit());
    }

    private void Update()
    {
        currentTimetoLive -= Time.deltaTime;
        timerText.text = currentTimetoLive.ToString("F2");
        if (!pulpitSpawnFlag && PulpitController.instance.pulpitCount<2 && currentTimetoLive <= timeToNextPulpit)
        {
            PulpitController.instance.SpawnPulpit(gameObject.transform.position);
            pulpitSpawnFlag = true;
        }
    }

    private IEnumerator DestroyPulpit()
    {
        yield return new WaitForSeconds(timeToLive);

        PulpitController.instance.pulpitCount--;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !entered)
        {
            entered = true;
            GameController.instance.Score++;
        }
    }


}
