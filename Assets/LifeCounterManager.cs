using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCounterManager : MonoBehaviour
{
    int _maxLife = 4;
    public int _currentLife;
    Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _currentLife = _maxLife;
    }

    public void LoseLife()
    {
        Debug.LogError("LoseLife");
        _currentLife--;

        if( _currentLife <= 0 )
        {
            //Switch scene to end scene
            SceneManager.LoadScene(1);
        }

        UpdateLifeCounterUI();
    }

    public void GainLife(int life)
    {
        _currentLife += life;

        if( _currentLife > _maxLife )
        {
            _currentLife = _maxLife;
        }

        UpdateLifeCounterUI();
    }

    public void ResetLife()
    {
        _currentLife = _maxLife;
        UpdateLifeCounterUI();
    }

    void UpdateLifeCounterUI()
    {
        _image.sprite = Resources.Load<Sprite>($"Sprites/LifeCounter/hud-{_currentLife}");
    }
}
