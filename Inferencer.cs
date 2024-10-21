using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELC1013_T1
{
    public class Inferencer
    {
        private readonly List<PropositionNode> premisses;

        public List<PropositionNode> Inferred { get; private set; }

        public IEnumerable<PropositionNode> Propositions
        {
            get
            {
                foreach (PropositionNode proposition in premisses)
                    yield return proposition;
                foreach (PropositionNode proposition in Inferred)
                    yield return proposition;
            }
        }

        public Inferencer(IEnumerable<PropositionNode> premisses)
        {
            this.premisses = premisses.ToList();
            Inferred = new List<PropositionNode>();
        }

        public void Infer()
        {
            bool shouldStop = false;
            while (!shouldStop)
            {
                shouldStop = true;
                foreach (PropositionNode inferred in GetRules().ToList())
                {
                    if (!Inferred.Contains(inferred))
                    {
                        Inferred.Add(inferred);
                        shouldStop = false;
                    }
                }
            }
        }

        private IEnumerable<PropositionNode> GetRules()
        {
            foreach (PropositionNode modusPonens in ModusPonens())
                yield return modusPonens;

            foreach (PropositionNode modusTollens in ModusTollens())
                yield return modusTollens;

            foreach (PropositionNode silogismoHipotetico in SilogismoHipotetico())
                yield return silogismoHipotetico;

            foreach (PropositionNode silogismoDisjuntivo in SilogismoDisjuntivo())
                yield return silogismoDisjuntivo;

            foreach (PropositionNode deMorgan in DeMorgan())
                yield return deMorgan;
        }

        private IEnumerable<PropositionNode> ModusPonens()
        {
            var ifthenNodes = Propositions.OfType<IfThenNode>();
            foreach (PropositionNode conclusion in ifthenNodes
                .Where(ifthenNode => IsValid(ifthenNode.leftNode))
                .Select(ifthenNode => ifthenNode.rightNode))
                yield return conclusion;
        }

        private IEnumerable<PropositionNode> ModusTollens()
        {
            var ifthenNodes = Propositions.OfType<IfThenNode>();
            foreach (PropositionNode conclusion in ifthenNodes
                .Where(ifthenNode => IsValid(!ifthenNode.rightNode))
                .Select(ifthenNode => !ifthenNode.leftNode))
                yield return conclusion;
        }

        private IEnumerable<PropositionNode> SilogismoHipotetico()
        {
            var ifthenNodes = Propositions.OfType<IfThenNode>();
            foreach (IfThenNode leftNode in ifthenNodes)
            {
                foreach (PropositionNode conclusion in ifthenNodes
                    .Where(rightNode => leftNode.rightNode.Equals(rightNode.leftNode))
                    .Select(rightNode => new IfThenNode() { leftNode = leftNode.leftNode, rightNode = rightNode.rightNode }))
                    yield return conclusion;
            }
        }

        private IEnumerable<PropositionNode> SilogismoDisjuntivo()
        {
            var orNodes = Propositions.OfType<OrNode>();
            foreach (PropositionNode conclusion in orNodes
                .Where(orNode => IsValid(!orNode.leftNode))
                .Select(orNode => orNode.rightNode))
                yield return conclusion;
            foreach (PropositionNode conclusion in orNodes
                .Where(orNode => IsValid(!orNode.rightNode))
                .Select(orNode => orNode.leftNode))
                yield return conclusion;
        }

        private IEnumerable<PropositionNode> DeMorgan()
        {
            var notNodes = Propositions.OfType<NotNode>();
            foreach (PropositionNode anyNode in notNodes
                .Select(notNode => notNode.node))
            {
                if (anyNode is AndNode andNode)
                    yield return !andNode.leftNode | !andNode.rightNode;
                else if (anyNode is OrNode orNode)
                    yield return !orNode.leftNode & !orNode.rightNode;
            }
        }

        private bool IsValid(PropositionNode anyNode)
        {
            if (Propositions.Contains(anyNode))
                return true;
            if (anyNode is AndNode andNode)
                return IsValid(andNode.leftNode) && IsValid(andNode.rightNode);
            if (anyNode is OrNode orNode)
                return IsValid(orNode.leftNode) || IsValid(orNode.rightNode);
            foreach (AndNode andPropositions in Propositions.OfType<AndNode>())
            {
                if (anyNode.Equals(andPropositions.leftNode) || anyNode.Equals(andPropositions.rightNode))
                    return true;
            }
            return false;
        }
    }
}
