using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallRandomBackup : MonoBehaviour
{
    public Vector3 impulse = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 0.5f;
    int counter = 0;
    //how often per 60 frames
    public int frequency = 25;

    // Start is called before the first frame update

    void move(){
        if(counter > (60 / frequency)){
            impulse = new Vector3(Random.Range(-1.0f,1.0f)*speed, 0.0f, Random.Range(-1.0f,1.0f)*speed);
            GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
            counter = 0;
        }   

        counter++;
    }
    void Start()
    {   

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        
    }
}
