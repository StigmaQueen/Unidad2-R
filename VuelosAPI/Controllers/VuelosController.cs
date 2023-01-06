using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuelosAPI.DTOs;
using VuelosAPI.Models;
using VuelosAPI.Repositories;

namespace VuelosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        Repository<Vuelos> repository;
       

        public VuelosController(sistem21_vuelosContext context)
        {
            repository = new(context);

        }

        [HttpGet]
        public IActionResult Get()
        {
          

            var vuelos = repository.Get().OrderBy(x => x.Fecha).ToList();


            if (vuelos.Count() == 0)
                return NotFound();

            ActualizarEntrantes(vuelos);

            vuelos = repository.Get().OrderBy(x => x.Fecha).ToList();

            return Ok(vuelos);
        }

       
        [HttpPost]
        public IActionResult Post(VueloDTO vuelo)
        {

            if (Validate(vuelo, out List<string> errores))
            {

                Vuelos v = new()
                {
                    Clave = vuelo.Clave,
                    Destino = vuelo.Destino,
                    Estado = vuelo.Estado,
                    Fecha = vuelo.Fecha,
                    Origen = vuelo.Origen,
                    Puerta = vuelo.Puerta
                };
                repository.Insert(v);
                return Ok();
            }

            else
                return BadRequest(errores);
        }

        [HttpPut]
        public IActionResult Put(VueloDTO vuelo)
        {


            if (Validate(vuelo, out List<string> errores))
            {
                var v = repository.Get().FirstOrDefault(x => x.Clave == vuelo.Clave);

                if (v == null)
                {
                    errores.Add("No se encontro el vuelo a editar");
                    return NotFound(errores);
                }

                v.Puerta = vuelo.Puerta;
                v.Destino = vuelo.Destino;
                v.Origen = vuelo.Origen;
                v.Estado = vuelo.Estado;
                v.Fecha = vuelo.Fecha;

                repository.Update(v);
                return Ok();
            }

            else
                return BadRequest(errores);
        }

        [HttpDelete]
        public IActionResult Delete(VueloDTO vuelo)
        {
            var v = repository.Get().Find(vuelo.Id);

            if (v == null)
                return BadRequest("El vuelo no existe o ya ha sido eliminado");

            repository.Delete(v);
            return Ok();

        }

        private bool Validate(VueloDTO vuelo, out List<string> errors)
        {
            errors = new();

            var horalocal = DateTime.Now.AddHours(2);

            //Validar
            if (string.IsNullOrWhiteSpace(vuelo.Clave))
                errors.Add("La clave no debe ir vacia");
            if (string.IsNullOrWhiteSpace(vuelo.Destino))
                errors.Add("El destino no debe ir vacio");
            if (string.IsNullOrWhiteSpace(vuelo.Origen))
                errors.Add("El orgien no debe ir vacio");
            //importante--sumnarle dos horas a esto cuando se vaya a publicar. OJO

            if (vuelo.Id == 0)
                if (vuelo.Fecha < horalocal)
                    errors.Add($"La fecha del vuelo a ingresar no debe ser menor a la fecha y hora actual {horalocal}.");

            if (vuelo.Estado < 1 || vuelo.Estado > 5)
                errors.Add("Escriba un estado valido");




            //Esto funciona cuando hacemos un post
            if (!string.IsNullOrWhiteSpace(vuelo.Puerta.ToString()))
            {

                if (vuelo.Id == 0)

                {
                    if (repository.Get().Select(x => x.Puerta).Any(x => x == vuelo.Puerta))
                        errors.Add("Ya se esta usando esa puerta");
                }
                else
                {
                    if (repository.Get().Select(x => new VueloDTO { Id = x.Id, Puerta = x.Puerta }).
                        Any(x => x.Puerta == vuelo.Puerta && vuelo.Id != x.Id))
                        errors.Add("Ya se esta usando esa puerta");
                }
            }
            else
                errors.Add("Debe escribir la puerta que ocupara el avion");

            if (vuelo.Id == 0)
                if (repository.Get().Select(x => x.Clave).Any(x => x == vuelo.Clave))
                    errors.Add("Ya existe un vuelo con esa clave");

            return errors.Count == 0;

        }

        private void ActualizarEntrantes(List<Vuelos> vuelos)
        {
            var VeulosEntrantes = vuelos;
            var Hora = DateTime.Now.AddHours(2);

            //Primero debo recorrer uno por uno

            for (int i = 0; i < VeulosEntrantes.Count(); i++)
            {
                var vueloactual = VeulosEntrantes[i];
                var HoraDiezSegundos = vueloactual.Fecha.AddSeconds(10);
                var Hora20Segundos = vueloactual.Fecha.AddSeconds(40);

                if (vueloactual.Estado == (int)Estado.ATiempo && vueloactual.Fecha < Hora)
                {
                    vueloactual.Estado = (int)Estado.Abordando;
                    repository.Update(vueloactual);
                }

                if (vueloactual.Estado == (int)Estado.Abordando && HoraDiezSegundos < Hora)
                {
                    vueloactual.Estado = (int)Estado.Despegado;
                    repository.Update(vueloactual);
                }

                if ((vueloactual.Estado == (int)Estado.Cancelado || vueloactual.Estado == (int)Estado.Despegado)
                     && Hora20Segundos < Hora)
                {
                    repository.Delete(vueloactual);
                }


            }

        }

    }
}
