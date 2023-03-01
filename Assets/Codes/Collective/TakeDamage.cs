using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TakeDamage : MonoBehaviour
{
    [SerializeField] float durationHeal;
    [SerializeField] float LerpAmount;
    [SerializeField] float damageAmount;
    [SerializeField] float healthAmount;
    [SerializeField] Image Health;
    [SerializeField] Image Damage;
    [SerializeField] GameObject canvasObject;
    [SerializeField] float instantHealth;


    Death death;
    Collider colliderOfSelf;
    Fire fire;
    private void Start()
    {
        colliderOfSelf = GetComponent<Collider>();
        death = GetComponent<Death>();
    }
    private void OnEnable()
    {
        if (transform.CompareTag("Soldier"))
        {
            fire = transform.GetComponent<Fire>();
            fire.enabled = true;
        }
        durationHeal = 5;
        Damage.fillAmount = 1;
        Health.fillAmount = 1;
        instantHealth = healthAmount;
    }

    private void Update()
    {
        if (instantHealth < healthAmount)
        {
            durationHeal -= Time.deltaTime;
        }

        if (durationHeal < 0)
        {
            instantHealth += 1f;
            Health.fillAmount= instantHealth / healthAmount;
            Damage.fillAmount= instantHealth / healthAmount;
        }
        if (instantHealth == healthAmount)
        {
            Damage.fillAmount = 1;
            Health.fillAmount = 1;
            canvasObject.SetActive(false);
            durationHeal = 5;
        }
    }

    public void takeDamageFunc()// collider kapamasýný ayarla
    {
        if (colliderOfSelf.enabled)
        {
            if (instantHealth <= 0)
            {
                if (transform.CompareTag("Soldier"))
                    fire.enabled = false;
                canvasObject.SetActive(false);
                death.deathPlayer();
            }
            else
            {
                durationHeal = 5;
                canvasObject.SetActive(true);
                instantHealth -= damageAmount;
                Health.fillAmount = instantHealth / healthAmount;

                Damage.DOFillAmount(instantHealth / healthAmount, 1);
            }
        }
    }

}
