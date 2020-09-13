using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    // Get access to the different UI elements in the ships world-space UI and actively track and display
    // ships health, shield and weapon reload times. 
    [Header("HEALTH:")]
    public Slider healthbar;
    public Gradient healthGradient;
    public Image healthFill;

    [Header("SHIELD:")]
    [Space]
    public Slider shieldBar;
    public Gradient shieldGradient;
    public Image shieldBarFill;
    [Space]
    public GameObject shieldTimer;
    public Image shieldTimerFill;

    private ShipController shipCon;

    private void Start()
    {
        shipCon = GetComponentInParent<ShipController>();
        ReloadTimerFill(shieldTimerFill);
        shieldTimer.SetActive(false);
    }

    // -------- RADIAL TIMERS -------- //

    public void ReloadTimerFill(Image fillImage)
    {
        fillImage.fillAmount = 1f;
    }

        // Shield
    public IEnumerator ShieldTimer(float duration, ShieldController shieldCon)
    {
        print("hey");
        shieldTimer.SetActive(true);

        float startTime = Time.time;
        float time = duration;
        float value = 0;

        while (Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            value = time / duration;
            shieldTimerFill.fillAmount = value;
            yield return null;
        }

        shieldTimer.SetActive(false);
        SetMaxShieldValue(shipCon.shipShield.Stat.Value);
        shipCon.currentShield = shipCon.shipShield.Stat.Value;
        shieldCon.shield.SetActive(true);
    }

        // Weapons
    public IEnumerator WeaponReloadTimer(Image reloadDial, float duration, WeaponController wCon)
    {
        float startTime = Time.time;
        float time = duration;
        float value = 0;

        while (Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            value = time / duration;
            reloadDial.fillAmount = value;
            yield return null;
        }

        wCon.FireWeapon();
    }


    // -------- HEALTH AND SHIELD BARS -------- //

        // Heath
    public void SetMaxHealthValue(float health)
    {
        healthbar.maxValue = health;
        healthbar.value = health;
        healthFill.color = healthGradient.Evaluate(1f);
    }

    public void SetHealthBar(float health)
    {
        healthbar.value = health;
        healthFill.color = healthGradient.Evaluate(healthbar.normalizedValue);
    }

        // Shield
    public void SetMaxShieldValue(float shield)
    {
        shieldBar.maxValue = shield;
        shieldBar.value = shield;
        shieldBarFill.color = shieldGradient.Evaluate(1f);
    }

    public void SetShieldBar(float shield)
    {
        shieldBar.value = shield;
        shieldBarFill.color = shieldGradient.Evaluate(shieldBar.normalizedValue);
    }
}
