using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWord : MonoBehaviour
{
  public GameObject _LetterPrefab;
  public Transform _Parent;

  public Dictionary<int, Letter> _Letters = new Dictionary<int, Letter>();

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

      /*current_focusletterIndex++;
      if(current_focusletterIndex >= _Letters.Count)
      {
        current_focusletterIndex = _Letters.Count - 1;
      }*/
    }
  }
  public void DeleteLetter(int index)
  {
    if (index >= 0)
    {
      _Letters[index].DeleteLetterChar();

      /*current_focusletterIndex--;
      if (current_focusletterIndex < 0)
      {
        current_focusletterIndex = 0;
      }*/
    }
  }

}