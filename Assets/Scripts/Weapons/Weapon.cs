using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public string weaponName;

    public int totalAmmo;

    protected GameObject bulletType;

    protected GameObject muzzle;

    public bool ready;

    public float fireRate;

    public AudioClip soundEffect;

    public abstract IEnumerator Fire();

    public abstract void Reload();

    protected void GameOver() => ready = false;
}
