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
            SpawnTile(tileList[i]);
            tileQueue.Enqueue(tileList[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTile(GameObject tile)
    {
        Instantiate(tile,transform.forward*distance,transform.rotation);
        distance += tileLength;
    }

    private void moveTile()
    {
        GameObject tile = tileQueue.Dequeue();
        tile.transform.position = transform.forward * distance;
        distance += tileLength;
        tile.transform.rotation = transform.rotation;
        tileQueue.Enqueue(tile);
    }

}
