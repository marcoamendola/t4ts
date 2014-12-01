using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TS
{
    public class ObservableMemberOutputAppender : OutputAppender<TypeScriptInterfaceMember>
    {
        public ObservableMemberOutputAppender(StringBuilder output, int baseIndentation, Settings settings)
            : base(output, baseIndentation, settings)
        {
        }

        public override void AppendOutput(TypeScriptInterfaceMember member)
        {
            AppendIndendation();

            bool isOptional = member.Optional || (member.Type is NullableType);
            
            if (member.Type is ArrayType)
            {
                var array = (ArrayType)member.Type;
                Output.AppendFormat("{0} = ko.observableArray<{1}>([])",
                    member.Name,
                    FixType(array.ElementType)
                );
            }
            else
            {
                Output.AppendFormat("{0} = ko.observable<{1}>()",
                    member.Name,
                    FixType(member.Type)
                );
            }
            
            Output.AppendLine(";");
        }

        string FixType(TypescriptType type)
        {
            if (type is BoolType)
            {
                if (Settings.CompatibilityVersion != null && Settings.CompatibilityVersion < new Version(0, 9, 0))
                    return "bool";
                else
                    return "boolean";
            }

            return type.ToString();
        }
    }
}
