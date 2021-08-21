using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(AudioSource), typeof(SpriteRenderer))]
public class MoneyBag : MonoBehaviour, IDropable
{
    [SerializeField] private int goldAmount = 10;
    private AudioSource audioSource;
    private BoxCollider2D col;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        CheckSound();
        col = transform.GetComponent<BoxCollider2D>();
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    public void GivePower()
    {
        GameManager.AddGold(goldAmount);
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        if(!audioSource.mute) audioSource.Play();
        
        spriteRenderer.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(audioSource.clip.length);

        Destroy(this.gameObject);
    }

    //check if sound on/off;
    private void CheckSound()
    {
        if (PlayerPrefs.GetString("SOUND", "on").Equals("off"))
            audioSource.mute = true;
        else
            audioSource.mute = false;
    }
}
