// i have used this script as ScriptableObject for questions, answers...data required for my puzzle huntter app

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AnswerData
{
    public string[] answers;
}
[CreateAssetMenu( fileName = "S1StringsData",menuName="ScriptableObjects",order=1)]
public class S1StringsData : ScriptableObject
{
    public string[] questions;
//    public string[] answers;
    public  List<AnswerData> answers;
    public Sprite[] images;
    public Sprite[] hints;
    public Sprite[] AnsExp;
}
