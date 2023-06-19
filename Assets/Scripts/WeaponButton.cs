using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public GameObject Weapon;
    public GameObject getWeapon
    {
        get { return Weapon; }
    }
    WeaponSelect weaponSelect;
    public float weaponPrice;
    private void Awake()
    {
        weaponSelect = FindObjectOfType<WeaponSelect>();
        GetComponent<Image>().color = Color.black;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnWeaponButtonClick()
    {

        //change the color
        //white on select and not white on non selected
        weaponSelect.WeaponChange(this);
    }
  
}
