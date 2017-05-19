using Ultimate;
using UnityEngine;

public class Floor : MonoBehaviour
{    
    public Transform target;
    public float distance;

    public GameObject platformPrefab;

    public Vector2 grid;    
    public float spacing;

    public int amount;

    public Platform[] platforms { private set; get; }

    private Vector3 detectPosition;

    private struct Tile
    {
        public Vector3 position;
        public Platform platform;

        public Tile(Vector3 position)
        {
            this.position = position;
            platform = null;
        }
    }

    private Tile[] tiles;

    public Transform followtarget;
    private float updateDelay = 0.1f;
    private float elpasedTime = 0f;

    private void Awake()
    {
        followtarget.position = GameManager.Instance.InitHipPosition;

        tiles = new Tile[(int)(grid.x * grid.y)];    
        
        UGL.contentsManager.CreateInstancePool(new InstancePool("Platforms", platformPrefab, amount));

        if (GameManager.Instance != null)
        {
            Vector3 newPos = GameManager.Instance.InitHipPosition;
            newPos.y = 0f;
            transform.position = newPos;
        }   

        for (int i = 0; i < grid.y; i++)
        {
            for (int j = 0; j < grid.x; j++)
            {
                float platfromXPos = (-1 * (grid.x / 2) + j) * spacing;
                float platfromZPos = (-1 * (grid.y / 2) + i) * spacing;

                tiles[(int)(i * grid.y) + j] = new Tile(new Vector3(transform.position.x + platfromXPos, 0f, transform.position.z + platfromZPos));
            }
        }
    }

    private void Update()
    {
        if (elpasedTime < updateDelay)
        {
            elpasedTime += UnityEngine.Time.deltaTime;
            return;
        }

        detectPosition = target.position;
        detectPosition.y = 0f;

        for (int i = 0; i < grid.y; i++)
        {
            for (int j = 0; j < grid.x; j++)
            {
                int index = (int) (i * grid.y) + j;

                if ((tiles[index].position - detectPosition).sqrMagnitude < distance * distance)
                {                    
                    if (!tiles[index].platform)
                    {
                        GameObject platform = UGL.contentsManager.CreateInstance("Platforms");
                        tiles[index].platform = platform.GetComponent<Platform>();
                        platform.transform.localPosition = new Vector3(tiles[index].position.x, tiles[index].platform.downYPos, tiles[index].position.z);

                        tiles[index].platform.index = index;
                        tiles[index].platform.downDelay = Random.Range(tiles[index].platform.downMinDelay, tiles[index].platform.downMaxDelay);
                    }


                    if(tiles[index].platform)
                    {                        
                        tiles[index].platform.isDown = false;
                        tiles[index].platform.elapsedTime = 0f;
                    }
                }
            }
        }
    }

    public void RemovePlatform(int index)
    {
        tiles[index].platform = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectPosition, distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.localPosition, new Vector3(spacing * (grid.x - 1), 0f, spacing * (grid.y - 1)));
    }
}
