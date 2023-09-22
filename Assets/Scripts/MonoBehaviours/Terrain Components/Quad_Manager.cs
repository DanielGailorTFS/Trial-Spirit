using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad_Manager : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject northQuad;
    public GameObject southQuad;
    public GameObject eastQuad;
    public GameObject westQuad;
    private string playerTag = "Player";
    private string creatureTag = "Creature";

    // Start is called before the first frame update
    void Start()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag(playerTag)) || (other.gameObject.CompareTag(creatureTag)))
        {
            GameObject[] allQuads = { northQuad, southQuad, eastQuad, westQuad };
            Debug.Log("Player or Creature entered trigger");
            foreach (GameObject quad in allQuads)
            {


                if (CanInstantiateHere(quad))
                {
                    Debug.Log("Can instantiate at " + northQuad.name);
                    //SpawnBaseBlock(quad.transform.position);
                }
                else
                {
                    Debug.Log("Cannot instantiate here");
                }
            }           
        }
    }

    bool CanInstantiateHere(GameObject quad)
    { 
        BoxCollider myCollider = quad.GetComponent<BoxCollider>();
        if (myCollider)
        { 
            Vector3 center = myCollider.bounds.center;
            Vector3 halfExtents = myCollider.bounds.extents;
            halfExtents.y = halfExtents.y * 2;
            Debug.Log("Center for " + quad.name + ": " + center);

            Collider[] Colliders = Physics.OverlapBox(center, halfExtents, Quaternion.identity, LayerMask.GetMask("Block"));
            foreach (Collider collider in Colliders)
            {
                Debug.Log(quad.name + " is colliding with " + collider.name);
            }

            return !Physics.CheckBox(center, halfExtents, Quaternion.identity, LayerMask.GetMask("Block"));
        }
        return false;
    }





}
