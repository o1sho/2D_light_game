using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] private int _vitality; //Влияет на кол-во хп  
    [SerializeField] private int _stamina; //Влияет на кол-во стамины и её реген.  
    [SerializeField] private int _mobility; //Влияет на перезарядку рывка (dash) и макс. высоту прыжка.  
    [SerializeField] private int _ammunition; //Макс. кол-во пуль в кармане.

    private void Start()
    {

    }

    public int GetVitality()
    {
        return _vitality;
    }
    public int GetStamina()
    {
        return _stamina;
    }
    public int GetMobility()
    {
        return _mobility;
    }
    public int GetAmmunition()
    {
        return _ammunition;
    }

    public void SetVitality(int count)
    {
        _vitality += count;
    }
    public void SetStamina(int count)
    {
        _stamina += count;
    }
    public void SetMobility(int count)
    {
        _mobility += count;
    }
    public void SetAmmunition(int count)
    {
        _ammunition += count;
    }

}
