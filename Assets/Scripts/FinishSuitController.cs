using UnityEngine;

public class FinishSuitController : MonoBehaviour
{
    [SerializeField] private GameObject finishBar,
        finishSuitLegs,
        finishSuitChest,
        finishSuitArms,
        finishSuitHead,
        finishSuitDecal;

    [SerializeField] private float suitProgress, requiredScore = 1000.0f;

    private Vector3 _finishBarBaseScale;

    public void Start()
    {
        suitProgress = 0.0f;
        _finishBarBaseScale = finishBar.transform.localScale;
    }

    public void BuildSuit(float score)
    {
        suitProgress += score;

        var progresspercentage = requiredScore / suitProgress;
        finishBar.transform.localScale = new Vector3(_finishBarBaseScale.x * progresspercentage, _finishBarBaseScale.y,
            _finishBarBaseScale.z);

        switch (progresspercentage)
        {
            case >= 1.0f:
                finishSuitDecal.SetActive(true);
                break;
            case >= 0.8f:
                finishSuitHead.SetActive(true);
                break;
            case >= 0.6f:
                finishSuitArms.SetActive(true);
                break;
            case >= 0.45f:
                finishSuitChest.SetActive(true);
                break;
            case >= 0.25f:
                finishSuitLegs.SetActive(true);
                break;
        }
    }

    public void SuitDestruction(float currentHealth, float maxHealth)
    {
        var healthPercentage = currentHealth / maxHealth;
        Debug.Log(healthPercentage);
        switch (healthPercentage)
        {
            case <= 0:
                DestroyPart(finishSuitChest);
                break;
            case <= .25f:
                DestroyPart(finishSuitLegs);
                break;
            case <= .55f:
                DestroyPart(finishSuitArms);
                break;
            case <= .80f:
                DestroyPart(finishSuitHead);
                break;
        }
    }

    public void DestroyPart(GameObject part)
    {
        part.SetActive(false);
    }
}