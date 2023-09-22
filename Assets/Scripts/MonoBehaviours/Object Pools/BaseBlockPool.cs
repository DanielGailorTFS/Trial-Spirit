using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class BaseBlockPool : MonoBehaviour
{
    public GameObject baseBlockPrefab;
    public int poolSize = 100;

    private Queue<GameObject> blocks;

    // Start is called before the first frame update
    void Start()
    {
        blocks = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject block = Instantiate(baseBlockPrefab);
            block.SetActive(false);
            blocks.Enqueue(block);
        }
    }

    public GameObject GetBlock()
    {
        if (blocks.Count > 0)
        {
            GameObject block = blocks.Dequeue();
            block.SetActive(true);
            return block;
        }
        else
        {
            return Instantiate(baseBlockPrefab);
        }
    }

    public void ReturnBlock(GameObject block)
    {
        block.SetActive(false);
        blocks.Enqueue(block);
    }
}
