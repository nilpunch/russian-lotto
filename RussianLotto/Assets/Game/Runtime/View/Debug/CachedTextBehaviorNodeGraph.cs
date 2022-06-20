using System;
using System.Collections.Generic;
using System.Text;
using BananaParty.BehaviorTree;

namespace RussianLotto.View
{
    public class CachedTextBehaviorNodeGraph : ITreeGraph<IReadOnlyBehaviorNode>
    {
        private readonly bool _lineBreaks;

        private readonly Dictionary<BehaviorNodeStatus, string> _statusToString = new()
        {
            { BehaviorNodeStatus.Idle, "" },
            { BehaviorNodeStatus.Failure, "" },
            { BehaviorNodeStatus.Success, "" },
            { BehaviorNodeStatus.Running, "" }
        };

        private readonly StringBuilder _stringBuilder = new();
        private string _indentation = string.Empty;
        private readonly Stack<int> _childrenToDraw = new();

        public CachedTextBehaviorNodeGraph(bool lineBreaks = false)
        {
            _lineBreaks = lineBreaks;
        }

        private int CurrentChildrenToDrawCount
        {
            get
            {
                if (_childrenToDraw.Count > 0)
                    return _childrenToDraw.Peek();

                return 0;
            }
        }

        public void StartChildGroup(int childCount)
        {
            if (_childrenToDraw.Count > 0)
            {
                _indentation += CurrentChildrenToDrawCount > 0 ? "│ " : "  ";
            }
            else
            {
                if (_lineBreaks)
                {
                    // Adding empty line after the root node.
                    _stringBuilder.Append('\n');
                    _stringBuilder.Append('│');
                }
            }

            _childrenToDraw.Push(childCount);
        }

        public void Write(IReadOnlyBehaviorNode behaviorNode)
        {
            if (_stringBuilder.Length > 0)
            {
                _stringBuilder.Append('\n');
                _stringBuilder.Append(_indentation);
                _stringBuilder.Append(CurrentChildrenToDrawCount > 1 ? "├╴" : "└╴");
            }

            _stringBuilder.Append($"{behaviorNode.Name} {_statusToString[behaviorNode.Status]}");

            if (_childrenToDraw.Count > 0)
            {
                _childrenToDraw.Push(_childrenToDraw.Pop() - 1);
            }
        }

        public void EndChildGroup()
        {
            _childrenToDraw.Pop();

            if (_lineBreaks)
            {
                // Adding empty line after last child node.
                if (CurrentChildrenToDrawCount > 0)
                {
                    _stringBuilder.Append('\n');
                    _stringBuilder.Append(_indentation);
                }
            }

            _indentation = _indentation.Substring(0, Math.Max(_indentation.Length - 2, 0));
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }

        public void Clear()
        {
            _indentation = string.Empty;
            _childrenToDraw.Clear();
            _stringBuilder.Clear();
        }
    }
}
