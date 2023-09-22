using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Terrain Object", menuName = "Terrain Object")]
public class Scriptable_Terrain : ScriptableObject
{
    public event Action OnBlockDestroyed;

    [SerializeField] private float blockHealthMax;
    [SerializeField] private float blockHealth;
    [SerializeField] private GameObject blockGenerator;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private bool isTriggered;

    public GameObject BlockPrefab => blockPrefab;
    public GameObject FloorPrefab => floorPrefab;
    public bool IsTriggered => isTriggered;

    public void Initialize()
    {
        blockHealth = blockHealthMax;
        isTriggered = false;
    }

    public void WallHit()
    {
        if (isTriggered == false)
        {
            isTriggered = true;
            FloorSpawn();
            DeductBlockHealth(20f);
        }
        else
        {
            DeductBlockHealth(20f);
        }
    }

    private void DeductBlockHealth(float amount)
    {
        blockHealth -= amount;
        if (blockHealth <= 0)
        {
            BlockDestroyed();
        }
    }

    public void BlockDestroyed()
    {
        OnBlockDestroyed?.Invoke();
    }

    private void FloorSpawn()
    {
        Vector3 position = blockPrefab.transform.position;
        Instantiate(floorPrefab, position, Quaternion.identity);
    }





}
