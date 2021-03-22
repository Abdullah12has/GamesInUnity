using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{

    [SerializeField]
    private KeyCode RestartGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(RestartGame))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
