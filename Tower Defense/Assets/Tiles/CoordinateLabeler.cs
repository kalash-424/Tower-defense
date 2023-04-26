using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultcolor = Color.white;
    [SerializeField] Color blockedcolor = Color.gray;

    [SerializeField] Color visitedcolor = Color.yellow;
    [SerializeField] Color pathcolor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    //Waypoints waypoint;            // old script
    GridManager gridManager;

    void Awake(){
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false ;   

        // waypoint = GetComponentInParent<Waypoints>();     //old script
        displayCoordinates();
    }

    void Update()
    {
        if(!Application.isPlaying){ 
            displayCoordinates();
            updateNameofObject();
            label.enabled = true;   //enable this to see coordinates in editing mode
        }

        colorCoordinates();
        ToggleLabels();
    }

    void colorCoordinates(){
        if(gridManager == null) return;

        Node node = gridManager.getNode(coordinates);

        // if(!(waypoint.IsPlaceable)){
        //     label.color = blockedcolor;
        // }                                      //old script
        // else{
        //     label.color = defaultcolor;
        // }
        if(node == null) return;
        
        if(!node.isWalkable){
            label.color = blockedcolor;
        }
        else if(node.isPath){
            label.color = pathcolor;
        }
        else if(node.visited){
            label.color = visitedcolor;
        }
        else {
            label.color = defaultcolor;
        }

    }

    void ToggleLabels(){
        if(Input.GetKeyDown(KeyCode.C)){
            label.enabled = ! label.IsActive();

        }

    }

    void displayCoordinates(){
        if(gridManager == null) return;

        coordinates.x = (int)(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = (int)(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + ", " + coordinates.y;
    }

    void updateNameofObject(){
        transform.parent.name = coordinates.ToString();
    }
}
