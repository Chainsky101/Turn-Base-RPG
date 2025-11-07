using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class TeamManager : MonoBehaviour
    {
        // private static TeamManager instance;
        //
        // public static TeamManager Instance
        // {
        //     get
        //     {
        //         if (instance is null)
        //         {
        //             GameObject obj = new GameObject("Team Manager");
        //             instance = obj.AddComponent<TeamManager>();
        //         }
        //         return instance;
        //     }    
        // }
        public static TeamManager Instance { get; private set; }
        [Header("Team Setup")] public List<GameObject> playerTeamPrefabs = new();
        public int maxSize = 5;
        private string playDataPath = "/PlayData.json";      //easy to load and save
        private void Awake()
        {
            SetupSingleton();
            playDataPath = Application.persistentDataPath + playDataPath;
            LoadData();
            print(playDataPath);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                SaveData();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ClearArchive();
            }
        }

        public bool AddToTeam(GameObject characterPrefab)
        {
            if (CanAddToTeam())
            {
                playerTeamPrefabs.Add(characterPrefab);
                SaveData();
                return true;
            }
            return false;
        }

        public void RemoveFromTeam(GameObject characterPrefab)
        {
            playerTeamPrefabs.Remove(characterPrefab);
            SaveData();
        }
        private bool CanAddToTeam()
        {
            return playerTeamPrefabs.Count < maxSize;
        }
        private void SetupSingleton()
        {
            if (Instance is null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region DataPersistance

        private void LoadData()
        {
            if (File.Exists(playDataPath))
            {
                string jsonData = File.ReadAllText(playDataPath);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                foreach (var membersName in playerData.teamMembersName)
                {
                    GameObject obj = Resources.Load<GameObject>("Characters/" + membersName);
                    if (obj is not null)
                    {
                        playerTeamPrefabs.Add(obj);
                    }
                }
            
            }
        }

        private void SaveData()
        {
            PlayerData data = GetTeamMembersName();
            string names = JsonUtility.ToJson(data);
            File.WriteAllText(playDataPath,names);
        }

        private PlayerData GetTeamMembersName()
        {
            PlayerData data = new PlayerData();
            data.teamMembersName = new List<string>();
            foreach (var obj in playerTeamPrefabs)
            {
                data.teamMembersName.Add(obj.name);
            }

            return data;
        }

        private void ClearArchive()
        {
            File.Delete(playDataPath);
        }
        #endregion
    }
}
