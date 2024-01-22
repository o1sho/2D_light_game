using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] private int _vitality; //������ �� ���-�� ��  
    [SerializeField] private int _stamina; //������ �� ���-�� ������� � � �����.  
    [SerializeField] private int _strength; //������ �� ���� �������.  
    [SerializeField] private int _ammunition; //����. ���-�� ���� � �������.

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
    public int GetStrength()
    {
        return _strength;
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
    public void SetStrength(int count)
    {
        _strength += count;
    }
    public void SetAmmunition(int count)
    {
        _ammunition += count;
    }

}
