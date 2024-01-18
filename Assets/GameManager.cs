using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerLives = 3;
    public int initialPickups = 5;
    public int initialEnemies = 3;

    public int currentPickups;
    public int currentEnemies;

    private int livesLostInCurrentLevel = 0;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetGame();
    }

    public void PlayerLostLife()
    {
        playerLives--;
        livesLostInCurrentLevel++;
        CheckGameEnd();
    }

    public void TriggerEndpoint()
    {
        AdjustDifficulty();
        RestartLevel();
    }

    private void AdjustDifficulty()
     {
         if (livesLostInCurrentLevel == 0)
         {
             currentEnemies = Mathf.Min(currentEnemies + 1, maxEnemies); 
             currentPickups = Mathf.Max(currentPickups - 1, minPickups); 
         }
         else
         {
             currentEnemies = Mathf.Max(currentEnemies - livesLostInCurrentLevel, minEnemies); 
             currentPickups = Mathf.Min(currentPickups + livesLostInCurrentLevel, maxPickups); 
         }

         livesLostInCurrentLevel = 0;
     } 




    private void CheckGameEnd()
    {
        if (playerLives <= 0)
        {
            Debug.Log("Game Over!");
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetGame()
    {
        playerLives = 3;
        currentPickups = initialPickups;
        currentEnemies = initialEnemies;
        livesLostInCurrentLevel = 0;
        

    }

    private int maxPickups = 3;
    private int minPickups = 1;
    private int maxEnemies = 5;
    private int minEnemies = 1;

    


}
