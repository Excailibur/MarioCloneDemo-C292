using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // This here creates a static instance of the UIManager class.
    // What that means is that it will be accessible by any class in the entire game,
    // without having to manually create fields and assign them to refer to a specific instance of UIManager./
    // The way it's used in this class is what's called the "Singleton Programming Pattern" or "Singleton Design Pattern"
    // or more simply, the "Singleton Pattern".
    public static UIManager Instance;

    // We're declaring a field here to reference our UI text object in the scene so we can update it.
    [SerializeField] TextMeshProUGUI pointText;

    // UI object for coin count.
    [SerializeField] TextMeshProUGUI coinText;

    // Field to store a reference to the timer display.
    [SerializeField] TextMeshProUGUI timerText;

    // Field to store a reference to the winning message text.
    // NOTE: Make sure that this object is disabled before the game starts.
    // You can do this by disabling it through code in Start() or you can just
    // uncheck the little checkbox in the Inspector for this object next to the name at the very top.
    // The official terms are Active and Inactive.
    [SerializeField] TextMeshProUGUI winText;

    // This field stores a reference to the restart button to restart the game after winning.
    [SerializeField] GameObject restartButton;

    // This field stores a reference to the End Game button.
    [SerializeField] GameObject endButton;

    // This is to store the total current points that the player has gained during the game.
    private int currentPoints = 0;

    // This is to store how many coins have been picked up.
    private int currentCoins = 0;

    // We'll use this to set how many seconds the player has to finish the level.
    [SerializeField] float levelTimeLimit = 400f;

    // Remember, Awake() is called before Start(). This is a good place to do any setup for things that are references in other scripts in Start().
    // So because our "Instance" field is static and will/could be used by any class, anywhere, at any time,
    // it makes sense to make sure it's configured in Awake() so that it's correctly set up for any class that wants to use it later.
    // You could do this in Start() but what if another script wanted to access this and manipulate the UI in Start()?
    // You don't have (easy) control over which script will call its Start() method first.
    // If this was in Start() and it got called AFTER another script tried to reference it in that script's Start(), it would throw an error.
    // Therefore, Awake() is safer to use in this case.
    void Awake()
    {
        // First, we check to see if there is already an instance of this class being stored by our Instance field.
        // We do this by checking if it's null, or empty.
        if (Instance == null)
        {
            // If Instance is in fact null and nothing has been assigned to it yet,
            // we will make Instance refer to THIS specific instance of this script.
            Instance = this;
        }
        // If Instance has already been set to a specific instance of the UIManager class...
        else
        {
            // We will simply destroy the gameObject with any additional copies of this script on it.
            // This ensures that there is only ever ONE instance of the UIManager script and it's stored in our static field, Instance.
            Destroy(gameObject);
        }
        // If this doesn't make total sense, let's try to explain it.
        // Let's say there are two empty gameObjects in our scene, and both have this UIManager script on them.
        // Remember, Awake() isn't called on every single thing at the same time, but it runs through each Awake()
        // method in each script sequentially. So what would happen is immediately when the game starts, the Awake() method would be called on the
        // first gameObject with this UIManager script on it. It's the very beginning of the game, so we know Instance
        // hasn't been assigned a value yet, so it makes THIS gameObject Instance.
        // Then, the Awake() method for the second gameObject with this class on it would be called.
        // For the second one (and third, fourth, etc. etc.) when it runs the Awake() method, it checks,
        // and sees that Instance has already been set, so it just destroys itself.
        // After it has run through every single gameObject with the UIManager script on it, only the very first one checked
        // will still exist in the scene, and will also be what Instance is refering to.
        // All copies will be destroyed and not exist anymore in the scene. This is what a Singleton is: A globally accessible instance of a class,
        // where it's ensured there is only ONE copy of it. You might be asking, "Why not make the whole class static?".
        // Good question, if we did, then ALL the fields and methods would also have to be static, which means we couldn't have it
        // referring to a specific UI element to update.
    }

    // Update is called every frame.
    private void Update()
    {
        // First we'll subtract Time.deltaTime from the levelTimeLimit, this essentially acts like a
        // timer counting down seconds. Remember, at 60fps, one frame is 1/60th of a second.
        // So, 1/60 * 60 = 1 second.
        levelTimeLimit -= Time.deltaTime;
        // Round the time, convert it to an int, and store it in a local variable.
        int roundedTime = Mathf.RoundToInt(levelTimeLimit);
        // Update the timer display UI to show the remaining time in seconds.
        timerText.text = roundedTime.ToString();

        // Check the value of the timer, if it's 0, the level restarts.
        if (levelTimeLimit <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    // This method simply takes an int as an argument and then adds that passed int to the currentScore.
    // After that, it will update the UI element for displaying the score with the new current score.
    public void IncreaseScore(int points)
    {
        currentPoints += points;
        pointText.text = currentPoints.ToString();
    }

    // This increases and updates the UI for coins gathered.
    public void IncreaseCoins(int coins)
    {
        currentCoins += coins;
        coinText.text = "x" + currentCoins;
    }

    // This method is for winning the game.
    public void WinLevel()
    {
        // This pauses the game by setting the game speed to 0.
        // Note you can also slow down and speed up the game by using the Time.timeScale property.
        Time.timeScale = 0f;

        // Increase our score.
        IncreaseScore(500);

        // Next, we'll display the "You Win!" text object that was originally disabled.
        // We do this by setting the entire object to Active.
        // We need to access the gameObject that our TextMeshProUGUI component is attached to,
        // then call the SetActive() method on it to make it visible in the scene.
        winText.gameObject.SetActive(true);

        // We also need to display the end game and restart game button.
        // NOTE: It would probably be better to instead have the two buttons as children of the Win Text.
        // That way we could activate the winText game object, and it would also activate all children
        // and show the buttons as well, but I wanna show this way as well.
        restartButton.SetActive(true);
        endButton.SetActive(true);
    }

    // This method is for losing the game and restarting from the beginning.
    public void RestartLevel()
    {
        // This will simply reload the scene that corresponds with the passed index, so scene 0.
        // In this case, I only have one scene, so it's obviously scene 0.
        // If you have more than 1 scene, you need to make sure you're loading the correct one.
        // You can find the index of each scene, reorder them, and also add and remove scenes from the game build.
        // Go to File -> Build Settings. From there you can see all your scenes that will be included in the build,
        // and you can add more and move them around.
        SceneManager.LoadScene(0);
    }

    // This method is for exiting the game.
    public void ExitGame()
    {
        // This quits the game.
        // NOTE: This works when the game is actually built.
        // it won't quit the game while running it inside the editor.
        Application.Quit();
    }
}