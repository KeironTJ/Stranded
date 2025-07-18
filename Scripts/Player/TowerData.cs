using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace StrandedDefence.Player
{
    [Serializable]
    public class TowerData
    {
        public int level;
        public float damage;
        public float fireRate;
        public float range;

        // Add more attributes or skills as needed

        public TowerData()
        {
            level = 1;
            damage = 10f;
            fireRate = 1f;
            range = 5f;
        }

        // Optionally, add a method to clone/copy this data
        public TowerData Clone()
        {
            return (TowerData)this.MemberwiseClone();
        }
    }
    
}