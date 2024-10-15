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
                    return pn switch
                    {
                         PremiseNode premise => Eval(premise.node),
                          AtomicNode   an    => 1u == ((i >> atomics.IndexOf(an.name)) & 1u),
                             NotNode   un    => !Eval(  un.node),
                             AndNode   an    =>  Eval(  an.leftNode) && Eval(  an.rightNode),
                              OrNode   on    =>  Eval(  on.leftNode) || Eval(  on.rightNode),
                          IfThenNode  itn    => !Eval( itn.leftNode) || Eval( itn.rightNode),
                        IfOnlyIfNode ioin    =>  Eval(ioin.leftNode) == Eval(ioin.rightNode),
                        _ => throw new ArgumentException($"Invalid node type: {pn.GetType}"),
                    };
                }
            Skip:;
            }

            return isValid;
        }
    }
}
