using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class BattleLauncher : MonoBehaviour
{
    public static BattleLauncher Instance { get; private set; }

    private void Awake()
    {
        SetupSingleton();
    }

    private void SetupSingleton()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }
    public void Launch()
    {
        
    }
}