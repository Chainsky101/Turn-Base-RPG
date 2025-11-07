using System;
using UnityEngine;

namespace Player
{
    public class RecruitableNPC : MonoBehaviour
    {
        public GameObject prefab;
        private bool isPlayerNearby;
        private bool hasJoined;

        // Update is called once per frame
        void Update()
        {
            if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
            {
                TryRecruit();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNearby = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNearby = false;
            }
        }

        private void TryRecruit()
        {
            if (!hasJoined)
            {
                bool success = TeamManager.Instance.AddToTeam(prefab);
                if (success)
                {
                    hasJoined = true;
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Player's team is full.");
                }
            }
        }
    }
}
