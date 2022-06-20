using BananaParty.BehaviorTree;
using RussianLotto.Client;

namespace RussianLotto.Behavior
{
    public class RenderNode<TView, TModel> : BehaviorNode where TModel : IVisualization<TView>
    {
        private readonly TView _view;
        private readonly TModel _model;

        public RenderNode(TView view, TModel model)
        {
            _view = view;
            _model = model;
        }

        public override BehaviorNodeStatus OnExecute(long time)
        {
            _model.Visualize(_view);
            return BehaviorNodeStatus.Success;
        }

        public override string Name => base.Name + _view.GetType().Name;
    }
}
