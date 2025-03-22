using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WFCNode", menuName = "WFC/Node")]
[System.Serializable]
public class WFCNode : ScriptableObject
{
    public string name;
    public GameObject prefab;

    [Header("Identical valid neighbors in all directions\n(Overrules individual direction rules)")]
    [SerializeField] bool omnidirectionalRules = false;
    public WFC_Connection All;

    [Header("Valid neighbors in individual directions")]
    public WFC_Connection Top;
    public WFC_Connection Bottom;
    public WFC_Connection Left;
    public WFC_Connection Right;

    [Header("Relative weight of node")]
    public float Weight = 1.0f;

    void OnValidate()
    {
        if(omnidirectionalRules)
        {
            Top = All;
            Bottom = All;
            Left = All;
            Right = All;
        }
    }
}

[System.Serializable]
public class WFC_Connection
{
    public List<WFCNode> CompatibleNodes = new List<WFCNode>();
}