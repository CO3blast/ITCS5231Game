using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
public class MenuHandler : MonoBehaviour
{
    
    // Start is called before the first frame update
    public static MenuHandler instance;
    public TextMeshProUGUI topScoreTxt;
    // Update is called once per frame
    public void sceneTransfer(){
        SceneManager.LoadScene("LevelOne");
        
    }
    public void quitGame(){
        Application.Quit();
        EditorApplication.isPlaying=false;
    }
    public void Awake(){
        if(GameManager.instance){
            if(GameManager.instance.score>GameManager.instance.topScore){
            GameManager.instance.topScore=GameManager.instance.score;
        topScoreTxt.text=GameManager.instance.topScore.ToString();
           
        }
            GameManager.instance.finishGame();
        }
    }
}
