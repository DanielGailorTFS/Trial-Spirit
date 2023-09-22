using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SC_BaseBlockManager : MonoBehaviour
{
    public GameObject wallGenerator;
    public GameObject baseFloor;
    public GameObject baseBlock;
    bool triggered = false;
    float currentHealth = 100f;
    float blockHealthMax = 100f;
    Animator anim;





    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        currentHealth = blockHealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            WallDestroyed();
        }
    }

    public void WallHit()
    {
        if (triggered == false)
        {
            triggered = true;
            FloorSpawn();
            BlockCheck();
            currentHealth -= 20f;
        }
        else
        {
            currentHealth -= 20f;
        }
    }

    void WallDestroyed()
    {
        anim.SetBool("isDestroyed", true);
        Destroy(gameObject);
    }

    void BlockCheck()
    {
        if (triggered)
        {
            GameObject generator = Instantiate(wallGenerator, transform.position, transform.rotation);
            generator.transform.parent = gameObject.transform;
        }
    }

    void FloorSpawn()
    {
        Vector3 position = gameObject.transform.position;
        //SC_GameManager.GMInstance.SpawnBaseFloor(position);
    }
}
