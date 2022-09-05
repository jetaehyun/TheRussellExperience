using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour {

    [SerializeField]
    private float typewriterSpeed = 50f;

    private void Start() {
        
    }

    public Coroutine Run(string text, TMP_Text textLabel) {
        return StartCoroutine(TypeText(text, textLabel));
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel) {

        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while(charIndex < text.Length) {
            t += Time.deltaTime * typewriterSpeed;

            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            textLabel.text = text.Substring(0, charIndex);

            yield return null;
        }

        textLabel.text = text; 
    }
}