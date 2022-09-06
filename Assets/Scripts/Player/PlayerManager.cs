using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;

    [SerializeField] private GameObject leoPrefab;
    [SerializeField] private GameObject willPrefab;
    [SerializeField] private GameObject johnnyPrefab;
    private static GameObject instance;
    private SceneSwitcher sceneSwitcher;
    Transform mainCamera;
    private MapManager mapManager;
    private MessageUi messageUi;
    private GameObject player;
    public static bool blockPlayerAction { get; private set; }

    private List<GameObject> npcList;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = gameObject;
        }
        else
            Destroy(gameObject);

        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        messageUi = GameObject.Find("Canvas").GetComponent<MessageUi>();
        sceneSwitcher = GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>();
        npcList = new List<GameObject>();
        mainCamera = Camera.main.transform;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

    }

    // Update is called once per frame
    void Update()
    {
        if (mapManager.sceneChange)
        {
            initNpc(sceneSwitcher.location.curr, mapManager.npcSpawnPoints);
            initPlayer(mapManager.spawnPoint);
            mapManager.sceneChange = false;
        }

        blockPlayerAction = (messageUi.isOpen) ? true : false;
    }

    private void OnSceneUnloaded(Scene current)
    {
        foreach (var npc in npcList)
        {
            Destroy(npc);
        }
    }

    private void initPlayer(Vector3 startLocation)
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
            Camera.main.GetComponent<Camera>().backgroundColor = new Color32(35, 35, 50, 255);
        }

        startLocation.z = 0;
        startLocation.y = startLocation.y + 0.05f;

        player = Instantiate(playerPrefab, startLocation, Quaternion.identity) as GameObject;
        Debug.Log($"Initializing player to: {startLocation}");
        // set camera to player and position main camera at bird view
        mainCamera.SetParent(player.transform);
        Vector3 pos = startLocation;
        pos.z = -14;
        mainCamera.position = pos;

    }

    private void initNpc(string roomName, List<NpcData> npcData)
    {

        if (npcData.Capacity == 0) { return; }

        GameObject spawn(NpcData data)
        {
            switch (data.tag)
            {
                case "leo":
                    return Instantiate(leoPrefab, data.position, Quaternion.identity);
                case "johnny":
                    return Instantiate(johnnyPrefab, data.position, Quaternion.identity);
                case "will":
                    return Instantiate(willPrefab, data.position, Quaternion.identity);

                default:
                    return null;
            }
        }


        foreach (var d in npcData)
        {
            GameObject obj = spawn(d);

            npcList.Add(obj);
        }

    }
}
