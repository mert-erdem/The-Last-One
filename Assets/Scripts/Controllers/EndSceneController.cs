using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;

    void Start()
    {
        SetTimeText();
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }

    private void SetTimeText()
    {
        float time = PlayerPrefs.GetFloat("SURVIVETIME", 0);

        textTime.text = (int)time / 60 + ":" + (int)time % 60;
    }
}
