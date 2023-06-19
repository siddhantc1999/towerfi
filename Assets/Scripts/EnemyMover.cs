using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public List<Node> Path=new List<Node>();
    public List<Vector2Int> pathVector;
    GameObject pathLane;
    Pathfinder pathfinder;
    GridManager gridManager;
    public Vector3 endPosition;
    public Vector3 startPosition;
    public Vector3 presentPosition;
    public Vector3 whileendPosition;
    public float maintimer;
 
    [SerializeField] float damage;
    [SerializeField] float lives=3;
    [SerializeField] ParticleSystem particleSystem;
    public float enemyspeed;
    /// //////

    // Start is called before the first frame update
    private void Awake()
    {

        //particleSystem.enableEmission = false;
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        FindObjectOfType<Pathfinder>().regeneratepath += RegeneratePath;
    }
    void OnEnable()
    {
        //Debug.Log("here on enable");
        /* ReturnToStart(Vector2Int.zero);*/
        lives = 3;
        RegeneratePath();
    }
    //private void ReturnToStart(Vector2Int startcoordinates)
    //{
    //    transform.position = gridManager.GetPositionFromCoordinates(pathfinder.getstartcoordinates);
    //}
        //create aniotgher methodf for findpath and startcoroutine move
        public void RegeneratePath()
    {
        //probelm lies here in regenarte path getting called even if iuts not actove
        if (gameObject.activeInHierarchy==true)
        {

         
       /*     Debug.Log("in active heirerchy sibling indiex "+transform.GetSiblingIndex());*/
            StopAllCoroutines();
          /*  Debug.Log("the start position "+transform.position);*/
            FindPath();
         /*   ReturnToStart();*/
            StartCoroutine(Move());
        }

        //if anything goes wrong uncomment the below
       /* else
        {
         
            FindPath();
        }*/
        //StopAllCoroutines();
        //FindPath();
        //ReturnToStart();
        //StartCoroutine(Move());
    }
  
    private void ReturnToStart()
    {
       
        //problem lies here
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.getstartcoordinates);
    }
    private void ReturnToStart(Vector2Int startcoordinates)
    {
        
        transform.position = gridManager.GetPositionFromCoordinates(startcoordinates);
    }

    private void FindPath()
    {


        Path.Clear();
        pathVector.Clear();
        //thgis has to be called again
        //Debug.Log("in find new path");
        /*Debug.Log("the position"+transform.position);*/
      /*  Debug.Log("here in find path");*/
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        Path = pathfinder.GetNewPath(coordinates);
       
        foreach(Node path in Path)
        {
            pathVector.Add(path.coordinates);
        }
        //Debug.Log("the coordinates "+Path[1].coordinates);
    }

    IEnumerator Move()
    {
        int i;
        //foreach to for
        // for(int i=0;i<Path.count;i++)
        //path with node
        //foreach (Node waypointnode in Path)
        //i should start from 1
       

        for (i = 1; i < Path.Count; i++)
        {


            //check here
             startPosition = gameObject.transform.position;
           
            presentPosition = gameObject.transform.position;
            endPosition = gridManager.GetPositionFromCoordinates(Path[i].coordinates);
            float timer = 0;
            transform.LookAt(endPosition);
            while(timer<1f)
            {
                
                maintimer = timer;

                transform.position = Vector3.Lerp(startPosition, endPosition,timer);
               
                timer += Time.deltaTime*enemyspeed;
                //Debug.Log("the value of i "+i);
                //Debug.Log("the endposition "+ endPosition);
                //whileendPosition = endPosition;


                yield return new WaitForEndOfFrame();
            }
        }
        //have to change here
        ScoringSystem.Instance.Sublives();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
   
    private void OnParticleCollision(GameObject other)
    {
        lives--;
        if (lives <= 0)
        {
            //particleSystem.enableEmission = true;

            gameObject.SetActive(false);
        }
    }
}
