// using System.Collections;
// using System.Text.RegularExpressions;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.Tilemaps;

// public class Door : MonoBehaviour
// {
//     private SceneSwitcher sceneManager;
//     public GameObject playerPromptPrefab;
//     private GameObject playerPrompt;
//     private MessageUi messageUi;
//     private Tilemap tileMap;
//     private bool inProximity;
//     public List<Vector3Int> doorPositions;
//     public Dictionary<Vector3Int, string> sceneNames;
//     private Vector3Int doorKey;

//     private void Start()
//     {
//         doorPositions = new List<Vector3Int>();

//         sceneManager = GameObject.Find(ManagerNames.SCENE_SWITCHER).GetComponent<SceneSwitcher>();
//         messageUi = GameObject.Find("Canvas").GetComponent<MessageUi>();

//         tileMap = this.transform.GetComponent<Tilemap>();
//         initDoorPositions();
//         initSceneNames();
//         UpdateMapManager();

//         // SceneManager.sceneLoaded += OnSceneLoaded;
//         playerPrompt = Instantiate(playerPromptPrefab, Vector3.zero, Quaternion.identity, this.transform);
//         inProximity = false;

//     }

//     private void Update()
//     {

//         if (inProximity && messageUi.decision == MessageUi.Click.ACCEPTED)
//         {
//             if (sceneNames.TryGetValue(doorKey, out string sceneName))
//             {
//                 sceneManager.loadScene(sceneNames[doorKey]);
//             }
//             else
//             {
//                 Debug.Log($"{doorKey}: Key not found...");
//             }
//         }
//     }

//     // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     // {

//     // }

//     private void UpdateMapManager()
//     {
//         MapManager mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
//         mapManager.tilemap = tileMap;

//         foreach (var keyVal in sceneNames)
//         {
//             if (keyVal.Value.Equals(sceneManager.location.prev))
//             {
//                 mapManager.spawnPoint = tileMap.GetCellCenterWorld(keyVal.Key);
//                 Debug.Log($"Setting spawnpoint: {mapManager.spawnPoint}");
//             }
//         }

//         mapManager.sceneChange = true;
//     }

//     private void initDoorPositions()
//     {
//         if (tileMap == null) { return; }

//         foreach (var position in tileMap.cellBounds.allPositionsWithin)
//         {
//             if (!tileMap.HasTile(position)) { continue; }

//             doorPositions.Add(position);
//         }
//     }

//     private void initSceneNames()
//     {
//         sceneNames = new Dictionary<Vector3Int, string>();

//         foreach (Transform child in transform)
//         {
//             Vector3Int pos = tileMap.WorldToCell(child.position);
//             sceneNames.Add(pos, child.tag);
//             Debug.Log($"Adding: {child.tag}, Location: {child.position}");
//         }
//     }

//     private string GetProperRoomName(string roomName)
//     {

//         if (roomName.Equals(SceneNames.WILL_ROOM))
//         {
//             return "Will and Johnny's room";
//         }

//         string[] split = Regex.Split(roomName, @"(?<!^)(?=[A-Z])");

//         if (split.Length < 2) { return ""; }

//         return $"{split[0]}'s {split[1]}";


//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         inProximity = true;
//         doorKey = tileMap.WorldToCell(other.transform.position);

//         if (sceneNames.TryGetValue(doorKey, out string sceneName))
//         {
//             string roomName = (sceneName.Equals(SceneNames.MAIN_ROOM)) ? "Main room" : GetProperRoomName(sceneName);

//             messageUi.OpenMessageBox($"Enter {roomName}?");
//         }
//         else
//         {
//             Debug.Log("Door key not found...");
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         inProximity = false;
//     }
// }