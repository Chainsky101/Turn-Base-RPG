using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance { get;private set; }
        [SerializeField] private BattleSpawnPoint[] spawnPoints;
        
        private void Start()
        {
            SetupSingleton();
            LaunchBattle();
        }

        private void SetupSingleton()
        {
            if (Instance is not null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void StartBattle(List<GameObject> playerPrefabs)
        {
            SpawnPlayerCharacters(playerPrefabs);
        }

        private void SpawnPlayerCharacters(List<GameObject> playerPrefabs)
        {
            for (int i = 0; i < playerPrefabs.Count; i++)
            {
                int spawnIndex = i;
                if (spawnPoints[spawnIndex] != null)
                {
                    GameObject obj = Instantiate(playerPrefabs[i], spawnPoints[i].transform.position,
                        Quaternion.identity);
                    Character playerCharacter = obj.GetComponent<Character>();
                }
            }
        }

        public void LaunchBattle()
        {
            BattleLauncher.Instance.Launch();
        }
        
    }
}