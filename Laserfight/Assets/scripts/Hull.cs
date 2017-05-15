using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour {

    public float hullIntegrity {get; private set;}
    public float maxHull;
    public Debris[] debrisPrefabs;
    public int visualExplosionStrength;
    public Laser[] laserPrefabs;
    public Turret turretPrefab;

    void Awake(){
        hullIntegrity = maxHull;

        float zDiff = 1f / laserPrefabs.Length;
        float xPos = 0.5f;
        for(int i = 0; i < laserPrefabs.Length; i++){
            float zPos = -0.5f + (zDiff / 2) + zDiff * i;
            Turret prefab = turretPrefab;
            prefab.laserPrefab = laserPrefabs[i];

            Turret turret = Instantiate<Turret>(prefab);
            turret.Initialize(this, new Vector3(xPos, 0, zPos), new Vector3(0, 90 ,0));
            /*turret.transform.parent = transform;
            turret.transform.localPosition = new Vector3(xPos, 0, zPos);
            //turret.transform.Translate(new Vector3(xPos, 0, zPos));
            turret.transform.localRotation = Quaternion.Euler(0,90,0);*/
        }
    }

    void Explode(){
        Vector3 explosionDirection = Random.onUnitSphere * (visualExplosionStrength / 10.0f);
        for(int i = 0; i < visualExplosionStrength; i++){
            Debris prefab = debrisPrefabs[Random.Range(0, debrisPrefabs.Length)];
            Debris debris = Instantiate<Debris>(prefab);
            debris.transform.localPosition = transform.position;
            debris.transform.localScale = Random.Range(0.2f,1.5f) * Vector3.one;
            debris.transform.localRotation = Random.rotation;
            debris.Body.velocity = explosionDirection;
        }
    }

    public void ReduceHull(float damage){
        hullIntegrity -= damage;
        if(hullIntegrity<=0){
            Destroy(transform.parent.gameObject);
            Explode();
        }
    }

    

}
