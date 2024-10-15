using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELC1013_T1
{
    public class Inferencer
    {
        public List<PropositionNode> Propositions;

        public Inferencer()
        {
            Propositions = new List<PropositionNode>();
        }

        public void Infer(PropositionNode premise)
        {
            if (Propositions.Contains(premise))
                return;

            List<PropositionNode> inferred = GetRules(premise).ToList();
            Propositions.Add(premise);

            foreach (PropositionNode proposition in inferred)
                Infer(proposition);
        }

        private IEnumerable<PropositionNode> GetRules(PropositionNode premise)
        {
            foreach (PropositionNode modusPonens in ModusPonens(premise))
                yield return modusPonens;

            foreach (PropositionNode modusTollens in ModusTollens(premise))
                yield return modusTollens;

            foreach (PropositionNode silogismoHipotetico in SilogismoHipotetico(premise))
                yield return silogismoHipotetico;

            foreach (PropositionNode silogismoDisjuntivo in SilogismoDisjuntivo(premise))
                yield return silogismoDisjuntivo;
        }

        private IEnumerable<PropositionNode> ModusPonens(PropositionNode anyNode)
        {
            foreach (PropositionNode conclusion in Propositions.OfType<IfThenNode>()
                .Where(ifthenNode => ifthenNode.leftNode.Equals(anyNode))
                .Select(ifthenNode => ifthenNode.rightNode))
                yield return conclusion;
            if (anyNode is IfThenNode ifthenNode)
            {
                foreach (PropositionNode conclusion in Propositions
                 .Where(anyNode => ifthenNode.leftNode.Equals(anyNode))
                 .Select(anyNode => ifthenNode.rightNode))
                 yield return conclusion;
            }
        }

        private IEnumerable<PropositionNode> ModusTollens(PropositionNode anyNode)
        {
            if (anyNode is IfThenNode ifthenNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<NotNode>()
                    .Where(notNode => ifthenNode.rightNode.Equals(notNode.node))
                    .Select(_ => ifthenNode.rightNode))
                    yield return conclusion;
            }
            if (anyNode is NotNode notNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<IfThenNode>()
                    .Where(ifthenNode => ifthenNode.rightNode.Equals(notNode.node))
                    .Select(ifthenNode => new NotNode() { node = ifthenNode.leftNode }))
                    yield return conclusion;
            }
        }

        private IEnumerable<PropositionNode> SilogismoHipotetico(PropositionNode anyNode)
        {
            if (anyNode is IfThenNode leftNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<IfThenNode>()
                    .Where(rightNode => leftNode.rightNode.Equals(rightNode.leftNode))
                    .Select(rightNode => new IfThenNode() { leftNode = leftNode.leftNode, rightNode = rightNode.rightNode }))
                    yield return conclusion;
            }
            if (anyNode is IfThenNode rightNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<IfThenNode>()
                    .Where(leftNode => leftNode.rightNode.Equals(rightNode.leftNode))
                    .Select(leftNode => new IfThenNode() { leftNode = leftNode.leftNode, rightNode = rightNode.rightNode }))
                    yield return conclusion;
            }
        }

        private IEnumerable<PropositionNode> SilogismoDisjuntivo(PropositionNode anyNode)
        {
            if (anyNode is OrNode orNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<NotNode>()
                    .Where(notNode => orNode.leftNode.Equals(notNode.node))
                    .Select(_ => new NotNode() { node = orNode.rightNode }))
                    yield return conclusion;
                foreach (PropositionNode conclusion in Propositions.OfType<NotNode>()
                    .Where(notNode => orNode.rightNode.Equals(notNode.node))
                    .Select(_ => new NotNode() { node = orNode.leftNode }))
                    yield return conclusion;
            }
            if (anyNode is NotNode notNode)
            {
                foreach (PropositionNode conclusion in Propositions.OfType<OrNode>()
                    .Where(orNode => orNode.leftNode.Equals(notNode.node))
                    .Select(orNode => new NotNode() { node = orNode.rightNode }))
                    yield return conclusion;
                foreach (PropositionNode conclusion in Propositions.OfType<OrNode>()
                    .Where(orNode => orNode.rightNode.Equals(notNode.node))
                    .Select(orNode => new NotNode() { node = orNode.leftNode }))
                    yield return conclusion;
            }
        }
    }
}
