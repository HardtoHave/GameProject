using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    //player's transform object
    public Transform player;
    //setup switch to detect player positon
    bool m_IsPlayerInRange;
    //declare game ending object
    public GameEnding gameEnding;

    //trigger event, after player get into the view area, change switch value
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }
    //player leave view area
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //when trigger is on
        if (m_IsPlayerInRange)
        {
            //set vector of ray direction
            Vector3 direction = player.position - transform.position + Vector3.up;
            //create ray
            Ray ray = new Ray(transform.position, direction);

            //ray hit object
            RaycastHit raycastHit;
            //using physics system to launch ray
            if(Physics.Raycast(ray,out raycastHit))
            {
                //if catch the player
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
