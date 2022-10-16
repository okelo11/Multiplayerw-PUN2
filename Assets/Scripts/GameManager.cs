using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class GameManager :MonoBehaviourPunCallbacks
{
    
    public Button enterServerButton;
    public Button enterLobbyButton;
    public Button CreateOrJoinRoomButton;
    public Image backGround;
    public TMP_InputField nickNameOfPlayer;




    public override void OnConnectedToMaster()
    {
        Debug.Log("Server'a girildi");
        enterServerButton.gameObject.SetActive(false);
        enterLobbyButton.gameObject.SetActive(true);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby'ye girildi");
        enterLobbyButton.gameObject.SetActive(false);
        CreateOrJoinRoomButton.gameObject.SetActive(true);
        nickNameOfPlayer.gameObject.SetActive(true);
       
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Oda kuruldu");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Oda'ya girildi");
        backGround.gameObject.SetActive(false);
        CreateOrJoinRoomButton.gameObject.SetActive(false);
        nickNameOfPlayer.gameObject.SetActive(false);
        GameObject g=PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(0,30),0, Random.Range(0,30)), Quaternion.identity);
        g.GetComponent<PhotonView>().Owner.NickName = nickNameOfPlayer.text;
        

    }

    
    public void EnterServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void EnterLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoomJoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("oda", new RoomOptions { MaxPlayers = 3, IsVisible = true },typedLobby:default);
    }
    
}
