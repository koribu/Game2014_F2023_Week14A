using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    GameObject _onScreenControllers;

    [SerializeField]
    bool _testing;

    SoundManager _soundManager;
    // Start is called before the first frame update
    void Awake()
    {
        if(!_testing)
        {
            _onScreenControllers = GameObject.Find("ScreenController");
            _onScreenControllers.SetActive(Application.platform != RuntimePlatform.WindowsPlayer &&
                                           Application.platform != RuntimePlatform.WindowsEditor);
        }

        _soundManager = FindObjectOfType<SoundManager>();


    }

    void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0))
            _soundManager.PlayMusic(Sound.MAIN_MUSIC);
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneAt(1))
            _soundManager.PlayMusic(Sound.END_MUSIC);

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(0); //for game play scene
    }
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(1);
    }

}
