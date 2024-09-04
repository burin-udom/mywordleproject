using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GuessWord : MonoBehaviour
{
  public GameObject _LetterPrefab;
  public Transform _Parent;

  public Dictionary<int, Letter> _Letters = new Dictionary<int, Letter>();
  public string _GuessWordString = "";
  void Start()
  {
    //SetupLetterNumbers(test_letterNumbers);
  }

  public void SetupLetterNumbers(int letterNumber)
  {
    _Letters = new Dictionary<int, Letter>();

    for(int i = 0; i < letterNumber; i++)
    {
      var letterObj = Instantiate(_LetterPrefab, _Parent);
      var letter = letterObj.GetComponent<Letter>();
      _Letters.Add(i, letter);
    }
  }

  public void SetLetterByOrder(char letterChar, int index)
  {
    if(_Letters.Count > index)
    {
      _Letters[index].SetLetterChar(letterChar);
      if(_GuessWordString.Length < _Letters.Count)
      {
        _GuessWordString += letterChar;
      }
      else
      {
        _GuessWordString = _GuessWordString.Substring(0, index) + letterChar;
      }
    }
  }
  public void DeleteLetter(int index)
  {
    if (index >= 0)
    {
      _Letters[index].DeleteLetterChar();
      _GuessWordString = _GuessWordString.Substring(0, index);
    }
  }

  public bool CheckingGuessWord(string word, List<Letter> guessLetters)
  {
    bool isGuessingRight = true;

    List<char> matchingLetters = word.ToCharArray().ToList();
    foreach( char letter in matchingLetters)
    {
      //Debug.Log("Checking Guess Char : " + letter);
    }
    //matchingLetters = word.ToCharArray().ToList();

    if(word.Length == _Letters.Count)
    {
      for(int i = 0; i < _Letters.Count; i++)
      {
        Letter letterCheck = _Letters[i];
        Letter letterInput = guessLetters[i];
        char letterWord = word[i];

        if (letterWord == letterCheck.letter_char)
        {
          //Debug.Log("Checking Guess Char[Exact] Remove: " + letterCheck.letter_char);
          letterCheck.status = LetterStatus.Exact;
          if(letterInput.status == LetterStatus.Wait || letterInput.status == LetterStatus.Exist)
          {
            letterInput.status = LetterStatus.Exact;
          }
          matchingLetters.Remove(letterCheck.letter_char);
        }
        else if (matchingLetters.Contains(letterCheck.letter_char))
        {
          //Debug.Log("Checking Guess Char[Exist] Remove: " + letterCheck.letter_char);
          isGuessingRight = false;
          letterCheck.status = LetterStatus.Exist;
          if (letterInput.status == LetterStatus.Wait)
          {
            letterInput.status = LetterStatus.Exist;
          }
          matchingLetters.Remove(letterCheck.letter_char);
        }
        else
        {
          //Debug.Log("Checking Guess Char[None] : " + letterCheck.letter_char);
          isGuessingRight = false;
          letterCheck.status = LetterStatus.None;
          if (letterInput.status == LetterStatus.Wait)
          {
            letterInput.status = LetterStatus.None;
          }
        }
      }
    }

    return isGuessingRight;
  }

}