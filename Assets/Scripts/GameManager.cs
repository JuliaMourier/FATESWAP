using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    // SCENE NAME
    private string levelName;

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

    // STARS
    private int currentStarsNumber = 0;
    public Image starSliderImage;
    public TextMeshProUGUI countdownText;
    public int countdownTime;

    // MENUS
    public GameObject gameOverMenuUI;
    public GameObject endOfLevelMenuUI;

    public AudioSource swapSourceSound;
    public MainMenu mainMenu;
    // AUDIO
    public AudioClip gameOverAudioClip;
    public AudioClip endOfLevelAudioClip;
    private AudioSource audioSource;
    public AudioSource noteSound;
    public AudioSource keySound;

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
    public bool multi = false;

    void Awake() {
        // Get and pass the AudioSource component to the audioSource attribute
        audioSource = GetComponent<AudioSource>();
        levelName = SceneManager.GetActiveScene().name;

        solo = !Lucie.gameObject.activeInHierarchy;
        // Initialization of the indexByCharacter dictionary
        if(!multi){
            indexByCharacter.Add(Henrik, 0);
            indexByCharacter.Add(Fei, 1);
            indexByCharacter.Add(Victoria, 2);
            indexByCharacter.Add(Lucie, 3);
        }
        else {
            Henrik = GameObject.Find("Henrik(Clone)").GetComponent<Character>();
            Victoria = GameObject.Find("Victoria(Clone)").GetComponent<Victoria>();
            Lucie = GameObject.Find("Lucie(Clone)").GetComponent<Character>();
            Fei = GameObject.Find("Fei(Clone)").GetComponent<Fei>();
        }
        
        if (!solo)
        {
            sliderSwap.maxValue = swapDelay;
        }
        if(solo){
            sliderSwap.gameObject.SetActive(false);
        }
    }

    void Start() {
        // We invoke the swapCharacters() method repeatedly according to the swapDelay value
        StartCoroutine(CountdownTimerToNull());

        if (!solo && !multi)
        {   
            InvokeRepeating(nameof(swapCharacters), swapDelay, swapDelay); 
        // Coroutine that handles the timer 
       
            //InvokeRepeating(nameof(swapCharacters), swapDelay, swapDelay);
        }
    }

    //Check if the heroes are out of the map
    public void Update(){
        if(CharactersOutOfMap()){
            HeroesTakeDamage();
        }

        // While the fill amount of the star slider is not null, we decrement it every second
        if (starSliderImage.fillAmount > 0) {
            starSliderImage.fillAmount -= 1.0f / countdownTime * Time.deltaTime;
        }
        
       
        //Gives the position between the initial and final position to make a smooth transition
        if (!solo && !isGameOver)
        {
            sliderSwap.value = elapsedTime;
            elapsedTime += Time.deltaTime; //increment the time
        }
    }

    // when a character find a collectable
    public void CollectableFound(Collectables collectable)
    {
        if (collectable.gameObject.name == "Key")
        {
            keySound.Play();
            hasKey = true;
            key.color = Color.white;
            collectable.gameObject.SetActive(false);
            //Set the door's Color to black
            exit.color = Color.black;
        }
        if (collectable.gameObject.name == "Note")
        {
            noteSound.Play();
            note.color = Color.white;
            collectable.gameObject.SetActive(false);
            //string scene = levelName;
            //mainMenu.Cadenas(scene);
        }
    }

    public void NoteFound(){
        note.color = Color.white;
        currentStarsNumber++;
    }

    IEnumerator CountdownTimerToNull() {
        int remainingTime = countdownTime;
        while (remainingTime > 0) {
            countdownText.text = timeToString(remainingTime);
            yield return new WaitForSeconds(1f);
            remainingTime--;         
        }
        countdownText.text = timeToString(remainingTime);
    }

    // This method returns a given time (in second) to a formatted string with minutes and seconds
    private string timeToString(int time) {
        int minutes = 0;
        int seconds = 0;
        string secondsToString;
        // Calculates the minutes and seconds according to the time value
        if (time > 60) {
            minutes = time/60;
            seconds = time%60;
        } else {
            seconds = time;
        }
        // Add the character '0' before the seconds number if it is lower than 10
        if (seconds < 10) {
            secondsToString = "0" + seconds;
        } else {
            secondsToString = seconds.ToString();
        }
        // Returns the string result
        return minutes + ":" + secondsToString;
    }

    public void WinTheGame(Door door) {
        if (hasKey) {
            Time.timeScale = 0f;
            currentStarsNumber++;
            if (starSliderImage.fillAmount > 0) {
                currentStarsNumber++;
            }
            // If the current stars number for level is greater than the saved record (for this level), we update it
            if (currentStarsNumber > PlayerPrefs.GetInt("Stars" + levelName)) {
                PlayerPrefs.SetInt("Stars" + levelName, currentStarsNumber);
            }

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
        currentStarsNumber = 0;
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
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Restart the current level
    public void RestartLevel() {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
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
            if (!solo && !multi)
            {
                foreach (KeyValuePair<Character, int> entry in indexByCharacter)
                {
                    entry.Key.GetComponentInChildren<TempMovement>().swapControls(entry.Value);
                }
            }
        }

    }
    
        




}
