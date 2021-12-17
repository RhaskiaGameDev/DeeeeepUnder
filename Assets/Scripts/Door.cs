using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    PlayerMovement plr;
    public Transform move;
    public Transform moveB;

    private void Awake()
    {
        plr = FindObjectOfType<PlayerMovement>();
    }

    private void OnMouseDown()
    {
        if (plr.underWater)
        {
            plr.underWater = false;
            plr.transform.position = move.position;
        }
        else
        {
            plr.underWater = true;
            plr.transform.position = moveB.position;
        }
    }
}
