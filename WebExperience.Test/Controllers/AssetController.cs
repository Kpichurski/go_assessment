using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebExperience.Test.DataTransferObject;
using WebExperience.Test.Models;

namespace WebExperience.Test.Controllers
{
    public class AssetController : ApiController
    {
        // TODO:
        // Create an API controller via REST to perform all CRUD operations on the asset objects created as part of the CSV processing test
        // Visualize the assets in a paged overview showing the title and created on field
        // Clicking an asset should navigate the user to a detail page showing all properties
        // Any data repository is permitted
        // Use a client MVVM framework


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getasset/")]
        public HttpResponseMessage GetAsset(int pageindex, int pagesize)
        {
            using (DBEntities entities = new DBEntities())
            {
                var assetdata = entities.Asset.OrderBy(x => x.AssetId).Skip(pageindex).Take(pagesize).ToList();

                if (assetdata == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                var asset = new List<Asset>();

                return Request.CreateResponse(HttpStatusCode.OK, new { assetdata, totalcount =  entities.Asset.Count()});
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("asset/{id}")]
        public HttpResponseMessage Asset(string id)
        {

            using (DBEntities entities = new DBEntities())
            {
                if (id == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var assetdata = entities.Asset.FirstOrDefault(x => x.AssetId == id);

                if (assetdata == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return  Request.CreateResponse(HttpStatusCode.OK, new List<Asset> { assetdata });
            }
        }

        [System.Web.Http.Route("asset/update/{Id}")]
        public async Task<HttpResponseMessage> UpdateAssetAsync(string Id, [FromBody] AssetDto dto)
        {

            using (DBEntities entities = new DBEntities())
            {
                if (Id == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var asset = entities.Asset.FirstOrDefault(x => x.AssetId == Id);

                if (asset == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                asset.Country = dto.Country;
                asset.Email = dto.Email;
                asset.MimeType = dto.MimeType;
                asset.Description = dto.Description;
                asset.FileName = dto.FileName;

                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, asset);
            }
        }

        public async Task<HttpResponseMessage> DeleteAssetAsync(string Id)
        {

            using (DBEntities entities = new DBEntities())
            {
                if (Id == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                var asset = entities.Asset.FirstOrDefault(x => x.AssetId == Id);

                if (asset == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                entities.Asset.Remove(asset);

                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK, asset);

            }
        }


        public async Task<HttpResponseMessage> CreateAssetAsync([FromBody] AssetDto dto)
        {
            using (DBEntities entities = new DBEntities())
            {
                if (dto == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                entities.Asset.Add(new Asset
                {
                    AssetId = Guid.NewGuid().ToString(),
                    Country = dto.Country,
                    Email = dto.Email,
                    MimeType = dto.MimeType,
                    FileName = dto.FileName,
                    Description = dto.Description
                });

                await entities.SaveChangesAsync();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

    }
}
