using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGameManager : MonoBehaviour
{
    public bool cow;
    public GameObject cowImg;
    public bool chicken;
    public GameObject chickenImg;
    public bool horse;
    public GameObject horseImg;
    public bool elephant;
    public GameObject elephantImg;
    [Space]
    public bool mushiMush;
    public GameObject mushiMushImg;
    public bool matsuneHiku;
    public GameObject matsuneHikuImg;
    public bool woopa;
    public GameObject woopaImg;
    public bool seal;
    public GameObject sealImg;
    [Space]
    public GameObject winStar;
    public AudioSource audioSource;
    public AudioClip winSound;
    public GameObject exclamationMarkBook;

    bool won;

    public void CheckWinCondition()
    {
        if (cow && chicken && horse && elephant && mushiMush && matsuneHiku && woopa && seal && !won)
        {
            winStar.SetActive(true);
            audioSource.PlayOneShot(winSound);
            exclamationMarkBook.SetActive(true);
            won = true;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void CheckCow()
    {
        cow = true;
        cowImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckChicken()
    {
        chicken = true;
        chickenImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckHorse()
    {
        horse = true;
        horseImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckElephant()
    {
        elephant = true;
        elephantImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckMushiMush()
    {
        mushiMush = true;
        mushiMushImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckMatsuneHiku()
    {
        matsuneHiku = true;
        matsuneHikuImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckWoopa()
    {
        woopa = true;
        woopaImg.SetActive(false);
        CheckWinCondition();
    }
    public void CheckSeal()
    {
        seal = true;
        sealImg.SetActive(false);
        CheckWinCondition();
    }
}