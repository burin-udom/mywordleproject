using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
  public static GameController instance;

  public StageData stageData;

  public GameObject _GuessWordPrefab;
  public Transform _GuessWordPanelParent;

  public List<GuessWord> _GuessWordList = new List<GuessWord>();
  public List<Letter> _GuessLetters = new List<Letter>();

  private GuessWord current_guessword;
  private int current_focusGuessWordIndex = 0;
  private int current_focusletterIndex = 0;

  public string _RandomWord;

  public bool isGameStart = false;

  public UnityEvent<string> onGameStartingEvent;
  public UnityEvent<string> onGameEndingEvent;

  private void Awake()
  {
    if(instance != null)
    {
      return;
    }
    instance = this;
  }

  /*void Start()
  {
    StartGame();
  }
  void Update()
  {
    if(Input.GetKeyUp(KeyCode.A))
    {
      SetGuessWordCharByOrder('a');
    }
    if (Input.GetKeyUp(KeyCode.S))
    {
      SetGuessWordCharByOrder('s');
    }
    if (Input.GetKeyUp(KeyCode.D))
    {
      SetGuessWordCharByOrder('d');
    }
    if (Input.GetKeyUp(KeyCode.E))
    {
      SetGuessWordCharByOrder('e');
    }
    if (Input.GetKeyUp(KeyCode.Delete))
    {
      RemoveGuessWordChar();
    }
    if (Input.GetKeyUp(KeyCode.Space))
    {
      TryGuessWord();
    }
  }*/

  public void InitializeWordle()
  {
    for(int i = 0; i < stageData.stage_guesstimes; i++)
    {
      var guessWordPanel = Instantiate(_GuessWordPrefab, _GuessWordPanelParent);
      var guessWord = guessWordPanel.GetComponent<GuessWord>();
      guessWord.SetupLetterNumbers(stageData.stage_wordlength);
      _GuessWordList.Add(guessWord);
    }
  }

  public void StartGame()
  {
    isGameStart = true;
    InitializeWordle();
    int randomIndex = Random.Range(0, stageData.stage_wordspool.Count);
    _RandomWord = stageData.stage_wordspool[randomIndex];
    current_guessword = _GuessWordList[current_focusGuessWordIndex];
    onGameStartingEvent?.Invoke("word: " + _RandomWord);
  }

  public void InputLetterGuess(Letter letter)
  {
    if (!isGameStart) return;

    if(_GuessLetters.Count < _RandomWord.Length)
    {
      _GuessLetters.Add(letter);
      SetGuessWordCharByOrder(letter.letter_char);
    }
  }
  public void InputGuessChar(string guessCharString)
  {
    if (!isGameStart) return;

    if (guessCharString.Length == 1)
    {
      char guessChar = guessCharString[0];
      SetGuessWordCharByOrder(guessChar);
    }
  }
  private void SetGuessWordCharByOrder(char guessChar)
  {
    SetGuessWordChar(guessChar);
    current_focusletterIndex++;
    if (current_focusletterIndex >= current_guessword._Letters.Count)
    {
      current_focusletterIndex = current_guessword._Letters.Count - 1;
    }
  }
  private void SetGuessWordChar(char guessChar)
  {
    current_guessword.SetLetterByOrder(guessChar, current_focusletterIndex);
  }
  public void RemoveGuessWordChar()
  {
    if (!isGameStart) return;

    var letter = current_guessword._Letters[current_focusletterIndex];
    if (letter != null && letter.letter_char == '-')
    {
      current_focusletterIndex -= 1;
      if(current_focusletterIndex < 0)
      {
        current_focusletterIndex = 0;
      }
    }

    if(_GuessLetters.Count > 0)
    {
      _GuessLetters.RemoveAt(current_focusletterIndex);
    }
    current_guessword.DeleteLetter(current_focusletterIndex);
  }

  public void TryGuessWord()
  {
    if (!isGameStart) return;

    if (_RandomWord.Length == current_guessword._GuessWordString.Length)
    {
      bool isGuessingRight = current_guessword.CheckingGuessWord(_RandomWord, _GuessLetters);
      if (!isGuessingRight)
      {
        if (current_focusGuessWordIndex == stageData.stage_guesstimes - 1)
        {
          isGameStart = false;
          int guessTimes = (current_focusGuessWordIndex + 1);
          string summary = "GAME ENDING!!\n Guess Times: " + guessTimes.ToString() + "\nCorrect Word: " + _RandomWord.ToUpper();
          onGameEndingEvent?.Invoke(summary);
        }

        current_focusGuessWordIndex += 1;
        if (current_focusGuessWordIndex < _GuessWordList.Count)
        {
          current_guessword = _GuessWordList[current_focusGuessWordIndex];
        }
      }
      else
      {
        isGameStart = false;
        int guessTimes = (current_focusGuessWordIndex + 1);
        string summary = "GAME ENDING!!\n Guess Times: " + guessTimes.ToString() + "\nCorrect Word: " + _RandomWord.ToUpper();
        onGameEndingEvent?.Invoke(summary);
      }

      current_focusletterIndex = 0;
      _GuessLetters.Clear();
      _GuessLetters = new List<Letter>();
    }
  }

  public void GameReset()
  {
    current_focusGuessWordIndex = 0;
    foreach(GuessWord guessWord in _GuessWordList)
    {
      if(guessWord != null)
      {
        Destroy(guessWord.gameObject);
      }
    }
    _GuessWordList.Clear();

    var Letters = FindObjectsOfType<Letter>();
    foreach(Letter letter in Letters)
    {
      letter.ResetLetter();
    }
  }

}