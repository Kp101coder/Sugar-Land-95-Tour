using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Dhyan Vyas

public class InfoUIToggle : MonoBehaviour

{

    [SerializeField] private CanvasGroup infoCard;
    [SerializeField] private TextMeshProUGUI textComponent;

    public void OpenCard(string info) {
        infoCard.alpha = 1;
        infoCard.blocksRaycasts = true;
        textComponent.text = info;
    }

    public void CloseCard() {
        infoCard.alpha = 0;
        infoCard.blocksRaycasts = false;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) {
            CloseCard();
        }
    }
}
