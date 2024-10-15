using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELC1013_T1
{
    internal static class DirectTable
    {
        internal static bool IsValid(List<string> atomics, List<PremiseNode> premises)
        {
            bool isValid = true;

            for (ulong i = 0; isValid && (i < (1u << premises.Count)); i++)
            {
                for (int j = 0; j < premises.Count - 1; j++)
                    if (!Eval(premises[j]))
                        goto Skip;

                isValid = Eval(premises[premises.Count - 1]);

                bool Eval(PropositionNode pn)
                {
                    switch (pn)
                    {
                        case PremiseNode premise:
                            return Eval(premise.node);
                        case AtomicNode an:
                            return 1u == ((i >> atomics.IndexOf(an.name)) & 1u);
                        case UnaryNode un:
                            Debug.Assert(UnaryOperator.Not == un.type);
                            return !Eval(un.node);
                        case BinaryNode bn:
                            return bn.type switch
                            {
                                BinaryOperator.And      =>  Eval(bn.leftNode) && Eval(bn.rightNode),
                                BinaryOperator.Or       =>  Eval(bn.leftNode) || Eval(bn.rightNode),
                                BinaryOperator.IfOnlyIf =>  Eval(bn.leftNode) == Eval(bn.rightNode),
                                BinaryOperator.IfThen   => !Eval(bn.leftNode) || Eval(bn.rightNode),
                                _ => throw new ArgumentException("Invalid node type"),
                            };
                        default: throw new ArgumentException($"Invalid node type: {pn.GetType}");
                    }
                }
            Skip:;
            }

            return isValid;
        }
    }
}
