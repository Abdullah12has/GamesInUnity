using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Vector3 v3Force;

    [SerializeField]
    private KeyCode Forward;
    [SerializeField]
    private KeyCode Backward;






    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate() //
    {
        if (Input.GetKey(Forward))
        {
            GetComponent<Rigidbody>().velocity += v3Force;

        }
        else if (Input.GetKey(Backward))
        {
            GetComponent<Rigidbody>().velocity -= v3Force;
        }

       
    }
}
