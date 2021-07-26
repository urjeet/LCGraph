using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GraphPlotter : MonoBehaviour
{
    public string nodeInputFile;    // Name of node input file, no extension
    public string edgeInputFile;    // Name of edge input file, no extension

    private List<Dictionary<string, object>> nodeDataList;      // List to hold data from Node dataset
    private List<Dictionary<string, object>> edgeDataList;      // List to hold data from Element (edge) dataset

    // Booleans to toggle the type of map visualized
    public bool flowRateMap;

    // Indices for node data columns
    public int columnNode = 0;
    public int columnX = 1;
    public int columnY = 2;
    public int columnZ = 3;

    // Node data column names
    public string nodeNumbers;
    public string xName;
    public string yName;
    public string zName;

    // Indices for element data columns
    public int columnX1 = 0;
    public int columnY1 = 1;
    public int columnZ1 = 2;
    public int columnX2 = 3;
    public int columnY2 = 4;
    public int columnZ2 = 5;
    public int columnFlow = 6;
    public int columnRed = 7;
    public int columnGreen = 8;
    public int columnBlue = 9;

    // Element data column names
    public string x1Name;
    public string y1Name;
    public string z1Name;
    public string x2Name;
    public string y2Name;
    public string z2Name;
    public string flowName;
    public string redName;
    public string greenName;
    public string blueName;

    // public float graphScale = 10;   // Toggle the scale of the graph

    public GameObject NodePrefab;  // Prefab for data points to be instantiated
    public GameObject NodeHolder;  // Object to contain instantiated prefabs

    //public LineRenderer EdgePrefab;   // Prefab for edge data points to be instantiated
    public LineRenderer lineComponent;
    public GameObject EdgePrefab;   // Prefab for edge data points to be instantiated
    //public GameObject EdgeHolder;  // Object to contain instantiated prefabs

    // Start is called before the first frame update
    void Start()
    {
        // Load the data into the list
        nodeDataList = CSVReader.Read(nodeInputFile);
        edgeDataList = CSVReader.Read(edgeInputFile);
        // Debug Step - Check if CSV was read correctly
        Debug.Log(nodeDataList);
        Debug.Log(edgeDataList);

        // Declare List of column names for nodes and edges
        List<string> columnNodeList = new List<string>(nodeDataList[1].Keys);
        List<string> columnEdgeList = new List<string>(edgeDataList[1].Keys);

        // Debug Step - Print number and name of columns
        Debug.Log("There are " + columnNodeList.Count + " columns in Node CSV");
        foreach (string key in columnNodeList) {
            Debug.Log("The Node column name is " + key);
        }
        Debug.Log("There are " + columnEdgeList.Count + " columns in Element CSV");
        foreach (string key in columnEdgeList)
        {
            Debug.Log("The Edge column name is " + key);
        }

        // Assign column name from columnNodeList to Name variables
        nodeNumbers = columnNodeList[columnNode];
        xName = columnNodeList[columnX];
        yName = columnNodeList[columnY];
        zName = columnNodeList[columnZ];

        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);

        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);

        // Assign column name from columnEdgeList to Name variables
        x1Name = columnEdgeList[columnX1];
        y1Name = columnEdgeList[columnY1];
        z1Name = columnEdgeList[columnZ1];
        x2Name = columnEdgeList[columnX2];
        y2Name = columnEdgeList[columnY2];
        z2Name = columnEdgeList[columnZ2];
        flowName = columnEdgeList[columnFlow];
        redName = columnEdgeList[columnRed];
        greenName = columnEdgeList[columnGreen];
        blueName = columnEdgeList[columnBlue];

        //EdgePrefab = GetComponent<LineRenderer>();
        //EdgePrefab.positionCount = 2;
        

        // Loop through nodeDataList
        for (var i = 0; i < nodeDataList.Count; i++) {
            // Get normalized values in nodeDataList at ith "row", in "column" Name
            float x = Convert.ToSingle(nodeDataList[i][xName]);     // - xMin)/(xMax - xMin);
            float y = Convert.ToSingle(nodeDataList[i][yName]);     // - yMin)/(yMax - yMin);
            float z = Convert.ToSingle(nodeDataList[i][zName]);     //- zMin)/(zMax - zMin);

            // Instantiate the GameObject with coordinates defined above
            GameObject nodePoint = Instantiate(NodePrefab, new Vector3(x, y, z), Quaternion.identity);
            nodePoint.transform.parent = NodeHolder.transform;  // Make NodeHolder the parent of nodePoint

            string nodeName = "" + nodeDataList[i][nodeNumbers];    // Label each Node with its corresponding number
            nodePoint.transform.name = nodeName;     // Assign names to the prefabs

        }   // End of for loop for nodeDataList

        // Loop through edgeDataList
        for (var i = 0; i < edgeDataList.Count; i++)
        {
            // Get normalized values in nodeDataList at ith "row", in "column" Name
            float x1 = Convert.ToSingle(edgeDataList[i][x1Name]);     // - xMin)/(xMax - xMin);
            float y1 = Convert.ToSingle(edgeDataList[i][y1Name]);     // - yMin)/(yMax - yMin);
            float z1 = Convert.ToSingle(edgeDataList[i][z1Name]);     //- zMin)/(zMax - zMin);
            float x2 = Convert.ToSingle(edgeDataList[i][x2Name]);     // - xMin)/(xMax - xMin);
            float y2 = Convert.ToSingle(edgeDataList[i][y2Name]);     // - yMin)/(yMax - yMin);
            float z2 = Convert.ToSingle(edgeDataList[i][z2Name]);     //- zMin)/(zMax - zMin);
            float flow = Convert.ToSingle(edgeDataList[i][flowName]);
            float red = Convert.ToSingle(edgeDataList[i][redName]);
            float green = Convert.ToSingle(edgeDataList[i][greenName]);
            float blue = Convert.ToSingle(edgeDataList[i][blueName]);

            GameObject edgeChild = new GameObject();
            edgeChild.AddComponent<LineRenderer>();
            lineComponent = edgeChild.GetComponent<LineRenderer>();
            lineComponent.SetPosition(0, new Vector3(x1, y1, z1));
            lineComponent.SetPosition(1, new Vector3(x2, y2, z2));
            lineComponent.SetWidth(2.0f, 2.0f);
            if (flowRateMap){
                Color edgeColor = new Color(red, green, blue);
                lineComponent.SetColors(edgeColor, edgeColor);
                Material lineMaterial = new Material(Shader.Find("UI/Default"));
                lineComponent.material = lineMaterial;
            }
            edgeChild.transform.parent = EdgePrefab.transform;

        }   // End of for loop for edgeDataList
    }   // End of Start()

    /**void DrawEdge(Transform firstT, Transform secondT)
    {
        EdgePrefab.SetPosition(0, firstT.position);
        EdgePrefab.SetPosition(1, secondT.position);
    }**/

    private float FindMaxValue(string columnName)
    {
        // Set initial value to first value
        float maxValue = Convert.ToSingle(nodeDataList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < nodeDataList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(nodeDataList[i][columnName]))
                maxValue = Convert.ToSingle(nodeDataList[i][columnName]);
        }

        return maxValue;
    }

    private float FindMinValue(string columnName)
    {

        float minValue = Convert.ToSingle(nodeDataList[0][columnName]);

        // Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < nodeDataList.Count; i++)
        {
            if (Convert.ToSingle(nodeDataList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(nodeDataList[i][columnName]);
        }

        return minValue;
    }

}