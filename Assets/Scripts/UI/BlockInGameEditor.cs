using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInGameEditor : MonoBehaviour
{

    [SerializeField]
    private GameObject editPanel;
    [SerializeField]
    private GameObject inGameHeaderPanel;

    private bool _isEditMenuOpen = false;

    private bool _isCanEdit = false;

    private BlockController _currentBlock = null;

    void Start()
    {
        GameManager.GameStarted += () => _isCanEdit = true;
        GameManager.GameEnded += () => _isCanEdit = false;
    }

    private void OnDestroy()
    {
        GameManager.GameStarted -= () => _isCanEdit = true;
        GameManager.GameEnded -= () => _isCanEdit = false;
    }

    private void Update()
    {
        if (!_isCanEdit) return;

        if(Input.GetKeyDown(KeyCode.R))
        {
            _isEditMenuOpen = !_isEditMenuOpen;
            GameManager.Instance.isGamePaused = _isEditMenuOpen;
            ShowEditMenu(_isEditMenuOpen);
        }

        if (Input.GetMouseButtonDown(0) && _isEditMenuOpen)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider == null) return;
            
            if (hit.collider.GetComponent<BlockController>() != null )
            {
                if (hit.collider.GetComponent<BlockController>().IsMoving)
                {   //Do your thing.
                    _currentBlock = null;
                    _currentBlock = hit.collider.GetComponent<BlockController>();
                }
            }
            
        }
    }


    private void ShowEditMenu(bool isShow)
    {
        editPanel.SetActive(isShow);
        inGameHeaderPanel.SetActive(!isShow);
    }


    public void SetColor(int colorID)
    {
        if (_currentBlock == null) return;

        Color tempColor = Color.white;

        switch (colorID)
        {
            case 0:
                tempColor = Color.white;
                break;
            case 1:
                tempColor = Color.black;
                break;
            case 2:
                tempColor = Color.yellow;
                break;
            case 3:
                tempColor = new Color32(128, 0, 128, 255);
                break;
            case 4:
                tempColor = Color.red;
                break;
            case 5:
                tempColor = Color.gray;
                break;
            case 6:
                tempColor = Color.blue;
                break;
            case 7:
                tempColor = Color.green;
                break;
            default:
                tempColor = Color.white;
                break;
        }

     _currentBlock.blockGenerator.ChangeColor(_currentBlock.currentType,tempColor,true);
    }


    public void SetShape(int shapeID)
    {
        if (_currentBlock == null) return;

        EnumCollection.BlockType tempShape = EnumCollection.BlockType.I_Block;

        switch (shapeID)
        {
            case 0:
                tempShape = EnumCollection.BlockType.I_Block;
                break;
            case 1:
                tempShape = EnumCollection.BlockType.O_Block;
                break;
            case 2:
                tempShape = EnumCollection.BlockType.L_Block;
                break;
            case 3:
                tempShape = EnumCollection.BlockType.T_Block;
                break;
            case 4:
                tempShape = EnumCollection.BlockType.Z_Block;
                break;
            default:
                tempShape = EnumCollection.BlockType.I_Block;
                break;
        }

        _currentBlock.blockGenerator.GenerateBlocks(tempShape);
        _currentBlock.currentType = tempShape;
    }
}
