using UnityEngine;

public class FinishSuitController : MonoBehaviour
{
    [SerializeField] private GameObject finishBar,
        finishSuitLegs,
        finishSuitChest,
        finishSuitRightArms,
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
            case >= 0.45f:
                finishSuitChest.SetActive(true);
                break;
        }
    }

    public void DestroyPart(GameObject part)
    {
        part.SetActive(false);
    }
}