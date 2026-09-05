using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public bool gameEnded = false;//has the game ended?
    public float timeToWin;
    public float invincibleDuration;
    private float hatPickupTime;
    [Header("Players")]
    public string playerPrefabLocation;
    public Transform[] spawnPoints;
    public PlayerController[] Players;
    public int playerWithHat;
    private int playerInGame;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Players = new PlayerController[PhotonNetwork.PlayerList.Length];
        photonView.RPC("ImInGame", RpcTarget.All);
    }
}
