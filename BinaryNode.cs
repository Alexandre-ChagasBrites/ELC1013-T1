﻿namespace ELC1013_T1
{
    public sealed class AndNode : BinaryNode
    {
        public override sealed bool Eval(ref readonly Evaluator context)
        {
            return leftNode.Eval(in context) && rightNode.Eval(in context);
        }

        public override ReadOnlyMemory<char> PropositionOperator => " ^ ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => " e ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is AndNode && base.Equals(other);
        }
    }
    public sealed class OrNode : BinaryNode
    {
        public override sealed bool Eval(ref readonly Evaluator context)
        {
            return leftNode.Eval(in context) || rightNode.Eval(in context);
        }

        public override ReadOnlyMemory<char> PropositionOperator => " V ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => " ou ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is OrNode && base.Equals(other);
        }
    }
    public sealed class IfThenNode : BinaryNode
    {
        public override sealed bool Eval(ref readonly Evaluator context)
        {
            return !leftNode.Eval(in context) || rightNode.Eval(in context);
        }

        public override ReadOnlyMemory<char> PropositionOperator => " -> ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "se ".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => ", então ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is IfThenNode && base.Equals(other);
        }
    }
    public sealed class IfOnlyIfNode : BinaryNode
    {
        public override sealed bool Eval(ref readonly Evaluator context)
        {
            return leftNode.Eval(in context) == rightNode.Eval(in context);
        }

        public override ReadOnlyMemory<char> PropositionOperator => " <-> ".AsMemory();
        public override ReadOnlyMemory<char> GuessBefore => "se ".AsMemory();
        public override ReadOnlyMemory<char> GuessMiddle => ", e somente se ".AsMemory();

        public override bool Equals(PropositionNode other)
        {
            return other is IfOnlyIfNode && base.Equals(other);
        }
    }
}
