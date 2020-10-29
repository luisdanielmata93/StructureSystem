using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureSystem.Model
{
    public class Material
    {
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                    _id = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                    _name = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                    _description = value;
            }
        }

        private double _dfm;
        public string Dfm
        {
            get
            {
                return _dfm.ToString("0.###");
            }
            set
            {
                _dfm = FormulaProcessor(value);
            }
        }

        private double _dvm;
        public string Dvm
        {
            get
            {
                return _dvm.ToString();
            }
            set
            {
                _dvm = FormulaProcessor(value);
            }
        }

        private double _em;
        public string Em
        {
            get
            {
                return _em.ToString();
            }
            set
            {
                _em = FormulaProcessor(value);
            }
        }

        private double _gm;
        public string Gm
        {
            get
            {
                return _gm.ToString();
            }
            set
            {
                _gm = FormulaProcessor(value);
            }
        }

        private double _vm;
        public string vm
        {
            get
            {
                return _vm.ToString();
            }
            set
            {
                _vm = FormulaProcessor(value);
            }
        }

        private double _pv;
        public string PV
        {
            get
            {
                return _pv.ToString();
            }
            set
            {
                _pv = FormulaProcessor(value);
            }
        }


        private double FormulaProcessor(string formula)
        {
            double result = 0.0;
            try
            {
                string operation = string.Empty;

                if (formula.Contains("Dfm"))
                    operation = formula.Replace("Dfm", Dfm);
                if (formula.Contains("Em"))
                    operation = formula.Replace("Em", Em);

                if (!string.IsNullOrEmpty(operation))
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("expression", typeof(string), operation);
                    DataRow row = table.NewRow();
                    table.Rows.Add(row);
                    result = double.Parse((string)row["expression"]);
                }

                result = Convert.ToDouble(formula);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

    }//end of class
}//end of namespace
