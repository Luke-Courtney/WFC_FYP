using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WFCBuilder : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    //2D array to store collapsed tiles to reference
    private WFCNode[,] _grid;

    //List containing all possible nodes
    public List<WFCNode> nodes = new List<WFCNode>();

    //List of all nodes that still need to be collapsed
    private List<Vector2Int> toCollapse = new List<Vector2Int>();

    private Vector2Int[] offsets = new Vector2Int[]{
        new Vector2Int(0,1),    //Top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0),   //Left
    };


    private void Start()
    {
        //Initialize grid as an array of nodes
        _grid = new WFCNode[width, height];

        CollapseWorld();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string SampleScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(SampleScene);
        }
    }

    //Loop through and collapse all nodes in grid
    private void CollapseWorld()
    {
        //Clear toCollapse list just to be safe
        toCollapse.Clear();

        toCollapse.Add(new Vector2Int(Random.Range(0, width), Random.Range(0, height)));

        while (toCollapse.Count > 0)
        {
            //Position of X and Y of toCollapse[0]
            int x = toCollapse[0].x;
            int y = toCollapse[0].y;

            //Create list potentialNodes that containes all possible nodes
            List<WFCNode> potentialNodes = new List<WFCNode>(nodes);

            //Loop through neighbours
            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int neighbor = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

                if (IsInsideGrid(neighbor))
                {
                    WFCNode neighborNode = _grid[neighbor.x, neighbor.y];

                    if (neighborNode != null)
                    {
                        //If neighbor != null, then its been collapsed and we can use its tile to reduced amount of possibilities of current node
                        //Switch which neighbour is being checked, top, bottom, right or left
                        switch (i)
                        {
                            case 0:
                                RemoveInvalidNodes(potentialNodes, neighborNode.Bottom.CompatibleNodes);    //Which nodes can go below neighbour to the top
                                break;

                            case 1:
                                RemoveInvalidNodes(potentialNodes, neighborNode.Top.CompatibleNodes);       //Which nodes can go above neighbour below
                                break;

                            case 2:
                                RemoveInvalidNodes(potentialNodes, neighborNode.Left.CompatibleNodes);      //Which nodes can go left of neighbour to the right
                                break;

                            case 3:
                                RemoveInvalidNodes(potentialNodes, neighborNode.Right.CompatibleNodes);     //Which nodes can go right of neighbour to the left
                                break;
                        }
                    }
                    else
                    {
                        //If neighbour is null (uncollapsed) and doesnt exist in toCollapse, add to toCollapse
                        if (!toCollapse.Contains(neighbor)) { toCollapse.Add(neighbor); }
                    }
                }
            }

            //No valid nodes catch
            if (potentialNodes.Count < 1)
            {
                _grid[x, y] = nodes[0];  // nodes[0] is a blank tile to be put where no other tiles are valid
                Debug.LogWarning("Attempted to collapse at " + x + "," + y + " but found no valid nodes");
            }
            else
            {
                // Calculate the total weight of all potential nodes
                float totalWeight = 0f;
                foreach (var node in potentialNodes)
                {
                    totalWeight += node.Weight;
                }

                // Pick a random value between 0 and totalWeight
                float randomValue = Random.Range(0, totalWeight);

                // Select the node based on weighted probabilities
                foreach (var node in potentialNodes)
                {
                    if (randomValue < node.Weight)
                    {
                        _grid[x, y] = node;
                        break;
                    }
                    randomValue -= node.Weight;
                }
            }

            //Instantiate prefab of tile
            GameObject newNode = Instantiate(_grid[x, y].prefab, new Vector3(x, 0f, y), Quaternion.identity);

            toCollapse.RemoveAt(0);

        }
    }

    //Checks if position is inside grid
    private bool IsInsideGrid(Vector2Int v2Int)
    {
        if (v2Int.x > -1 && v2Int.x < width && v2Int.y > -1 && v2Int.y < height)
        {
            return true;
        }
        return false;
    }

    //Removes invalid neighbor nodes from potentialNodes list
    //TODO - Modify to use connectors on tile prefabs
    private void RemoveInvalidNodes(List<WFCNode> potentialNodes, List<WFCNode> validNodes)
    {
        for (int i = potentialNodes.Count - 1; i > -1; i--)
        {
            if (!validNodes.Contains(potentialNodes[i]))
            {
                potentialNodes.RemoveAt(i);
            }
        }
    }
}