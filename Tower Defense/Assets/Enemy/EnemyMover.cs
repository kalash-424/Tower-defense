using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 2f)] float speed = 0.1f;

    List<Node> path = new List<Node>();
    
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    
    void OnEnable()
    {
        // Spawn enemy to the start position again
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.Startcoord); 

        RecalculatePath(true);
    }

    void Awake(){
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath){
        Vector2Int coordinates = new Vector2Int();

        if(resetPath){
            coordinates = pathfinder.Startcoord;
        }
        else{
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());

    }


    IEnumerator FollowPath(){
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startpos = transform.position;
            Vector3 endpos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelpercent = 0f;

            transform.LookAt(endpos);

            while (travelpercent < 1f){
                travelpercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startpos, endpos, travelpercent);
                yield return new WaitForEndOfFrame();
            }
        }
        
        enemy.Stealmoney();
        gameObject.SetActive(false);
    }
}
