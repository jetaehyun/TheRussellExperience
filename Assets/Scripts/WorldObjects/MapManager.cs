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
    }

    private void Update()
    {

    }
}