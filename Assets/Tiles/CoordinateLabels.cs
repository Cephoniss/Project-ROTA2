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
TextMeshPro label;
Vector2Int coordinates = new Vector2Int();
Waypoint waypoint;
  
    void Awake() 
    {
      label = GetComponent<TextMeshPro>();
      waypoint = GetComponentInParent<Waypoint>();
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
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
