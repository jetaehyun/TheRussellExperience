using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    private AudioSource music;

    private void Start() {
        music = gameObject.transform.Find("BGMusic").GetComponent<AudioSource>();
        startButton.onClick.AddListener(OnStartClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        StartCoroutine(FadeIn(music, 2f));

    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene(SceneNames.TED_ROOM);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }

    private IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}