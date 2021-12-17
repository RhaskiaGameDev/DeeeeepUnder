using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float speed;
    public float amount;
    Vector2 startingPos;

    float time;
    float totalTime;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            startingPos.y = transform.localPosition.y;
            time = Time.time;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            totalTime += (Time.time - time);

            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, startingPos.y + (Mathf.Sin(totalTime * speed) * amount));

            time = Time.time;
        }

    }
}
