using UnityEngine;
using UnityEngine.UI;

public class InputToggler : MonoBehaviour
{
    [SerializeField] GameObject touchInput;
    [SerializeField] GameObject hintOverlay;
    bool showHintOverlayWhenUnpaused = false;

    [SerializeField] Button keyboardButton;
    [SerializeField] Button touchButton;
    [SerializeField] Sprite[] buttonSprites;
    Image keyboardButtonSprite;
    Image touchButtonSprite;

    void Awake()
    {
        keyboardButtonSprite = keyboardButton.GetComponent<Image>();
        touchButtonSprite = touchButton.GetComponent<Image>();

        keyboardButton.onClick.AddListener(() => SetInput(false));
        touchButton.onClick.AddListener(() => SetInput(true));
    }

    void Start()
    {
        // Set whether to use touch input by default
        SetInput(true);
    }

    void Update()
    {
        if (showHintOverlayWhenUnpaused && !Global.isGamePaused)
        {
            hintOverlay.SetActive(true);
            showHintOverlayWhenUnpaused = false;
        }

        // Clicking anywhere will hide the hint overlay
        if (hintOverlay.activeSelf && Input.GetMouseButtonDown(0))
        {
            hintOverlay.SetActive(false);
            showHintOverlayWhenUnpaused = false;
        }
    }

    void SetInput(bool useTouchInput)
    {
        Global.useTouchInput = useTouchInput;
        touchInput.SetActive(useTouchInput);
        UpdateButtonSprites(Global.useTouchInput);

        // Turning on touch input will show the hint overlay
        showHintOverlayWhenUnpaused = useTouchInput == true;
    }

    void UpdateButtonSprites(bool useTouchInput)
    {
        if (useTouchInput)
        {
            keyboardButtonSprite.sprite = buttonSprites[0];
            touchButtonSprite.sprite = buttonSprites[1];
        }
        else
        {
            keyboardButtonSprite.sprite = buttonSprites[1];
            touchButtonSprite.sprite = buttonSprites[0];
        }
    }
}
