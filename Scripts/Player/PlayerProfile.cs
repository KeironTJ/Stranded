using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

namespace StrandedDefence.Player
{
    // Represents the player's profile, including currencies and tower data
    [Serializable]
    public class PlayerProfile
    {
        // Player Information
        public string uniqueID; // Unique identifier for the profile
        public DateTime creationDate; // Date when the profile was created
        public DateTime lastModified; // Date when the profile was last modified
        public string playerName;

        // Player Currencies
        public int primaryCurrency;
        public int secondaryCurrency;
        public int thirdCurrency;
        public int forthCurrency;

        // The player's MASTER tower (upgraded from the main menu)
        public TowerData masterTower;

        public PlayerProfile()
        {
            primaryCurrency = 0;
            secondaryCurrency = 0;
            thirdCurrency = 0;
            forthCurrency = 0;

            uniqueID = Guid.NewGuid().ToString();
            playerName = uniqueID; // Default to unique ID if no name is set
            creationDate = DateTime.Now;
            lastModified = DateTime.Now;

            masterTower = BuildTowerDataFromResources();
            // Do NOT reference PlayerProfileManager.CurrentProfile here!
        }

        public TowerData BuildTowerDataFromResources()
        {
            var towerData = new TowerData("Master Tower");

            var groupDefs = new Dictionary<string, string>
            {
                { "Attack", "AttributesSO/Attack" },
                { "Defence", "AttributesSO/Defence" },
                { "Economy", "AttributesSO/Economy" }
            };

            foreach (var kvp in groupDefs)
            {
                TowerAttribute[] attributes = Resources.LoadAll<TowerAttribute>(kvp.Value);
                foreach (var attr in attributes)
                {
                    var attributeState = new AttributeState(attr);
                    towerData.AddAttributeToGroup(kvp.Key, attributeState);
                }
            }

            return towerData;
        }

        public void UpgradeTowerDataToLatest(TowerData towerData)
        {
            var groupDefs = new Dictionary<string, string>
            {
                { "Attack", "AttributesSO/Attack" },
                { "Defence", "AttributesSO/Defence" },
                { "Economy", "AttributesSO/Economy" }
            };

            foreach (var kvp in groupDefs)
            {
                TowerAttribute[] attributes = Resources.LoadAll<TowerAttribute>(kvp.Value);
                foreach (var attr in attributes)
                {
                    var attributeState = new AttributeState(attr);
                    towerData.AddAttributeToGroup(kvp.Key, attributeState);
                }
            }
        }
    }

}