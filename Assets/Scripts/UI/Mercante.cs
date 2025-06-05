using UnityEngine;

public class Mercante : MonoBehaviour
{
    [SerializeField] private GameObject negozio;

    public void ClickMercante()
    {
        negozio.SetActive(!negozio.activeSelf);
    }
}
