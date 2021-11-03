using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // LIVES
    public int lives = 3;
    public Lives firstLive;
    public Lives secondLive;
    public Lives thirdLive;

    // CHARACTERS
    public Character Lucie;
    public Character Victoria;
    public Character Fei;
    public Character Henrik;

    // COLLECTABLES
    public bool hasKey {get; private set;} = false;

    // TEXTS
    public Text textEndLvl;

    // MENUS
    public GameObject gameOverMenuUI;

    // AUDIO
    public AudioClip gameOverAudioClip;
    private AudioSource audioSource;

    // EXIT
    public SpriteRenderer exit;

    // SWAP SETTINGS
    private Dictionary<Character, int> indexByCharacter = new Dictionary<Character, int>();
    private float swapDelay = 3.0f;

    void Awake() {
        // Get and pass the AudioSource component to the audioSource attribute
        audioSource = GetComponent<AudioSource>();

        // Initialization of the indexByCharacter dictionary
        indexByCharacter.Add(Fei, 0);
        indexByCharacter.Add(Henrik, 1);
        indexByCharacter.Add(Victoria, 2);
        // indexByCharacter.Add(Lucie, 3);
    }

    void Start() {
        // We invoke the swapCharacters() method repeatedly according to the swapDelay value
        InvokeRepeating(nameof(swapCharacters), swapDelay, swapDelay);
    }

    //Check if the heroes are out of the map
    public void Update(){
        if(CharactersOutOfMap()){
            HeroesTakeDamage();
        }
    }

    // when a character find a collectable
    public void CollectableFound(Collectables collectable){
        hasKey = true;
        collectable.gameObject.SetActive(false);
        //Set the door's Color to black
        exit.color = Color.black;
        //TODO
    }

    public void DoorEnter(Door door){
        if(hasKey){
            // display the text "Well done"
           this.textEndLvl.enabled = true;
           DeactivateCharacters();
        }
    }

    // Set the display of lives and set GameOver when the heroes don't have any live left
    public void HeroesTakeDamage(){
        if(lives == 3){
            thirdLive.LooseLive();
        }
        else if(lives == 2){
            secondLive.LooseLive();
        }
        else if(lives == 1){
            firstLive.LooseLive();
            GameOver(); //The heroes have lost all their lives
        }
      
        lives--; //decrement the lives
    }

    // Game over : end of the level
    public void GameOver(){
        this.gameOverMenuUI.SetActive(true);
        audioSource.clip = gameOverAudioClip;
        audioSource.loop = true;
        audioSource.Play();
        DeactivateCharacters();
    }

    // Restart the current level
    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load the main menu scene
    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    //Test if one of the character is out of the map
    private bool CharactersOutOfMap(){
        if(Lucie.transform.position.y < -5 || Victoria.transform.position.y < -5 || Fei.transform.position.y < -5 || Henrik.transform.position.y < -5 ){
            return true;
        }
        return false;
    }

    private void DeactivateCharacters(){
        this.Lucie.gameObject.SetActive(false);
        this.Fei.gameObject.SetActive(false);
        this.Victoria.gameObject.SetActive(false);
        this.Henrik.gameObject.SetActive(false);
    }

    // Method to set the controls index for each character
    // The new index has to be different of the current one
    private void swapCharacters() {
        var indexList = new List<int>(){0, 1, 2};
        var allCharactersHaveANewIndex = false;
        int rndIndex;
        // As long as each character doesn't have a new index, we start the process again
        while(!allCharactersHaveANewIndex) {
            rndIndex = Random.Range(0, indexList.Count-1);
            // Process for the first character (Fei)
            if (indexByCharacter[Fei] != rndIndex) {
                indexByCharacter[Fei] = rndIndex;
                indexList.Remove(rndIndex);
                // Process for the last two characters
                if ((indexByCharacter[Henrik] != indexList[0]) && (indexByCharacter[Victoria] != indexList[1])) {
                    indexByCharacter[Henrik] = indexList[0];
                    indexByCharacter[Victoria] = indexList[1];
                } else {
                    indexByCharacter[Henrik] = indexList[1];
                    indexByCharacter[Victoria] = indexList[0];
                }
                allCharactersHaveANewIndex = true;
            }
        }
        // We update the controls of each character according to its new assigned index
        foreach (KeyValuePair<Character, int> entry in indexByCharacter) {
            entry.Key.GetComponentInChildren<TempMovement>().swapControls(entry.Value);            
        }
    }


}
