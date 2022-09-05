using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;
    private static GameObject instance;
    Transform mainCamera;
    private Vector3Int startLocation;
    private MapManager mapManager;

    private MessageUi messageUi;
    private GameObject player;
    public static bool blockPlayerAction { get; private set; }

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
        mainCamera = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (mapManager.sceneChange)
        {
            initPlayer(mapManager.spawnPoint);
            mapManager.sceneChange = false;
        }

        blockPlayerAction = (messageUi.isOpen) ? true : false;
    }

    private void initPlayer(Vector3 startLocation)
    {
        if (mainCamera == null)
            mainCamera = Camera.main.transform;

        startLocation.z = 0;

        player = Instantiate(playerPrefab, startLocation, Quaternion.identity) as GameObject;

        // set camera to player and position main camera at bird view
        mainCamera.SetParent(player.transform);
        Vector3 pos = startLocation;
        pos.z = -14;
        mainCamera.position = pos;
    }
}
