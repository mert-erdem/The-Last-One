using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Weapon
{
    [SerializeField] private int ammo = 50;
    [SerializeField] private float fireRatio = 0.15f;
    private GameObject muzzleFlash;


    public AutoRifle(GameObject muzzle)
    {
        #region Setting The Weapon

        weaponName = "Auto";
        base.muzzle = muzzle;
        totalAmmo = ammo;
        bulletType = Resources.Load("Auto_Bullet") as GameObject;
        muzzleFlash = Resources.Load("Muzzle_Flash") as GameObject;
        fireRate = fireRatio;
        ready = true;
        soundEffect = Resources.Load("Auto_Sound") as AudioClip;

        #endregion

        GameManager.actGameOver += this.GameOver;
    }

    public override IEnumerator Fire()
    {
        if (totalAmmo != 0)
        {
            totalAmmo--;
            CanvasController.ChangeAmmoText(totalAmmo);
            MonoBehaviour.Instantiate(bulletType, muzzle.transform.position + muzzle.transform.up, muzzle.transform.rotation);

            muzzle.GetComponent<SpriteRenderer>().enabled = true;

            ready = false;

            yield return new WaitForSeconds(fireRate);

            muzzle.GetComponent<SpriteRenderer>().enabled = false;

            ready = true;
        }
    }

    public override void Reload()
    {
        totalAmmo = ammo;
        CanvasController.ChangeNameText(this.weaponName);
        CanvasController.ChangeAmmoText(this.totalAmmo);
    }
}
