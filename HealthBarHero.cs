using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHero : MonoBehaviour
{
    public Image healthBarImage; 

    public short maxHealth = 200;
    public short currentHealth;

    private ManagerGameMain _gameManagerB;

    private void Start()
    {
        GameObject managerObject = GameObject.Find("ManagerOfGAme");
        _gameManagerB = managerObject.GetComponent<ManagerGameMain>();
        
        currentHealth = 0;
    }

    public short ChangeBanknote()
    {
        currentHealth += (short)_gameManagerB.banknote;
        HealthBarStatus();
        return currentHealth;
    }
    public short ChangeBanknoteSchool()
    {
        currentHealth += (short)_gameManagerB.banknote;
        HealthBarStatus();
        return currentHealth;
    }
    public short ChangeHealthBarMinusBottle()
    {
        
        currentHealth -= (short)_gameManagerB.bottle;
        HealthBarStatus();
        return currentHealth;
        
    }
    public short ChangeHealthBarMinusParty()
    {
        
        currentHealth -= (short)_gameManagerB.party;
        HealthBarStatus();
        return currentHealth;
        
    }
    
    
    public void HealthBarStatus()
    {
        currentHealth = (short)Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBarImage.fillAmount = (float)currentHealth / 200; //надо преобразовать на десятичную дроюь, что бы играться с fill amount
    }
}