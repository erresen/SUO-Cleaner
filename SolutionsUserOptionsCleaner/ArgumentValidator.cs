using System;
using System.Collections.Generic;

namespace SolutionsUserOptionsCleaner
{
    class ArgumentValidator
    {
        public ArgumentValidator(IList<string> args)
        {
            Args = args ?? new List<string>();
            Rules = new List<Func<IList<string>, bool>>();
            Errors = new List<string>();
            ProcessErrorsOnInvalid = false;
        }
        public IList<string> Args { get; private set; }
        public bool ProcessErrorsOnInvalid { get; set; }
        public List<Func<IList<string>, bool>> Rules { get; set; }
        public List<string> Errors { get; private set; }
        private Action<string> _errorProcessingAction = (err) => Console.WriteLine(err);

        public bool Valid()
        {
            RunRules();
            return Errors.Count == 0;
        }

        public void ProcessErrors()
        {
            Errors.ForEach(_errorProcessingAction);
        }
        
        public void SetErrorProcessor(Action<string> processingAction)
        {
            _errorProcessingAction = processingAction;
        }

        private void RunRules()
        {
            foreach (var rule in Rules)
            {
                TryRule(rule);
            }
        }

        private void TryRule(Func<IList<string>, bool> rule)
        {
            try
            {
                if (RuleFails(rule))
                    AddError();
            }
            catch (Exception e)
            {
                AddError(e);
            }
        }

        private bool RuleFails(Func<IList<string>, bool> rule)
        {
            return !rule.Invoke(Args);
        }

        private void AddError()
        {
            Errors.Add("Rule returned false.");
        }

        private void AddError(Exception e)
        {
            Errors.Add($"Rule threw exception: {e.Message}");
        }
    }
}
