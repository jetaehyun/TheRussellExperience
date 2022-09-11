using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject floatingTextPrefab;
    private FloatingText floatingText;
    private GameObject interactionIcon;
    private bool inProximity;
    private float playTime;
    private readonly string animationLabel = "floatingText";

    private void Start()
    {
        Vector3 pos = gameObject.transform.position;
        pos.y += 0.15f;
        interactionIcon = Instantiate(floatingTextPrefab, pos, Quaternion.identity) as GameObject; 
        floatingText = interactionIcon.GetComponent<FloatingText>();
        interactionIcon.SetActive(false);

        interactionIcon.transform.SetParent(gameObject.transform);
    }

    private void Update()
    {
        if (inProximity && Input.GetKeyDown(KeyCode.E))
        {
            if (audioSource.isPlaying)
            {
                playTime = audioSource.time;
                audioSource.Stop();
            }
            else
            {
                audioSource.time = playTime;
                audioSource.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactionIcon.SetActive(true);
        floatingText.Activate(animationLabel);
        inProximity = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        floatingText.Deactivate();
        interactionIcon.SetActive(false);
        inProximity = false;
    }
}