using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{ 
    public int playerHp;
    public int playetMaxHp=100;
    public GameObject bullet;
    [SerializeField]
    GameObject _bulletSpawnPoint;
    GameObject _tempBullet;
    [SerializeField]
    Image _healthBar;
    [SerializeField]
    Canvas _healthBarCanas;
    PhotonView me;
    GameObject scoreB;
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreBoardItemPrefab;

    private void Start()
    {                      
        playerHp = playetMaxHp;
        //PhotonNetwork.NickName = Random.Range(0, 19).ToString();
        container = GameObject.Find("ScoreBoard").transform;
         scoreB=PhotonNetwork.Instantiate("scores", container.position,Quaternion.identity);
        scoreB.gameObject.transform.SetParent(container);
        scoreB.transform.localScale = new Vector3(1, 1, 1);





    }
    private void Update()
    {     if(GetComponent<PhotonView>().IsMine)
         GetComponent<PhotonView>().RPC("RPC_ScoreBoardDestroy",RpcTarget.AllBuffered);

        if (GetComponent<PhotonView>().IsMine)
        {

        
            var photonViews = FindObjectsOfType<PhotonView>();
            foreach (var view in photonViews)
            {
                // var player = view.Owner;
                //player != null &&
                if (view.tag == "Player")
                {
                    _healthBarCanas.gameObject.transform.LookAt(view.gameObject.transform);
                    
                }
            }
        }


        if (GetComponent<PhotonView>().IsMine)
        {
            if (playerHp <= 0)
            {
                GetComponent<PhotonView>().RPC("RPC_Dead", RpcTarget.All, null);
            }
        }

        
        #region bulletFire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                //GetComponent<PhotonView>().RPC("RPC_Bullet", RpcTarget.All);
                _tempBullet = PhotonNetwork.Instantiate("Bullet", _bulletSpawnPoint.transform.position, _bulletSpawnPoint.transform.rotation);
                _tempBullet.GetComponent<Rigidbody>().velocity = _tempBullet.transform.forward * 30;
                
            }
        }
        #endregion
      
    }
 


    [PunRPC]
    void RPC_TakeDmg(int dmg)
    {
      
             playerHp -= dmg;
        
            _healthBar.fillAmount = (float)playerHp / (float)playetMaxHp;
              
        
    }
    [PunRPC]
    void RPC_ScoreBoardDestroy()
    {
        
        scoreB.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<PhotonView>().Owner.NickName;
        scoreB.gameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = playerHp.ToString();


    }
    [PunRPC]
    void RPC_Dead()
    {
        StartCoroutine("Dead");
    }

    IEnumerator Dead()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(5);
        playerHp = playetMaxHp;
        _healthBar.fillAmount = 1;
        transform.position = new Vector3(Random.Range(0, 30), 0, Random.Range(0, 30));
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
