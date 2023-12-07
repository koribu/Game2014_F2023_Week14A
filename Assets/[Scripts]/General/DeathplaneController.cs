using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathplaneController : MonoBehaviour
{
    Vector3 _spawnPoint;

    SoundManager _soundManager;
    LifeCounterManager _lifeCounterManager;

    // Start is called before the first frame update
    void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
        _lifeCounterManager = FindObjectOfType<LifeCounterManager>();
        _spawnPoint = new Vector3(0, 3, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("I fallen");
            _lifeCounterManager.LoseLife();
            collision.gameObject.transform.position = _spawnPoint;
            _soundManager.PlaySound(Channel.PLAYER_DEATH_SFX, Sound.DEATH);

           

        }
    }

    public void SpawnPlayerToCheckPoint()
    {
        FindObjectOfType<PlayerBehavior>().transform.position= _spawnPoint;
    }

    public void UpdateSpawnPoint(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }
}
