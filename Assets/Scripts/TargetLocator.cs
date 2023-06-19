using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    public EnemyMover[] enemyMovers;
    [SerializeField] Transform headLocator;
    public float enemydistance;
    public float maxdistance;
    float closestdistance;
    GameObject closestenemy;
    [SerializeField]GameObject target;
    public float thresholddistance=20f;
    [SerializeField] ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        maxdistance = Mathf.Infinity;
        enemyMovers = FindObjectsOfType<EnemyMover>();
       
        closestdistance = maxdistance;
        Debug.Log(1 == 1);
    }

    // Update is called once per frame
    void Update()
    {
        //enemydistance = Vector3.Distance(gameObject.transform.position,);
        ClosestEnemy();
        AimWeapon();
        
    }

    private void ClosestEnemy()
    {
        foreach(EnemyMover enemymover in enemyMovers)
        {
            if(enemymover.gameObject.activeInHierarchy)
            {
                closestdistance = Vector3.Distance(transform.position, enemymover.transform.position);
            }
            if(closestdistance<maxdistance)
            {
                closestenemy = enemymover.gameObject;
                maxdistance = closestdistance;
            }
            target = closestenemy;
        }
    }

    private void AimWeapon()
    {
        float checkdistance = Vector3.Distance(transform.position,target.transform.position);
        if (target != null && checkdistance<=thresholddistance)
        {
            //Debug.Log("the name of enemy mover" + target.transform.name);
            headLocator.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            particleSystem.enableEmission = true;
        }
        else
        {
            target = null;
            particleSystem.enableEmission = false;
        }
    }
}
