using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocksSprites = new GameObject[9];

    [SerializeField]
    private BlocksDataSo blocksData = null;

    public List<GameObject> GenerateBlocks(EnumCollection.BlockType sentType)
    {
        ResetBlocks();

        List<GameObject> resultList = new List<GameObject>();

        if (blocksSprites != null && blocksData != null)
        {
            var tempBlockData = blocksData.GetBlockByType(sentType);

            if (tempBlockData == null)
            {
                Debug.LogError("Block Not Found In Data Base " + sentType);
                return null;
            }

            if (blocksSprites.Length != tempBlockData.blockStructure.Length)
                Debug.LogError("Block Structure is Incorrect" + sentType);

            for (int i = 0; i < blocksSprites.Length; i++)
            {
                blocksSprites[i].gameObject.SetActive(tempBlockData.blockStructure[i]);
                ChangeColor(sentType, tempBlockData.blocksColor);
                if (tempBlockData.blockStructure[i])
                    resultList.Add(blocksSprites[i]);
            }
        }

        return (resultList.Count > 0) ? resultList : null;
    }

    public void ChangeColor(EnumCollection.BlockType sentType, Color sentColor,bool isUpdateColorDataBase = false)
    {
        if (blocksSprites != null)
        {
            for (int i = 0; i < blocksSprites.Length; i++)
            {
                blocksSprites[i].GetComponent<SpriteRenderer>().color = sentColor;
            }
        }

        if (blocksData != null && isUpdateColorDataBase)
            blocksData.UpdateTypeColor(sentType, sentColor) ;
    }


    private void ResetBlocks()
    {
        if (blocksSprites != null)
        {
            foreach (var block in blocksSprites)
            {
                block.gameObject.SetActive(false);
            }
        }
    }
}