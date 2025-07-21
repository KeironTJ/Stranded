using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Attribute")]
public class TowerAttribute : ScriptableObject
{
    public string attributeName;
    public float baseValue;

    public int workshopLevel;
    public int maxWorkshopLevel;
    public float workshopBonusPerLevel;
    public float baseWorkshopCost;
    public float workshopCostMultiplier;

    public int researchLevel;
    public int maxResearchLevel;
    public float researchMultiplierPerLevel;
    public float baseResearchCost;
    public float researchCostMultiplier;
}