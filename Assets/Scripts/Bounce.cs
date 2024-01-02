using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    void OnCollisionEnter(Collision collision){
        if(gameObject.tag == "NorthSouth"){
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(collision.gameObject.GetComponent<Rigidbody>().velocity.x, collision.gameObject.GetComponent<Rigidbody>().velocity.y, collision.gameObject.GetComponent<Rigidbody>().velocity.z*-1);
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);
        }
        else if(gameObject.tag == "EastWest"){
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(collision.gameObject.GetComponent<Rigidbody>().velocity.x*-1, collision.gameObject.GetComponent<Rigidbody>().velocity.y, collision.gameObject.GetComponent<Rigidbody>().velocity.z);
            Debug.Log(collision.gameObject.GetComponent<Rigidbody>().velocity);
        }

    }
    */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
