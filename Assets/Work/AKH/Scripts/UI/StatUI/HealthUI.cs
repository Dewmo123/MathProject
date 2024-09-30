public class HealthUI : StatUI
{
    public override void AfterFindPlayer()
    {
        _playerStat = _player.healthCompo;
        base.AfterFindPlayer();
    }
}
