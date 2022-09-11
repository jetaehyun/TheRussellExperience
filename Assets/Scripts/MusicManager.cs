using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AudioClip defaultMusic;

    private AudioClip mainClip;
    private static GameObject instance;
    private float mainMusicTime;

    [SerializeField] private bool debugMode;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
            Destroy(gameObject);

        audioSource.clip = defaultMusic;
        mainClip = defaultMusic;
        SceneManager.sceneUnloaded += this.OnSceneUnloaded;
        SceneManager.sceneLoaded += this.OnLoadCallback;

    }

    public void PlayMusic(AudioClip clip, float startTime)
    {
        if (mainMusicTime == 0)
        {
            mainMusicTime = audioSource.time;
        }

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.time = startTime;
        audioSource.Play();
    }

    public float StopMusic()
    {
        float time = audioSource.time;
        PlayDefault();
        return time;
    }

    public void PlayDefault()
    {
        PlayMusic(mainClip, mainMusicTime);
        mainMusicTime = 0;
    }

    public bool isCurrentClip(AudioClip clip)
    {
        return audioSource.clip == clip;
    }

    private void OnSceneUnloaded(Scene current)
    {
        if (audioSource.clip == defaultMusic) { return; }

        PlayDefault();
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        AudioClip sceneClip = (SceneManager.GetActiveScene().name.Equals(SceneNames.NICK_CLOSET)) ? bossMusic : defaultMusic;

        if (sceneClip != mainClip)
        {
            mainMusicTime = 0;
            mainClip = sceneClip;
            PlayDefault();
        }
    }

    private void Update()
    {
        if (debugMode && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}