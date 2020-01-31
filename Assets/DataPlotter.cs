using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    // Data from CSV reader.
    private List<Dictionary<string, object>> pointList;

    // Scale of the plot.
    public float plotScale = 10;

    // Indices for columns to be assigned.
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // Column names.
    public string xName;
    public string yName;
    public string zName;

    // The prefab cloned to graph data points.
    public GameObject PointPrefab;

    // The parent of all points (so that we don't dump all points into the hierarchy).
    public GameObject PointHolder;

    public void PlotDataFromString(string data)
    {
        // Delete all children of the current point holder.
        foreach (Transform child in PointHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        pointList = CSVReader.Read(data);

        //Log to console
        Debug.Log(pointList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);

        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in the CSV");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);

        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);

        //Loop through Pointlist
        for (var i = 0; i < pointList.Count; i++)
        {
            // Get value in poinList at ith "row", in "column" Name, normalize
            float x = (Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);

            float y = (Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);

            float z = (Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

            //instantiate the prefab with coordinates defined above
            GameObject dataPoint = Instantiate(PointPrefab, new Vector3(x, y, z) * plotScale, Quaternion.identity);
            dataPoint.GetComponent<Renderer>().material.color = new Color(x, y, z, 1.0f);
            dataPoint.transform.parent = PointHolder.transform;
        }
    }

    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 1; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {

        float minValue = Convert.ToSingle(pointList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 1; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }

        return minValue;
    }
}