using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public static GameController instance;

  public StageData stageData;

  public GameObject _GuessWordPrefab;
  public Transform _GuessWordPanelParent;

  public List<GuessWord> _GuessWordList = new List<GuessWord>();

  private GuessWord current_guessword;
  private int current_focusletterIndex = 0;

  private void Awake()
  {
    if(instance != null)
    {
      return;
    }
    instance = this;
  }

  void Start()
  {
    InitializeWordle();
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
    if (Input.GetKeyUp(KeyCode.Delete))
    {
      RemoveGuessWordChar();
    }
  }

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
    current_guessword = _GuessWordList[0];
  }
  public void SetGuessWordCharByOrder(char guessChar)
  {
    SetGuessWordChar(guessChar);
    current_focusletterIndex++;
    if (current_focusletterIndex >= current_guessword._Letters.Count)
    {
      current_focusletterIndex = current_guessword._Letters.Count - 1;
    }
  }
  public void SetGuessWordChar(char guessChar)
  {
    current_guessword.SetLetterByOrder(guessChar, current_focusletterIndex);
  }
  public void RemoveGuessWordChar()
  {
    var letter = current_guessword._Letters[current_focusletterIndex];
    if(letter != null && letter.letter_char == '-')
    {
      current_focusletterIndex -= 1;
    }
    current_guessword.DeleteLetter(current_focusletterIndex);
  }

}