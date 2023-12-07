using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarController : MonoBehaviour
{
    float _health;
    float _maxHealth;
    public Slider _slider;

    LifeCounterManager _lifeCounterManager;
    // Start is called before the first frame update
    void Start()
    {
        _lifeCounterManager = FindObjectOfType<LifeCounterManager>();

        _slider = GetComponentInChildren<Slider>();
        _maxHealth = _slider.maxValue;
       _health = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamage(float damage)
    {
        _health -= damage;
        UpdateHealthBar();
        if(_health <= 0 )
        {
            //You Are Death
            _lifeCounterManager.LoseLife();
            FindObjectOfType<DeathplaneController>().SpawnPlayerToCheckPoint();

            ResetHealthBar();
        }


    }

    void UpdateHealthBar()
    {
        _slider.value = _health;
    }

    void ResetHealthBar()
    {
        _health = _maxHealth;
        UpdateHealthBar();
    }
}
