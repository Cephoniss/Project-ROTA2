using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{

TextMeshPro label;
Vector2Int coordinates = new Vector2Int();
  
    void Awake() 
    {
      label = GetComponent<TextMeshPro>();
      DisplayCoordinates();
    }
    
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }
    
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10);
        label.text = coordinates.x + "," + coordinates.y;
        //UnityEditor.EditorSnapSettings.move.x
    }
    
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

}
