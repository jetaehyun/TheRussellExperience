using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/DialogObject")]
public class DialogObject : ScriptableObject {

 [SerializeField] [TextArea] private string[] dialog;
 [SerializeField] private Response[] responses;

 public string[] Dialog => dialog;

 public bool HasResponses => responses != null && responses.Length > 0;

 public Response[] Responses => responses;

}