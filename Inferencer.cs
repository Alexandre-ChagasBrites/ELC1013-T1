using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        if (premise is UnaryNode notNode && notNode.type == UnaryOperator.Not)
        {
            foreach( PropositionNode modusTollens in ModusTollens(notNode))
                yield return modusTollens;

            foreach (PropositionNode silogismoDisjuntivo in SilogismoDisjuntivo(notNode))
                yield return silogismoDisjuntivo;
        }

        if (premise is BinaryNode ifthenNode && ifthenNode.type == BinaryOperator.IfThen)
        {
            foreach (PropositionNode silogismoHipotetico in SilogismoHipotetico(ifthenNode))
                yield return silogismoHipotetico;
        }
    }

    private IEnumerable<PropositionNode> ModusPonens(PropositionNode premise)
    {
        return Propositions.OfType<BinaryNode>()
            .Where(node => node.type == BinaryOperator.IfThen && node.leftNode.Equals(premise))
            .Select(node => node.rightNode);
    }

    private IEnumerable<PropositionNode> ModusTollens(UnaryNode notNode)
    {
        return Propositions.OfType<BinaryNode>()
            .Where(node => node.type == BinaryOperator.IfThen && node.rightNode.Equals(notNode.node))
            .Select(node => new UnaryNode() { type = UnaryOperator.Not, node = node.leftNode });
    }

    private IEnumerable<PropositionNode> SilogismoHipotetico(BinaryNode ifthenNode)
    {
        return Propositions.OfType<BinaryNode>()
            .Where(node => node.type == BinaryOperator.IfThen && node.rightNode.Equals(ifthenNode.leftNode))
            .Select(node => new BinaryNode() { type = BinaryOperator.IfThen, leftNode = node.leftNode, rightNode = ifthenNode.rightNode });
    }

    private IEnumerable<PropositionNode> SilogismoDisjuntivo(UnaryNode notNode)
    {
        return Propositions.OfType<BinaryNode>()
            .Where(node => node.type == BinaryOperator.Or && node.leftNode.Equals(notNode.node))
            .Select(node => new UnaryNode() { type = UnaryOperator.Not, node = node.rightNode });
    }
}
