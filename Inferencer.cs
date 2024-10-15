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

            if (premise is NotNode notNode)
            {
                foreach (PropositionNode modusTollens in ModusTollens(notNode))
                    yield return modusTollens;

                foreach (PropositionNode silogismoDisjuntivo in SilogismoDisjuntivo(notNode))
                    yield return silogismoDisjuntivo;
            }

            if (premise is IfThenNode ifthenNode)
            {
                foreach (PropositionNode silogismoHipotetico in SilogismoHipotetico(ifthenNode))
                    yield return silogismoHipotetico;
            }
        }

        private IEnumerable<PropositionNode> ModusPonens(PropositionNode premise)
        {
            return Propositions.OfType<BinaryNode>()
                .Where(node => node is IfThenNode && node.leftNode.Equals(premise))
                .Select(node => node.rightNode);
        }

        private IEnumerable<PropositionNode> ModusTollens(NotNode notNode)
        {
            return Propositions.OfType<BinaryNode>()
                .Where(node => node is IfThenNode && node.rightNode.Equals(notNode.node))
                .Select(node => new NotNode() { node = node.leftNode });
        }

        private IEnumerable<PropositionNode> SilogismoHipotetico(BinaryNode ifthenNode)
        {
            return Propositions.OfType<BinaryNode>()
                .Where(node => node is IfThenNode && node.rightNode.Equals(ifthenNode.leftNode))
                .Select(node => new IfThenNode() { leftNode = node.leftNode, rightNode = ifthenNode.rightNode });
        }

        private IEnumerable<PropositionNode> SilogismoDisjuntivo(NotNode notNode)
        {
            return Propositions.OfType<BinaryNode>()
                .Where(node => node is OrNode && node.leftNode.Equals(notNode.node))
                .Select(node => new NotNode() { node = node.rightNode });
        }
    }
}
