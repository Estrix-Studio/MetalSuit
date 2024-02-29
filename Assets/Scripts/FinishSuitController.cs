using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSuitController : MonoBehaviour
{
    [SerializeField]
    GameObject finishBar, finishSuitLeftLeg, finishSuitRightLeg, finishSuitChest, finishSuitRightArm, finishSuitLeftArm, finishSuitHead, finishSuitDecal;

    [SerializeField]
    float suitProgress, requiredScore = 1000.0f;

    Vector3 finishBarBaseScale;

    // Start is called before the first frame update
    void Start()
    {
        suitProgress = 0.0f;
        finishBarBaseScale = finishBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //determines what parts of the suit to activate based on how much progress the player made, based on their score
    public void BuildSuit(float score)
    {
        suitProgress += score;

        float progresspercentage = requiredScore / suitProgress;
        finishBar.transform.localScale = new Vector3(finishBarBaseScale.x * progresspercentage, finishBarBaseScale.y, finishBarBaseScale.z);

        if(progresspercentage >= 0.15f)
        {
            finishSuitLeftLeg.SetActive(true);
            if(progresspercentage >= 0.3f)
            {  
                finishSuitRightLeg.SetActive(true);

                if(progresspercentage >= 0.45f)
                {
                    finishSuitChest.SetActive(true);

                    if(progresspercentage >= 0.55f)
                    {
                        finishSuitLeftArm.SetActive(true);

                        if(progresspercentage >= 0.65f)
                        {
                            finishSuitRightArm.SetActive(true);

                            if(progresspercentage >= 0.8f)
                            {
                                finishSuitHead.SetActive(true);

                                if(progresspercentage >= 1.0f)
                                {
                                    finishSuitDecal.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void SuitDestruction(int currentHealth, int maxHealth)
    {
        float healthPercentage = currentHealth / maxHealth;
        if(healthPercentage <= 80)
        {
            DestroyPart(finishSuitHead);
            if(healthPercentage <= 70)
            {
                DestroyPart(finishSuitLeftArm);
                if(healthPercentage <= 55)
                {
                    DestroyPart(finishSuitRightArm);
                    if (healthPercentage <= 40)
                    {
                        DestroyPart(finishSuitLeftLeg);
                        if(healthPercentage <=25)
                        {
                            DestroyPart(finishSuitRightLeg);
                            if (healthPercentage <= 0)
                            {
                                DestroyPart(finishSuitChest);
                            }
                        }
                    }
                }
            }
        }
    }

    public void DestroyPart(GameObject part)
    {
        part.SetActive(false);
    }
}
