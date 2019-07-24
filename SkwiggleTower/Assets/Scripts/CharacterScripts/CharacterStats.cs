using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Valarie Script: Character Stats hold total health, TakeDamage, Heal, gainXP, and Die. UML made
/// </summary>
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField]
    public int currentHealth;
    public int xp { get; private set; }

    public Stat damage;
    public Stat armor;
    public Stat capacity;

    public event System.Action<int, int> OnHealthChanged; //Delegate On Health Changed


    // RON
    public Ability primary;
    public Ability secondary;
    public Ability ultimate;

    public int direction;


    public AudioSource projectileHitSource;



    private void Awake()
    {
        currentHealth = maxHealth; //initial value
        xp = 5;
        direction = 1;
    }

    //add passive healing 

    public void TakeDamage(int damage)
    {
        if (this != null)
        {
            //damage -= armor.GetValue();
            //damage = Mathf.Clamp(damage, 0, int.MaxValue); //prevents negative damage values 

            currentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage.");

            OnHealthChanged?.Invoke(maxHealth, currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int healing)
    {
        if(currentHealth == maxHealth)
        {
            return;
        }
        currentHealth += Mathf.Clamp(healing, 1, maxHealth); //prevents excessive Health

        OnHealthChanged?.Invoke(maxHealth, currentHealth);
    }

    public void gainXP(int _xp) //Gain XP
    {
        xp += _xp;
    }

    public virtual void Die()
    {
        //Die in some way, should be overriden
        // Debug.Log(transform.name + " died.");
        
    }



}
