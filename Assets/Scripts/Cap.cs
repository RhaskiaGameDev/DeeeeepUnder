using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : MonoBehaviour
{
    public GameObject check;
    public GameObject capO;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool cap = true;
        foreach (var col in Physics.OverlapBox(check.transform.position, new Vector3(0.5f, 1, 0.5f)))
        {
            if (col.CompareTag("cap") && col.gameObject != gameObject && col.gameObject != check && col.gameObject != capO)
            {
                cap = false;
            }
        }

        capO.SetActive(cap);
    }
}
