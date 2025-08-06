using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attribute")]
public class TowerAttribute : ScriptableObject
{
    public string attributeName;
    public float baseValue;

    // Workshop Attributes - MAIN MENU UPGRADES
    public bool unlocked;
    public int level;
    public int maxLevel;
    public float bonusPerLevel;
    public float basePrimaryCost; // MAIN MENU UPGRADE COST
    public float baseSecondaryCost; // INROUND UPGRADE COST

    // Research Attributes
    public bool researchUnlocked;
    public int researchLevel;
    public int maxResearchLevel;
    public float baseResearchCost; 
    
}