using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using BrewedInk.CRT;
public class GameManager : MonoBehaviour
{   
    [SerializeField] public Transform currentRespawnTrans;
    public  Transform playerTrans;
    public static GameManager instance;
    public GUIHandler GUI;
    public float score;
    [SerializeField] public PlayerManager player;
    public float topScore;
    // Start is called before the first frame update
    private void Awake(){
        if(instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        score=0;
    }
    // Update is called once per frame
    void Update()
    {
        GUI.scoreCounter.text=score.ToString();
        if(player.health<=0){
            playerTrans.position=currentRespawnTrans.position+Vector3.up;
            sceneTransfer(SceneManager.GetActiveScene().name);
            player.health=100;
        }
        
    }
    public void sceneTransfer(string scene){
        SceneManager.LoadScene(scene);
        
    }
    public void quitGame(){
        Application.Quit();
        EditorApplication.isPlaying=false;
    }
    public void finishGame(){
        Cursor.lockState = CursorLockMode.None;
         Destroy(player.gameObject);
        Destroy(GUI.gameObject);
         Destroy(gameObject);
    }

}
