using UnityEngine;

[System.Serializable]
public class BlockData
{
    public string ID;
    public EnumCollection.BlockType type;
    private const int SIZE = 5;
    public bool[] blockStructure = new bool[SIZE];
    public Color blocksColor = Color.white;
}