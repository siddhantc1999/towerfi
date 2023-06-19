using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    //public GameObject selectedWeapon;
    //public GameObject setselectedWeapon
    //{
    //    get { return selectedWeapon; }
    //}
    public event Action WeaponChangedEvent;



    //public float selectedWeaponPrice;
    WeaponButton selectedweaponbutton;

    public WeaponButton GetWeaponButton
    {
        get { return selectedweaponbutton; }
    }


    //[SerializeField] GameObject defaultgameobject;
    //[SerializeField] float defaultprice;
    [SerializeField] WeaponButton defaultweaponbutton;
    // Start is called before the first frame update
    void Start()
    {
        //selectedWeapon = defaultweaponbutton.getWeapon;
        //selectedWeaponPrice = defaultweaponbutton.weaponPrice;
        WeaponChange(defaultweaponbutton);
        //selectedWeapon.GetComponent<>()
        //place here default weapon and price 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponChange(WeaponButton weaponbutton)
    {
        if(selectedweaponbutton!=null)
        {
            selectedweaponbutton.GetComponent<Image>().color = Color.black;
        }
        
        selectedweaponbutton = weaponbutton;
        selectedweaponbutton.GetComponent<Image>().color = Color.white;
        //selectedWeapon = weaponbutton.Weapon;
        //selectedWeaponPrice = weaponbutton.weaponPrice;

        ///asking all the wapoints or the coordinates to change the selected weapon
        WeaponChangedEvent?.Invoke();
    }
    
}
