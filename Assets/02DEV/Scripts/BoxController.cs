using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public void SetLetter(string letter)
    {
        textMesh.text = letter.ToUpper();
    }
}