using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float CurrentCurrency;
    int lives=3;
    public int wave=1;
    public float enemyspeed;
    public float minenemyspeed =1f;
    public float maxenemyspeed =4f;
    public float maxobjectpooltime;
    public float minobjectpooltime;
    public float objectpooltime;
    public float timer;
    public event Action<int> poolreset;
    public int currentsceneindex;
    public bool iscoroutine=true;
    ObjectPool objectPool;
    public int CurrentScene;
    public int leveltime;
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
    public float getenemyspeed
    {
        get { return enemyspeed; }
        set { enemyspeed = value; }
    }
    private void Awake()
    {
       
        wave = 1;
        Currentwave(wave);
        objectPool = FindObjectOfType<ObjectPool>();
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("the build index"+SceneManager.GetActiveScene().buildIndex);
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
      
        if (timer < leveltime)
        {
          
            timer += 1;
        }
        else if (timer == leveltime)
        {
            if (iscoroutine)
            {
                iscoroutine = false;
                if (wave < 3)
                {
                    wave++;
                    poolreset(wave);
                    Currentwave(wave);
                }
                //sceneloader here
                else
                {
                    wave++;
                    poolreset(wave);
                    StartCoroutine(Loadnextscreen());
                }
                StartCoroutine(timerstart());
            }
        }
       
       
        
    }

    IEnumerator Loadnextscreen()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(CurrentScene + 1);
    }

    IEnumerator timerstart()
    {
        yield return new WaitForSeconds(4f);
        Poolrestart();
    }


    private void Poolrestart()
    {
        timer = 0;
        iscoroutine = true;
        objectPool.setispool(wave);
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
   

