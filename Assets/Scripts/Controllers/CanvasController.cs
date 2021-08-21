using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTime;

    private static TextMeshProUGUI textAmmo, textWeapon, textGold;


    private void Awake()
    {
        textAmmo = GameObject.Find("Text_Ammo").GetComponent<TextMeshProUGUI>();
        textWeapon = GameObject.Find("Text_Weapon").GetComponent<TextMeshProUGUI>();
        textGold = GameObject.Find("Text_Gold").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        GameManager.actGameOver += GameOver;
        InvokeRepeating("ChangeTimeText", 0, 1);
    }

    private void ChangeTimeText() => textTime.text = (int)Time.timeSinceLevelLoad / 60 + ":" + (int)Time.timeSinceLevelLoad % 60;

    public static void ChangeAmmoText(int ammo) => textAmmo.text = ammo.ToString();

    public static void ChangeNameText(string name) => textWeapon.text = name;

    public static void ChangeGoldText(int gold) => textGold.text = gold.ToString();

    private void GameOver() => CancelInvoke();
}
