using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelinstantiatedestination : MonoBehaviour
{
    public List<Transform> instantiatepoints;
    public List<Transform> getinstantiatepoints
    {
        get { return instantiatepoints; }
    }
    public List<Transform> destinationpoints;
    public List<Transform> getdestinationpoints
    {
        get { return destinationpoints; }
    }
    public static Levelinstantiatedestination instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance!=null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
