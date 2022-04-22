using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class AI : MonoBehaviour
{

    private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;

    private void Awake()
    {
        pathfindingMovement = GetComponent < EnemyPathfindingMovement();
    }
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }



    private Vector3 GetRoamingPosition()
    {
        startingPosition + UtilsClass.GetRandomDir() * Random.range(10f, 70f);
    }

    // Update is called once per frame
    private void Update()
    {
        pathfindingMovement.MoveTo(roamPosition);
        float reachedPositionDistance = 1f;
        if (Vector3.Distance(transform.position,roamPosition) < reachedPositionDistance{

        }
    } 
}
