using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    private TextMeshPro tmp;

    [SerializeField] 
    private float visiblityTime = 1f, floatingSpeed = 25f, alphaSpeed = 4f;

    private Color textColor;

    void Start()
    {
        tmp = transform.GetComponent<TextMeshPro>();
        textColor = tmp.color;
    }

    void Update()
    {
        visiblityTime -= Time.deltaTime;
        transform.position += new Vector3(0, floatingSpeed, 0) * Time.deltaTime;

        //disappearing effect;
        textColor.a -= alphaSpeed * Time.deltaTime;
        tmp.color = textColor;

        if (visiblityTime<0)
            Destroy(this.gameObject);
    }

    public void ChangeText(int newText)
    {
        tmp.text = newText.ToString();
    }
}
