using TMPro;
using UnityEngine;

public class HintDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;

    public void ChangeUIText(string text)
    {
        textObject.text = text;
    }
}
