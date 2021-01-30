using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndReplay : MonoBehaviour
{
    private List<UserInput> inputList;
    private PlayerScript playerScript;
    public void Start()
    {
        inputList = new List<UserInput>();
        playerScript = gameObject.GetComponent<PlayerScript>();
    }

    public void Add(UserInput input)
    {
        inputList.Add(input);
    }

    public void PlayInputs()
    {
        StartCoroutine(ApplyInput(0));
    }

    private IEnumerator ApplyInput(int index)
    {
        if(index < inputList.Count) //input available
        {
            yield return new WaitForSeconds(inputList[index].delta);
            Invoke(inputList[index].action, 0f);
            StartCoroutine(ApplyInput(index+1));
        }
        else //No more inputs to play
        {
            playerScript.Freeze();
            yield return null;
        }
    }
}
