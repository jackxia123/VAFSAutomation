using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace RBT.Universal.Keywords
{
    /// <summary>
    /// KeyWord Stimuli Start
    /// </summary>
    /// 
    [DataContract]
    public class DoLineManipulation : PairedDependentKeyword
    {
        
        public DoLineManipulation()
        {
            InitKeyword();
        }

        public DoLineManipulation(ObservableCollection<string> interruptLines,ObservableCollection<string> shortLines)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "interrupt_lines")).ValueList = interruptLines;
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "short_lines")).ValueList = shortLines;


        }
        public DoLineManipulation(ObservableCollection<string> interruptLines)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "interrupt_lines")).ValueList = interruptLines;

        }
        public DoLineManipulation(ObservableCollection<string> shortLines,string dummyHolder)
        {
            InitKeyword();
            ((ListDependentParameter)DependentParameters.First(x => x.Name == "short_lines")).ValueList = shortLines;

        }


        private void InitKeyword()
        {
            Name = "do_Line_manipulation";            
            Description = @"Do line manipulations (interrupt or short) in LabCar Breakout Box.

>>Use along with ‘reset_Line_manipulation’.


";
            Delay = 100;
            UsageLimit = 1;
            AddPairAfter("reset_Line_manipulation");
            PairType = KeywordPairType.One2One;
            string paraDesc = @"Specify the line names to be manipulated. (either interrupt or short)

>>Line names have to be the labels which are defined in LabCar mapping file or ‘Kxxx’.
>>‘xxx’ in ‘Kxxx’ is the number on the LabCar break out box.

e.g.1 test_sequence = @(…,’do_Line_manipulation ‘,…,’reset_Line_manipulation’,…)
         interrupt_lines  = @( ‘EVFL’ , ‘EVFR’ , ‘AVFL’ , ‘AVRR’ ) 
e.g.2 test_sequence = @(…,’do_Line_manipulation ‘,…,’reset_Line_manipulation’,…)
         short_lines       = @( ‘EVRR’ , ‘GND’)


";
            DependentParameters.Add(new ListDependentParameter("interrupt_lines", paraDesc, null, ""));
            DependentParameters.Add(new ListDependentParameter("short_lines", paraDesc, null, ""));

            ParametrizationType = DependentKeywordParameterizationType.SeperateParameterized;
            ParameterCompositionOption = DependentKeywordParameterCompositionOption.Disjunctive; 
            
            

        }

        public override bool ParametersValidated()
        {
            ListDependentParameter paraInterrupt = (ListDependentParameter)DependentParameters.First(x => x.Name == "interrupt_lines");
            ListDependentParameter paraShort= (ListDependentParameter)DependentParameters.First(x => x.Name == "short_lines");

            if ((paraInterrupt.ValueList != null) && (paraShort.ValueList != null))
            {
                if (paraInterrupt.ValueList.Count > 0 && paraShort.ValueList.Count == 2)
                {
                    return true;
                }
            }
            else
            {
                if ((paraInterrupt.ValueList != null) && paraInterrupt.ValueList.Count > 0)
                {
                    return true;
                }
                if ((paraShort.ValueList != null) && paraShort.ValueList.Count == 2)
                {
                    return true;
                }


            }
            return false;

        }

    }
}
