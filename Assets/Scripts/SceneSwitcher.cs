using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    public (string prev, string curr) location;
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

        Debug.Log($"Loading: {dest}");
        location = (location.curr, dest);
        SceneManager.LoadScene(dest);

    }

    public void loadSingleScene(string dest, string src)
    {
        Debug.Log($"Loading: {dest}, Unloading {src}");

        SceneManager.LoadScene(dest);
        SceneManager.UnloadSceneAsync(src);
    }
}