using Microsoft.AspNetCore.Mvc;


namespace DetailsForm.Controllers
{
    public class Details : Controller
    {

        private static List<Details> detailsList = new List<Details>();
        [HttpPost]
        public IActionResult Post([FromBody] Details detail)
        {
            detailsList.Add(detail);
            return Ok(detailsList);
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(detailsList);
        }

    }
}






