using UnityEngine;

namespace Battle
{
    public class BattleSpawnPoint : MonoBehaviour
    {
        public Character Spawn(Character character)
        {
            Character characterToSpawn = Instantiate<Character>(character, this.transform);
            return characterToSpawn;
        }
    }
}