using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //contains all the tiles

    [SerializeField] private List<GameObject> tileList;

    private Queue<GameObject> tileQueue = new Queue<GameObject>();
    [SerializeField] private GameObject player;

    private float distance;
    [SerializeField]private float tileLength;

    
    void Start()
    {
        for(int i=0;i<tileList.Count;i++)
        {
            if (i==0)
            {
                SpawnTile(tileList[0]);
            }
            else
            {
                SpawnTile(tileList[i]);
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 500)
        {
            if (player.transform.position.z > distance - 290)
            {
                moveTile();
                moveTile();
            }
        }

    }

    private void SpawnTile(GameObject tile)
    {
        GameObject tileObject = Instantiate(tile,transform.forward*distance,transform.rotation);
        tileQueue.Enqueue(tileObject);
        distance += tileLength;
    }

    private void moveTile()
    {
        GameObject tile = tileQueue.Dequeue();
        Debug.Log($"{tile.name}");
        tile.transform.position = transform.forward * distance;
        distance += tileLength;
        tile.transform.rotation = transform.rotation;
        tileQueue.Enqueue(tile);
    }

}
