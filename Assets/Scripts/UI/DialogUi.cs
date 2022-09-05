using System.Collections;
using UnityEngine;
using TMPro;

public class DialogUi : MonoBehaviour {

    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogObject testDialog;
    [SerializeField] private GameObject dialogBox;

    private TypeWriterEffect t;
    private ResponseHandler responseHandler;

    private void Start() {
        t = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogBox();
        // ShowDialog(testDialog);
    }

    public void ShowDialog(DialogObject dialogObject) {
        dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogObject));
    }
    
    private IEnumerator StepThroughDialog(DialogObject dialogObject) {

        for (int i = 0; i < dialogObject.Dialog.Length; i++) {
            string dialog = dialogObject.Dialog[i];

            yield return t.Run(dialog, textLabel);

            if (i == dialogObject.Dialog.Length - 1 && dialogObject.HasResponses) {
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogObject.HasResponses) {
            responseHandler.ShowResponses(dialogObject.Responses);
        } 
        else 
        {
            CloseDialogBox();
        }

    }

    private void CloseDialogBox() {
        dialogBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}