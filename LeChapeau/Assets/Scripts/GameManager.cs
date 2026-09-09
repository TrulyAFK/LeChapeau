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
    public PlayerController[] players;
    public int playerWithHat;
    private int playersInGame;

    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
        NetworkManager.instance.photonView.RPC("IminGame", RpcTarget.All);
    }
    //rpc
    public void ImInGame(){
        playersInGame++;
        if(playersInGame==PhotonNetwork.PlayerList.Length){
            SpawnPlayer();
        }
    }
    void SpawnPlayer(){
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation,spawnPoints[Random.Range(0,spawnPoints.Length)].position,Quaternion.identity);
        PlayerController playerScript = playerObj.GetComponent<PlayerController>();
        playerScript.photonView.RPC("Initialize",RpcTarget.All,PhotonNetwork.LocalPlayer);
    }
    public PlayerController GetPlayer(int playerId){
        return players.First(x=>x.getId()==playerId);
    }
    public PlayerController GetPlayer(GameObject playerObject){
        return players.First(x=>x.gameObject==playerObject);
    }
    //rpc
    public void GiveHat(int playerId, bool initialGive){
        if(!initialGive){
            GetPlayer(playerWithHat).SetHat(false);
        }
        playerWithHat = playerId;
        GetPlayer(playerId).SetHat(true);
        hatPickupTime=Time.time;
    }
    public bool CanGetHat(){
        if(Time.time>hatPickupTime+invincibleDuration){
            return true;
        } else{
            return false;
        }
    }
    //rpc
    public void WinGame(int playerId){
        gameEnded=true;
        PlayerController player = GetPlayer(playerId);
        GameUI.instance.SetWinText(player.photonPlayer.NickName);
        Invoke("GoBackToMenu",3.0f);
    }
    void GoBackToMenu(){
        PhotonNetwork.LeaveRoom();
        NetworkManager.instance.ChangeScene("Menu");
    }
}
