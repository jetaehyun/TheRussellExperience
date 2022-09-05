using UnityEngine;

public class EventSystemManager : MonoBehaviour
{
    private static GameObject instance;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = gameObject;
        }
        else
            Destroy(gameObject);
    }
}