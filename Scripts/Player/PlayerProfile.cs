using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace StrandedDefence.Player
{
    // Represents the player's profile, including currencies and tower data
    [Serializable]
    public class PlayerProfile
    {
        // Player Information
        public string playerName;
        public int highestWaveReached;


        public int primaryCurrency;
        public int secondaryCurrency;
        public int thirdCurrency;
        public int forthCurrency;

        // The player's MASTER tower (upgraded from the main menu)
        public TowerData masterTower;

        // Add more fields as needed, e.g.:
        // public List<UnlockedSkill> unlockedSkills;
        // public int highestWaveReached;
        // public string playerName;

        public PlayerProfile()
        {
            primaryCurrency = 0;
            secondaryCurrency = 0;
            thirdCurrency = 0;
            forthCurrency = 0;

            //Update UI



            masterTower = new TowerData();
        }
    }

}