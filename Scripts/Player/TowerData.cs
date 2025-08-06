using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace StrandedDefence.Player
{
    [Serializable]
    public class AttributeState
    {
        [SerializeField] private TowerAttribute attribute; // Reference to ScriptableObject asset
        [SerializeField] private int level;
        [SerializeField] private int inRoundLevel;

        public TowerAttribute Attribute => attribute;
        public int Level => level;
        public int InRoundLevel => inRoundLevel;

        public AttributeState(TowerAttribute attribute, int level = 0, int inRoundLevel = 0)
        {
            this.attribute = attribute;
            this.level = level;
            this.inRoundLevel = inRoundLevel;
        }
    }

    [Serializable]
    public class AttributeGroup
    {
        [SerializeField] private string groupName;
        [SerializeField] private List<AttributeState> attributes = new List<AttributeState>();

        public string GroupName => groupName;
        public IReadOnlyList<AttributeState> Attributes => attributes;

        public AttributeGroup(string groupName)
        {
            this.groupName = groupName;
        }

        public void AddAttribute(AttributeState attributeState)
        {
            if (!attributes.Any(a => a.Attribute == attributeState.Attribute))
            {
                attributes.Add(attributeState);
            }
        }
    }

    [Serializable]
    public class TowerData
    {
        [SerializeField] private string name;
        [SerializeField] private List<AttributeGroup> groups = new List<AttributeGroup>();

        public string Name => name;
        public IReadOnlyList<AttributeGroup> Groups => groups;

        public TowerData(string name)
        {
            this.name = name;
        }

        public AttributeGroup GetOrCreateGroup(string groupName)
        {
            var group = groups.FirstOrDefault(g => g.GroupName == groupName);
            if (group == null)
            {
                group = new AttributeGroup(groupName);
                groups.Add(group);
            }
            return group;
        }

        public void AddAttributeToGroup(string groupName, AttributeState attributeState)
        {
            var group = GetOrCreateGroup(groupName);
            group.AddAttribute(attributeState);
        }
    }
}