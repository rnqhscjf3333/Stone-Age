using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grandFatherDialogue : baseDialogue
{
    public GameObject camera;
    public void AcceptButton()
    {
        dialoguePanel.gameObject.SetActive(false);
        camera.SetActive(true);
        GameObject grandfatherquest = GameObject.FindGameObjectWithTag("grandfatherquest");
        GameObject grandfather = GameObject.FindGameObjectWithTag("grandfather");

        if (grandfatherquest != null && grandfather != null)
        {
            StartDialogue(grandfatherquest, grandfather);
        }
        else
        {
            Debug.Log("������Ʈ�� ã�� ���� ��쿡 ���� ó��");
        }
    }
}
