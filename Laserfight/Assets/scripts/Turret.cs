using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public Laser laserPrefab;
    public float maxExtraFireTime;
    private float timeSinceFire;
    private float extraFireTime;
    private Transform root;

    void Awake(){
        extraFireTime = Random.Range(0, maxExtraFireTime);
    }

    public void Initialize(Hull parent, Vector3 position, Vector3 rotation){
        transform.parent = parent.transform;
        transform.localPosition = position;
        transform.localEulerAngles = rotation; 
        transform.localScale = Vector3.one;
        root = parent.transform.root;
    }

    void Fire(){
        SpawnLaser();
        extraFireTime = Random.Range(0, maxExtraFireTime);
        timeSinceFire = 0;
    }
    
    void SpawnLaser(){
        Laser prefab = laserPrefab;
        Laser laser = Instantiate<Laser>(prefab);
        Vector3 direction = this.transform.localRotation * Vector3.forward;
        Vector3 position = new Vector3(0, 0, 0);
        
        laser.Initiate(this, direction, position);

        /*float xDiff = this.transform.localScale.x / 2.0f;
        float zDiff = this.transform.localScale.z / 2.0f;
        zDiff = Random.Range(-zDiff,zDiff);
        prefab.transform.localPosition = transform.position + new Vector3(xDiff, 0, zDiff);*/

        


    }

    void FixedUpdate () {
        timeSinceFire += Time.deltaTime;
        if(timeSinceFire >= laserPrefab.fireCooldown + extraFireTime){
            Fire();
        }

    }
}
