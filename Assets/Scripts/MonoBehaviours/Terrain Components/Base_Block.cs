using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Block : MonoBehaviour
{
    public GameObject wallGenerator;
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
}
