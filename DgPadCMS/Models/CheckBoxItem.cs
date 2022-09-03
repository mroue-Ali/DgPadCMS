using System.Collections.Generic;

namespace DgPadCMS.Models
{
    public class CheckBoxItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Term> terms { get; set; }

        public bool IsChecked { get; set; }
    }
}
