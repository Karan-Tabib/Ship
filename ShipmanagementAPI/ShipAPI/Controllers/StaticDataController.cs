using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Data;
using System.Xml.Linq;
namespace ShipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticDataController : ControllerBase
    {
        [HttpGet]
        [Route("Years")]
        public List<int> GetYears()
        {
            string filename = "D:\\My Project\\ShipmanagementAPI\\ShipAPI\\SasticFiles\\Years.xml";
            
            //DataSet ds = new DataSet();
            //DataReader dr = ds.ReadXml("D:\\My Project\\ShipmanagementAPI\\ShipAPI\\SasticFiles\\Years.xml");
            
            XElement xml =XElement.Load(filename);
            List<int> data =xml.Elements("year").Select(x => (int)x).ToList();
            //string[] json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);

            return data;
        }
    }
}
