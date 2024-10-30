using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [SerializeField] GunData gunData;
    [SerializeField] Transform muzzle;
    private InputManagerScript inputManager;

    float timeSinceLastShot;

    private void Start()
    {
        inputManager = InputManagerScript.Instance;
    }

    private void Update()
    {
        if (inputManager.PlayerShot())
        {
            Shoot();
        }
        if (inputManager.PlayerReload())
        {
            StartReload();
        }
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
        else
        {
            Debug.Log("Out of Ammo");
            StartReload();
        }
    }

    private void OnGunShot()
    {
        //throw new NotImplementedException();
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        Debug.Log("reloading");

        yield return new WaitForSeconds(gunData.reloadTime);
        if (gunData.totalAmmo >= gunData.magSize && gunData.currentAmmo == 0)
        {
            gunData.currentAmmo = gunData.magSize;
            gunData.totalAmmo = gunData.totalAmmo - gunData.magSize;
        }
        else if (gunData.totalAmmo >= gunData.magSize && gunData.currentAmmo != 0)
        {
            gunData.totalAmmo = gunData.totalAmmo - (gunData.magSize - gunData.currentAmmo);
            gunData.currentAmmo = gunData.magSize;
        }
        else if(gunData.totalAmmo < gunData.magSize && gunData.totalAmmo > 0)
        {
            gunData.currentAmmo = gunData.totalAmmo;
            gunData.totalAmmo = 0;
        }
        if(gunData.totalAmmo <= 0)
        {
            gunData.totalAmmo = 0;
        }

        gunData.reloading = false;
        Debug.Log("reloaded");
    }
}
