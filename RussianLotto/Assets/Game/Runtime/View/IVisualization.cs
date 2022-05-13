namespace RussianLotto.View
{
    public interface IVisualization<in T>
    {
        void Visualize(T view);
    }
}
