using UnityEngine;

public class StructureType : MonoBehaviour
{
    public Spawnable type;
    [SerializeField] GameObject RangePreview = null;

    private void Start()
    {
        DeactivateRange();
    }


    public void ActivateRange()
    {
        if(RangePreview != null) RangePreview.SetActive(true);
    }

    public void DeactivateRange()
    {
        if (RangePreview != null) RangePreview.SetActive(false);
    }

}
