namespace RussianLotto.Client
{
    public class ClassicCardFactory : IFactory<ICard>
    {
        public ICard Create()
        {
            return new Card(null);
        }
    }
}
