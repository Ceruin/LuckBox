using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //  ---------------------------------------
    //                      TODO
    //  Make grid spacing follow size
    //  Customize Center
    //  Clean Code
    //  ---------------------------------------

    [SerializeField] GameObject baseGridCell;
    [SerializeField] int generationRadius = 5;
    [SerializeField] bool makeGrid = false;

    public List<GameObject> gridCells = new List<GameObject>();

    public class BoundSize
    {
        public Vector3 size { get; set; }

        public BoundSize(Vector3 size)
        {
            this.size = size;
        }
    }

    BoundSize boundSize = new BoundSize(new Vector3(1,1,1));

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGrid)
        {
            if (gridCells.Count <= 0)
            {
                NewGenGrid(); // infinite loop lmao
            }
        }
        else
        {          
            for (int i = 0; i < gridCells.Count; i++)
            {
                Destroy(gridCells[i]);fff
            }
            gridCells.Clear();
        }
    }

    private void NewGenGrid()
    {
        baseGridCell.SetActive(true);
        int totalHeight = generationRadius * 2 - 1; // how far out I need to generate
        Vector3 centerPoint = Vector3.zero; // set the center to gen from
                                            // from the center we start creating the positive coords, 4 corners of coords x,y -x,-y -x,y x,-y
        List<Vector3> fullCords = GenCoords(centerPoint);
        List<Vector3> mergeList = new List<Vector3>();

        foreach (Vector3 coord in fullCords)
        {
            Vector3 newCoord = new Vector3(-coord.x, coord.y, -coord.z);
            mergeList.Add(newCoord);
        }
        fullCords = MergeList(fullCords, mergeList);

        foreach (Vector3 coord in fullCords)
        {
            Vector3 newCoord = new Vector3(-coord.x, coord.y, coord.z);
            mergeList.Add(newCoord);
        }
        fullCords = MergeList(fullCords, mergeList);

        foreach (Vector3 coord in fullCords)
        {
            GameObject newGridCell = Instantiate<GameObject>(baseGridCell, coord, Quaternion.identity);
            newGridCell.transform.parent = transform;
            gridCells.Add(newGridCell);
        }

        //gridCells = gridCells.Distinct().ToList();

        // n^2 - 1 = outer ring
        // generate a center cell
        // use n to figure out how far we go
        // use the formula to get the width

        // get coords from this, start with negative of our width to positive. for each number in that
        // we create a pair based on the other numbers.
        // -3, -2, -1, 0, 1, 2, 3
        // -3,-2 -3,-1 -3,0 ,-3,1 etc
        // lines essentially
        // then we just try to create the grid at those coords maybe add a collission check
        // maybe each cell on creation checks for neighbors, then they update each other?
        // cells distance matches cell personal width

        // - - - - -
        // - - - - -
        // - - - - - 
        // - - - - -
        // - - - - - 
    }

    private static List<Vector3> MergeList(List<Vector3> fullCords, List<Vector3> mergeList)
    {
        fullCords.AddRange(mergeList);
        fullCords = fullCords.Distinct().ToList();
        mergeList.Clear();
        return fullCords;
    }

    private List<Vector3> GenCoords(Vector3 centerPoint)
    {
        List<Vector3> fullCords = new List<Vector3>();
        Vector3 previousCoord = new Vector3();
        int area = generationRadius * generationRadius;
        for (int i = 0; i < area; i++)
        {   
            if (fullCords.Contains(centerPoint))
            {
                Vector3 toBeAdded = previousCoord;
                toBeAdded.x += boundSize.size.x;
                if (toBeAdded.x >= generationRadius)
                {
                    toBeAdded.x = centerPoint.x;
                    toBeAdded.z += boundSize.size.z;
                }
                previousCoord = toBeAdded;
                fullCords.Add(toBeAdded);
            }
            else
            {
                previousCoord = centerPoint;
                fullCords.Add(centerPoint);
            }
            // imagine it like rows
            // increment x till we reacg cap
            // then up the y and do the same until y is cap
        }

        // check if coord is in list before adding

        return fullCords;
    }

    private void GenerateGridCells()
    {
        generateNewCells(baseGridCell);
    }

    private void generateNewCells(GameObject gridCell)
    {
        for (int distance = 0; distance < generationRadius; distance++)
        {
            List<Vector3> coords = new List<Vector3>();
            coords.Add(new Vector3(0, transform.position.y, distance));
            coords.Add(new Vector3(0, transform.position.y, -distance));
            for (int x = 0; x < distance+1; x++)
            {     
                coords.Add(new Vector3(x, transform.position.y, distance));
                coords.Add(new Vector3(-x, transform.position.y, distance));
                // - axis
                coords.Add(new Vector3(x, transform.position.y, -distance));
                coords.Add(new Vector3(-x, transform.position.y, -distance));
            }

            coords.Add(new Vector3(distance, transform.position.y, 0));
            coords.Add(new Vector3(-distance, transform.position.y, 0));
            for (int y = 0; y < distance+1; y++)
            { 
                coords.Add(new Vector3(distance, transform.position.y, y));
                coords.Add(new Vector3(distance, transform.position.y, -y));
                // - axis
                coords.Add(new Vector3(-distance, transform.position.y, y));
                coords.Add(new Vector3(-distance, transform.position.y, -y));
            }            

            foreach (Vector3 coord in coords) {
                GameObject newGridCell = Instantiate<GameObject>(gridCell, coord, Quaternion.identity);
                newGridCell.transform.parent = transform;
                gridCells.Add(newGridCell);
            }

            gridCells = gridCells.Distinct().ToList();
        }
    }
}
