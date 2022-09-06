using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{

    private static GameObject instance;
    public Tilemap tilemap;

    public Vector3 spawnPoint;

    public List<NpcData> npcSpawnPoints { get; private set; }

    public bool sceneChange = false;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
            spawnPoint = new Vector3(-0.2f, 0.3f, 0); // init point, do not change
        }
        else
            Destroy(gameObject);


        SceneManager.sceneLoaded += this.OnLoadCallback;
        SceneManager.sceneUnloaded += this.OnSceneUnloaded;
        npcSpawnPoints = new List<NpcData>();
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject spawnPoint = GameObject.Find("NpcSpawnPoint");

        if (spawnPoint == null) { return; }

        foreach (Transform point in spawnPoint.transform)
        {
            npcSpawnPoints.Add(new NpcData(point.tag, point.position));
        }
    }

    private void OnSceneUnloaded(Scene current)
    {
        npcSpawnPoints.Clear();
    }

    private void Update()
    {

    }
}