using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Utils
{
    public static class SequenceHelper 
    {
        public static void AddSequence(this Sequence thisSeq, Sequence seq)
        {
            thisSeq.AppendCallback(() => seq.Play());
        }

        public static void AddSequentialSequence(this Sequence thisSeq, Sequence seq, float interval)
        {
            thisSeq.AppendInterval(interval);
            thisSeq.AppendCallback(() => seq.Play());
        }

        public static Sequence SequentialSequence(List<Sequence> seq)
        {
            Sequence sequence = DOTween.Sequence();
            seq.ForEach(s => sequence.AddSequence(s));
            return sequence;
        }
        public static Sequence SimultaneousSequence(List<Sequence> sequenceCallbacks)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                sequenceCallbacks.ForEach(f => f.Play());
            });
            return sequence;
        }

        public static Sequence SequentialSequence(Func<Sequence> sequence)
        {
            if (sequence == null) return DOTween.Sequence();
            return SequentialSequence(sequence.GetInvocationList()
                .Select(f => f as Func<Sequence>)
                .Select(f => f()).ToList());
        }

        public static Sequence SimultaneousSequence(Func<Sequence> sequence)
        {
            if (sequence == null) return DOTween.Sequence();
            return SimultaneousSequence(sequence.GetInvocationList()
                .Select(f => f as Func<Sequence>)
                .Select(f => f()).ToList());
        }
    }
}