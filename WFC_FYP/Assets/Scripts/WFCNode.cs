using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WFCNode", menuName = "WFC/Node")]
[System.Serializable]
public class WFCNode : ScriptableObject
{
    public string name;
    public GameObject prefab;
    public WFC_Connection Top;
    public WFC_Connection Bottom;
    public WFC_Connection Left;
    public WFC_Connection Right;
    public float Weight = 1.0f;
    public float biasWeight = 1.0f; //The weight for the odds that at tile will match its neighbor. Gets updated in WFCBuilder

    private void Awake()
    {
        Weight = biasWeight; // Initialize biasWeight with the starting Weight
    }
}


[System.Serializable]
public class WFC_Connection
{
    public List<WFCNode> CompatibleNodes = new List<WFCNode>();
}