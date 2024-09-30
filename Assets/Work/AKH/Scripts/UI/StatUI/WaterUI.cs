public class WaterUI : StatUI
{
    public override void AfterFindPlayer()
    {
        _playerStat = _player.waterCompo;
        base.AfterFindPlayer();
    }
}
