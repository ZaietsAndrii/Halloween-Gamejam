using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiScreenPopUpCutScene : MonoBehaviour
{
    public Button tapToContinueScreen;
    public Button pauseButton;
    public GameObject SpitOnScreen;

    private void Start()
    {
        tapToContinueScreen?.onClick.AddListener(NextLvl);
        pauseButton?.onClick.AddListener(NextLvl);
    }


    private void NextLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
