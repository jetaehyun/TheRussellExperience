using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{

    [SerializeField] private Rooms entranceTo;
    [SerializeField] private Rooms currRoom;
    [SerializeField] private bool lockedRoom;
    public Direction entranceDirection;
    private MessageUi messageUi;
    private SceneSwitcher sceneManager;
    private MapManager mapManager;
    private PlayerManager playerManager;
    private bool inProximity;

    private void Start()
    {
        sceneManager = GameObject.Find(ManagerNames.SCENE_SWITCHER).GetComponent<SceneSwitcher>();
        playerManager = GameObject.Find(ManagerNames.PLAYER_MANAGER).GetComponent<PlayerManager>();
        messageUi = GameObject.Find("Canvas").GetComponent<MessageUi>();
        mapManager = GameObject.Find(ManagerNames.MAP_MANAGER).GetComponent<MapManager>();
        inProximity = false;
    }

    private void Update()
    {
        if (inProximity && messageUi.decision == MessageUi.Click.ACCEPTED)
        {

            if (lockedRoom)
            {
                messageUi.OpenMessageBox("The door appears to be locked...", false);
                inProximity = false;
                return;
            }

            sceneManager.loadScene(entranceTo.ToString());
            mapManager.prevRoom = currRoom.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inProximity = true;
        string entranceName = entranceTo.ToString();
        string roomName = (entranceName.Equals(SceneNames.MAIN_ROOM)) ? "Main room" : GetProperRoomName(entranceName);
        messageUi.OpenMessageBox($"Enter {roomName}?");

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inProximity = false;
    }

    private string GetProperRoomName(string roomName)
    {
        if (roomName.Equals(SceneNames.WILL_ROOM))
        {
            return "Will and Johnny's room";
        }

        string[] split = Regex.Split(roomName, @"(?<!^)(?=[A-Z])");

        if (split.Length < 2) { return ""; }

        return $"{split[0]}'s {split[1]}";

    }
}