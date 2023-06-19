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
    public float objectpooltime;         //1f-4f
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
        yield return new WaitForSeconds(4F);

        while (true )
        {
            
            if (ispool)
            {

                EnableObjectPool();
                objectpooltime = UnityEngine.Random.Range(1,GameManager.Instance.objectpooltime);

            }
            yield return new WaitForSeconds(objectpooltime);
        }
    }
    public void setispool(int num)
    {
        if (ispool)
        {
            ispool = false;
        }
        else if(!ispool)
        {
            ispool = true;
        }
    }
    public void Poolinactive(int num)
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
        for (int j = 0; j < poolLength; j++)
        {
          
            if (enemylist[j].activeInHierarchy == false)
            {
                enemylist[j].transform.position = startposition;
           
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
