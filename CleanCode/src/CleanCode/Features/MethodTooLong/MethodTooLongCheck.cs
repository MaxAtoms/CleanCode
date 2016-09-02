using CleanCode.Resources;
using CleanCode.Settings;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.MethodTooLong
{
    public class MethodTooLongCheck : MonoValueCheck<IMethodDeclaration, int>
    {
        public MethodTooLongCheck(IContextBoundSettingsStore settingsStore)
            : base(settingsStore)
        {
        }

        protected override void ExecuteCore(IMethodDeclaration constructorDeclaration, IHighlightingConsumer consumer)
        {
            var maxLength = Value;

            var statementCount = constructorDeclaration.CountChildren<IStatement>();
            if (statementCount > maxLength)
            {
                var highlighting = new Highlighting(Warnings.Warning_MethodTooLong, constructorDeclaration.GetNameDocumentRange());
                consumer.AddHighlighting(highlighting);
            }
        }

        protected override int Value
        {
            get { return SettingsStore.GetValue((CleanCodeSettings s) => s.MethodTooLongMaximum); }
        }

        protected override bool IsEnabled
        {
            get { return SettingsStore.GetValue((CleanCodeSettings s) => s.MethodTooLongEnabled); }
        }
    }
}