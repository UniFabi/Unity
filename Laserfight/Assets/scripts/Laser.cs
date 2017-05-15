using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    public float timeToLive;
    public float damage;
    public float fireCooldown;
    public Transform originRoot {get; private set;}
    public Debris[] debrisPrefabs;
    public int visualShieldExplosionStrength;
    public int visualHullExplosionStrength;

    private float timeLived;

    public void Initiate(Turret originRoot, Vector3 direction, Vector3 position){
        Debug.Log(direction);
        this.originRoot = originRoot.transform.root;
        //this.direction = direction;
        this.transform.localPosition = originRoot.transform.position;
        this.transform.localRotation = originRoot.transform.rotation;
        this.transform.Translate(position);

        this.transform.Rotate(90 + Random.Range(-3f, 3f), 0 + Random.Range(-3f, 3f), 0 + Random.Range(-3f, 3f));
    }

    void DamageShield(Shield shield){
        float hullDamage = shield.reduceShield(damage);
        if(hullDamage==0){
            Destroy(gameObject);
            ShowShieldImpact(shield);
        }
        //else reduce damage, damage--
    }

    void ShowShieldImpact(Shield shield){
        Vector3 explosionDirection = (transform.position - shield.transform.position).normalized * speed + Random.onUnitSphere * (speed / 5.0f);
        //TODO: Function that return debris prefabs for different types of lasers and shields
        for(int i = 0; i < visualShieldExplosionStrength; i++){
            Debris prefab = debrisPrefabs[Random.Range(0, debrisPrefabs.Length)];
            Debris debris = Instantiate<Debris>(prefab);
            debris.transform.localPosition = transform.position;
            debris.transform.localScale = Random.Range(0.2f,0.4f) * Vector3.one;
            debris.transform.localRotation = Random.rotation;
            debris.Body.velocity = explosionDirection;
        }
    }

    void ShowHulldImpact(Hull hull){
        Vector3 explosionDirection = (transform.position - hull.transform.position).normalized * speed + Random.onUnitSphere * (speed / 5.0f);
        //TODO: Function that return debris prefabs for different types of lasers and shields
        for(int i = 0; i < visualHullExplosionStrength; i++){
            Debris prefab = debrisPrefabs[Random.Range(0, debrisPrefabs.Length)];
            Debris debris = Instantiate<Debris>(prefab);
            debris.transform.localPosition = transform.position;
            debris.transform.localScale = Random.Range(0.2f,0.8f) * Vector3.one;
            debris.transform.localRotation = Random.rotation;
            debris.Body.velocity = explosionDirection;
        }
    }

    void Awake () {
        direction = direction.normalized;
    }

    void OnTriggerEnter(Collider enteredCollider){
        if(enteredCollider.CompareTag("Shield")){
            Shield shield = enteredCollider.gameObject.GetComponent<Shield>();
            if(shield.transform.root != originRoot){
                DamageShield(shield);
            }
        }
    }

    void OnCollisionEnter(Collision enteredCollision){
        if(enteredCollision.collider.CompareTag("Hull")){
            //Debug.Log("Hull");
            if(enteredCollision.transform.root != originRoot){
                Hull hull = enteredCollision.gameObject.GetComponent<Hull>();
                ShowHulldImpact(hull);
                hull.ReduceHull(damage);
                Destroy(gameObject);
            }
        }
    }


    void FixedUpdate () {
        timeLived += Time.deltaTime;
        if(timeLived>=timeToLive){
            Destroy(gameObject);
        }
        this.transform.Translate(Vector3.up * speed);
    }
}
