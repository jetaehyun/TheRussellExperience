using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gate : MonoBehaviour {
    
    [SerializeField] private Tilemap tm;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite doorLeft;
    [SerializeField] private Sprite doorRight;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject[] gateSides;
    private FloatingText floatingText;
    private GameObject interactionIcon;

    private readonly string animationLabel = "floatingText";
    private bool inProximity;
    private bool isGateOpen;


    private void Start() {
        isGateOpen = false;
        Vector3 pos = gameObject.transform.position;
        pos.y -= 0.05f;

        interactionIcon = Instantiate(floatingTextPrefab, pos, Quaternion.identity) as GameObject;
        floatingText = interactionIcon.GetComponent<FloatingText>();
        interactionIcon.SetActive(false);

        interactionIcon.transform.SetParent(gameObject.transform);
    }

    private void Update() {

        if (inProximity && Input.GetKeyDown(KeyCode.E) && !isGateOpen) 
        {
            audioSource.time = 0.1f;
            audioSource.Play();

            OpenGate();
            ResetGateEffect();

            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isGateOpen) { return; }
        interactionIcon.SetActive(true);
        floatingText.Activate(animationLabel);

        inProximity = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        ResetGateEffect();
    }

    private void OpenGate()
    {
        TileBase left = tm.GetTile(tm.WorldToCell(gateSides[0].transform.position));
        TileBase right = tm.GetTile(tm.WorldToCell(gateSides[1].transform.position));
        Tile newLeft = TileBase.CreateInstance<Tile>();
        Tile newRight = TileBase.CreateInstance<Tile>();
        newLeft.sprite = doorLeft;
        newRight.sprite = doorRight;

        tm.SwapTile(left, newLeft);
        tm.SwapTile(right, newRight);

        isGateOpen = true;
    }

    private void ResetGateEffect()
    {
        floatingText.Deactivate();
        interactionIcon.SetActive(false);
        inProximity = false;
    }


}