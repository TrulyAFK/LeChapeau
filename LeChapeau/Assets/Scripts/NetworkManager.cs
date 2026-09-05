using UnityEngine;
using TMPro;
using Photon.Pun;
using JetBrains.Annotations;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Menu menu;
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
        menu = FindAnyObjectByType<Menu>();
        if (menu)
        {
            Debug.Log("Menu found");
        }
        else
        {
            Debug.Log("Menu not found");
        }
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
    [PunRPC]
    public void UpdateLobbyUI()
    {
        menu.UpdateLobbyUI();
    }
}

