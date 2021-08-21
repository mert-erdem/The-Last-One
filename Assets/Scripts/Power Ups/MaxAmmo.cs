using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(AudioSource), typeof(SpriteRenderer))]
public class MaxAmmo : MonoBehaviour, IDropable
{
    private Player player;

    private AudioSource audioSource;
    private BoxCollider2D col;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();       
    }

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        CheckSound();
        col = transform.GetComponent<BoxCollider2D>();
        spriteRenderer= transform.GetComponent<SpriteRenderer>();
    }

    public void GivePower()
    {
        player.weapon.Reload();
        StartCoroutine(PlaySound());             
    }

    private IEnumerator PlaySound()
    {
        if (!audioSource.mute)
        {
            audioSource.Play();
        }

        col.enabled = false;
        spriteRenderer.enabled = false;

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
