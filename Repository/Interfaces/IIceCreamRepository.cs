using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IIceCreamRepository
    {
        Task<List<DomainModels.IceCream>> getDataFeed(int id);
        Task<List<DomainModels.IceCream>> getIceCreams(int id);
        Task<DomainModels.IceCream> getSingleIceCream(int id);
        Task<DomainModels.IceCream> postIceCream(DomainModels.IceCream iceCream);
        Task<DomainModels.IceCream> UpdateIceCream(DomainModels.IceCream iceCream);
        Task Delete(int id);
        Task<List<DomainModels.IceCream>> GetOtherUserReviews(int userId);

    }
}
