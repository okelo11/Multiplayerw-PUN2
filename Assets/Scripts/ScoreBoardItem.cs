using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class ScoreBoardItem : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI deathText;


    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
    } 
       
        
}
