using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    int hp, maxhp;
    bool dead = false;
    public delegate void VoidEventHandler();
    public delegate void IntEventHandler(int amount);
    public event VoidEventHandler DeathEvent;
    public event VoidEventHandler ReviveEvent;
    public event IntEventHandler DamageEvent;
    public event IntEventHandler HealEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (maxhp < 1)
            maxhp = 1;
        if (hp < 1)
            hp = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if(hp<=0)
        {
            hp = 0;
        }
        DamageEvent(amount);
    }

    public void Heal(int amount)
    {
        hp += amount;
        HealEvent(amount);
    }

    public void Die()
    {
        if (!dead) 
        {
            dead = true;
            DeathEvent();
        }
    }

    public void Revive()
    {
        dead = false;
        ReviveEvent();
    }


}
