using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    [SerializeField] private Transform damagePopup;
    private static Transform dPopupStatic;
    private static TextMeshPro dPopupTMP;

    private static float zRot;

    private void Start()
    {
        dPopupStatic = damagePopup;
        dPopupTMP = dPopupStatic.GetComponent<TextMeshPro>();
    }

    public static void CreateDamagePopup(Vector2 position, int damage)
    {
        dPopupTMP.text = damage.ToString();
        zRot = Random.Range(-25f, 25f);
        Instantiate(dPopupStatic, position, Quaternion.Euler(0, 0, zRot));
    }
}
