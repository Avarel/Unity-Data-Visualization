using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour
{

    // Name of the input file, no extension
    public string inputfile;


    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Use this for initialization
    void Start()
    {

        // Set pointlist to results of function Reader with argument inputfile
        pointList = CSVReader.Read(inputfile);

        //Log to console
        Debug.Log(pointList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in CSV");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);
    }

}