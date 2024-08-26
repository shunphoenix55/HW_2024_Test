using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Pulpit : MonoBehaviour
{
   private float timeToLive;
    private float minTimeToLive;
    private float maxTimeToLive;
    
    private float timeToNextPulpit;

    private bool entered = false;
    private bool isDespawning = false;
    [HideInInspector]
    public bool pulpitSpawnFlag = false;


    [Header("Pulpit Timer")]
    public float currentTimetoLive;
    public float destroyAnimationTime = 0.5f;

    [Space]
    public TMP_Text timerText;
    public Material despawnMaterial; // material to animate the pulpit when it's about to despawn
    public Color originalColor;
    public Color targetColor;



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

        // set the flag to spawn a new pulpit when the current pulpit is about to despawn
        if (!pulpitSpawnFlag && 
            PulpitController.instance.pulpitCount<2 && 
            currentTimetoLive <= (timeToNextPulpit + PulpitController.instance.spawnAnimationTime))
        {
            PulpitController.instance.SpawnPulpit(gameObject.transform.position);
            pulpitSpawnFlag = true;
        }

        // if the pulpit is about to despawn, animate the material
        if (!isDespawning && currentTimetoLive <= 1f)
        {
            isDespawning = true;
            GetComponentInChildren<MeshRenderer>().material = despawnMaterial;

            // Store the original color of the material
            despawnMaterial.color = originalColor;
            // Make the material flash between the original color and the target color
            despawnMaterial.DOColor(targetColor, 0.25f).SetLoops(4, LoopType.Yoyo);
        }
    }

    private IEnumerator DestroyPulpit()
    {
        yield return new WaitForSeconds(timeToLive);

        PulpitController.instance.pulpitCount--;

        // Disable timer text
        timerText.enabled = false;

        // Animate the scale to zero and destroy the pulpit
        transform.DOScale(Vector3.zero, destroyAnimationTime)
            .OnComplete(() => Destroy(gameObject));


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
