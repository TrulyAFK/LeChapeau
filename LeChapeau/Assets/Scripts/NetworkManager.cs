using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using JetBrains.Annotations;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
    }


    //other script rpcs
    [PunRPC]
    public void UpdateLobbyUI()
    {
        Menu menu = FindAnyObjectByType<Menu>();
        menu.UpdateLobbyUI();
    }

    [PunRPC]
    public void IminGame(){
        GameManager.instance.ImInGame();
    }
    /*
    [PunRPC]
    public void Initialize(PlayerController player){
        player.Initialize(PhotonNetwork.LocalPlayer);
    }
    GameManager game = FindAnyObjectByType<GameManager>();
    */
    [PunRPC]
    public void GiveHat(int playerId, bool initialGive){
        GameManager.instance.GiveHat(playerId,initialGive);
    }
    [PunRPC]
    public void WinGame(int playerId){
        GameManager.instance.WinGame(playerId);
    }
}

