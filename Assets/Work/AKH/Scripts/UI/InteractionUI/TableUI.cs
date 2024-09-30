using UnityEngine;

public class TableUI : InteractionUI
{
    [SerializeField] private SpriteRenderer _houseRenderer;
    [SerializeField] private BoxCollider2D _houseCollider;
    private ClothUI _coreCloth;
    private ItemSO _leather;
    private ItemSO _wood;
    private ItemSO _rock;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        _coreCloth = GetComponentInChildren<ClothUI>();
        _leather = GameManager.instance.GetItemSO("°¡Á×");
        _wood = GameManager.instance.GetItemSO("³ª¹«");
        _rock = GameManager.instance.GetItemSO("µ¹");
    }
    public void ChangeCloth(ClothSO cloth)
    {
        if (cloth != GameManager.instance.CurCloth.Value && _leather.cnt.Value >= cloth.leatherCount)
        {
            _leather.cnt.Value -= cloth.leatherCount;
            _coreCloth.SetCurCloth(cloth);
        }
    }
    public void ChangeHouse(HouseSO house)
    {
        if (house != GameManager.instance.CurHouse.Value && house.woodCount <= _wood.cnt.Value && house.rockCount <= _rock.cnt.Value)
        {
            _wood.cnt.Value -= house.woodCount;
            _rock.cnt.Value -= house.rockCount;
            GameManager.instance.SetHouseSO(house);
            _houseRenderer.sprite = house.houseImage;
            _houseCollider.size = house.colliderSize;
        }
    }
    public override void AddDic()
    {
        InteractionManager.instance.InteractionUIDic.Add(MyType, this);
    }
}
