using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int hpPlayer;
    [SerializeField] private int hpMaxPlayer;

    public RectTransform hpBar;
    private Slider hpBarSlider;

    private PlayerCharacteristics _playerCharacteristics;

    private void Awake()
    {
        _playerCharacteristics = GetComponent<PlayerCharacteristics>();
        hpBarSlider = hpBar.GetComponent<Slider>();
    }

    private void Start()
    {
        SetHpMaxPlayer();
        UpdateUIHpBar();
        FullHealHp();
    }

    private void Update()
    {
        hpBarSlider.value = hpPlayer;
    }

    public void TakeHit(int damage)
    {
        hpPlayer -= damage;
    }

    public void SetHP(int count)
    {
        hpPlayer += count;
    }

    private void SetHpMaxPlayer()
    {
        hpMaxPlayer = _playerCharacteristics.GetVitality() * 25 + 100;
    }
    private void UpdateUIHpBar()
    {
        hpBar.sizeDelta = new Vector2(hpMaxPlayer, hpBar.sizeDelta.y); //обновление макс. значения полоски хп
        hpBarSlider.maxValue = hpMaxPlayer; //обновление макс. значения параметра слайдера
    }
    public void FullHealHp()
    {
        hpPlayer = hpMaxPlayer;
    }

}
