using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField]
    private int PointValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Sphere")
        {
            other.GetComponent<PlayerCollectable>().points += PointValue;
            Destroy(gameObject);
        }
    }
}
