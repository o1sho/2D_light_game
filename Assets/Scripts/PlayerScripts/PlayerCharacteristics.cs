using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacteristics : MonoBehaviour
{
    [SerializeField] private int _vitality; //������ �� ���-�� ��  
    [SerializeField] private int _stamina; //������ �� ���-�� ������� � � �����.  
    [SerializeField] private int _mobility; //������ �� ����������� ����� (dash) � ����. ������ ������.  
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
