using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHighScore;
    [SerializeField] private Button buttonSound;
    [SerializeField] private Sprite soundOn, soundOff;

    private void Start()
    {
        string value = PlayerPrefs.GetString("SOUND", "on");

        if (value.Equals("on"))
            buttonSound.image.sprite = soundOn;
        else
            buttonSound.image.sprite = soundOff;

        SetHighScoreText();
    }

    public void LoadGame() => SceneManager.LoadScene("Game");

    private void SetHighScoreText()
    {
        float time = PlayerPrefs.GetFloat("TIME", 0);

        if (time > 0)
            textHighScore.text = "High Score = " + (int)time / 60 + ":" + (int)time % 60;
        else
            textHighScore.text = "High Score = 0:0";
    }

    public void SetSound()
    {
        string value = PlayerPrefs.GetString("SOUND", "on");

        if (value.Equals("on"))
        {
            buttonSound.image.sprite = soundOff;
            PlayerPrefs.SetString("SOUND", "off");
        }
        else
        {
            buttonSound.image.sprite = soundOn;
            PlayerPrefs.SetString("SOUND", "on");
        }

        print(PlayerPrefs.GetString("SOUND", "on"));
    }
}
