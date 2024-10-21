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

            Evaluator e = new() { truthyness = 0, atomics = atomics };

            for (e.truthyness = 0; isValid && (e.truthyness < (1u << atomics.Count)); e.truthyness++)
            {
                for (int j = 0; j < premises.Count - 1; j++)
                    if (!premises[j].Eval(in e))
                        goto Skip;

                isValid = premises[premises.Count - 1].Eval(in e);
                Skip:;
            }

            return isValid;
        }
    }
}
