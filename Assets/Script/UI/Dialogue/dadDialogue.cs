using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dadDialogue : baseDialogue
{
    public GameObject camera;
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject dadQuest = GameObject.FindGameObjectWithTag("dadquest");
        GameObject dad = GameObject.FindGameObjectWithTag("dad");

        if (dadQuest != null && dad != null)
        {
            StartDialogue(dadQuest, dad);
        }
        else
        {
            Debug.Log("������Ʈ�� ã�� ���� ��쿡 ���� ó��");
        }
    }
}
