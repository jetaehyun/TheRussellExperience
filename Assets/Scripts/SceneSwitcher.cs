using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum Rooms
{
    MainRoom,
    TedRoom,
    LeoRoom,
    WillRoom,
    NickRoom,
    NickCloset
}

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;
    private AudioSource audioSource;

    private (string prev, string curr) location;
    private static GameObject instance;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = gameObject;
            location = (SceneNames.TED_ROOM, SceneNames.TED_ROOM);
        }
        else
            Destroy(gameObject);
    }

    public void loadScene(string dest)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(dest);

        if (buildIndex == -1)
        {
            Debug.Log($"{dest} does not exist...");
            return;
        }

        location = (location.curr, dest);
        PlaySound(openDoor);

        Debug.Log($"Loading: {dest}, Old Location: {location.prev}, New Location: {location.curr}");
        SceneManager.LoadScene(dest);


    }

    public void loadSingleScene(string dest, string src)
    {
        Debug.Log($"Loading: {dest}, Unloading {src}");

        SceneManager.LoadScene(dest);
        SceneManager.UnloadSceneAsync(src);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySound(closeDoor);

        var es = FindObjectsOfType<EventSystem>();
        if (es.Length > 1)
        {
            for (int i = 1; i < es.Length; i++)
            {
                Destroy(es[i].gameObject);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.priority = 0;
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}