namespace RussianLotto.Client
{
    public interface IVisualization<in TView>
    {
        void Visualize(TView view);
    }
}



