using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
  private Color initColor;

  public LetterStatus status
  {
    get { return currentStatus; }
    set
    {
      currentStatus = value;
      SetLetterColor(currentStatus);
    }
  }

  private void Start()
  {
    initColor = image_letterColor.color;
  }

  private void SetLetterColor(LetterStatus newStatus)
  {
    switch (status)
    {
      case LetterStatus.None:
        image_letterColor.color = new Color(0.2f, 0.2f, 0.2f);
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
    if (text_letter != null)
    {
      text_letter.text = ch.ToString().ToUpper();
    }
  }
  public void DeleteLetterChar()
  {
    letter_char = '-';
    if (text_letter != null)
    {
      text_letter.text = "";
    }
  }

  public void ResetLetter()
  {
    status = LetterStatus.Wait;
    image_letterColor.color = initColor;
  }

}
