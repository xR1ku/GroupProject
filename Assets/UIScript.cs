using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    int maxAmmo;
    int currentAmmo;
    float currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] Target target;
    [SerializeField] TextMeshProUGUI ammoCount;
    [SerializeField] GunData gunData;
    string finalText;



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = target.returnHealth();
        currentAmmo = gunData.currentAmmo;
        maxAmmo = gunData.magSize;
        finalText = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        ammoCount.text = finalText;
        healthBar.value = currentHealth;
    }
}
