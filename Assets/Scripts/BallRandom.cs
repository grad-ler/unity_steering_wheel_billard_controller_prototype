using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net;
//using M2Mqtt;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;


public class BallRandom : MonoBehaviour
{
    public bool debug = false;
    //for mqtt
    public string ipAddress = "127.0.0.1"; //10.9.0.21 for final; 127.0.0.1 for local

    MqttClient mqttClient;

    string subTopic = "unity/player/#";
    string pubTopicPosition = "unity/ball/position";
    string pubTopicVelocity = "unity/ball/velocity";

    int framesPerSend = 10;
    int frameCounter = 0;

    //mechanics through mqtt

    public bool shockMode = false;
    public bool shot = false;
    public bool activeWhite = false;
    int livetime = 0;
    int liveTimeMax = 50*10;
    public float limit = 20.0f;

    int stunTime = 3;
    public int stunned = 0;
    Vector3 ballImpulse, ballPosition;
    string team;
    public GameObject white, whiteInitiated;

    struct Team{
        public string name;
        public float limit;
    };

    static string[] whiteList = {"master", "team1", "team2", "team3", "team4", "team5", "team6"};

    Team[] teams = new Team[whiteList.Length];

    Vector3 currentPostition;

    //for random movement
    int counter = 0;
    public Vector3 impulse = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 0.5f;
    public int frequency = 1;       //how often per 50 frames
    
    // message buildup: [startPosition, impulse] -> [x,y,z,dx,dy,dz]
    
    

    void Start()
    {   
        mqttClient = new MqttClient(ipAddress);

        string clientId = Guid.NewGuid().ToString();
        mqttClient.Connect(clientId); //put in credentials here as additional arguments

        mqttClient.MqttMsgPublishReceived += callback;
        mqttClient.Subscribe(new string[] {subTopic}, new byte[] {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE});

        gameObject.transform.position = new Vector3(0.0f, 2.5f, 0.0f);


        for(int i = 0; i < whiteList.Length; i++){

            teams[i].name = whiteList[i];
            teams[i].limit = limit;
            if(debug){Debug.Log(i);}
        }

        currentPostition = transform.position;

    }


    void converter(string message, float limiter, bool relative){
    
        if(debug){Debug.Log(message);}
        string trim = message.Trim(new char[] {' ', '[', ']'});
        if(debug){Debug.Log(trim);}
        string[] values = trim.Split(',');

        if(relative){
        ballPosition = new Vector3((currentPostition.x + float.Parse(values[0])),
                                   (currentPostition.y + float.Parse(values[1])), 
                                   (currentPostition.z + float.Parse(values[2])));
        }
        else{
            ballPosition = new Vector3(float.Parse(values[0]),
                                       float.Parse(values[1]), 
                                       float.Parse(values[2]));
        }
        ballImpulse = new Vector3(float.Parse(values[3]), 
                                  float.Parse(values[4]), 
                                  float.Parse(values[5]));

        for(int i = 0; i<3; i++){
            if(ballImpulse[i] >= limiter){
                ballImpulse[i] = limiter;
            }
            if(ballImpulse[i] <= -limiter){
                ballImpulse[i] = -limiter;
            }
        }
        if(debug){Debug.Log(limiter);}

    }

    void restoreLimit(){
        for(int i = 0; i < teams.Length; i++){
            if(teams[i].limit < limit){
                teams[i].limit += limit/1000.0f;
            }
            if(teams[i].limit > limit){
                teams[i].limit = limit;
            }
        }
    }
    
    void callback(object sender, MqttMsgPublishEventArgs e){
        bool relative = false;
        string[] splitTopic = e.Topic.Split('/');
        if(debug){Debug.Log(splitTopic[2]);}
        if(debug){Debug.Log("start Callback");}
        if(splitTopic.Length >= 4){
            if(splitTopic[3] == "relative"){
                relative = true;
            }
        }
        
        if(!shot){

            for(int i = 0; i < teams.Length; i++){
                    
                if(teams[i].name == splitTopic[2]){
                    converter(System.Text.Encoding.UTF8.GetString(e.Message), teams[i].limit, relative);
                    shot=true;
                    teams[i].limit *= 0.5f;
                }
            }       
        }
    }


    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "White"){
            Destroy(collision.gameObject);
            shot = false;
            activeWhite = false;
            livetime = 0;
            stunned = 50*stunTime;
        }
    }

    void move(){
        if(counter > (50 / frequency)){
            impulse = new Vector3(UnityEngine.Random.Range(-1.0f,1.0f)*speed, 
                                  0.0f, 
                                  UnityEngine.Random.Range(-1.0f,1.0f)*speed);
            
            GetComponent<Rigidbody>().AddForce(impulse, ForceMode.Impulse);
            counter = 0;
        }   

        counter++;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!shot){
            if(stunned == 0){
                move();
            }
            else{
                stunned--;
            }
            restoreLimit();        

        }
        else if (shot && !activeWhite){
            if(shockMode){
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
            Instantiate(white);
            whiteInitiated = GameObject.FindGameObjectsWithTag("White")[0];
            whiteInitiated.transform.position = ballPosition;
            whiteInitiated.GetComponent<Rigidbody>().AddForce(ballImpulse, ForceMode.Impulse);
            if(debug){Debug.Log(ballImpulse);}
            activeWhite = true;
        }
        if(activeWhite == true){
            livetime++;
        }
        if(livetime > liveTimeMax){
            livetime = 0;
            Destroy(whiteInitiated);
            activeWhite = false;
            shot = false;
        }

        currentPostition = transform.position;

        if(frameCounter>=framesPerSend){
            mqttClient.Publish(pubTopicPosition, System.Text.Encoding.UTF8.GetBytes(gameObject.transform.position.ToString()));
            mqttClient.Publish(pubTopicVelocity, System.Text.Encoding.UTF8.GetBytes(gameObject.GetComponent<Rigidbody>().velocity.ToString()));
            frameCounter = 0;
        }
        frameCounter++;

    }
}
