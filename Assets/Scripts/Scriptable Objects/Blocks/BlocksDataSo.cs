using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BlocksDataSo", menuName = "Tetris/BlocksDataSo", order = 1)]
public class BlocksDataSo : ScriptableObject
{
    [SerializeField]
    private List<BlockData> blocksList = new List<BlockData>();

    public BlockData GetBlockByType(EnumCollection.BlockType sentType)
    {
        BlockData result = null;

        if (blocksList != null)
        {
            foreach (var block in blocksList)
            {
                if (block.type == sentType)
                {
                    result = block;
                    break;
                }
            }
        }
        return result;
    }

    public void UpdateTypeColor(EnumCollection.BlockType sentType,Color sentColor)
    {
        if (blocksList != null)
        {
            foreach (var block in blocksList)
            {
                if (block.type == sentType)
                {
                    block.blocksColor = sentColor;
                    break;
                }
            }
        }
    }
}