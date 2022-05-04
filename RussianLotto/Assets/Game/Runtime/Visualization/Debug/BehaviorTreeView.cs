using System;
using BananaParty.BehaviorTree;
using TMPro;
using UnityEngine;

namespace RussianLotto.View
{
    public class BehaviorTreeView : MonoBehaviour, ITreeGraph<IReadOnlyBehaviorNode>
    {
        [SerializeField] private TextMeshProUGUI _debugText = null;

        private CachedTextBehaviorNodeGraph _treeGraph;

        private void Awake()
        {
            _treeGraph = new CachedTextBehaviorNodeGraph();
        }

        public void StartChildGroup(int childCount)
        {
            _treeGraph.StartChildGroup(childCount);
        }

        public void Write(IReadOnlyBehaviorNode vertex)
        {
            _treeGraph.Write(vertex);
        }

        public void EndChildGroup()
        {
            _treeGraph.EndChildGroup();
        }

        public void Project()
        {
            _debugText.text = _treeGraph.ToString();
            _treeGraph.Clear();
        }
    }
}
