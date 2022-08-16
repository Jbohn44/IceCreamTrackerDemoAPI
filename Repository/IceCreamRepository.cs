using Data.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels;
using System.Security.Cryptography.X509Certificates;
using Repository.Interfaces;

namespace Repository
{
    public class IceCreamRepository : IIceCreamRepository
    {
        private IceCreamDataContext _context;

        public IceCreamRepository(IceCreamDataContext context)
        {
            _context = context;
        }

        public async Task<List<DomainModels.IceCream>> getDataFeed(int id)
        {
            using (_context)
            {
                var iceCreams = await _context.IceCreams.Where(i => i.UserId == id)
                    .OrderByDescending(d => d.ReviewDate)
                    .Take(4)
                    .ToListAsync();
                return iceCreams.Select(Map).ToList();
            }
        }

        public async Task<List<DomainModels.IceCream>> getIceCreams(int id)
        {
            using (_context)
            {
                var iceCreams = await _context.IceCreams.Where(i => i.UserId == id).OrderByDescending(d => d.ReviewDate).ToListAsync();
                return iceCreams.Select(Map).ToList();
            }

        }
        public async Task<DomainModels.IceCream> getSingleIceCream(int id)
        {
            using (_context)
            {
                var iceCream = await _context.IceCreams.FirstOrDefaultAsync(i => i.IceCreamId == id);
                return Map(iceCream);
            }
        }
        public async Task<DomainModels.IceCream> postIceCream(DomainModels.IceCream iceCream)
        {
            using (_context)
            {
                var ratingList = new List<Data.DataModels.Rating>();
                var serviceList = new List<Data.DataModels.Service>();
                var ic = new Data.DataModels.IceCream 
                {
                    UserId = iceCream.UserId,
                    Location = iceCream.Location,
                    Business = iceCream.Business,
                    FlavorName = iceCream.FlavorName,
                    Comments = iceCream.Comments,
                    ReviewDate = iceCream.ReviewDate
                    
                };
                _context.IceCreams.Add(ic);
                await _context.SaveChangesAsync();

                ratingList.Add(new Data.DataModels.Rating 
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 1,
                    RatingValue = iceCream.OverAllRating.RatingValue
                });
                ratingList.Add(new Data.DataModels.Rating
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 2,
                    RatingValue = iceCream.FlavorRating.RatingValue
                });
                ratingList.Add(new Data.DataModels.Rating
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 3,
                    RatingValue = iceCream.CreaminessRating.RatingValue
                });
                ratingList.Add(new Data.DataModels.Rating
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 4,
                    RatingValue = iceCream.IcinessRating.RatingValue
                });
                ratingList.Add(new Data.DataModels.Rating
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 5,
                    RatingValue = iceCream.DensityRating.RatingValue
                });
                ratingList.Add(new Data.DataModels.Rating
                {
                    IceCreamId = ic.IceCreamId,
                    RatingTypeId = 6,
                    RatingValue = iceCream.ValueRating.RatingValue
                });

                _context.Ratings.AddRange(ratingList);
                await _context.SaveChangesAsync();

                if(iceCream.Services.Count > 0)
                {
                    foreach(var service in iceCream.Services)
                    {
                        serviceList.Add(new Data.DataModels.Service
                        {
                            ServiceTypeId = service.ServiceTypeId,
                            IceCreamId = ic.IceCreamId
                        });
                    }

                    _context.Services.AddRange(serviceList);
                    await _context.SaveChangesAsync();
                }
                return Map(ic);

            }

        }

        public async Task<DomainModels.IceCream> UpdateIceCream(DomainModels.IceCream iceCream)
        {
            using (_context)
            {
                var ic = await _context.IceCreams.FirstOrDefaultAsync(x => x.IceCreamId == iceCream.IceCreamId);
                ic.FlavorName = iceCream.FlavorName;
                ic.Location = iceCream.Location;
                ic.Business = iceCream.Business;
                ic.Comments = iceCream.Comments;
                ic.ReviewDate = iceCream.ReviewDate.Value.ToLocalTime();
                foreach(var rating in ic.Ratings)
                {
                    switch (rating.RatingTypeId)
                    {
                        case 1:
                            rating.RatingValue = iceCream.OverAllRating.RatingValue;
                            break;
                        case 2:
                            rating.RatingValue = iceCream.FlavorRating.RatingValue;
                            break;
                        case 3:
                            rating.RatingValue = iceCream.CreaminessRating.RatingValue;
                            break;
                        case 4:
                            rating.RatingValue = iceCream.IcinessRating.RatingValue;
                            break;
                        case 5:
                            rating.RatingValue = iceCream.DensityRating.RatingValue;
                            break;
                        case 6:
                            rating.RatingValue = iceCream.ValueRating.RatingValue;
                            break;
                    }
                }
                _context.Update(ic);
                await _context.SaveChangesAsync();

                if (iceCream.Services.Count == 0)
                {
                   if(ic.Services.Count > 0)
                    {
                        foreach(var s in ic.Services)
                        {
                            _context.Services.Remove(s);
                        }
                        await _context.SaveChangesAsync();
                    }
                }

                // loop through existing services and determine if services coming from domain are existing already
                if(iceCream.Services.Count > 0)
                {
                    var dataServiceTypeIds = ic.Services.Select(x => (int)x.ServiceTypeId).ToList();
                    var domainServiceTypeIds = iceCream.Services.Select(x => (int)x.ServiceTypeId).ToList();
                    var servicesToAdd = new List<Data.DataModels.Service>();
                    var servicesToDelete = new List<Data.DataModels.Service>();

                    foreach(var s in dataServiceTypeIds)
                    {
                        if (!domainServiceTypeIds.Contains(s))
                        {
                            _context.Services.Remove(ic.Services.FirstOrDefault(x => x.ServiceTypeId == s));
                            await _context.SaveChangesAsync();
                        }
                    }

                    foreach(var s in domainServiceTypeIds)
                    {
                        if (!dataServiceTypeIds.Contains(s))
                        {
                            _context.Services.Add(new Data.DataModels.Service { IceCreamId = ic.IceCreamId, ServiceTypeId = s });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                ic.Services = _context.Services.Where(x => x.IceCreamId == ic.IceCreamId).ToList();
                return Map(ic);
            }
        }

        public async Task Delete(int id)
        {
            using (_context)
            {
                var ic = await _context.IceCreams.FirstOrDefaultAsync(x => x.IceCreamId == id);
                foreach(var rating in ic.Ratings)
                {
                    _context.Ratings.Remove(rating);
                }
                await _context.SaveChangesAsync();
                foreach(var service in ic.Services)
                {
                    _context.Services.Remove(service);
                }
                await _context.SaveChangesAsync();
                _context.IceCreams.Remove(ic);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<DomainModels.IceCream>> GetOtherUserReviews(int userId)
        {
            var userReviews = await _context.IceCreams.Where(x => x.UserId != userId).Take(5).ToListAsync();

            return userReviews.Select(Map).ToList();
        }
        private DomainModels.IceCream Map(Data.DataModels.IceCream iceCream)
        {
            //datamodel not returning proxy on save for service.servicetype... gonna implement a switch possibly
            var serviceList = new List<DomainModels.Service>();
            foreach(var service in iceCream.Services)
            {
                // this is a verbose way to get the service type name but will do until I can get proxies to work
                string sName = "";
                switch (service.ServiceTypeId)
                {
                    case 1:
                        sName = "Dine-in";
                        break;
                    case 2:
                        sName = "Carry-out";
                        break;
                    case 3:
                        sName = "Drive-thru";
                        break;
                }
                serviceList.Add(new DomainModels.Service
                {
                    ServiceId = service.ServiceId,
                    ServiceTypeId = service.ServiceTypeId,
                    IceCreamId = iceCream.IceCreamId,
                    ServiceName = sName

                });
            }
            var ic = new DomainModels.IceCream
            {
                IceCreamId = iceCream.IceCreamId,
                UserId = iceCream.UserId,
                FlavorName = iceCream.FlavorName,
                Location = iceCream.Location,
                Business = iceCream.Business,
                Comments = iceCream.Comments,
                ReviewDate = iceCream.ReviewDate,
                OverAllRating = new DomainModels.Rating 
                { 
                    RatingType = "OverAll",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 1).RatingValue
                },
                FlavorRating = new DomainModels.Rating 
                {
                    RatingType = "Flavor",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 2).RatingValue
                },
                CreaminessRating = new DomainModels.Rating 
                {
                    RatingType = "Creaminess",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 3).RatingValue
                },
                IcinessRating = new DomainModels.Rating
                {
                    RatingType = "Iciness",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 4).RatingValue
                },
                DensityRating = new DomainModels.Rating 
                {
                    RatingType = "Density",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 5).RatingValue
                },
                ValueRating = new DomainModels.Rating
                {
                    RatingType = "Value",
                    RatingValue = iceCream.Ratings.FirstOrDefault(r => r.RatingTypeId == 6).RatingValue
                },
                Services = serviceList
                
            };

            return ic;
        }
        
    }
}
