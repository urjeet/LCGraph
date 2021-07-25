using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GizmosGraphComponent : MonoBehaviour
{
    private GizmosGraph<Vector3, float> graph;    // Create a Graph object

    // Start is called before the first frame update
    void Start()
    {
        graph = new GizmosGraph<Vector3, float>();    // Initialize Graph object
        var node1 = new Node<Vector3>() { Value = RandomVectorGenerator(), NodeColor = Color.yellow };  // Generate first node with random position
        var node2 = new Node<Vector3>() { Value = RandomVectorGenerator(), NodeColor = Color.yellow };  // Generate second node with random position
        var node3 = new Node<Vector3>() { Value = RandomVectorGenerator(), NodeColor = Color.yellow };  // Generate third node with random position
        var edge1 = new Edge<float, Vector3>() { Value = 1.0f, From = node1, To = node2, EdgeColor = Color.red };   // Generate edge with weight 1.0 from node 1 to node 2
        var edge2 = new Edge<float, Vector3>() { Value = 1.0f, From = node3, To = node1, EdgeColor = Color.red };   // Generate edge with weight 1.0 from node 3 to node 1
        var edge3 = new Edge<float, Vector3>() { Value = 1.0f, From = node2, To = node3, EdgeColor = Color.red };   // Generate edge with weight 1.0 from node 2 to node 3

        // Add all node and edge objects to the Graph object
        graph.Nodes.Add(node1);
        graph.Nodes.Add(node2);
        graph.Nodes.Add(node3);
        graph.Edges.Add(edge1);
        graph.Edges.Add(edge2);
        graph.Edges.Add(edge3);
    }

    void OnDrawGizmos() // Draws the actual structures in 3D **Gizmos is only used for debugging, so does not show up in actual build**
    {
        if (graph == null)  // If Graph object has no node and edge objects added, call Start()
        {
            Start();
        }

        foreach (var node in graph.Nodes)   // For each loop to draw all the nodes added to the Graph object
        {
            Gizmos.color = node.NodeColor;
            Gizmos.DrawWireSphere(node.Value, 0.125f);  // Draw a wire sphere for the nodes at their position with 0.125 radius
        }

        foreach (var edge in graph.Edges)   // For each loop to draw all the edges added to the Graph object
        {
            Gizmos.color = edge.EdgeColor;
            Gizmos.DrawLine(edge.From.Value, edge.To.Value);    // Draw a line the first position to second position (From - To)
        }
    }

    public Vector3 RandomVectorGenerator()  // Method to generate random Vector3 value for position
    {
        var minPosition = new Vector3(0, 0, 0); // Set minimum Vector3
        var maxPosition = new Vector3(10, 10, 10);  // Set maximum Vector3
        var randomPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));   // Generate new Vector3 in the range of each (x, y, z)
        return randomPosition;  // Return random Vector3
    }

    // Buggy random Node method: **Writing this method to generate random Node so From and To can be randomized like position**
    /**public List<Node<Vector3>> RandomNodeGenerator(List<Node<Vector3>> nodeList) 
    {
        var randIndex = Random.Range(0, nodeList.Count);
        return nodeList[randIndex];
    }**/
}


