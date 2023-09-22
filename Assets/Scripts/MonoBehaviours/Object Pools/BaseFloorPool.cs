using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFloorPool : MonoBehaviour
{
    public GameObject baseFloorPrefab;
    public int poolSize = 100;

    private Queue<GameObject> floors;

    // Start is called before the first frame update
    void Start()
    {
        floors = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject floor = Instantiate(baseFloorPrefab);
            floor.SetActive(false);
            floors.Enqueue(floor);
        }
    }

    public GameObject GetFloor()
    {
        if (floors.Count > 0)
        {
            GameObject floor = floors.Dequeue();
            floor.SetActive(true);
            return floor;
        }
        else
        {
            return Instantiate(baseFloorPrefab);
        }
    }

    public void ReturnFloor(GameObject floor)
    {
        floor.SetActive(false);
        floors.Enqueue(floor);
    }
}
