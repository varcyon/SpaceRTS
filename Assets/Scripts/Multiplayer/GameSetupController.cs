using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Cinemachine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSetupController : MonoBehaviour
{
    public static List<GameObject> playerList = new List<GameObject>();
    [SerializeField] List<GameObject> playerListDisplay = new List<GameObject>();
    public static List<GameObject> startPosistions= new List<GameObject>();
    public  List<GameObject> startPos = new List<GameObject>();
    bool gameActive;
    public static CinemachineVirtualCameraBase cam;
    public CinemachineVirtualCameraBase camDisp;

    private void Awake()
    {
        cam = camDisp;


    }
    void Start()
    {
        startPosistions = startPos;
       
        CreatePlayer(); //Create a networked player object for each player that loads into the multiplayer scenes.
        Invoke("SetupPlayerList", 2f);
        CameraControl.i.SetCam();
        gameActive = true;

    }
    private void Update()
    {
        playerListDisplay = playerList;

    }
    private void SetupPlayerList()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerList.Add(player);
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMainMenu(){
        PhotonNetwork.Disconnect();
        // while(PhotonNetwork.IsConnected){
        //     return;
        // }
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToLobby(){
        PhotonNetwork.Disconnect();
        // while(PhotonNetwork.IsConnected){
        //     return;
        // }
        SceneManager.LoadScene("Lobby");
    }
}
