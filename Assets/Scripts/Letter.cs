using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LetterStatus
{
  Wait,
  None,
  Exist,
  Exact
}

public class Letter : MonoBehaviour
{
  [SerializeField]
  private Text text_letter;
  [SerializeField]
  private Image image_letterColor;

  public char letter_char;

  private LetterStatus currentStatus;

  public LetterStatus status
  {
    get { return currentStatus; }
    set
    {
      currentStatus = value;
      SetLetterColor(currentStatus);
    }
  }

  private void SetLetterColor(LetterStatus newStatus)
  {
    switch (status)
    {
      case LetterStatus.None:
        image_letterColor.color = Color.gray;
        break;
      case LetterStatus.Exist:
        image_letterColor.color = Color.yellow;
        break;
      case LetterStatus.Exact:
        image_letterColor.color = Color.green;
        break;
    }
  }

  public void SetLetterChar(char ch)
  {
    letter_char = ch;
    text_letter.text = ch.ToString().ToUpper();
  }
  public void DeleteLetterChar()
  {
    letter_char = '-';
    text_letter.text = "";
  }

}
