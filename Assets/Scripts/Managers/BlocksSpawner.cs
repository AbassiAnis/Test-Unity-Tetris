using UnityEngine;

public class BlocksSpawner : MonoBehaviour
{
    public float cellsWidth;
    public float cellsHeight;

    public float timeSpeedFactor;

    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private Transform spanwPosition;

    private Transform[,] blockGrid = new Transform[10, 20];

    public static BlocksSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("There is more the once instance of Block Spawner");
            return;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        BlockController.BlockLanded += AddToGird;
        GameManager.GameStarted += SpawnNewBlock;
    }

    private void OnDestroy()
    {
        BlockController.BlockLanded -= AddToGird;
        GameManager.GameStarted -= SpawnNewBlock;
    }

    private void AddToGird(GameObject[] blockList)
    {
        /*foreach (var block in blockList)
        {
            int roundexX = Mathf.RoundToInt(block.gameObject.transform.position.x);
            int roundexY = Mathf.RoundToInt(block.gameObject.transform.position.y);

            blockGrid[roundexX, roundexY] = block.transform;
        }*/

        SpawnNewBlock();
    }

    private void SpawnNewBlock()
    {
        Instantiate(
            blockPrefab,
            spanwPosition.position,
            Quaternion.identity
            );
    }
}