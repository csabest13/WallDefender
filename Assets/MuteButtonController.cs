using UnityEngine;
using UnityEngine.UI;

public class MuteButtonController : MonoBehaviour
{
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    private Button button;
    private Image buttonImage;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // Add a listener to the button to call the ToggleMute method when clicked
        button.onClick.AddListener(ToggleMute);

        // Initialize button image based on GameManager's mute state
        UpdateButtonImage();
    }

    private void ToggleMute()
    {
        // Toggle the mute state in GameManager
        MuteManager.Instance.ToggleMute();

        // Update the button image based on the new mute state
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        // Change the button image based on the mute state
        if (MuteManager.Instance.IsMuted)
        {
            buttonImage.sprite = muteSprite;
        }
        else
        {
            buttonImage.sprite = unmuteSprite;
        }
    }
}