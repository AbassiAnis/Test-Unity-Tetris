using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour , IClickable
{

    public bool IsMoving { get {return _isCanMove ;} }

    public EnumCollection.BlockType currentType;

    public BlockGenerator blockGenerator { get; private set; }

    private List<GameObject> _currentBlocks = new List<GameObject>();

    private float _internalSpeedTimer = 0f;

    private Vector3 _rotationPointRelativeToWorld;

    private Vector2 _blockSizeOffset = new Vector2(1f, 1f);

    public static Action<GameObject[]> BlockLanded;

    private bool _isCanMove = true;


    // Start is called before the first frame update
    private void Start()
    {
        if (this.GetComponent<BlockGenerator>() != null && blockGenerator == null)
        {
            blockGenerator = this.GetComponent<BlockGenerator>();

            if (blockGenerator != null)
                _currentBlocks = blockGenerator.GenerateBlocks(currentType);
        }
        GameManager.GameEnded += StopMoving;

    }

    private void OnDestroy()
    {
        GameManager.GameEnded -= StopMoving;

    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isCanMove || GameManager.Instance.isGamePaused) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(new Vector3(-1, 0, 0));

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(new Vector3(1, 0, 0));

        if (Input.GetKeyDown(KeyCode.Space))
            Rotate();

        _internalSpeedTimer += 1 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            Move(new Vector3(0, -1, 0));
        else if (_internalSpeedTimer > BlocksSpawner.Instance.timeSpeedFactor)
        {
            _internalSpeedTimer = 0;
            Move(new Vector3(0, -1, 0));
        }
    }

    private void Move(Vector3 moveDirection)
    {
        var lastPosition = gameObject.transform.position;

        gameObject.transform.position += moveDirection;

        if (!IsInsideBounds())
        {
            gameObject.transform.position = lastPosition;
        }
    }

    private void Rotate()
    {
        transform.RotateAround(transform.TransformPoint(_rotationPointRelativeToWorld), new Vector3(0, 0, 1), 90);
        if (!IsInsideBounds())
            transform.RotateAround(transform.TransformPoint(_rotationPointRelativeToWorld), new Vector3(0, 0, 1), -90);
    }

    private bool IsInsideBounds()
    {
        var result = true;
        foreach (var block in _currentBlocks)
        {
            int roundexX = Mathf.RoundToInt(block.gameObject.transform.position.x);
            int roundexY = Mathf.RoundToInt(block.gameObject.transform.position.y);

            if (roundexX < 0 || roundexX >= BlocksSpawner.Instance.cellsWidth ||
                 roundexY < 0 || roundexY >= BlocksSpawner.Instance.cellsHeight)
            {
                if (roundexY < 0)
                    _isCanMove = false;

                result = false;
                break;
            }
        }

        if (!_isCanMove)
            BlockLanded?.Invoke(_currentBlocks.ToArray());

        return result;
    }

    private bool IsBlockPositioned()
    {
        bool result = false;

        return result;
    }

    private void StopMoving()
    {
        _isCanMove = true;
    }

    /*[ContextMenu("Update Type")]
    private void OnValidate()
    {
        if (blockGenerator != null)
            _currentBlocks = blockGenerator.GenerateBlocks(currentType);
    }*/

    public void OnEdit()
    {
        throw new NotImplementedException();
    }
}