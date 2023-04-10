using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Level0() {
        LevelData.canPoolSize = 100;
        LevelData.cansSoldGoal = 50;
        LevelData.cansNumToSpawn = 55;
        LevelData.maxTime = 600;
        SceneManager.LoadScene("MainScene");
    }
    
    public void Level1() {
        LevelData.canPoolSize = 100;
        LevelData.cansSoldGoal = 75;
        LevelData.cansNumToSpawn = 80;
        LevelData.maxTime = 550;
        SceneManager.LoadScene("MainScene");
    }
    
    public void Level2() {
        LevelData.canPoolSize = 120;
        LevelData.cansSoldGoal = 100;
        LevelData.cansNumToSpawn = 105;
        LevelData.maxTime = 500;
        SceneManager.LoadScene("MainScene");
    }

    public void Quit() {
        Application.Quit();
    }
}

public static class LevelData {
    public static int canPoolSize { get; set; }
    public static int cansSoldGoal { get; set; }
    public static int cansNumToSpawn { get; set; }
    public static float maxTime { get; set; }
}