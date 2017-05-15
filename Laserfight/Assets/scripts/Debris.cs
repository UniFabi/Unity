using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Debris : MonoBehaviour {

    public float timeToLive;
    private float timeLived;

    public Rigidbody Body {get; private set;}

    // Use this for initialization
    void Awake(){
        Body = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        timeLived += Time.deltaTime;
        if(timeLived >= timeToLive){
            Destroy(gameObject);
            return;
        }

    }
}
