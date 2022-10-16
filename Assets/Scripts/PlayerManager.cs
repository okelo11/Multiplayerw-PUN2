using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Transform _weaponTransform;

    Rigidbody _rb;
    float _horizontal, _vertical;
    float _mouseY;
    PhotonView _Client;
    Vector3 _rbVelocity;
    public int speed=2;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _Client = GetComponent<PhotonView>();
    }
    private void Update()
    {
        _mouseY = Input.GetAxis("Mouse Y");
        _vertical = Input.GetAxis("Vertical")*speed;
        _horizontal = Input.GetAxis("Horizontal")*speed;
        WeaponRotate();
        PlayerRotate();



    }
    private void FixedUpdate()
    {
     
        if (_Client.IsMine)
        {
            _rb.velocity = transform.forward * _vertical * Time.fixedDeltaTime*speed;
        }
                
    }
    void WeaponRotate()
    {
       
        

        if (_Client.IsMine) 
        {
        
        _weaponTransform.gameObject.transform.Rotate(-_mouseY * 5, 0f, 0f,Space.Self);
        if (_weaponTransform.eulerAngles.x >= 0 && _weaponTransform.eulerAngles.x<=100)
        {
          
                _weaponTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
            else if(_weaponTransform.eulerAngles.x<=315&& _weaponTransform.eulerAngles.x >= 200)
        {
       
                _weaponTransform.localRotation = Quaternion.Euler(315, 0, 0);
                
        }
         
        }
        
        
    }
    void PlayerRotate()
    {
        if (_Client.IsMine)
            transform.Rotate(0, _horizontal*Time.deltaTime*3, 0);
    }
}
