using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float staminaPlayer;
    [SerializeField] private int staminaMaxPlayer;
    private int cooldownStaminaMultiplier;
    private bool _staminaHealing;

    public RectTransform staminaBar;
    private Slider staminaBarSlider;

    private PlayerCharacteristics _playerCharacteristics;

    private void Awake()
    {
        _playerCharacteristics = GetComponent<PlayerCharacteristics>();
        staminaBarSlider = staminaBar.GetComponent<Slider>();
    }

    private void Start()
    {
        SetStaminaMaxPlayer();
        SetCooldownStaminaMultiplier();
        UpdateUIStaminaBar();
        FullHealStamina();
        StartHealStamina();
    }

    private void Update()
    {
        HealStamina();
        staminaBarSlider.value = staminaPlayer;
    }

    private void SetStaminaMaxPlayer()
    {
        staminaMaxPlayer = _playerCharacteristics.GetStamina() * 20 + 150;
    }
    private void SetCooldownStaminaMultiplier()
    {
        cooldownStaminaMultiplier = _playerCharacteristics.GetStamina() * 5 + 30;
    }
    private void UpdateUIStaminaBar()
    {
        staminaBar.sizeDelta = new Vector2(staminaMaxPlayer, staminaBar.sizeDelta.y); //обновление макс. значения полоски хп
        staminaBarSlider.maxValue = staminaMaxPlayer; //обновление макс. значения параметра слайдера
    }
    public void FullHealStamina()
    {
        staminaPlayer = staminaMaxPlayer;
    }
    public void HealStamina()
    {
        if (staminaPlayer < staminaMaxPlayer && _staminaHealing)
        {
            staminaPlayer += cooldownStaminaMultiplier * Time.deltaTime;
        }
    }

    public void SpendStamina(float count)
    {
        staminaPlayer -= count;
    }

    public float GetStamina()
    {
        return staminaPlayer;
    }

    public void StopHealStamina()
    {
        _staminaHealing= false;
    }
    public void StartHealStamina()
    {
        _staminaHealing= true;
    }
}
