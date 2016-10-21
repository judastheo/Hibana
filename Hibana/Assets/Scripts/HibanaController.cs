﻿using UnityEngine;
using System.Collections;

public class HibanaController : UnityStandardAssets._2D.Platformer2DUserControl
{
    public bool _canFire = true;
    public GameObject _firePrefab;
    public Transform _fireSpawn;
    public int _fireSpeed = 6;
    public float _timeBetweenFires = 1.0f;
    public float _fireTimeOfLife = 1.0f;
    public int _loads = 5;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
        if (_canFire)
        {
            if (Input.GetMouseButtonDown(0) && _loads > 0)
            {
                StartCoroutine(Fire());
            }
        }
    }
    
    private IEnumerator Fire()
    {
        _canFire = false;
        Transform fireSpawn = _fireSpawn;
        Vector3 direction = m_Character.isFacingRight() ? fireSpawn.right : -fireSpawn.right;

        var fire = (GameObject)Instantiate(_firePrefab, fireSpawn.position, fireSpawn.rotation);
        fire.GetComponent<Rigidbody2D>().velocity = direction * _fireSpeed;
        Destroy(fire, _fireTimeOfLife);
        GameManager.GetInstance().LoadMunition(-1);
        yield return new WaitForSeconds(_timeBetweenFires);
        _canFire = true;
    }
}
