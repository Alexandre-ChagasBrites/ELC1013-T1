namespace ELC1013_T1
{
    public sealed class AndNode : BinaryNode
    {
        public AndNode(PropositionNode rhs, PropositionNode lhs) : base(rhs, lhs) { }

		public override PropositionPrecedence Precedence() => PropositionPrecedence.And;

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
        public OrNode(PropositionNode rhs, PropositionNode lhs) : base(rhs, lhs) { }

		public override PropositionPrecedence Precedence() => PropositionPrecedence.Or;

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
        public IfThenNode(PropositionNode rhs, PropositionNode lhs) : base(rhs, lhs) { }

		public override PropositionPrecedence Precedence() => PropositionPrecedence.IfThen;

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
        public IfOnlyIfNode(PropositionNode rhs, PropositionNode lhs) : base(rhs, lhs) { }

		public override PropositionPrecedence Precedence() => PropositionPrecedence.IfOnlyIf;

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
