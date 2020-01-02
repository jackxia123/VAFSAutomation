using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord ECU On
    /// </summary>
    /// 
    [Serializable]
    public class WaitForUser : DependentKeyword
    {
        public WaitForUser()
        {
            InitKeyword();
        }
        public WaitForUser(ObservableCollection<string> userComments)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "Text_for_user")).ValueList = userComments;
        }

        private void InitKeyword()
        {
            Name = "waitForUser";            
            Description = @"Suspend a sequence in the middle of execution and show a pop-up message on the screen. After press “OK” button it will resume the sequence. 
Can be used for semi automation test to stop a sequence and do some manipulations by manual.

";
            Delay = 0;
            UsageLimit = 255;

            DependentParameters.Add(new ListDependentParameter("Text_for_user", @"Give a message that will be displayed on a pop up window. 

e.g. test_sequence = @(…,’waitForUser’,…,’ waitForUser’,…)
       Text_for_user = @( ‘free comment1’ , ‘free comment2’ )

", null, "0"));

            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Unique;
            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;


        }

        public override bool ParametersValidated()
        {
            
            if (((ListDependentParameter)DependentParameters.First(x => x.Name == "Text_for_user")).ValueList.Count>0)
            {
                return true;

            }

            return false;

        }

       

    }
}
