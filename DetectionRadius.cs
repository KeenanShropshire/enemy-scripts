using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectionRadius : MonoBehaviour
{
    public bool DetectionArea; //whether the object has entered the area you set

    //set enemy to attack player character
    private void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "TRex")
        {
            DetectionArea = true;
            collide.gameObject.GetComponent<TRex>().radius = true;
            
        }

        if (collide.gameObject.tag == "Enemy")
        {
            DetectionArea = true;
            collide.gameObject.GetComponent<EnemyMove>().radius = true;
        }

    }

    //end enemy chasing and return to patrol
    private void OnTriggerExit(Collider collide)
    {
        if (collide.gameObject.tag == "TRex")
        {
            DetectionArea = false;
            collide.gameObject.GetComponent<TRex>().radius = false;
        }

        if (collide.gameObject.tag == "Enemy")
        {
            DetectionArea = false;
            collide.gameObject.GetComponent<EnemyMove>().radius = false;
        }
    }

}
