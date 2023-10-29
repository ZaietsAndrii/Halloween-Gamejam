using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiScreenPopUp : MonoBehaviour
{
    public PlayerController player;
    public Slider mouseSensetivitySlider;

    private void Start()
    {
        mouseSensetivitySlider.onValueChanged.AddListener(delegate { OnMouseSensetivityChanged(); });
    }

    private void OnMouseSensetivityChanged()
    {
        player.xMouseSensetivity = mouseSensetivitySlider.value;
        player.yMouseSensetivity = mouseSensetivitySlider.value;
    }
}
