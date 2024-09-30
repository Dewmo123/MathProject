public class HungryUI : StatUI
{
    public override void AfterFindPlayer()
    {
        _playerStat = _player.hungryCompo;
        base.AfterFindPlayer();
    }
}
