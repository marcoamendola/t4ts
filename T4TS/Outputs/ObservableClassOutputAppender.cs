using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4TS
{
    public class ObservableClassOutputAppender: OutputAppender<TypeScriptInterface>
    {
        private bool InGlobalModule { get; set; }

        public ObservableClassOutputAppender(StringBuilder output, int baseIndentation, Settings settings, bool inGlobalModule)
            : base(output, baseIndentation, settings)
        {
            this.InGlobalModule = inGlobalModule;
        }

        public override void AppendOutput(TypeScriptInterface tsInterface)
        {
            BeginInterface(tsInterface);

            AppendMembers(tsInterface);
            
            if (tsInterface.IndexedType != null)
                AppendIndexer(tsInterface);

            EndInterface();
        }

        private void AppendMembers(TypeScriptInterface tsInterface)
        {
            var appender = new ObservableMemberOutputAppender(Output, BaseIndentation + 4, Settings);
            foreach (var member in tsInterface.Members)
                appender.AppendOutput(member);
        }

        private void BeginInterface(TypeScriptInterface tsInterface)
        {
            AppendIndentedLine("/** Generated from " + tsInterface.FullName + " **/");

            if (InGlobalModule)
                AppendIndented("class " + tsInterface.Name + "Observable");
            else
                AppendIndented("export class " + tsInterface.Name + "Observable");

            if (tsInterface.Parent != null)
                Output.Append(" extends " + (tsInterface.Parent.Module.IsGlobal ? "" : tsInterface.Parent.Module.QualifiedName + ".") + tsInterface.Parent.Name);

            Output.AppendLine(" {");
        }

        private void EndInterface()
        {
            AppendIndentedLine("}");
        }

        private void AppendIndexer(TypeScriptInterface tsInterface)
        {
            AppendIndendation();
            Output.AppendFormat("    [index: number]: {0};", tsInterface.IndexedType);
            Output.AppendLine();
        }
    }
}
