using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static GameObject instance;
    [SerializeField] private GameObject menuObject;
    public static bool blockPlayerAction { get; private set; }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuObject.SetActive(!menuObject.activeSelf);
            blockPlayerAction = menuObject.activeSelf;
        }
    }
}