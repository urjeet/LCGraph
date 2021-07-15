using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDataPlotter : MonoBehaviour
{
    public string inputFile;    // Name of input file, no extension
    private List<Dictionary<string, object>> nodeDataList; // List to hold data from NodeReader
    
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

    public GameObject NodePrefab;  // Prefab for data points to be instantiated

    // Start is called before the first frame update
    void Start()
    {
        nodeDataList = CSVReader.Read(inputFile);   // Load the data into the list
        Debug.Log(nodeDataList);    // Debug Step - Check if CSV was read correctly

        List<string> columnList = new List<string>(nodeDataList[1].Keys);   // Declare List of column names
        Debug.Log("There are " + columnList.Count + " columns in CSV");     // Debug Step - Print number and name of columns
        foreach (string key in columnList) {
            Debug.Log("Column name is " + key);
        }

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

        // Loop through nodeDataList
        for (var i = 0; i < nodeDataList.Count; i++) {
            // Get value in nodeDataList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(nodeDataList[i][xName]);
            float y = System.Convert.ToSingle(nodeDataList[i][yName]);
            float z = System.Convert.ToSingle(nodeDataList[i][zName]);

            // Instantiate the prefab with coordinates defined above
            Instantiate(NodePrefab, new Vector3(x, y, z), Quaternion.identity);

        }
    }

}