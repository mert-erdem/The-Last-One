using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : Weapon
{
    [SerializeField] private int ammo = 20;
    [SerializeField] private float fireRatio = 0.4f;
    [SerializeField] private int rayDamage = 100;

    private LineRenderer lineRenderer;

    public RayGun(GameObject muzzle, LineRenderer lineRenderer)
    {
        #region Setting The Weapon

        weaponName = "Raygun";
        this.lineRenderer = lineRenderer;
        base.muzzle = muzzle;
        totalAmmo = ammo;
        fireRate = fireRatio;
        ready = true;
        soundEffect = Resources.Load("RayGun_Sound") as AudioClip;

        #endregion

        GameManager.actGameOver += this.GameOver;
    }

    public override IEnumerator Fire()
    {
        if (totalAmmo != 0)
        {
            totalAmmo--;
            CanvasController.ChangeAmmoText(totalAmmo);

            #region Checking the hit
            RaycastHit2D raycast = Physics2D.Raycast(muzzle.transform.position, muzzle.transform.up);

            if (raycast)
            {
                if (raycast.collider.tag == "Enemy")
                {
                    raycast.transform.gameObject.GetComponent<Beetles>().TakeDamage(rayDamage);
                    PopupController.CreateDamagePopup(raycast.collider.transform.position, rayDamage);
                }
                if(raycast.collider.tag=="PowerUp")
                {
                    raycast.transform.gameObject.GetComponent<IDropable>().GivePower();
                }

                lineRenderer.SetPosition(0, muzzle.transform.position);
                lineRenderer.SetPosition(1, raycast.point);
            }
            #endregion

            lineRenderer.enabled = true;

            yield return new WaitForSeconds(0.1f);

            lineRenderer.enabled = false;

            ready = false;

            yield return new WaitForSeconds(fireRate);

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
