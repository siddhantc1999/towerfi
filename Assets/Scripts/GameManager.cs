using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float CurrentCurrency;
    int lives=3;
    int wave=1;
    public float enemyspeed;
    float minenemyspeed=1f;
    float maxenemyspeed=4f;
    float maxobjectpooltime;
    float minobjectpooltime;
    public float objectpooltime;
    public float timer;
    public event Action poolreset;
    public bool iscoroutine=true;
    ObjectPool objectPool;
    public int getlives
    {
        get { return lives; }
        set { lives = value; }
    }
    public float getCurrentCurrency
    {
        get { return CurrentCurrency; }
        set { CurrentCurrency = value; }
    }
    public float getobjectpooltime
    {
        get { return objectpooltime; }
        set { objectpooltime = value; }
    }
    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        InvokeRepeating("Pooltime", 0f,1f);
    }
    private void Pooltime()
    {
      
        if (timer < 10f)
        {
            //we will have to check here
            timer += 1;
        }
        else if (timer == 10f)
        {
            if (iscoroutine)
            {
                iscoroutine = false;
                poolreset?.Invoke();
                wave++;
                Currentwave(wave);
                //setall gameobjet inactive
                StartCoroutine(timerstart());
            }
        }
       
       
        
    }

    IEnumerator timerstart()
    {
        yield return new WaitForSeconds(8f);
        timer = 0;
        iscoroutine = true;
        objectPool.setispool();
    }

    public void Currentwave(int wave)
    {
        switch (wave)
        {
            case 1:
                enemyspeed = minenemyspeed;
                objectpooltime = maxobjectpooltime;
                break;
            case 2:
                enemyspeed = (minenemyspeed + maxenemyspeed) / 2;
                objectpooltime = (maxobjectpooltime+ minobjectpooltime)/2;
                break;
            case 3:
                enemyspeed = maxenemyspeed;
                objectpooltime = minobjectpooltime;
                break;

        }
    }

    }
    // Start is called before the first frame update
   

