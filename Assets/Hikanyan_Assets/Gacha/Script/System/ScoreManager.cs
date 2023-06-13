using System;
using UniRx;

[Serializable]
public class ScoreManager
{
    private IntReactiveProperty _score = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> Score => _score;

    private IntReactiveProperty _coin = new IntReactiveProperty(0);
    public IReadOnlyReactiveProperty<int> Coin => _coin;

    public void AddScore(int points)
    {
        _score.Value += points;
    }

    public void ResetScore()
    {
        _score.Value = 0;
    }

    public void AddCoin(int amount)
    {
        _coin.Value += amount;
    }

    public void ResetCoin()
    {
        _coin.Value = 0;
    }
}