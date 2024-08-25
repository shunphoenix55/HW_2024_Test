using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public float speed;
}

[System.Serializable]
public struct PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

// Struct containing PlayerData and PulpitData
public struct DoofusData
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

[DefaultExecutionOrder(-100)]
public class GameDataManager : MonoBehaviour
{
    // Singleton class that contains player data and pulpit data
    // This script is attached to the GameManager object

    public static GameDataManager instance;

    public DoofusData doofusData;
    public PlayerData playerData;
    public PulpitData pulpitData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Read Doofusdata from JSON file
        string json = Resources.Load<TextAsset>("doofus_diary").ToString();
        doofusData = JsonUtility.FromJson<DoofusData>(json);
        playerData = doofusData.player_data;
        pulpitData = doofusData.pulpit_data;


    }


}
