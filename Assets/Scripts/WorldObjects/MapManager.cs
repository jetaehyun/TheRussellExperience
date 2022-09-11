using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None
}

public class MapManager : MonoBehaviour
{
    private static GameObject instance;
    public Vector3 playerSpawnPoint;
    public string prevRoom;
    public List<NpcData> npcSpawnPoints { get; private set; }
    public Dictionary<string, Transform> entrancesDict { get; private set; }
    public bool sceneChange;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
            playerSpawnPoint = new Vector3(-0.2f, 0.3f, 0); // init point, do not change
        }
        else
            Destroy(gameObject);


        SceneManager.sceneLoaded += this.OnLoadCallback;
        SceneManager.sceneUnloaded += this.OnSceneUnloaded;
        entrancesDict = new Dictionary<string, Transform>();
        npcSpawnPoints = new List<NpcData>();
        sceneChange = false;
        prevRoom = string.Empty;
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject npcSpawnPoint = GameObject.Find("NpcSpawnPoint");

        if (npcSpawnPoint != null)
        {
            foreach (Transform point in npcSpawnPoint.transform)
            {
                npcSpawnPoints.Add(new NpcData(point.tag, point.position));
            }
        }

        GameObject _playerSpawnPoint = GameObject.FindGameObjectWithTag(prevRoom);
        if (_playerSpawnPoint != null)
        {
            playerSpawnPoint = _playerSpawnPoint.transform.GetChild(0).position;
        }

        GetAllEntrances();
        sceneChange = true;

    }

    public Direction GetSpawnDirection()
    {
        if (string.IsNullOrEmpty(prevRoom)) { return Direction.None; }

        return entrancesDict[prevRoom].GetComponent<Entrance>().entranceDirection;
    }

    private void GetAllEntrances()
    {
        GameObject entrances = GameObject.Find("Doors");

        foreach (Transform door in entrances.transform)
        {
            entrancesDict.Add(door.tag, door);
        }
    }

    private void OnSceneUnloaded(Scene current)
    {
        npcSpawnPoints.Clear();
        entrancesDict.Clear();
    }

    private void Update()
    {

    }
}