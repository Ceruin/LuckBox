using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private Neighbors neighbors = new Neighbors();
    private Vector3 position;

    public class Neighbors
    {
        public GridCell left { get; set; }
        public GridCell right { get; set; }
        public GridCell up { get; set; }
        public GridCell down { get; set; }
    }

    private void Start()
    {
        position = transform.position;

        FindNeighbors();
    }

    private void FindNeighbors()
    {
        List<Collider> hitColliders = Physics.OverlapSphere(position, 3).ToList();

        hitColliders.RemoveAll(p => p.gameObject.tag != "Cell");

        foreach (var hitCollider in hitColliders)
        {
            GridCell cell = hitCollider.gameObject.GetComponent<GridCell>();

            var heading = cell.getPOS() - getPOS();
            var distance = (heading).magnitude;
            var direction = heading / distance;

            if (distance == 1)
            {
                if (direction.Equals(Vector3.left))
                {
                    neighbors.left = cell;
                }
                else if (direction.Equals(Vector3.right))
                {
                    neighbors.right = cell;
                }
                else if (direction.Equals(Vector3.forward))
                {
                    neighbors.up = cell;
                }
                else if (direction.Equals(Vector3.back))
                {
                    neighbors.down = cell;
                }
            }
           
            print("Found Cube @: " + cell.getPOS());           
        }

        string printNeighbors = string.Format(@"
                Neighbors:
                Left [{0}]
                Right [{1}]
                Up [{2}]
                Down [{3}]",
                this.neighbors.left != null ? this.neighbors.left.getPOS().ToString() : "",
                this.neighbors.right != null ? this.neighbors.right.getPOS().ToString() : "",
                this.neighbors.up != null ? this.neighbors.up.getPOS().ToString() : "",
                this.neighbors.down != null ? this.neighbors.down.getPOS().ToString() : "");
                
        print(printNeighbors);
    }

    public Vector3 getPOS()
    {
        return position;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
