﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    [SerializeField]
    string TagName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter(Collider c)
    {

        if(c.CompareTag(TagName) )
        {
            SceneManager.LoadScene(SceneName);
            
           
        }
    }
        
}
