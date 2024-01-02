using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White : MonoBehaviour
{
    void onCollisonEnter(Collision collision){
        if(collision.gameObject.tag == "Ball"){
            Destroy(gameObject);
        }
    }

}
