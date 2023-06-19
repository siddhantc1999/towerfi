using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyprefab;
    [SerializeField] int poolLength;
    public List<GameObject> enemylist;
    //turn this on for prev state
    [SerializeField] Vector3 startposition;
    public List<Transform> instantiatingpoints=new List<Transform>();
    public int j;
    public int objectpooltime;         //1f-4f
    public bool ispool=false;
   
    private void Awake()
    {
        enemylist = new List<GameObject>();
       
        //reset the pool here
        GameManager.Instance.poolreset += setispool;
        GameManager.Instance.poolreset += Poolinactive;
        for (int i=0;i<poolLength;i++)
        {
            GameObject enemy = Instantiate(enemyprefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            enemylist.Add(enemy);
       
            enemy.SetActive(false);
        }

        instantiatingpoints = Levelinstantiatedestination.instance.getinstantiatepoints;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyInstantiate());
    }

   IEnumerator EnemyInstantiate()
    {
       
        while(true )
        {
            if (ispool)
            {
                EnableObjectPool();
            }
            //for (int j = 0; j < poolLength; j++)
            //{
            //    //i value is incrementing itself
            //    Debug.Log("the value of j "+j);
            //    //Debug.Log("gameobject active " + enemylist[i].activeInHierarchy);
            //    if (enemylist[j].activeInHierarchy == false)
            //    {

            //        //Debug.Log("the value of i "+i);
            //        enemylist[j].SetActive(true);
            //        //enemylist[j].transform.position = startposition;
            //        //return;
            //        yield return new WaitForSeconds(1f);
            //    }
            //}


            yield return new WaitForSeconds(objectpooltime);
        }
    }
    public void setispool()
    {
        if (ispool)
        {
            Debug.Log("here in ispool true");
            ispool = false;
        }
        else if(!ispool)
        {
            Debug.Log("here in ispool false");
            ispool = true;
        }
    }
    public void Poolinactive()
    {
        for (int j = 0; j < poolLength; j++)
        {
            if (enemylist[j].activeInHierarchy == true)
            {
                enemylist[j].SetActive(false);
            }
        }
    }
    private void EnableObjectPool()
    {
        startposition = instantiatingpoints[UnityEngine.Random.Range(0, instantiatingpoints.Count)].transform.position;

     /*   Debug.Log("the start position in object pool "+startposition);*/
        for (int j = 0; j < poolLength; j++)
        {
            //i value is incrementing itself
            //Debug.Log("the value of j "+j);
            //Debug.Log("gameobject active " + enemylist[i].activeInHierarchy);
            if (enemylist[j].activeInHierarchy == false)
            {
                enemylist[j].transform.position = startposition;
                //Debug.Log("the value of j " + j);
                enemylist[j].SetActive(true);
           
               

                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
