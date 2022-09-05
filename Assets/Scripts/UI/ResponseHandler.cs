using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour {
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogUi dialogUi;
    private List<GameObject> tempResponseButtons = new List<GameObject>();
    

    private void Start() {
        dialogUi = GetComponent<DialogUi>();
    }

    public void ShowResponses(Response[] responses) {
        float responseBoxHeight = 0f;

        foreach (var resp in responses) {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = resp.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(resp));

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response) {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons) {
            Destroy(button);
        }

        tempResponseButtons.Clear();
        dialogUi.ShowDialog(response.DialogObject);

    }
}