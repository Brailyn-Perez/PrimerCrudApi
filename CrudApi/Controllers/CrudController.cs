using CrudApi.Models;
using CrudApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrudController : Controller
    {
        private ConnectionDB connectionDB = new ConnectionDB();
        private DataProvider dataProvider = new DataProvider();

        //Obtenemos todas las personas de manera automatica
        [HttpGet]
        public ActionResult GetData()
        {
            return Ok(dataProvider.GetData());
        }
        //Obtenemos personas mediante su ID
        [HttpGet("{IDpersona}")]
        public ActionResult GetByID(int IDpersona)
        {
            (List<PersonaModel>, bool) GetByID = dataProvider.GetByID(IDpersona);
            if (!GetByID.Item2)
            {
                return NotFound("La persona que buscas no existe");
            }
            return Ok(GetByID.Item1);
        }
        //Editamos un registro en la DB mediante PUT

        [HttpPut("{IDpersona}")]
        public ActionResult Put(int IDpersona, [FromBody] PersonaModel persona)
        {
            (List<PersonaModel>, bool) getByID = dataProvider.GetByID(IDpersona);

            if (!getByID.Item2)
            {
                return NotFound("La persona que quieres editar no existe");
            }

            dataProvider.PutPersonas(IDpersona, persona);
            (List<PersonaModel>, bool) MostrarPersonaEditada = dataProvider.GetByID(IDpersona);
            return Ok(MostrarPersonaEditada.Item1);

        }

        [HttpDelete("{IDpersona}")]
        public ActionResult Delete( int IDpersona)
        {
            (List<PersonaModel>, bool) getByID = dataProvider.GetByID(IDpersona);

            if (!getByID.Item2)
            {
                return NotFound("La persona que quieres editar no existe");
            }

            return Ok(dataProvider.DeletePersonas(IDpersona));
        }
    }
}
