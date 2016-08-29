using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Classified.Models;

namespace Classified.Controllers
{
    public class ClassifiedController : ApiController
    {
        private static readonly  IClassifiedRepository Repository = new ClassifiedRepository();

        //get all items
        //api/classified/get
        public IEnumerable<Listing> GetAllItems()
        {
            return Repository.Retrieve();
        }

        //add an item
        //api/classified/post
        public HttpResponseMessage PostItem(Listing item)
        {
            item = Repository.Create(item);
            var response = Request.CreateResponse<Listing>(HttpStatusCode.Created, item);

            var uri = Url.Link("DefaultApi", new { userId = item.ClassifiedId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        //edit an item
        //api/classified/put
        public void PutItem(int classifiedId, Listing item)
        {
            item.ClassifiedId = classifiedId;
            if (!Repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //remove user
        //api/classified/delete
        public void DeleteItem(int classifiedId)
        {
            var item = Repository.GetItemById(classifiedId)     ;
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Repository.Delete(classifiedId);
        }

        //get an item by Id
        //api/classified/get
        public Listing GetListedItemById(int classifiedId)
        {
            var item = Repository.GetItemById(classifiedId);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
    }
}
