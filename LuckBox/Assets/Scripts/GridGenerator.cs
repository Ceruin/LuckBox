using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject baseGridCell;
    [SerializeField] int cellsToGenerate = 50;

    public List<GameObject> gridCells = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateGridCell();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGridCell()
    {
        /* THIS IS FOR TRYING TO MAKE SURE THAT WE CAN GENERATE A CELL IN A SPACE, AKA NOT INSIDE A WALL
        // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        */

        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < cellsToGenerate; i++) {        
            GameObject newGridCell = Instantiate<GameObject>(baseGridCell, position, Quaternion.identity);
            newGridCell.transform.parent = transform;
            gridCells.Add(newGridCell);
            position.x += 1;
        }
    }
}
