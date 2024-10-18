using Player.Storage;
using Score.Factories;
using UniRx;

namespace Score.Services
{
    public class ScoreService
    {
        private ScoreFactory _scoreFactory;

        public ScoreService(ScoreFactory scoreFactory)
        {
            _scoreFactory = scoreFactory;

            PlayerStorage.Instance.PlayerAdded.Subscribe(x => Create(x.ScoreChanged, x.Name));
        }

        public void Create(ReactiveProperty<int> scoreObservable,string name)
        {
            var score = _scoreFactory.Create();
            score.Initialize( scoreObservable, name);
        }

    }
}