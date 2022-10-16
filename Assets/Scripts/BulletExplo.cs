using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletExplo : MonoBehaviour
{ public int bulletDmg = 25;
    [Range(0, 10)]
    public float expRadius = 2f;
    [SerializeField]
    private ParticleSystem expParticle;
    GameObject tempParticle;


    private void Update()
    { 

    }

    private void OnCollisionEnter(Collision other)
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            Collider[] affectedColliders = Physics.OverlapSphere(other.GetContact(0).point, expRadius);
         
         tempParticle= PhotonNetwork.Instantiate("BulletExpEffect", other.GetContact(0).point, Quaternion.identity);
         GetComponent<PhotonView>().RPC("Close", RpcTarget.All);
        

        foreach (var collider in affectedColliders)
        {
            Debug.Log(collider.name);
            if (collider.tag == "Player")
            {
                
                collider.GetComponent<PhotonView>().RPC("RPC_TakeDmg", RpcTarget.All, bulletDmg);
                Debug.Log("DMG!!!");

            }
            else
                continue;
        }

        StartCoroutine(WaitForParticleFinish());
        IEnumerator WaitForParticleFinish()
        {

            yield return new WaitForSeconds(1);

                           
                PhotonNetwork.Destroy(this.gameObject);
                PhotonNetwork.Destroy(tempParticle);
           
           

        }
    }

  

   
}
    [PunRPC]
    void Close()
    {

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;

    }
}
