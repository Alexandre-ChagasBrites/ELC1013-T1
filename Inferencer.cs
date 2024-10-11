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
        {
            return;
        }

        PropositionNode modusPonens = ModusPonens(premise);
        PropositionNode modusTollens = ModusTollens(premise);

        Propositions.Add(premise);

        if (modusPonens != null)
        {
            Infer(modusPonens);
        }

        if (modusTollens != null)
        {
            Infer(modusTollens);
        }
    }

    private PropositionNode ModusPonens(PropositionNode premise)
    {
        return Propositions.OfType<BinaryNode>()
            .Where(node => node.type == BinaryOperator.IfThen && node.leftNode.Equals(premise))
            .Select(node => node.rightNode)
            .FirstOrDefault();
    }

    private PropositionNode ModusTollens(PropositionNode premise)
    {
        if (premise is UnaryNode notNode && notNode.type == UnaryOperator.Not)
        {
            return Propositions.OfType<BinaryNode>()
                .Where(node => node.type == BinaryOperator.IfThen && node.rightNode.Equals(notNode.node))
                .Select(node => new UnaryNode() { type = UnaryOperator.Not, node = node.leftNode })
                .FirstOrDefault();
        }
        return null;
    }
}
