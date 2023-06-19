using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject wave1;
    [SerializeField] GameObject wave2;
    [SerializeField] GameObject wave3;
    GameObject prevwave;
    private void Awake()
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.poolreset += wavedisplay;
        prevwave = wave1;
        wavedisplay(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void wavedisplay(int wave)
    {
        if(wave==1)
        {
            prevwave.SetActive(false);
            wave1.SetActive(true);
            prevwave=wave1;
        }
        else
        if(wave==2)
        {
            prevwave.SetActive(false);
            wave2.SetActive(true);
            prevwave = wave2;
        }
        else
        if (wave == 3)
        {
            prevwave.SetActive(false);
            wave3.SetActive(true);
            prevwave = wave3;
        }

    }
}
