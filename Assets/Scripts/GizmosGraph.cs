using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosGraph<TNodeType, TEdgeType>    // Graph class, includes nodes and edges
{
    public GizmosGraph()
    {
        Nodes = new List<Node<TNodeType>>();    // Initialize nodes as a list
        Edges = new List<Edge<TEdgeType, TNodeType>>(); // Initialize edges as a list
    }
    public List<Node<TNodeType>> Nodes { get; private set; }    // Get and set the list of nodes
    public List<Edge<TEdgeType, TNodeType>> Edges { get; private set; } // Get and set the list of edges
}

public class Node<TNodeType>    // Node class
{
    public Color NodeColor { get; set; }    // Set the color of the Node
    public TNodeType Value { get; set; }    // Set the position of the Node (type Vector3)
}

public class Edge<TEdgeType, TNodeType> // Edge class
{
    public Color EdgeColor { get; set; }    // Set the color of the Edge
    public TEdgeType Value { get; set; }    // Set the weight of the Edge (type float)
    public Node<TNodeType> From { get; set; }   // Set the From Node of the Edge (type Node)
    public Node<TNodeType> To { get; set; } // Set the To Node of the Edge (type Node)
}