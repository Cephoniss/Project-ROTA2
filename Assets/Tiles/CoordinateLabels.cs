using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CoordinateLabels : MonoBehaviour
{

[SerializeField] Color defaultColor = Color.white;
[SerializeField] Color blockedColor = Color.gray;
[SerializeField] Color exploreColor = Color.yellow;
[SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);
TextMeshPro label;
Vector2Int coordinates = new Vector2Int();
//Waypoint waypoint;
GridMan gridman;
  
    void Awake() 
    {
      gridman = FindObjectOfType<GridMan>();  
      label = GetComponent<TextMeshPro>();
      //waypoint = GetComponentInParent<Waypoint>();
      label.enabled = false;
      DisplayCoordinates();
    }
    
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }
    
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if(gridman == null)
        {
            return;
        }

        Node node = gridman.GetNode(coordinates);

        if(node == null)
        {
            return;
        }

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploreColor;
        }
        else
        {
            label.color = defaultColor;
        }

       // if(waypoint.IsPlaceable)
       // {
       //     label.color = defaultColor;
       // }
       // else
       // {
       //     label.color = blockedColor;
       // }
    }
    void DisplayCoordinates()
    {
        if(gridman == null) {return;}
        
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridman.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridman.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
        //UnityEditor.EditorSnapSettings.move.x
    }
    
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

}
