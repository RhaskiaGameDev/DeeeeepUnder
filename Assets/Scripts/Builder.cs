using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    public GameObject[] buildings;
    public GameObject[] buildingsH;
    public GameObject hologram;
    int rotation = 0;
    bool building;

    Quaternion rotationQ;
    Vector3 position;

    public float round;
    public float height;

    List<GameObject> built = new List<GameObject>();

    int selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        HandleBuildings();

        if (Input.GetKeyDown(KeyCode.R)) rotation++;
        if (rotation == 4) rotation = 0;

        if (Input.GetKeyDown(KeyCode.B))
            building = !building;

        if (hologram.GetComponent<Structure>()._3X3)
        {
            hologram.SetActive(building && !Physics.CheckBox(hologram.transform.position, new Vector3(3.5f * 1.5f, 1, 3.5f * 1.5f)));
        }
        else
        {
            hologram.SetActive(building && !Physics.CheckBox(hologram.transform.position, new Vector3(3.5f * 0.5f, 1, 3.5f * 0.5f)));
        }


        switch (rotation)
        {
            case 0:
                rotationQ = Quaternion.Euler(new Vector3(0, 0, 0));
                break;

            case 1:
                rotationQ = Quaternion.Euler(new Vector3(0, 90, 0));
                break;

            case 2:
                rotationQ = Quaternion.Euler(new Vector3(0, 180, 0));
                break;

            case 3:
                rotationQ = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
        }


        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 10);
        position = new Vector3(Mathf.Round(hit.point.x / round) * round, height, Mathf.Round(hit.point.z / round) * round);

        hologram.transform.position = position;
        hologram.transform.rotation = rotationQ;

        if (building && Input.GetMouseButtonDown(0) && !Physics.CheckBox(hologram.transform.position, new Vector3(3.5f * 0.5f, 1, 3.5f * 0.5f)))
        {
            GameObject structure = Instantiate(buildings[selected], position, rotationQ);
            built.Add(structure);
        }
    }

    void HandleBuildings()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selected = 0;
            Destroy(hologram);
            hologram = Instantiate(buildingsH[0], position, rotationQ);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selected = 1;
            Destroy(hologram);
            hologram = Instantiate(buildingsH[1], position, rotationQ);
        }
    }
}
