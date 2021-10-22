using System.Collections.Generic;

namespace SweetAndSavory.Models
{
  public class Treat
  {
    public Treat()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }

    public int TreatId {get; set;}
    public string TreatName {get; set;}
    public string Description {get; set;}

    public virtual ICollection<FlavorTreat> JoinEntities {get;}
  }
}