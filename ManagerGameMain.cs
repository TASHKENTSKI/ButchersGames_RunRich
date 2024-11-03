using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ManagerGameMain : MonoBehaviour
{
    [Header("Main Settings")]
    public short money = 10;
    public short banknote = 10;
    public short bottle = 15;
    public short party = 25;
    public short school = 25;
    public short robber = 20;

    public string playerStatus;
    
    private ParticleSystem _particleSystem1;
    [SerializeField]private ParticleSystem _moneyParticle;
    
    public List<GameObject> mainHeroSkins;
    
    
    private HealthBarHero _healthBarB;
    
    public TextMeshPro textMeshProPlayerStatus;

    private bool accessParticle1 = true;
    private bool accessParticle2 = true;
    private bool accessParticle3 = true;
    private bool accessParticle4 = true;

    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        

    }

    private void Start()
    {
        
        
        GameObject particleSystem1 = GameObject.Find("player");
        _particleSystem1 = particleSystem1.GetComponent<ParticleSystem>();
        
        GameObject moneyParticle = GameObject.Find("MoneySpawn");
        _moneyParticle = moneyParticle.GetComponent<ParticleSystem>();
               
        GameObject healthBar = GameObject.Find("Health BAR");
        _healthBarB = healthBar.GetComponent<HealthBarHero>();
        
        GameObject textMeshProObject = GameObject.Find("PLAYER STATUS");
        textMeshProPlayerStatus = textMeshProObject.GetComponent<TextMeshPro>();
        
        Checker();
    }

    public void Checker()
    {
        if (money > 0 && money < 30)
        {
            playerStatus = "HOBO";
            textMeshProPlayerStatus.text = playerStatus;
            textMeshProPlayerStatus.color = new Color(1f, 0.5f, 0f);
            _healthBarB.healthBarImage.color = new Color(1f, 0.5f, 0f);
            mainHeroSkins[0].SetActive(true);
            mainHeroSkins[3].SetActive(false);
            mainHeroSkins[2].SetActive(false);
            mainHeroSkins[1].SetActive(false);
            mainHeroSkins[4].SetActive(false);
        }
        else if(money > 30 && money < 80)
        {
            if (accessParticle1)
            {
                _particleSystem1.Play();
                accessParticle1 = false;
            }
            
            playerStatus = "POOR";
            textMeshProPlayerStatus.text = playerStatus;
            textMeshProPlayerStatus.color = new Color(1f, 1f, 0f);
            _healthBarB.healthBarImage.color = new Color(1f, 1f, 0f);
            mainHeroSkins[1].SetActive(true);
            mainHeroSkins[3].SetActive(false);
            mainHeroSkins[2].SetActive(false);
            mainHeroSkins[4].SetActive(false);
            mainHeroSkins[0].SetActive(false);
            
        }
        else if(money > 80 && money < 120)
        {
            
            playerStatus = "DECENT";
            textMeshProPlayerStatus.text = playerStatus;
            if (accessParticle2)
            {
                _particleSystem1.Play();
                accessParticle2 = false;
            }
            
            _healthBarB.healthBarImage.color = new Color(0f, 1f, 0f);
            textMeshProPlayerStatus.color = new Color(0f, 1f, 0f);
            mainHeroSkins[2].SetActive(true);
            mainHeroSkins[3].SetActive(false);
            mainHeroSkins[4].SetActive(false);
            mainHeroSkins[1].SetActive(false);
            mainHeroSkins[0].SetActive(false);
        }
        else if(money > 120 && money < 200)
        {
            if (accessParticle3)
            {
                _particleSystem1.Play();
                accessParticle3 = false;
            }
            playerStatus = "RICH";
            textMeshProPlayerStatus.text = playerStatus;
            textMeshProPlayerStatus.color = new Color(0.5f, 1f, 1f);
            _healthBarB.healthBarImage.color = new Color(0.5f, 1f, 1f);
            mainHeroSkins[3].SetActive(true);
            mainHeroSkins[4].SetActive(false);
            mainHeroSkins[2].SetActive(false);
            mainHeroSkins[1].SetActive(false);
            mainHeroSkins[0].SetActive(false);
        }
        else if(money > 200)
        {
            if (money >= 200)
            {
                money = 200;
            }
            playerStatus = "MILLIONARE";
            textMeshProPlayerStatus.text = playerStatus;
            textMeshProPlayerStatus.color = new Color(1f, 0f, 1f);
            textMeshProPlayerStatus.fontSize = 1.43f;
            if (accessParticle4)
            {
                _particleSystem1.Play();
                accessParticle4 = false;
            }
            _healthBarB.healthBarImage.color = new Color(1f, 0f, 1f);
            mainHeroSkins[4].SetActive(true);
            mainHeroSkins[3].SetActive(false);
            mainHeroSkins[2].SetActive(false);
            mainHeroSkins[1].SetActive(false);
            mainHeroSkins[0].SetActive(false);
        }

        

    }

    public short SetMoneyZero()
    {
        money = 10;
        return money;
    }
    public short MoneyBottle()
    {
        
        money -= bottle;
        return money;
    }
    public short MoneyBanknote()
    {
        _moneyParticle.Play();
        money += banknote;
        return money;
    }
    public short MoneyParty()
    {
        money -= party;
        return money;
    }
    public short MoneySchool()
    {
        money += school;
        return money;
    }
    public short MoneyRobber()
    {
        money -= robber;
        return money;
    }
}
