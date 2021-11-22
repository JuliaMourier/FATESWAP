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

    public SpriteRenderer key;

    public SpriteRenderer note;

    // CHARACTERS
    public Character Lucie;
    public Character Victoria;
    public Character Fei;
    public Character Henrik;

    public List<Robot> listOfRobot;

    // COLLECTABLES
    public bool hasKey {get; private set;} = false;

    // MENUS
    public GameObject gameOverMenuUI;
    public GameObject endOfLevelMenuUI;

    public AudioSource swapSourceSound;

    // AUDIO
    public AudioClip gameOverAudioClip;
    public AudioClip endOfLevelAudioClip;
    private AudioSource audioSource;

    // EXIT
    public SpriteRenderer exit;

    public Slider sliderSwap;

    // SWAP SETTINGS
    private Dictionary<Character, int> indexByCharacter = new Dictionary<Character, int>();
    public float swapDelay = 15.0f;

    private float elapsedTime = 0.0f;

    private bool isGameOver = false; //boolean for the state of the game

    public Robot boss = null;

    public bool solo { get; private set; } = false;

    void Awake() {
        // Get and pass the AudioSource component to the audioSource attribute
        audioSource = GetComponent<AudioSource>();
        solo = !Lucie.gameObject.activeInHierarchy;        
        // Initialization of the indexByCharacter dictionary
        indexByCharacter.Add(Henrik, 0);
        indexByCharacter.Add(Fei, 1);
        indexByCharacter.Add(Victoria, 2);
        indexByCharacter.Add(Lucie, 3);
        if (!solo)
        {
        sliderSwap.maxValue = swapDelay;
        }
    }

    void Start() {
        // We invoke the swapCharacters() method repeatedly according to the swapDelay value
        if (!solo)
        {
            InvokeRepeating(nameof(swapCharacters), swapDelay, swapDelay);
        }
    }

    //Check if the heroes are out of the map
    public void Update(){
        if(CharactersOutOfMap()){
            HeroesTakeDamage();
        }

        //Gives the position between the initial and final position to make a smooth transition
        if (!solo)
        {
            sliderSwap.value = elapsedTime;
            elapsedTime += Time.deltaTime; //increment the time
        }
    }

    // when a character find a collectable
    public void CollectableFound(Collectables collectable){
        hasKey = true;
        key.color = Color.white;
        collectable.gameObject.SetActive(false);
        //Set the door's Color to black
        exit.color = Color.black;
    }

    public void NoteFound(){
        note.color = Color.white;
        //TODO : get star
    }

    public void WinTheGame(Door door) {
        if(hasKey) {
            Time.timeScale = 0f;
            // Display the end-of-level menu and play the associated audio clip
            this.endOfLevelMenuUI.SetActive(true);
            audioSource.clip = endOfLevelAudioClip;
            audioSource.loop = true;
            audioSource.Play();
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
        isGameOver = true;
        this.gameOverMenuUI.SetActive(true);
        audioSource.clip = gameOverAudioClip;
        audioSource.loop = true;
        audioSource.Play();
        DeactivateCharacters();
        DeactivateRobot();
    }

    // Load the next scene
    public void LoadNextScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Restart the current level
    public void RestartLevel() {
        isGameOver = false;
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

    private void DeactivateRobot(){
        foreach(Robot robot in listOfRobot){
            robot.Disable();
        }
    }

    // Method to set the controls index for each character
    // The new index has to be different of the current one

    private void swapCharacters() {
        
        //Reset the HUD slider for swap
        elapsedTime = 0.0f;
        if(!isGameOver){
            if(boss != null){
                boss.animator.SetTrigger("swap");
            }
            swapSourceSound.Play(); // Play the sound of swap
        
            List<int> indexList;
            var allCharactersHaveANewIndex = false;
            int rndIndex;
            int previousFeiIndex = indexByCharacter[Fei]; // We need this variable in case the second character process fails
            // As long as each character doesn't have a new index, we start the process again
            while(!allCharactersHaveANewIndex) {
                indexList = new List<int>(){0, 1, 2, 3};
                // Process for the first character (Fei)
                rndIndex = Random.Range(0, 4); // 0 included, 4 excluded
                if (previousFeiIndex != rndIndex) {
                    indexByCharacter[Fei] = rndIndex;
                    indexList.Remove(rndIndex);
                    // Process for the second character (Henrik)
                    rndIndex = Random.Range(0, 3); // 0 included, 3 excluded
                    if (indexByCharacter[Henrik] != indexList[rndIndex]) {
                        indexByCharacter[Henrik] = indexList[rndIndex];
                        indexList.RemoveAt(rndIndex);
                        // Process for the last two characters (Victoria and Lucie)
                        if ((indexByCharacter[Victoria] != indexList[0]) && (indexByCharacter[Lucie] != indexList[1])) {
                            indexByCharacter[Victoria] = indexList[0];
                            indexByCharacter[Lucie] = indexList[1];
                        } else {
                            indexByCharacter[Victoria] = indexList[1];
                            indexByCharacter[Lucie] = indexList[0];
                        }
                        allCharactersHaveANewIndex = true;
                    }
                }
            }
        
            // We update the controls of each character according to its new assigned index
            foreach (KeyValuePair<Character, int> entry in indexByCharacter) {
                entry.Key.GetComponentInChildren<TempMovement>().swapControls(entry.Value);            
            }
        }

    }
    
        




}
