using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnimation : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public Color firstColor;
    public Color secondColor;
    private void Awake()
    {
        textMeshProUGUI.CrossFadeColor(firstColor, 1, false, true);
    }


}
