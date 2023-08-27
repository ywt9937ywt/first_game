using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static PathFinder PfInstance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (PfInstance == null)
        {
            PfInstance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<Hex_tile> FindPath(Hex_tile origin, Hex_tile destination)
    {
        List<Hex_tile> path = TileNode.FindPath(origin,destination);
        //TileNode ndoe = new TileNode(origin, origin, destination, 0);
        return path;
    }

}
