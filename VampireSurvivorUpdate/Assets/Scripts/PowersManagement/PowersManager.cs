using UnityEngine;

namespace PowersManagement
{
    public class PowersManager : MonoBehaviour
    {
        public static PowersManager instance;
        //public PlayerStats playerStats;
        private GameObject player;
        private Power[] powers;


        private Power[] getPowers()
        {
            return powers;
        }

        private bool hasPower(Power power)
        {
            foreach (Power p in powers)
            {
                if (p == power)
                {
                    return true;
                }
            }
            return false;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameObject getPlayer()
        {
            return player;
        }
    }
}
