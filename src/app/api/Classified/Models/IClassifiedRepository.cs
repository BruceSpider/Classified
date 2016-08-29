using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classified.Models
{
    public interface IClassifiedRepository
    {
        //CRUD
        Listing Create(Listing item);
        IEnumerable<Listing> Retrieve();
        bool Update(Listing item);
        bool Delete(int classifiedId);
        //More
        Listing GetItemById(int classifiedId);

        //Categories

        IEnumerable<Category> RetrieveAllCategories();
    }
}
