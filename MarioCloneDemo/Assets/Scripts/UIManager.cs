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

    // This is to store the total current points that the player has gained during the game.
    private int currentPoints = 0;

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

    // This method simply takes an int as an argument and then adds that passed int to the currentScore.
    // After that, it will update the UI element for displaying the score with the new current score.
    public void IncreaseScore(int points)
    {
        currentPoints += points;
        pointText.text = "x" + currentPoints;

        //SceneManager.LoadScene(0);
    }
}