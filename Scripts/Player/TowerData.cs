using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace StrandedDefence.Player
{
    [Serializable]
    public class TowerData
    {
        // Tower attributes
        public int level;
        public int towerXP;

        // Attack attributes
        public float baseDamage;
        public float baseFireRate;
        public float baseRange;
        public float rotationSpeed;

        // Defense attributes
        public float baseHealth;

        // Economy attributes
        public float primaryCurrencyPerKill;
        public float secondaryCurrencyPerKill;

        // Add more attributes or skills as needed

        public TowerData()
        {
            // Initialize Tower Attributes
            level = 1;
            towerXP = 0;

            // Initialize Attack Attributes
            baseDamage = 1f;
            baseFireRate = 1f;
            baseRange = 5f;
            rotationSpeed = 1f;

            // Initialize Defense Attributes
            baseHealth = 10f;

            // Initialize Economy Attributes
            primaryCurrencyPerKill = 1f;
            secondaryCurrencyPerKill = 0.1f;

        }

        // Optionally, add a method to clone/copy this data
        public TowerData Clone()
        {
            return (TowerData)this.MemberwiseClone();
        }
    }
    
}