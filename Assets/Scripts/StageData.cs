using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/WordleStage", order = 1)]
public class StageData : ScriptableObject
{
  public int stage_guesstimes = 6;
  public int stage_wordlength = 5;
  public List<string> stage_wordspool = new List<string>();
}
