using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using Unity.AI.Navigation;


public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<NavMeshSurface>())
        {
            Debug.Log("I have nav mesh");
        }
        else
        {

            Debug.LogFormat("no nav mesh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
