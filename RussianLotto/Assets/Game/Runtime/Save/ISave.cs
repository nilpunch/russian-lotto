namespace RussianLotto.Save
{
    public interface ISave<TAbstract>
    {
        TAbstract Load();
        void Save(TAbstract instance);
    }
}
