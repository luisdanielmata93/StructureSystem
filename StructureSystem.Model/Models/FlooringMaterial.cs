using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;


namespace StructureSystem.Model
{
    public class FlooringMaterial : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }//end of class
}//end of namespace
