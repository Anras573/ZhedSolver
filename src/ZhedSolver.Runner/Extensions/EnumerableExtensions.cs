using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ZhedSolver.Runner.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
    
        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }
    
    public static IEnumerable<IList<T>> Permutations<T>(this IEnumerable<T> sequence)
    {
        if (sequence == null) throw new ArgumentNullException(nameof(sequence));

        return _(); IEnumerable<IList<T>> _()
        {
            using var iter = new PermutationEnumerator<T>(sequence);

            while (iter.MoveNext())
                yield return iter.Current;
        }
    }
    
    static IEnumerable<Action> NestedLoops(this Action action, IEnumerable<int> loopCounts)
    {
        return _(); IEnumerable<Action> _()
        {
            var count = loopCounts
                .DefaultIfEmpty()
                .Aggregate((acc, x) => acc * x);

            for (var i = 0; i < count; i++)
                yield return action;
        }
    }
    
    sealed class PermutationEnumerator<T> : IEnumerator<IList<T>>
    {
        readonly IList<T> _valueSet;
        readonly int[] _permutation;
        readonly IEnumerable<Action> _generator;

        IEnumerator<Action> _generatorIterator;
        bool _hasMoreResults;

        IList<T>? _current;

        public PermutationEnumerator(IEnumerable<T> valueSet)
        {
            _valueSet = valueSet.ToArray();
            _permutation = new int[_valueSet.Count];
            // The nested loop construction below takes into account the fact that:
            // 1) for empty sets and sets of cardinality 1, there exists only a single permutation.
            // 2) for sets larger than 1 element, the number of nested loops needed is: set.Count-1
            _generator = NestedLoops(NextPermutation, Enumerable.Range(2, Math.Max(0, _valueSet.Count - 1)));
            Reset();
        }

        [MemberNotNull(nameof(_generatorIterator))]
        public void Reset()
        {
            _current = null;
            _generatorIterator?.Dispose();
            // restore lexographic ordering of the permutation indexes
            for (var i = 0; i < _permutation.Length; i++)
                _permutation[i] = i;
            // start a new iteration over the nested loop generator
            _generatorIterator = _generator.GetEnumerator();
            // we must advance the nested loop iterator to the initial element,
            // this ensures that we only ever produce N!-1 calls to NextPermutation()
            _generatorIterator.MoveNext();
            _hasMoreResults = true; // there's always at least one permutation: the original set itself
        }

        object IEnumerator.Current => Current;

        public IList<T> Current
        {
            get
            {
                Debug.Assert(_current is not null);
                return _current;
            }
        }
        
        public bool MoveNext()
        {
            _current = PermuteValueSet();
            // check if more permutation left to enumerate
            var prevResult = _hasMoreResults;
            _hasMoreResults = _generatorIterator.MoveNext();
            if (_hasMoreResults)
                _generatorIterator.Current(); // produce the next permutation ordering
            // we return prevResult rather than m_HasMoreResults because there is always
            // at least one permutation: the original set. Also, this provides a simple way
            // to deal with the disparity between sets that have only one loop level (size 0-2)
            // and those that have two or more (size > 2).
            return prevResult;
        }

        void IDisposable.Dispose() => _generatorIterator.Dispose();
        
        void NextPermutation()
        {
            // find the largest index j with m_Permutation[j] < m_Permutation[j+1]
            var j = _permutation.Length - 2;
            while (_permutation[j] > _permutation[j + 1])
                j--;

            // find index k such that m_Permutation[k] is the smallest integer
            // greater than m_Permutation[j] to the right of m_Permutation[j]
            var k = _permutation.Length - 1;
            while (_permutation[j] > _permutation[k])
                k--;

            (_permutation[j], _permutation[k]) = (_permutation[k], _permutation[j]);

            // move the tail of the permutation after the jth position in increasing order
            for (int x = _permutation.Length - 1, y = j + 1; x > y; x--, y++)
                (_permutation[x], _permutation[y]) = (_permutation[y], _permutation[x]);
        }
        
        IList<T> PermuteValueSet()
        {
            var permutedSet = new T[_permutation.Length];
            for (var i = 0; i < _permutation.Length; i++)
                permutedSet[i] = _valueSet[_permutation[i]];
            return permutedSet;
        }
    }
}
