using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using AssemblyCSharp.Assets.MusicGame;

public class UIView : MonoBehaviour
{
    public GameObject startTips;
    public GameObject decisionResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        startTips.SetActive(false);
        Destroy(startTips);
    }

    public void ShowDecisionResult(DecisionResult result)
    {
        foreach (Transform child in decisionResult.transform)
        {
            child.gameObject.SetActive(false);
        }
        switch (result)
        {
            case DecisionResult.Perfect:
                decisionResult.transform.Find("PERFECT").gameObject.SetActive(true);
                break;
            case DecisionResult.Great:
                decisionResult.transform.Find("GREAT").gameObject.SetActive(true);
                break;
            case DecisionResult.Good:
                decisionResult.transform.Find("GOOD").gameObject.SetActive(true);
                break;
            case DecisionResult.Bad:
                decisionResult.transform.Find("BAD").gameObject.SetActive(true);
                break;
            case DecisionResult.Miss:
                decisionResult.transform.Find("MISS").gameObject.SetActive(true);
                break;
        }
    }
}
