using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushiMushTower : MonoBehaviour
{
    public GameObject mushiTower;
    public GameObject questionMark;
    public GameObject[] mushi;
    public GameObject[] ps;

    public void EnableNextMushi() {
        for (int i = 0; i < mushi.Length; i++)
        {
            if (!mushi[i].activeInHierarchy)
            {
                mushi[i].SetActive(true);
                if (mushi[i].name != "MushiMushPivot")
                {
                    mushi[i].GetComponent<Animator>().SetTrigger("Idle");
                }
                else { mushi[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle"); }

                if (i == mushi.Length - 1) {
                    mushiTower.GetComponent<NotableObject>().IsCurrentlyDetectable = true;
                    foreach (GameObject p in ps)
                    {
                        p.SetActive(true);
                    }
                    questionMark.SetActive(true);
                    FindAnyObjectByType<CowDialogueManager>().cowCurrentDialogueIndex++;
                }

                break;
            }
            else
            {
                mushi[i].GetComponent<Animator>().SetTrigger("Idle");
            }
        }
    }
}
